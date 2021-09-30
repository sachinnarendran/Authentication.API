using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Models
{
    [DynamoDBTable("Userlogin")]
    public class UserModel
    {
        [DynamoDBProperty("Username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}
