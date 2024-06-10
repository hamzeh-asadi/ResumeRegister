using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ResumeRegister.datalayer.Entities.Users
{
    public class UserRole
    
        {
        public UserRole()
        {
            
        }

        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }


        #region Relations

        public virtual UserInfo User { get; set; }
        public virtual Role Role { get; set; }

        #endregion

    
    }
}
