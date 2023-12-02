﻿using Swashbuckle.AspNetCore.SwaggerUI;

namespace RealEstateApp.Presentation.WebAPI.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstate API");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}
