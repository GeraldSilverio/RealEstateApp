using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUser { get; set; }
        public string Phone { get; set; }
        public int CountRealEstate {  get; set; }
        public bool IsVerified { get; set; }
        public List<string> Properties { get; set; }
        public List<string> Roles { get; set; }
        public bool IsActive { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
