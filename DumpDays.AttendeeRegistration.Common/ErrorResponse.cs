using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace DumpDays.AttendeeRegistration.Common
{
    public class ErrorResponse
    {
        public HttpStatusCode Code    { get; }
        public string         Message { get; }

        public ErrorResponse
        (
            HttpStatusCode code,
            string         message
        )
        {
            Code    = code;
            Message = message;
        }

        public string GetJson()
            => JsonConvert.SerializeObject(new { code = Code, message = Message });

        public Task SetErrorResponseIn(IOwinContext context)
        {
            context.Response.StatusCode = (int)Code;
            context.Response.ContentType = "text/json";
            return context.Response.WriteAsync(GetJson());
        }

        public void SetErrorResponseIn(HttpActionContext context)
        {
            context.Response = context.Request.CreateResponse(Code);
            context.Response.Content = new StringContent(GetJson());
            context.Response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
        }

        public IHttpActionResult SetErrorResponseIn(ApiController controller)
        {
            HttpResponseMessage response = new HttpResponseMessage(Code)
            {
                Content = new StringContent(GetJson())
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            return new ResponseMessageResult(response);
        }
    }
}
