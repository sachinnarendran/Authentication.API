using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Authentication.API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.UserRepository
{
    public class UserRepos : IUserRepository
    {

        private DynamoDBContext DynamoDBContext;
        private AmazonDynamoDBClient dynamoDBClient;
        private IConfiguration Configuration;
        private string accessKey, secretKey;

        public UserRepos(IDynamoDBContext _dynamoDBContext,IConfiguration configuration)
        {
            
            Configuration = configuration;
            accessKey = configuration.GetValue<string>("AWS:AccessKey");
            secretKey = configuration.GetValue<string>("AWS:SecretKey");
            dynamoDBClient = new AmazonDynamoDBClient(accessKey,secretKey,RegionEndpoint.USEast2);
            DynamoDBContext = new DynamoDBContext(dynamoDBClient);
        }
        public async Task<UserModel> AuthenticateUser(UserModel userModel)
        {
            try
            {
                UserModel userData = new UserModel();
                

                var query = new QueryRequest
                {
                    TableName = "Userlogin",
                    KeyConditionExpression = "Username = :v_Id and Password = :password",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        {":v_Id", new AttributeValue{S = userModel.UserName} },
                        { ":password", new AttributeValue { S = userModel.Password} }
                       
                    }
                };
                var resultSet = await dynamoDBClient.QueryAsync(query);
                var data = resultSet.Items.FirstOrDefault();
                var doc = Document.FromAttributeMap(data);
                userData = DynamoDBContext.FromDocument<UserModel>(doc);
                
                
                return userData;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
