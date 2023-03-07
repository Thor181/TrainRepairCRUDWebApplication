using System;
using System.Collections.Generic;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? IdRole { get; set; }
        public string RoleName { get; set; } = null!;
        public virtual Role? IdRoleNavigation { get; set; }
    }
}
