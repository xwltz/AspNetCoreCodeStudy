using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using WebApiSoft.Class;

namespace WebApiSoft
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

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().AddJsonOptions(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddCors(option =>
            {
                option.AddPolicy("Domain",
                    builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials());
            });

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "AspNetCoreWebApi微服务",
                    Description = "RESTful API",
                    TermsOfService = "MIT",
                    Contact = new Contact { Email = "61488281@qq.com", Name = "xwltz", Url = "http://www.xwltz.top" }
                });

                option.IncludeXmlComments("App_Data/AspNetCoreApiSwagger.xml");
                option.OperationFilter<HttpHeaderOperation>();
            });
        }

        /// <summary>
        ///
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseCors("Domain");

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseStaticFiles();
            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {
                //做出一个限制信息描述
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiSoft API V1");
                //显示在发出请求时发送的标题
                option.ShowExtensions();
            });
        }
    }
}