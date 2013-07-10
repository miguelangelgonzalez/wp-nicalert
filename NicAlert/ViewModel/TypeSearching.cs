using System;

namespace NicAlert.ViewModel
{
    [Flags]
    public enum TypeSearching
    {
        Domain, 
        TransactionByDomain, 
        TransactionById, 
        Entity, 
        People, 
        DnsServer
    }
}