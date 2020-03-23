using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        
        public IconModel FontIcon { get; set; }

        public string Key { get; set; }
    }
}
