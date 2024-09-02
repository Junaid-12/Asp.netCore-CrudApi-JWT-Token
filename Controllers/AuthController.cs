using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using WebApplicationApi.DataAccess;
using WebApplicationApi.Respositry;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _db;
        public AuthController(IAuthService db)
        {
            _db = db;
        }
        [HttpPost]
        public string Login( LoginModel loginModel)
        {

            var resutl = _db.Login(loginModel);
            return resutl;
        }
    }
}
