using Authentication.API.Models;
using Authentication.API.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.BusinessLogic
{
    public class Login : ILogin
    {

        private IUserRepository userRepository;
        
        public Login(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<UserModel> AuthenticateUser(UserModel userRequest)
        {
            var result = await userRepository.AuthenticateUser(userRequest);
            if(string.IsNullOrEmpty(result.UserName) && string.IsNullOrEmpty(result.Password))
            {
                result.IsAuthenticated = false;
                return result;
            }
            else
            {
                result.Password = string.Empty;
                result.IsAuthenticated = true;
            }
            return result;
        }

    }
}
