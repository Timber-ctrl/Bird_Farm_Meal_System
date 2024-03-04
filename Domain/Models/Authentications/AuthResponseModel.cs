using Google.Api.Gax.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Authentications
{
    public class AuthResponseModel
    {
            public string Access_token {  get; set; }   
            public  UserDataModel User { get; set; }    
    }
    public class UserDataModel
    {
        public Guid Uuid { get; set; }
        public string Role { get; set; } = null!;
        public InfoManager Data { get; set; }
        
    }
    public class InfoManager
    {
        public string DisplayName { get; set; } = null!;
        public string? PhotoURL { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
