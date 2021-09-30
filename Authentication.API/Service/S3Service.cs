using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Authentication.API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Authentication.API.S3Service
{
    public class S3ServiceRepo:IS3Service
    {
        private IConfiguration configuration;
        private AmazonS3Client amazonS3Client;
        private string accessKey, secretKey;

        public S3ServiceRepo(IConfiguration _configuration)
        {
            configuration = _configuration;
            accessKey = configuration.GetValue<string>("AWS:AccessKey");
            secretKey = configuration.GetValue<string>("AWS:SecretKey");
            amazonS3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast2);
        }
     public async Task<IEnumerable<Product>> GetFiles()
     {
            List<Product> products = new List<Product>();
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest
            {
                BucketName = configuration.GetValue<string>("AWS:BucketName")
            };
            var listObjectResponse = await amazonS3Client.ListObjectsAsync(listObjectsRequest);
            foreach (var item in listObjectResponse.S3Objects)
            {
                var fileContent = await amazonS3Client.GetObjectAsync(configuration.GetValue<string>("AWS:BucketName"), item.Key);
                StreamReader reader = new StreamReader(fileContent.ResponseStream);
                var content = reader.ReadToEnd();
                var productDetail = JsonSerializer.Deserialize<Product>(content);
                products.Add(productDetail);
            }
            return products;
        }
    }
}
