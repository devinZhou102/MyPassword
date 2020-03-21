using SQLite.Net.Attributes;
using SQLite.Net.Cipher.Interfaces;
using SQLite.Net.Cipher.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Models
{
    [Table("DataItemModel")]
    public class DataItemModel : IModel, SQLite.Net.Cipher.Interfaces.ICloneable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Icon { get; set; }

        public string CategoryKey { get; set; }

        [Secure]
        public string Name { get; set; }
        [Secure]
        public string Account { get; set; }
        [Secure]
        public string Password { get; set; }

        [Secure]
        public string Description { get; set; }

        string IModel.Id
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                int result = 0;
                if (int.TryParse(value, out result))
                {
                    Id = result;
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
