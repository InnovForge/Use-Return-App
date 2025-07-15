using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Use_Return_App.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(); 
        }
    }
}