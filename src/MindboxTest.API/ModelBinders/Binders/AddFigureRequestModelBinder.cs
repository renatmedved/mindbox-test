using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;
using MindboxTest.Figures.Proxy;
using MindboxTest.Handlers.AddFigure;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MindboxTest.API.ModelBinders.Binders
{
    public class AddFigureRequestModelBinder : IModelBinder
    {
        private readonly ProxyFigureDescriptionProvider _descriptionProvider;

        public AddFigureRequestModelBinder(ProxyFigureDescriptionProvider descriptionProvider)
        {
            _descriptionProvider = descriptionProvider;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            JObject jObject = await ParseJson(bindingContext.HttpContext);

            string type = jObject["type"].Value<string>();

            if (String.IsNullOrWhiteSpace(type))//it should be processed in handler
            {
                bindingContext.Result = ModelBindingResult.Success(MakeRequest(null, null));
                return;
            }

            Result<Type> descriptionTypeResult = _descriptionProvider.GetDescriptionType(type);

            if (descriptionTypeResult.Fail)//it should be processed in handler
            {
                bindingContext.Result = ModelBindingResult.Success(MakeRequest(type, null));
                return;
            }

            Type descriptionType = descriptionTypeResult.Data;
            string description = jObject["description"].ToString();

            IFigureDescription typedDescription = JsonConvert.DeserializeObject(description, descriptionType)
                as IFigureDescription;

            AddFigureRequest request = MakeRequest(type, typedDescription);

            bindingContext.Result = ModelBindingResult.Success(request);
        }

        private static AddFigureRequest MakeRequest(string type, IFigureDescription typedDescription)
        {
            var request = new AddFigureRequest
            {
                FigureDescription = typedDescription,
                FigureType = type
            };

            return request;
        }

        private async Task<JObject> ParseJson(HttpContext context)
        {
            using StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true);
            
            string json = await reader.ReadToEndAsync();

            JObject jObject = JObject.Parse(json);

            return jObject;
        }
    }
}
