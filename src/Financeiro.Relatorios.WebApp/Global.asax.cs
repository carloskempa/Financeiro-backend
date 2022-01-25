using System;
using System.Web;
using System.Web.Routing;

namespace Financeiro.Relatorios.WebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}