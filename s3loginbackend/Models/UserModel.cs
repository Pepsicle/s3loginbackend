using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s3loginbackend.Models
{
    public class UserModel
    {
        public string userName { get; set; }
        public int userId { get; set; }

        public UserModel(string username, int userid)
        {
            userName = username;
            userId = userid;
        }
    }
}
