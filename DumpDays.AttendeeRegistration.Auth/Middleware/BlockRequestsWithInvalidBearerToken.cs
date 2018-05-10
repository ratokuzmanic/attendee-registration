using System.Net;
using System.Threading.Tasks;
using DumpDays.AttendeeRegistration.Auth.Entities;
using DumpDays.AttendeeRegistration.Common;
using Microsoft.Owin;

namespace DumpDays.AttendeeRegistration.Auth.Middleware
{
    public class BlockRequestsWithInvalidBearerToken : OwinMiddleware
    {
        public BlockRequestsWithInvalidBearerToken(OwinMiddleware next) : base(next) { }

        public override Task Invoke(IOwinContext context)
        {
            var encodedBearerToken = context.Request.Headers.Get("Authorization");

            if (encodedBearerToken == null)
                return Next.Invoke(context);

            var maybeBearerToken = Jwt.BearerToken.CreateIfValid(encodedBearerToken);
            return maybeBearerToken.Case(
                some: bearerToken =>
                {
                    if(bearerToken.HasExpired)
                        return new ErrorResponse(
                            code: HttpStatusCode.Unauthorized,
                            message: "Bearer token has expired"
                        ).SetErrorResponseIn(context);

                    return Next.Invoke(context);
                },
                none: () => new ErrorResponse(
                    code: HttpStatusCode.Unauthorized,
                    message: "Bearer token is corrupted"
                ).SetErrorResponseIn(context)
            );
        }
    }
}
