using Authentication.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.S3Service
{
    public interface IS3Service
    {
        Task<IEnumerable<Product>> GetFiles();
    
    }
}
