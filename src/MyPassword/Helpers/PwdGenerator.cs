using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Utils
{

    public class PwdGenerator
    {
        private readonly string typeNumber = "0123456789";
        private readonly string typeLetterLowerCase = "abcdefghijkmnopqrstuvwxyz";
        private readonly string typeLetterUpperCase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        private readonly string typeCharactor = "!@#$%&*=+?";

        static PwdGenerator instance;

        public static PwdGenerator GetInstance()
        {
            if (null == instance)
            {
                instance = new PwdGenerator();
            }
            return instance;
        }

        /// <summary>
        /// 密码生成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Generate(PwdGeneratorParams param)
        {
            StringBuilder password = new StringBuilder();
            int length = param.Length;
            if (length < 6)
            {
                length = 6;
            }
            string seed = GeneratePwdSeed(param);
            for (int index = 0; index < length; index++)
            {
                password.Append(GetPwdCharactor(seed));
            }
            return password.ToString();
        }

        Random rand;
        private string GetPwdCharactor(string seed)
        { 
            int length = seed.Length;
            if (null == rand)
            {
                long tick = DateTime.Now.Ticks;
                rand = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            }
            int index = rand.Next() % length;
            return seed.Substring(index,1);
        }


        private string GeneratePwdSeed(PwdGeneratorParams param)
        {
            StringBuilder builder = new StringBuilder();
            if (param.HasNumber)
            {
                builder.Append(typeNumber);
            }
            if (param.HasLetterLowerCase)
            {
                builder.Append(typeLetterLowerCase);
            }
            if (param.HasLetterUpperCase)
            {
                builder.Append(typeLetterUpperCase);
            }
            if (param.HasCharactor)
            {
                builder.Append(typeCharactor);
            }
            return builder.ToString();
        }
    }
}
