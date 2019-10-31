using pricecheckingtool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Input;
using System.Windows.Threading;

namespace pricecheckingtool.Provider
{
    public sealed class Webservice
    {
        private string sessionID { get; set; }
        public HttpClient httpClient { get; private set; }
        public HttpClient httpClientWithCookie { get; private set; }

        public Webservice(string sessionID)
        {
            this.sessionID = sessionID;
            httpClient = new HttpClient();
            httpClientWithCookie = ModifyHttpClient();
        }

        private Cookie GetCookie()
        {
            Cookie cookie = new Cookie
            {
                Value = sessionID,
                Name = "POESESSID",
                Domain = "pathofexile.com",
                Secure = false,
                Path = "/",
                HttpOnly = false
            };

            return cookie;
        }

        // For Interactions with the Path of Exile API
        private HttpClient ModifyHttpClient()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(GetCookie());
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.CookieContainer = cookieContainer;
            HttpClient httpClient = new HttpClient(httpClientHandler);

            return httpClient;
        }
    }
}
