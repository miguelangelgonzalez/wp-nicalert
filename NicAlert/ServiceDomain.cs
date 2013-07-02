using System;
using System.Net;
using System.Runtime.Serialization.Json;
using NicAlert.Model;

namespace NicAlert
{
    public class ServiceDomain
    {
        public event EventHandler<DomainStatusEventArgs> SearchCompleted;

        public class DomainStatusEventArgs : EventArgs
        {
            public HttpStatusCode Status { get; private set; }
            public DomainInfo DomainInfo { get; set; }

            public DomainStatusEventArgs(HttpStatusCode status, DomainInfo domainInfo)
            {
                Status = status;
                DomainInfo = domainInfo;
            }
        }

        public void Search(string domain)
        {
            var webClient = new WebClient();
            var uri = new Uri(String.Concat("http://api.nicalert.com.ar/domains/", domain));
            webClient.OpenReadAsync(uri);
            webClient.OpenReadCompleted += WebClientOpenReadCompleted;
        }

        void WebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Exception error = e.Error;
            bool cancelled = e.Cancelled;
            var status = (HttpStatusCode)0;
            DomainInfo domainInfo = null;
            if (error == null && !cancelled)
            {
                try
                {
                    var jsonSerializer = new DataContractJsonSerializer(typeof(DomainInfo));
                    domainInfo = (DomainInfo)jsonSerializer.ReadObject(e.Result);
                    status = HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    error = new InvalidOperationException("Invalid Operation", ex);
                }
            }

            var webException = error as WebException;

            if (webException != null)
            {
                var webResponse = webException.Response as HttpWebResponse;

                if (webResponse != null)
                {
                    status = webResponse.StatusCode;
                }
            }

            if (SearchCompleted != null)
            {
                SearchCompleted(sender, new DomainStatusEventArgs(status, domainInfo));
            }
            
        }

    }
}