using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Response
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Surname { get; set; }
        public string Role { get; set; }
        public DateTime Expiration { get; set; }
    }
}
