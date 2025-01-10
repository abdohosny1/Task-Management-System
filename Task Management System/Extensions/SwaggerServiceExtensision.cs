using Microsoft.OpenApi.Models;

namespace MyTask_Management_System.Extensions
{
    public static class SwaggerServiceExtensision
    {
        public static IServiceCollection AddSwaggerDocumantion(this IServiceCollection services)
        {
            //builder.Services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

                /// add Authorization test in Swagger
                //    var securitySchema = new OpenApiSecurityScheme()
                //{
                //    Description = "JWT Auth Bearer Scheme",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "bearer",
                //    Reference = new OpenApiReference
                //    {
                //        Type = ReferenceType.Schema,
                //        Id = "Bearer"
                //    }
                //};

                //c.AddSecurityDefinition("Bearer", securitySchema);

                //var securityRequriment = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };

                //    c.AddSecurityRequirement(securityRequriment);
            });


            return services;
        }

        public static IApplicationBuilder UseSweggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;


        }
    }
}
