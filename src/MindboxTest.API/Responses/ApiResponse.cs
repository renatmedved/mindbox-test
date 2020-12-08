using MindboxTest.Contracts.Results;

using System.Collections.Generic;
using System.Linq;

namespace MindboxTest.API.Responses
{
    public class ApiResponse<TData>
    {
        public List<string> Errors { get; set; } = new List<string>();
        public TData Data { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(Result<TData> result)
        {
            if (result.Fail)
            {
                Errors = result.Errors.ToList();
            }
            else
            {
                Data = result.Data; 
            }
        }
    }
}
