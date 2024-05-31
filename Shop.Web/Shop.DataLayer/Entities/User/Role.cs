using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.DataLayer.Entities.User
{
    public class Role
    {

        [Key]
        public int RoleId { get; set; }

        [Display(Name=(" "))]
        [Required(ErrorMessage =" Please Enter {0}")]
        [MaxLength(200, ErrorMessage ="{0} can't Be Bigger than {1}")]
        public String RoleTitle { get; set; }

        #region Relations

        public virtual List <UserRole> UserRoles { get; set; }

        #endregion
    }
}
