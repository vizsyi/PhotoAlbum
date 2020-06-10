using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyPhotos.ViewModel.Validation
{
    public class ContentTypeValidationAttribute : ValidationAttribute
    {
        public List<string> EnabledContentType { get; set; }

        public ContentTypeValidationAttribute()
            : this(
                  new List<string> { "image/jpeg", "image/png" }
                  ,"Nem megfelelő képformátum: {0}. Ezekből lehet választani: {1}"
                  )
        { }

        public ContentTypeValidationAttribute(List<string> enabledContentType, string errorMessage)
        {
            EnabledContentType = enabledContentType;
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            IFormFile file = value as IFormFile;
            if (file == null)
            {
                return false;
            }
            return EnabledContentType.Contains(file.ContentType);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, string.Join(",", EnabledContentType));
        }
    }
}