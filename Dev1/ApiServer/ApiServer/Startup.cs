using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiServer.Models;


namespace ApiServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "Server =.\\SQLExpress; Database = PremierLeague; Trusted_Connection = True;";
            // ������������� �������� ������
            services.AddDbContext<PremierLeagueContext>(options => options.UseSqlServer(con));

            services.AddControllers(); // ���������� ����������� ��� �������������
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // ���������� ������������� �� �����������
            });
        }
    }
}
