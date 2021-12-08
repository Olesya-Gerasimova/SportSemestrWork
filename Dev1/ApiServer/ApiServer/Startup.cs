using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApiServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ApiServer.Interfaces;
using ApiServer.Services;
using ApiServer.Helpers;
using System;
using System.Text.Json.Serialization;

namespace ApiServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "Server =.\\SQLExpress; Database = PremierLeague; Trusted_Connection = True;";
            services.AddDbContext<PremierLeagueContext>(options => options.UseSqlServer(con));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = TokenHelper.Issuer,
                            ValidAudience = TokenHelper.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret))
                        };
                    });

            services.AddScoped<IPremierLeagueService, PremierLeagueService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            //services.AddControllers()
            //services.AddSingleton<IAuthorizationHandler, UserAuthenticationHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}
