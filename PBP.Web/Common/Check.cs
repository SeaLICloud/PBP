using System.ComponentModel.DataAnnotations;

namespace PBP.Web.Common
{
    public class FRequired : RequiredAttribute
    {
        public FRequired(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
