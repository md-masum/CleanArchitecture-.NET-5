using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Extensions
{
    // public class RemoveVersionFromParameter : IOperationFilter
    // {
    //     public void Apply(OpenApiOperation operation, OperationFilterContext context)
    //     {
    //         var versionParameter = operation.Parameters.Single(p => p.Name == "version");
    //         operation.Parameters.Remove(versionParameter);
    //     }
    // }
    //
    // public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    // {
    //     public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    //     {
    //         var toReplaceWith = new OpenApiPaths();
    //
    //         foreach (var (key, value) in swaggerDoc.Paths)
    //         {
    //             toReplaceWith.Add(key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
    //         }
    //
    //         swaggerDoc.Paths = toReplaceWith;
    //     }
    // }
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\WebApi.xml");
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Online School - WebApi",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "MD MASUM",
                        Email = "md.wr.masum@gmail.com",
                        Url = new Uri("https://md-masum.github.io/"),
                    }
                });

                // c.SwaggerDoc("v2",
                //     new OpenApiInfo
                //     {
                //         Version = "v2",
                //         Title = "v2 Online School - WebApi",
                //         Description = "This Api will be responsible for overall data distribution and authorization.",
                //         Contact = new OpenApiContact
                //         {
                //             Name = "MD MASUM",
                //             Email = "md.wr.masum@gmail.com",
                //             Url = new Uri("https://md-masum.github.io/"),
                //         }
                //     });
                //
                // c.OperationFilter<RemoveVersionFromParameter>();
                // c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // Ensure the routes are added to the right Swagger doc
                // c.DocInclusionPredicate((version, desc) =>
                // {
                //     if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
                //         return false;
                //
                //     var versions = methodInfo.DeclaringType?.GetCustomAttributes(true)
                //         .OfType<ApiVersionAttribute>()
                //         .SelectMany(attr => attr.Versions);
                //
                //     var maps = methodInfo
                //         .GetCustomAttributes(true)
                //         .OfType<MapToApiVersionAttribute>()
                //         .SelectMany(attr => attr.Versions)
                //         .ToArray();
                //
                //     return versions!.Any(v => $"v{v.ToString()}" == version)
                //            && (!maps.Any() || maps.Any(v => $"v{v.ToString()}" == version));
                // });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }
    }
}
