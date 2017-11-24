using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CompStore.Web.Infrastructure
{
    public class FormAuthenticate : IAuthenticate
    {
        public bool Authenticate(string login, string password)
        {
            bool result = FormsAuthentication.Authenticate(login, password);
            if (result)
                FormsAuthentication.SetAuthCookie(login, false);
            return result;
        }
    }
}