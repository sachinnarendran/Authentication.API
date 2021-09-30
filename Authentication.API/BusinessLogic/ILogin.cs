using Authentication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.BusinessLogic
{
    public interface ILogin
    {
        Task<UserModel> AuthenticateUser(UserModel userRequest);

    
    }
}
