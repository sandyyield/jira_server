using Jira_server.Dao.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jira_server
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jira_server", Version = "v1" });
                //添加引用 Swashbuckle.AspNetCore.Filters
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //添加请求头配置
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //services.AddControllers().AddNewtonsoftJson(setup =>
                //{
                //    setup.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();//驼峰命名返回
                //    setup.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; //忽略循环引用
                //    setup.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //默认日期格式化
                //});


            });
            //配置跨域
            services.AddCors(options => options.AddPolicy("any", builder => builder
                .WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));

            // 配置认证服务 添加jwt支持
            services.AddAuthentication(i =>
            {
                i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {

                //o.RequireHttpsMetadata = false;
                //o.SaveToken = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetSection("JWTSetting").GetSection("JWTIssuer").Value,
                    ValidAudience = Configuration.GetSection("JWTSetting").GetSection("JWTAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWTSetting").GetSection("JWTKey").Value)),
                    ClockSkew = TimeSpan.Zero
                };
                o.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = async context =>
                    {
                        Console.WriteLine($"OnMessageReceived:{context.Request.Headers.ToString()}");
                        context.Token = context.Request.Query["token"];

                    },

                    OnAuthenticationFailed = async context =>
                    {
                        Console.WriteLine($"OnAuthenticationFailed:{context.Exception}");
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                    },
                    OnTokenValidated = async context =>
                    {
                        Console.WriteLine($"onTokenValidated{DateTime.UtcNow.ToShortDateString()}:{context.Request}");


                    },

                };
            });


            //注入DbContext
            //services.AddDbContext<ApplicationDbContext>(o => o.UseSqlite($"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.db")}"));
            services.AddDbContext<ApplicationDbContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jira_server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();//认证
            app.UseAuthorization();//授权


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
