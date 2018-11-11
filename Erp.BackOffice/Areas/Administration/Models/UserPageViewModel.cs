namespace Erp.BackOffice.Areas.Administration.Models
{
    public class UserPageViewModel
    {
        public int UserId { get; set; }
        public int PageId { get; set; }
        public bool? View { get; set; }
        public bool? Edit { get; set; }
        public bool? Add { get; set; }
        public bool? Delete { get; set; }
        public bool? Import { get; set; }
        public bool? Export { get; set; }
        public bool? Print { get; set; }
    }
}