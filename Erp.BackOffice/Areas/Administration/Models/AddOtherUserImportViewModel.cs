using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class AddOtherUserImportViewModel
    {
        [Display(Name = "Username", ResourceType = typeof(Wording))]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessageResourceName = "Password_LengthError", ErrorMessageResourceType = typeof(Error), MinimumLength = 6)]
        [Display(Name = "Password", ResourceType = typeof(Wording))]
        public string Password { get; set; }

        [Display(Name = "Mã Nhân Viên")]
        public string UserCode { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(Wording))]
        public System.DateTime? DateOfBirth { get; set; }

        [Display(Name = "PlaceOfBirth", ResourceType = typeof(Wording))]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "IDCardNo", ResourceType = typeof(Wording))]
        public string IDCardNo { get; set; }

        [Display(Name = "IDCardDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> IDCardDate { get; set; }

        [Display(Name = "IDCardPlace", ResourceType = typeof(Wording))]
        public string IDCardPlace { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Wording))]
        public string Address { get; set; }

        [Display(Name = "PhoneNumer", ResourceType = typeof(Wording))]
        public string Mobile { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Gender", ResourceType = typeof(Wording))]
        public Nullable<bool> Sex { get; set; }

        [Display(Name = "Region", ResourceType = typeof(Wording))]
        public int RegionId { get; set; }

        [Display(Name = "Team Leader")]
        public Nullable<bool> TeamLeader { get; set; }

        [Display(Name = "Tên cửa hàng")]
        public string GroupName { get; set; }
        public int? GroupId { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public Status CheckStatus { get; set; }

        [Display(Name = "Level", ResourceType = typeof(Wording))]
        public string Level { get; set; }
        public int? LevelId { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Wording))]
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Wording))]
        public string PositionName { get; set; }
        public int? PositionId { get; set; }

        [Display(Name = "UserType", ResourceType = typeof(Wording))]
        public string UserTypeName { get; set; }
        public int? UserTypeId { get; set; }
        [Display(Name = "MCS ID")]
        public string MCS_ID { get; set; }
        [Display(Name = "Channel Code")]
        public string CHANNEL_CODE { get; set; }
        [Display(Name = "Channel Name")]
        public string CHANNEL_NAME { get; set; }
        [Display(Name = "Channel Address")]
        public string CHANNEL_ADDRESS { get; set; }
        
        public string IMEI { get; set; }
        public string BankName { get; set; }
        public string BankOwnerName { get; set; }
        public string BankBranch { get; set; }
        public string BankUserNumber { get; set; }



        public enum Status
        {
            [Display(Name = "Successfull")]
            Successfull = 1,

            [Display(Name = "Failed")]
            Failed = 0
        }
    }
}