using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Response
{
    public class EmployeeRoleResponseDTO
    {
        public int Id { get; set; } = default!;
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
    }
}
