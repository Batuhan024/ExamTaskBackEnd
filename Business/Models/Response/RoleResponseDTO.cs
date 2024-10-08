using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Response
{
    public class RoleResponseDTO
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public bool IsAdmin { get; set; } = default!;
    }
}
