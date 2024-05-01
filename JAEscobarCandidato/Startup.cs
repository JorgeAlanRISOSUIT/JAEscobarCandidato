using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JAEscobarCandidato.Startup))]
namespace JAEscobarCandidato
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
