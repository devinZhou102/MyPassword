﻿using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPassword.Manager
{
    public class LockManager
    {
        private const string KEY_GUESTURE_LOCK = "guestureLock";

        private static Lazy<LockManager> instance = new Lazy<LockManager>(()=>new LockManager());

        public static LockManager Instance => instance.Value;

        public string GuestureLock { get; private set; }
        
        private LockManager()
        {
            Read();
        }

        /// <summary>
        /// 手势密码 数字
        /// </summary>
        public String[] GesturePwdData = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="indexList"></param>
        /// <returns></returns>
        public bool IsLockValid(List<int> indexList)
        {
            if(null == indexList || indexList.Count < 4)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 生成手势密码 MD5加密
        /// </summary>
        /// <param name="indexList"></param>
        /// <returns></returns>
        public String GeneratePassword(List<int> indexList)
        {
            if (!IsLockValid(indexList)) return "";
            String pwd = "";
            foreach (int index in indexList)
            {
                if (index >= 0 && index < GesturePwdData.Length)
                {
                    pwd += GesturePwdData.ElementAt(index);
                }
            }
            return pwd;
        }

        private void Read()
        {
            GuestureLock = CrossSecureStorage.Current.GetValue(KEY_GUESTURE_LOCK, "");
        }

        public bool Save(string value)
        {
            if (CrossSecureStorage.Current.SetValue(KEY_GUESTURE_LOCK, value))
            {
                GuestureLock = value;
                return true;
            }
            return false;
        }
    }
}