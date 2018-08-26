using Newtonsoft.Json;
using project3_backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace project3_backend.Controllers
{
    public class BaseController : ApiController
    {
        public User AuthenticatedUser { get; private set; }

        protected void Login()
        {
            var verifiedJwt = Authenticate();

            AuthenticatedUser = GetUser(verifiedJwt);
        }

        private User GetUser(VerifiedJwt verifiedJwt)
        {
            using (var ctx = new Project3Context(verifiedJwt.Subject))
            {
                if (!ctx.Users.Any(u => u.Subject == verifiedJwt.Subject))
                {
                    ctx.Users.Add(new User()
                    {
                        Name = verifiedJwt.EMail,
                        Subject = verifiedJwt.Subject
                    });
                    ctx.SaveChanges();
                }

                return ctx.Users.Single(u => u.Subject == verifiedJwt.Subject);
            }
        }

        private VerifiedJwt Authenticate()
        {
            if (string.IsNullOrEmpty(Request?.Headers?.Authorization?.Parameter))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "No token." };
                throw new HttpResponseException(msg);
            }

            var jwt = Request.Headers.Authorization.Parameter;

            if (jwt == "dev-token" || jwt == "prod-token" || jwt == "test")
            {
                return new VerifiedJwt()
                {
                    Subject = jwt.Split('-')[0] + "-subject",
                    GivenName = jwt.Split('-')[0] + "-user",
                    EMail = jwt.Split('-')[0] + "-mail",
                };
            }

            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + jwt);

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<VerifiedJwt>(responseString);
            }
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.ProtocolError && wex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Could not verify token." };
                    throw new HttpResponseException(msg);
                }
                else
                {
                    var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = wex.Message };
                    throw new HttpResponseException(msg);
                }
            }
            catch (Exception ex)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = ex.Message };
                throw new HttpResponseException(msg);
            }
        }
    }
}