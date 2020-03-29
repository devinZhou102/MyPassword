using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Dtos
{
    public class PwdDataDto
    {
        public string CategoryKey { get; set; }

        public DataItemModel Data { get; set; }
    }
}
