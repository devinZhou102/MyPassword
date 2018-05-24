using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Models
{
    /// <summary>
    /// 密码生成配置参数
    /// </summary>
    public class PwdGeneratorParams
    {
        /// <summary>
        /// 需要数字
        /// </summary>
        public bool HasNumber{get;set; }
        /// <summary>
        /// 需要小写字母
        /// </summary>
        public bool HasLetterLowerCase { get; set; }
        /// <summary>
        /// 需要大写字母
        /// </summary>
        public bool HasLetterUpperCase { get; set; }
        /// <summary>
        /// 需要特殊字符
        /// </summary>
        public bool HasCharactor { get; set; }

        public int Length { get; set; }

        public bool IsOk()
        {
            if (!HasCharactor && !HasLetterLowerCase && !HasLetterUpperCase && !HasNumber)
            {
                return false;
            }
            return true;
        }

    }
}
