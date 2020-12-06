using FluentValidation.Results;

using System.Collections.Generic;
using System.Linq;

namespace MindboxTest.Infrastructure
{
    public static class FluentValidationExts
    {
        public static List<string> ErrorsToListString(this ValidationResult result)
        {
            return result.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
