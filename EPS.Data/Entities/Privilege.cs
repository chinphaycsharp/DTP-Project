using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Privilege
    {
        public Privilege()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
            UserPrivileges = new HashSet<UserPrivilege>();
        }

        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
        public bool? Status { get; set; }

        [InverseProperty("Privilege")]
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        [InverseProperty("Privilege")]
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }
    }

    public enum PrivilegeList
    {
        [Description("Xem người dùng")]
        ViewUser,
        [Description("Sửa người dùng")]
        ManageUser,
        [Description("Xem nhóm người dùng")]
        ViewRole,
        [Description("Sửa nhóm người dùng")]
        ManageRole,
        [Description("Xem menu")]
        ViewMenu,
        [Description("Sửa menu")]
        EditMenu,
        [Description("Thêm menu")]
        CreateMenu,
        [Description("Xoá menu")]
        DeleteMenu
    }
}
