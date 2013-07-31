using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using NicAlert.Model;
using NicAlert.Resources;
using NicAlert.ViewModel;

namespace NicAlert.Support
{
    [DataContract]
    public class ErrorMessage
    {
        [DataMember(Name = "message")]
        public object Message { get; set; }
    }

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
             if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
             {
                 var webClient = new WebClient();
                 _funcSerializeEntity += action;
                 webClient.OpenReadCompleted += WebClientOpenReadCompleted;
                 webClient.OpenReadAsync(uri);                 
             }
             else
             {
                 if (StatusCompleted != null)
                 {
                     StatusCompleted(this, new StatusEventArgs(HttpStatusCode.ServiceUnavailable, null));
                 } 
             }
        }

        private void WebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Exception error = e.Error;
            bool cancelled = e.Cancelled;
            HttpStatusCode status = 0;
            object message = null;

            if (error == null && !cancelled)
            {
                try
                {
                    if (e.Result.Length>0)
                    {
                        _funcSerializeEntity(e.Result);
                        status = HttpStatusCode.OK;    
                    }
                    else
                    {
                        status = HttpStatusCode.NoContent;    
                    }
                    
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
                    var stream = webResponse.GetResponseStream();
                    if (stream.Length > 0)
                    {
                        var obj = GetModel<ErrorMessage>(stream);
                        message = obj.Message;                        
                    }

                    if (webResponse.StatusCode == HttpStatusCode.NotFound && webResponse.Headers.Count == 0)
                    {
                        status = HttpStatusCode.ServiceUnavailable;
                    }
                    else
                    {
                        status = webResponse.StatusCode;    
                    }
                }
            }

            if (StatusCompleted != null)
            {
                StatusCompleted(sender, new StatusEventArgs(status, message));
            }

        }
       
        #endregion

        #region Public Members
        public event EventHandler<StatusEventArgs> StatusCompleted;

        #endregion

        #region Public Methods
        public void GetDomainTypes()
        {
            App.DomainTypes = new string[] {".com.ar", ".gov.ar", ".mil.ar", ".int.ar", ".net.ar", ".org.ar", ".tur.ar"};
            var uri = new Uri(String.Concat(App.UrlRoot, "/domains"));
            SetData(uri, stream => { App.DomainTypes = GetModel<string[]>(stream); });
        }

        public void Search(string term, TypeSearching typeSearching)
        {
            App.Term = term;

            var funcSearching = new Dictionary<TypeSearching, Action<string>>
            {
                {TypeSearching.Domain, s => SearchDomain(s) },
                {TypeSearching.TransactionByDomain, s => GetTransactionByDomain(s) },
                {TypeSearching.TransactionById, s => GetTransactionById(s) },
                {TypeSearching.Entity, s => GetEntity(s) },
                {TypeSearching.People, s => GetPeople(s) },
                {TypeSearching.DnsServer, s => GetDnsServer(s) }
            };

            funcSearching[typeSearching].Invoke(term);
        }

        public void SearchDomain(string domain)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/domains/", domain));
            SetData(uri, stream => { App.DomainInfo = GetModel<DomainInfo>(stream); });
        }

        public void GetTransactionByDomain(string domain)
        {
            var uri = new Uri(String.Concat(App.UrlRoot, "/domains/", domain, "/transactions"));
            SetData(uri, stream => { App.Transactions = GetModel<List<Transaction>>(stream); });
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

        public List<TypeSearchingViewModel> GetTypesSearching()
        {
            return new List<TypeSearchingViewModel>
            {
                new TypeSearchingViewModel {Text = AppResources.Domain, Value = TypeSearching.Domain},
                new TypeSearchingViewModel {Text = AppResources.TransactionByDomain, Value = TypeSearching.TransactionByDomain},
                new TypeSearchingViewModel {Text = AppResources.TransactionById, Value = TypeSearching.TransactionById},
                new TypeSearchingViewModel {Text = AppResources.Entity, Value = TypeSearching.Entity},
                new TypeSearchingViewModel {Text = AppResources.People, Value = TypeSearching.People},
                new TypeSearchingViewModel {Text = AppResources.DnsServer, Value = TypeSearching.DnsServer},
            };
        }
        #endregion

        
    }
}