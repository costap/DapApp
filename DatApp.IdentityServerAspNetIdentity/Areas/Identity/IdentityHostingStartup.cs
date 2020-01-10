using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DatApp.IdentityServerAspNetIdentity.Areas.Identity.IdentityHostingStartup))]
namespace DatApp.IdentityServerAspNetIdentity.Areas.Identity
{
    public class IdentityHostingStartup: IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}