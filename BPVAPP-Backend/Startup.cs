using BPVAPP_Backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BPVAPP_Backend
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
            services.AddCors(o => o.AddPolicy("ApiPolicy", builder =>
            {
                builder.WithOrigins("http://andydeklein.com")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services.AddDbContextPool<ApplicationDbContext>(
                // On localhost edit the connection string in appsettings.json
                options => options.UseMySql(Configuration.GetConnectionString("IdentityConnection"),
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(5, 5, 60), ServerType.MariaDb); // replace with your Server Version and Type
                        mysqlOptions.DisableBackslashEscaping();
                    }
            ));

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero, // remove delay of token when expire
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        
                    };
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseCors("ApiPolicy");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            CreateRolesandUsers(serviceProvider).Wait();
        }

        private async Task CreateRolesandUsers(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            bool x = await _roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                // first we create Admin rool    
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });

                // then doc
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Docent",
                });


                //Here we create a Admin super user who will maintain the website                   
                var admin = new IdentityUser
                {
                    UserName = "BpvAdmin",
                    Email = "BpvAdmin@bpv.com"
                };

                //Docent test account
                var docent = new IdentityUser
                {
                    UserName = "BpvDocent",
                    Email = "BpvDocent@bpv.com"
                };

                string userPWD = "P@ssw0rd!";

                var checkAdmin = await _userManager.CreateAsync(admin, userPWD);
                var checkDocent = await _userManager.CreateAsync(docent, userPWD);
                

                //Add default User to Role Admin    
                if (checkAdmin.Succeeded && checkDocent.Succeeded)
                {
                    var result1 = await _userManager.AddToRolesAsync(admin, new[] { "Admin", "Docent" });
                    var result2 = await _userManager.AddToRoleAsync(docent, "Docent");
                    
                    if(!result1.Succeeded || !result1.Succeeded)
                    {
                        // Display error message?
                    }
                }
            }
        }
    }
}
