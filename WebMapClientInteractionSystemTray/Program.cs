using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace WebMapClientInteractionSystemTray
{
    static class Program
    {


        static readonly Uri _baseAddress = new Uri("http://localhost:60064/");
        static void Test()
        {
            // Set up server configuration
            Console.WriteLine("Web API Self hosted on " + _baseAddress + " Hit ENTER to exit...");
            Console.ReadLine();
         //   server.CloseAsync().Wait();
        }




        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);
            config.MessageHandlers.Add(new Form1.CustomHeaderHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Create server
            using (var server = new HttpSelfHostServer(config))
            {
                // Start listening
                server.OpenAsync().Wait();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
