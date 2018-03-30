using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using System.Net.Http;
using WebApiEx4_5.Models;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Diagnostics;

namespace WebApiEx4_5.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();

        public ActionResult Index(CancellationToken cancellationToken)
        {

            var result = new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).AuthorizeAsync(cancellationToken).Result;
            if (result.Credential != null)
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "ASP.NET MVC Sample"
                });

                var list = service.Files.List().Execute();
                ViewBag.Message = "FILE COUNT IS: " + list.Items.Count();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CallBackTaskApi(CancellationToken cancellationToken)
        {

            return View();
        }

        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "ASP.NET MVC Sample"
                });

                // YOUR CODE SHOULD BE HERE..
                // SAMPLE CODE:
                var list = await service.Files.List().ExecuteAsync();
                ViewBag.Message = "FILE COUNT IS: " + list.Items.Count();
                return View();
            }
            else
            {
                return View();
                //return new RedirectResult(result.RedirectUri);
            }
        }

        class PostData
        {
            [JsonProperty("client_id")]
            public string client_id { get; set; }
            [JsonProperty("grant_type")]
            public string grant_type { get; set; }
            [JsonProperty("client_secret")]
            public string client_secret { get; set; }
            [JsonProperty("code")]
            public string code { get; set; }
            [JsonProperty("redirect_uri")]
            public string redirect_uri { get; set; }
        }
    }
}