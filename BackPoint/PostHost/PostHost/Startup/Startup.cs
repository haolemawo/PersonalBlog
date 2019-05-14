using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Post.Core.Configuration;
using Post.EntityFrameworkCore.EntityFrameworkCore;
using PostHost.Middlewares;
using Swashbuckle.AspNetCore.Swagger;

namespace PostHost.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "articleCore";
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //配置数据库
            services.AddAbpDbContext<PostDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            //添加自定义依赖注入
            services.AddRepositories();

            //配置MVC路由
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            //添加Jwt验证
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    //验证地址
                    options.Authority = "http://172.17.31.17:5001";
                    options.RequireHttpsMetadata = false;
                    
                    options.Audience = "blog";
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromDays(1);//一天验证一次
                    options.TokenValidationParameters.RequireExpirationTime = true;
                });

            //读取验证
            services.ReadConfigurations(_appConfiguration);

            //配置跨域
            services.AddCors(
                options=> options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder=>builder
                        //.WithOrigins(
                        //    _appConfiguration["App:CorsOrigins"]
                        //    .Split(",",StringSplitOptions.RemoveEmptyEntries)
                        //    .Select(o=>o.RemovePostFix("/"))
                        //    .ToArray()
                        //)
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                    ));

            //配置Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Post API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                // Assign scope requirements to operations based on AuthorizeAttribute
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services.AddAbp<PostWebModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp();

            //启用跨域
            app.UseCors(_defaultCorsPolicyName);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API V1");
            });
        }
    }
}
