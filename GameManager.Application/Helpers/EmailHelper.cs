using System;
using System.Threading.Tasks;
using GameManager.Domain.Contracts.Helpers;
using GameManager.Domain.Utils;
using RestSharp;
using RestSharp.Authenticators;

namespace GameManager.Application.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private const string API_KEY = "key-91a3c5bbf2d28bf6e752aaecbd644e69";
        private const string API_URL = "https://api.mailgun.net/v3";
        private const string DOMAIN = "sandbox3d2ea53d7b5d49bdad47e77a77b26139.mailgun.org";
        private const string EMAIL_SENDER = "Game manager <postmaster@sandbox3d2ea53d7b5d49bdad47e77a77b26139.mailgun.org>";

        public void Send(Email email)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(API_URL);
            client.Authenticator = new HttpBasicAuthenticator("api", API_KEY);

            var request = new RestRequest();
            request.AddParameter("domain", DOMAIN, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", EMAIL_SENDER);
            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Message);
            request.Method = Method.POST;
            request.AddParameter("to", email.To);

            client.Execute(request);
        }

        public Task SendAsync(Email email)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(API_URL);
            client.Authenticator = new HttpBasicAuthenticator("api", API_KEY);

            var request = new RestRequest();
            request.AddParameter("domain", DOMAIN, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", EMAIL_SENDER);
            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Message);
            request.Method = Method.POST;
            request.AddParameter("to", email.To);
            
            return client.ExecuteTaskAsync(request);
        }
    }
}
