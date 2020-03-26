using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Data.Models
{
    public class Book : LibraryAsset
    {
        [Required] 
        [StringLength(20)]
        [Display(Name = "ISBN #")] 
        public string Isbn { get; set; }

        [Required]
        [StringLength(60)]
        public string Author { get; set; }

        [Required]
        [Display(Name = "DDC")] 
        public string DeweyIndex { get; set; }
    }
}
