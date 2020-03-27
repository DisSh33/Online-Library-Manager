using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebMVC.Models.Catalog
{
    public class AssetHoldModel
    {
        public int PatronCardId { get; set; }
        public string PatronName { get; set; }
        public string HoldPlaced { get; set; }
    }
}
