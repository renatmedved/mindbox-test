using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

using MindboxTest.API.ModelBinders.Binders;
using MindboxTest.Handlers.AddFigure;

namespace MindboxTest.API.ModelBinders.Providers
{
    public class CustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(AddFigureRequest))
            {
                return new BinderTypeModelBinder(typeof(AddFigureRequestModelBinder));
            }

            return null;
        }
    }
}
