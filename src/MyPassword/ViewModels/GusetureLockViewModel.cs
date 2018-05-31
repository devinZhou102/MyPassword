using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public class GusetureLockViewModel:ViewModelBase
    {
        private readonly string[] MonthArray = { "Jan.","Feb.","Mar.","Apr.","May", "Jun.", "Jul.", "Aug.", "Sep.", "Oct.","Nov.","Dec." };

        private string _TodayDate;

        public string TodayDate
        {
            get
            {
                if(_TodayDate == null)
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

        public string Month
        {
            get
            {
                if(null == _Month)
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


        public GusetureLockViewModel()
        {
            TodayDate = DateTime.Now.Day.ToString();
            Month = GetMonthInEnglish(DateTime.Now.Month);
            CompleteCommand = new Command((arg) => CompleteExcute(arg));
        }

        private string GetMonthInEnglish(int month)
        {
            month = month - 1;
            if(month < 0 || month >=12)
            {
                return "";
            }
            return MonthArray[month];
        }

        public ICommand CompleteCommand { get; private set; }

        private void CompleteExcute(object checkList)
        {

            var result = "";
            if (checkList is List<int>)
            {
                var datas = checkList as List<int>;
                foreach (var item in datas)
                {
                    result += item + " ";
                }
            }
            Debug.WriteLine("GuestureLock :" + result);
        }
    }
}
