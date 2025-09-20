using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComJwtCrud.Application.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
    }
}
