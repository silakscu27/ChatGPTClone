﻿using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.WebApi.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ChatGPTClone.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserManager>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                // Set the default culture

                var defaultCulture = new CultureInfo("tr-TR");

                // Set supported cultures

                var supportedCultures = new List<CultureInfo>
                {
                    defaultCulture,
                    new CultureInfo("en-GB")
                };

                // Add supported cultures
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);

                options.SupportedCultures = supportedCultures;

                options.SupportedUICultures = supportedCultures;

                options.ApplyCurrentCultureToResponseHeaders = true;
            });

            return services;
        }
    }
}
