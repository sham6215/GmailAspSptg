using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
using System.Reflection;
using System.Net.Mail;

using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1; // Install-Package Google.Apis.Gmail.v1
using Google.Apis.Services;
using Google.Apis.Plus.v1; // Install-Package Google.Apis.Plus.v1

using GmailApp.Models;
using MimeKit;

namespace GmailApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Uses reflection to get the raw content out of a MailMessage.
        /// </summary>

        //
        // GET: /Home/

        [ValidateInput(false)]
        public async Task<ActionResult> Index(CancellationToken cancellationToken, GmailFormModel email)
        {
            //return View();

            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
                AuthorizeAsync(cancellationToken);

            AuthorizationCodeMvcApp o = new AuthorizationCodeMvcApp(this, new AppFlowMetadata());


            if (result.Credential != null)
            {
                if (email.to != null)
                {
                    BaseClientService.Initializer initializer = new BaseClientService.Initializer
                    {
                        HttpClientInitializer = result.Credential,
                        ApplicationName = "ASP.NET MVC Sample5"
                    };

                    var service = new GmailService(initializer);

                    string fromEmail = "adammorgan260@gmail.com";
                    //string fromName = @"AdamMorgan";
                    string fromName = "";

                    /// This code ss needed to extract signed in user info (email, display name)
                    var gps = new PlusService(initializer);
                    var me = gps.People.Get("me").Execute();
                    fromEmail = me.Emails.First().Value;
                    //fromName = me.DisplayName;
                    ////////////////////////////////////////

                    MimeMessage message = new MimeMessage();
                    message.To.Add(new MailboxAddress("", email.to));
                    if (email.cc != null)
                        message.Cc.Add(new MailboxAddress("", email.cc));
                    if (email.bcc != null)
                        message.Bcc.Add(new MailboxAddress("", email.bcc));
                    if (email.subject != null)
                        message.Subject = email.subject;

                    var addr = new MailboxAddress(fromName, fromEmail);
                    message.From.Add(addr);
                    if (email.body != null)
                    {
                        message.Body = new TextPart("html")
                        {
                            Text = email.body
                        };
                    }

                    var ms = new MemoryStream();
                    message.WriteTo(ms);
                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    Google.Apis.Gmail.v1.Data.Message msg = new Google.Apis.Gmail.v1.Data.Message();
                    string rawString = sr.ReadToEnd();

                    byte[] raw = System.Text.Encoding.UTF8.GetBytes(rawString);
                    msg.Raw = System.Convert.ToBase64String(raw);
                    var res = service.Users.Messages.Send(msg, "me").Execute();

                }
                return View();
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }

    }
}
