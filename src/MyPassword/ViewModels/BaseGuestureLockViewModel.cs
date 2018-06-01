using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using MyPassword.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public abstract class BaseGuestureLockViewModel:ViewModelBase
    {
        private readonly string[] MonthArray = { "Jan.", "Feb.", "Mar.", "Apr.", "May", "Jun.", "Jul.", "Aug.", "Sep.", "Oct.", "Nov.", "Dec." };

        private readonly string ColorRed = "#f72b03";
        private readonly string ColorBlue = "#6fa8dc";

        private string _TodayDate;

        public string TodayDate
        {
            get
            {
                if (_TodayDate == null)
                {
                    _TodayDate = "";
                }
                return _TodayDate;
            }
            set
            {
                _TodayDate = value;
                RaisePropertyChanged(nameof(TodayDate));
            }
        }

        private string _Month;

        private string _Message;

        public string Message
        {
            get
            {
                if(_Message == null)
                {
                    _Message = "";
                }
                return _Message;
            }
            set
            {
                _Message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }

        private string _MessageColor;

        public string MessageColor
        {
            get
            {
                if(_MessageColor == null)
                {
                    _MessageColor = ColorBlue;
                }
                return _MessageColor;
            }
            set
            {
                _MessageColor = value;
                RaisePropertyChanged(nameof(MessageColor));
            }
        }

        public string Month
        {
            get
            {
                if (null == _Month)
                {
                    _Month = "";
                }
                return _Month;
            }
            set
            {
                _Month = value;
                RaisePropertyChanged(nameof(Month));
            }
        }

        public BaseGuestureLockViewModel()
        {
            TodayDate = DateTime.Now.Day.ToString();
            Month = GetMonthInEnglish(DateTime.Now.Month);
            CompleteCommand = new Command((arg) => CompleteExcute(arg));
        }


        private string GetMonthInEnglish(int month)
        {
            month = month - 1;
            if (month < 0 || month >= 12)
            {
                return "";
            }
            return MonthArray[month];
        }

        public ICommand CompleteCommand { get; private set; }

        private void CompleteExcute(object checkList)
        {
            if (checkList is List<int>)
            {
                var datas = checkList as List<int>;
                if (!LockManager.Instance.IsLockValid(datas))
                {
                    Message = "至少连续绘制4个点";
                    MessageColor = ColorRed;
                }
                else
                {
                    string myLock = LockManager.Instance.GeneratePassword(datas);
                    CreateGuestureLockSuccess(myLock);
                }
            }
        }

        protected abstract void CreateGuestureLockSuccess(string strLock);

        //protected abstract void ShowErrorTips();
    }
}
