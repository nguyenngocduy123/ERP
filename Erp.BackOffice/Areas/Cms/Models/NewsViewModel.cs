using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice.Areas.Cms.Models
{
    public class NewsViewModel
    {
        public NewsViewModel()
        {
            ModifiedDate = CreatedDate = (DateTime)(PublishedDate = DateTime.Now);
        }

        public int Id { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime CreatedDate { get; set; }
        
        [Display(Name = "UpdatedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }
        
        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUser { get; set; }
        public string CreateUserName { get; set; }
        
        [Display(Name = "UpdatedUser", ResourceType = typeof(Wording))]
        public int? UpdateUser { get; set; }
        public string UpdateUserName { get; set; }
   
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "ValueInvalid")]
        public int? OrderNo { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(Wording))]
        public string ThumbnailPath { get; set; }

        public string ThumbnailPath64String { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Wording))]
        
        public string ImagePath { get; set; }

        public string ImagePath64String { get; set; }

        public HttpPostedFileBase SourceFileImage { get; set; }

        public HttpPostedFileBase SourceFileThumbnailImage { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Wording))]
        public int? CategoryId { get; set; }
        public virtual IEnumerable<Category> ListCategory { get; set; }

        [Display(Name = "Publish", ResourceType = typeof(Wording))]
        public bool? IsPublished { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Title", ResourceType = typeof(Wording))]
        [StringLength(250, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        public string Title { get; set; }

        [StringLength(5000, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "ShortDescription", ResourceType = typeof(Wording))]
        public string ShortContent { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        //[StringLength(50000, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError",  ErrorMessage = null)]
        [Display(Name = "Content", ResourceType = typeof(Wording))]
        public string Content { get; set; }
        
        [Display(Name = "Link", ResourceType = typeof(Wording))]
        public string Url { get; set; }

        [Display(Name = "PublishedDate", ResourceType = typeof(Wording))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedDate { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}