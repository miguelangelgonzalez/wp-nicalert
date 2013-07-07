using System;
using System.Net;

namespace NicAlert.Support
{
    public class StatusEventArgs : EventArgs
    {
        public HttpStatusCode Status { get; private set; }

        public StatusEventArgs(HttpStatusCode status)
        {
            Status = status;
        }
    }
}