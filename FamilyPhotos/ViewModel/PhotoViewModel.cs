using FamilyPhotos.ViewModel.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel
{
    public class PhotoViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [FormFileLengthValidation]
        [ContentTypeValidation]
        [Display(Name = "Picture")]
        public IFormFile PictureFromBrowser { get; set; }
        //MVC Asp:Net 4.6 IFormFile nincs. Helyette: public HttpPostedFileWrapper
     }
}
