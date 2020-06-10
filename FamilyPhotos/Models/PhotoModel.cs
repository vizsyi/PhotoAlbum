using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public Byte[] Picture { get; set; }

        public string ContentTye { get; set; }
    }
}
