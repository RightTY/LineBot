using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SevenDeadlySins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins
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
            #region --Configuration--
            //�z���O
            //services.AddSingleton(provider => Configuration);
            //�ϥΤ覡
            //�ϥγo�ӫ��O IConfiguration config
            //���Ȥ覡 config["DB:ConnectionStrings:DefaultConnection"]
            //�j���O
            services.Configure<Setting>(Configuration);
            //�ϥΤ覡
            //�ϥγo�ӫ��O IOptions<Class Name> settings   Class Name=�ҨϥΪ��j���O
            //���Ȥ覡 settings.Value.DB.ConnectionStrings.DefaultConnection;
            #endregion

            #region --Session--
            //services.AddDistributedMemoryCache();
            //services.AddSession();
            #endregion

            #region --HttpContext--
            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region --HttpClient--
            services.AddHttpClient();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SevenDeadlySins", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SevenDeadlySins v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
