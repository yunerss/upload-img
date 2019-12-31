using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImgProjects.Models
{
    public class Image
    {
        public String ImageName { get; set; }
        public String Type { get; set; }
        public Int32 Size { get; set; }
        public String ImageUrl { get; set; }
    }
}