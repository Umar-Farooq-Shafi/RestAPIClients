using System;
using System.IO;
using System.Net;

namespace RestAPIClients
{
    public enum HttpVerb
    {
        Get,
        Post,
        Put,
        Delete
    }

    public enum AuthenticationType
    {
        Basic,
        NTLM
    }

    public enum AuthenticationTechnique
    {
        RollYourOwn,
        NetworkCredentials
    }

    class RestClient
    {
        public string EndPoint { get; set; }
        private HttpVerb HttpMethod { get; set; }
        public AuthenticationType AuthType { get; set; }
        public AuthenticationTechnique AuthTechnique { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public RestClient()
        {
            EndPoint = string.Empty;
            HttpMethod = HttpVerb.Get;
        }

        public string MakeRequest()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(EndPoint);
            request.Method = HttpMethod.ToString();

            var authHeader = System.Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes(UserName + ":" + Password)
            );
            request.Headers.Add("Authortization", AuthType.ToString() + " " + authHeader);

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse) request.GetResponse();

                using Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    using StreamReader reader = new StreamReader(responseStream);
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return "{\"Error Message\":[\"" + e.Message.ToString() + "\"]:\"errors\":{}}";
            }
            finally
            {
                ((IDisposable) response)?.Dispose();
            }

            return string.Empty;
        }
    }
}
