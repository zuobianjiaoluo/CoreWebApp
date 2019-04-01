﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace OrderApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 运行时调用此方法。使用此方法配置HTTP请求管道。运行时调用此方法。使用此方法向容器中添加服务。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

#if debug
            //添加Swagger.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("OrderAPI", new Info { Title = "OrderAPI", Version = "v1",Description = "Ocelot项目"
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "OrderApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
#endif
        }

        /// <summary>
        /// 运行时调用此方法。使用此方法配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
#if debug
            //配置Swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderAPI");
                //加载汉化的js文件，注意 swagger.js文件属性必须设置为“嵌入的资源”。
                //c.InjectJavascript("/swagger/v1/ui/zh_CN.js");
            });
#endif
        }
    }
}
