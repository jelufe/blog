using Microsoft.AspNetCore.Builder;

namespace Blog.Api.Configurations
{
    public static class CorsConfiguration
    {
        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
        }
    }
}
