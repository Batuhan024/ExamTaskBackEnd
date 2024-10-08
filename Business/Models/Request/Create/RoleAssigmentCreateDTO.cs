using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Request.Create
{
    public class RoleAssigmentCreateDTO
    {
        public int AdminUserId { get; set; }
        public int EmployeeId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
