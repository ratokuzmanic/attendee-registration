using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using DumpDays.AttendeeRegistration.Auth.Services;

namespace DumpDays.AttendeeRegistration.Auth.Middleware
{
    public class BlockRequestWithInvalidBearerToken : OwinMiddleware
    {
        private readonly Options _options;

        public BlockRequestWithInvalidBearerToken(OwinMiddleware next, Options options) : base(next)
        {
            _options = options;
        }

        public override Task Invoke(IOwinContext context)
        {
            var bearerToken = JwtService.ReadBearerTokenFromHeader(context.Request.Headers);

            if (bearerToken == null)
                return Next.Invoke(context);

            if (JwtService.IsValid(bearerToken))
                return Next.Invoke(context);

            context.Response.StatusCode  = 401;
            context.Response.ContentType = "text/json";
            return context.Response.WriteAsync(_options.GetErrorResponseJson());
        }

        public class Options
        {
            public string ErrorCode    { get; set; }
            public string ErrorMessage { get; set; }

            public string GetErrorResponseJson()
            {
                var json = new Dictionary<string, string>();

                if (ErrorCode != null)
                    json.Add("code", ErrorCode);

                if (ErrorMessage != null)
                    json.Add("message", ErrorMessage);

                return JsonConvert.SerializeObject(json);
            }
        }
    }
}
