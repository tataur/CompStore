using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompStore.Web.Infrastructure
{
    public interface IAuthenticate
    {
        bool Authenticate(string login, string password);
    }
}
