using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Collections.Generic;

namespace OcelotDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot();//注入Ocelot服务
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#if debug
            //添加Swagger.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "网管服务", Version = "v1",Description = "Ocelot项目"});
            });
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            //var apis = new List<string> { "OrderAPI", "GoodAPI" };
            //app.UseMvc()
            //    .UseSwagger()
            //   .UseSwaggerUI(options =>
            //   {
            //       apis.ForEach(m =>
            //       {
            //           options.SwaggerEndpoint($"/{m}/swagger.json", m);
            //       });
            //   });

            //app.UseOcelot().Wait();//使用Ocelot中间件


            //var apis = new List<string> { "OrderAPI", "GoodAPI" };
            //app.UseMvc()
            //   .UseSwagger()
            //   .UseSwaggerUI(options =>
            //   {
            //       apis.ForEach(m =>
            //       {
            //           options.SwaggerEndpoint($"/{m}/swagger.json", m);
            //       });
            //   });
            app.UseMvc();
            app.UseOcelot().Wait();
        }
    }
}
