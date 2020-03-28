using SQLite.Net.Attributes;
using SQLite.Net.Cipher.Interfaces;
using SQLite.Net.Cipher.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Models
{
    [Table("table_data")]
    public class DataItemModel : IModel, SQLite.Net.Cipher.Interfaces.ICloneable
    {
        public static string TableName = "table_data";

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Icon { get; set; }

        public string CategoryKey { get; set; }

        public string Name { get; set; }

        public string Account { get; set; }
        [Secure]
        public string Password { get; set; }

        public string Phone { get; set; }

        public DateTimeOffset UpdateTime { get; set; }

        public string Description { get; set; }

        string IModel.Id
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                if (int.TryParse(value, out int result))
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
