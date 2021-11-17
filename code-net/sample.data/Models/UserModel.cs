using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Data.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public List<AddressModel> Addresses { get; set; }
    }
}
