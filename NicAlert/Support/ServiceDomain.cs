using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using NicAlert.Model;

namespace NicAlert.Support
{
    public class ServiceDomain
    {
        #region Private Members 
        private Action<Stream> _funcSerializeEntity;

        #endregion        

        #region Private Methods
        private static T GetModel<T>(Stream stream)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T));
            return (T)jsonSerializer.ReadObject(stream);
        }

        private void SetData(Uri uri, Action<Stream> action)
        {
            var webClient = new WebClient();
            _funcSerializeEntity += action;
            webClient.OpenReadCompleted += WebClientOpenReadCompleted;
            webClient.OpenReadAsync(uri);
        }

        private void WebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Exception error = e.Error;
            bool cancelled = e.Cancelled;
            var status = (HttpStatusCode)0;

            if (error == null && !cancelled)
            {
                try
                {
                    _funcSerializeEntity(e.Result);
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

            if (StatusCompleted != null)
            {
                StatusCompleted(sender, new StatusEventArgs(status));
            }

        }

        #endregion

        #region Public Members
        public event EventHandler<StatusEventArgs> StatusCompleted;

        #endregion

        #region Public Methods
        public void GetDomainTypes()
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/domains"));
            SetData(uri, stream => { App.DomainTypes = GetModel<string[]>(stream); });
        }

        public void Search(string domain)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/domains/", domain));
            SetData(uri, stream => { App.DomainInfo = GetModel<DomainInfo>(stream); });
        }

        public void GetTransactionByDomain(string domain)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/domains/", domain, "/transactions"));
            SetData(uri, stream => { App.Transaction = GetModel<List<Transaction>>(stream); });
        }

        public void GetTransactionById(string id)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/transactions/", id));
            SetData(uri, stream => { App.PendingTransaction = GetModel<Transaction>(stream); });
        }

        public void GetEntity(string name)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/entities/", name.Replace(" ", "+")));
            SetData(uri, stream => { App.Entity = GetModel<Entity>(stream); });
        }

        public void GetPeople(string name)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/people/", name.Replace(" ", "+")));
            SetData(uri, stream => { App.People = GetModel<People>(stream); });
        }

        public void GetDnsServer(string term)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/dns_servers/", term));
            SetData(uri, stream => { App.Dns = GetModel<Dns>(stream); });
        }

        #endregion
    }
}