using Authentication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.UserRepository
{
    public interface IUserRepository
    {
        Task<UserModel> AuthenticateUser(UserModel userModel);
    }
}
