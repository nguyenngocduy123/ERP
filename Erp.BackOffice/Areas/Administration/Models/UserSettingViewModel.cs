using Erp.BackOffice.App_GlobalResources;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class UserSettingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Key", ResourceType = typeof(Wording))]
        public string SettingName { get; set; }
        public string SettingKey { get; set; }
        public string SettingNote { get; set; }
        public int SettingId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Value", ResourceType = typeof(Wording))]
        public string Value { get; set; }
    }
}