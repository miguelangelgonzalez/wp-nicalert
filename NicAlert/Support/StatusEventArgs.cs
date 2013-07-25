using System;
using System.Net;

namespace NicAlert.Support
{
    public class StatusEventArgs : EventArgs
    {
        public HttpStatusCode Status { get; private set; }
        public object Message { get; set; }

        public StatusEventArgs(HttpStatusCode status, object message)
        {
            Status = status;
            Message = message;
        }
    }
}