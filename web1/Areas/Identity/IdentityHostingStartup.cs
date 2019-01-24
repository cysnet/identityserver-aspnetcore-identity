using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using web1.Areas.Identity.Data;
using web1.Models;

[assembly: HostingStartup(typeof(web1.Areas.Identity.IdentityHostingStartup))]
namespace web1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<web1Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("web1ContextConnection")));

                services.AddDefaultIdentity<web1User>()
                    .AddEntityFrameworkStores<web1Context>();
            });
        }
    }
}