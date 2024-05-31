using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.DataLayer.Entities.User
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Display(Name = ("UserName: "))]
        [Required(ErrorMessage = " Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} can't Be Bigger than {1}")]
        public string UserName { get; set; }

        [Display(Name = ("Email Address: "))]
        [Required(ErrorMessage = " Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} can't Be Bigger than {1}")]
        [EmailAddress(ErrorMessage = "Email Is Not Valid")]
        public string Email { get; set; }

        [Display(Name = ("Password: "))]
        [Required(ErrorMessage = " Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} can't Be Bigger than {1}")]
        public string Password { get; set; }

        [Display(Name = ("Active Code: "))]
        [MaxLength(50, ErrorMessage = "{0} can't Be Bigger than {1}")]

        public string ActiveCode { get; set; }

        [Display(Name = ("Statues: "))]
        public bool IsActive { get; set; }

        [Display(Name = ("Avatar: "))]
        [MaxLength(200, ErrorMessage = "{0} can't Be Bigger than {1}")]

        public string UserAvatar { get; set; }

        [Display(Name = ("Register Time: "))]
        public DateTime RegisterDate { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

        #endregion
    }
}
