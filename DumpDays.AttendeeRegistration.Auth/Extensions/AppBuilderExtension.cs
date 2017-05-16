using DumpDays.AttendeeRegistration.Auth.Middleware;
using Owin;

namespace DumpDays.AttendeeRegistration.Auth.Extensions
{
    public static class AppBuilderExtension
    {
        public static void BlockRequestWithInvalidBearerToken
            (
                this IAppBuilder app,
                BlockRequestWithInvalidBearerToken.Options options
            )
            => app.Use<BlockRequestWithInvalidBearerToken>(options);
    }
}
