/* 
    Copyright (c) 2012 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/

namespace NicAlert
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static NicAlert.Resources.AppResources localizedResources = new NicAlert.Resources.AppResources();

        public NicAlert.Resources.AppResources LocalizedResources { get { return localizedResources; } }
    }
}

