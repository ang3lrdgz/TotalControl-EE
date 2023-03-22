using System.Net;

namespace TotalControl_EE_API.Models
{
    /*This code is designed to handle responses from a web API. 
     * It provides a way to encapsulate the HTTP status code, success/failure 
     * information, error messages, and the result of the operation in a single response. 
     * 
     * This makes it easier to consume the API as they can work with a single response instead 
     * of having to check multiple different values.*/

    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; }
    }
}
