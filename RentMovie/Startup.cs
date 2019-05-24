using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentMovie.Startup))]
namespace RentMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
