using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
     

namespace WebApplicationApi.Respositry
{
    public interface IAuthService
    {
        string Login(LoginModel loginRequest);
    }
}
