using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPassword.Services
{
    public class PwdGenerateService:IPwdGenerateService
    {
        private readonly string typeNumber = "0123456789";
        private readonly string typeLetterLowerCase = "abcdefghijkmnopqrstuvwxyz";
        private readonly string typeLetterUpperCase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        private readonly string typeCharacter = "!@#$%&*=+?";

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

            if (param.HasCharactor) password.Append(GetPwdCharactor(typeCharacter));
            if (param.HasLetterLowerCase) password.Append(GetPwdCharactor(typeLetterLowerCase));
            if (param.HasLetterUpperCase) password.Append(GetPwdCharactor(typeLetterUpperCase));
            if (param.HasNumber) password.Append(GetPwdCharactor(typeNumber));

            string seed = GeneratePwdSeed(param);
            var len = length - password.Length;
            for (int index = 0; index < len; index++)
            {
                password.Append(GetPwdCharactor(seed));
            }
            password = GetDisorderlyOrder(password.ToString());
            return password.ToString();
        }

        /// <summary>
        /// 乱序
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private StringBuilder GetDisorderlyOrder(string value)
        {
            StringBuilder result = new StringBuilder();
            List<int> list = new List<int>();
            for (var index = 0; index < value.Length; index++) list.Add(index);
            //随机排序
            for (var c = 0; c < value.Length / 2; c++)
                list.Sort(delegate (int a, int b) { return (GetRandom()).Next(-1, 1); });
            foreach (var index in list)
            {
                result.Append(value.ElementAt(index));
            }
            return result;
        }

        private Random GetRandom()
        {
            if (null == rand)
            {
                long tick = DateTime.Now.Ticks;
                rand = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            }
            return rand;
        }

        Random rand;
        private string GetPwdCharactor(string seed)
        {
            int length = seed.Length;
            var rand = GetRandom();
            int index = rand.Next() % length;
            return seed.Substring(index, 1);
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
                builder.Append(typeCharacter);
            }
            return builder.ToString();
        }
    }
}
