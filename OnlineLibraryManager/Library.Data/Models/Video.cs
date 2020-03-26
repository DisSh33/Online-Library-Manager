using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Data.Models
{
    public class Video : LibraryAsset
    {
        [Required]
        [StringLength(60)]
        public string Director { get; set; }
    }
}
