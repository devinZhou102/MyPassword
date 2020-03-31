using MyPassword.Localization;
using MyPassword.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyPassword.ViewModels
{
    public abstract class BaseGuestureLockViewModel:BaseViewModel
    {
        private readonly string[] MonthArray = {
            "January.", "February.", "March.", "April.",
            "May.", "June.", "July.", "August.",
            "September.", "October.", "November.", "December." };

        private readonly string[] PoemArray =
        {
            "正月晴和风气新，纷纷已有醉游人。",
            "新年都未有芳华，二月初惊见草芽。",
            "故人西辞黄鹤楼，烟花三月下扬州。",
            "人间四月芳菲尽，山寺桃花始盛开。",
            "五月天山雪，无花只有寒。",
            "毕竟西湖六月中，风光不与四时同。",
            "七月七日长生殿，夜半无人私语时。",
            "八月秋高风怒号，卷我屋上三重茅。",
            "可怜九月初三夜，露似真珠月似弓。",
            "庭中栽得红荆树，十月花开不待春。",
            "十一月中长至夜，三千里外远行人。",
            "寒冬十二月，晨起践严霜。",
        };

        private readonly string[] PhraseArray =
        {
            "These violent delights,have violent ends.",
            "最是红衰绿减处，望尽天涯满是愁。",
            "弓开如秋月行天，箭去似流星落地。",
            "自在飞花轻似梦，无边丝雨细如愁。",
            "大江东去，浪淘尽，千古风流人物。",
            "Stay Hungry,Stay Foolish.",
            "Change, impermanence is characteristic of life.",
            "Being on sea, sail; being on land, settle.",
            "Better to light one candle than to curse the darkness.",
            "The first step is as good as half over.",
            "Fear can hold you prisoner. Hope can set you free.",
            "A strong man can save himself. A great man can save another.",
            "I guess it comes down to a simple choice:get busy living or get busy dying. ",
            "Hope is good thing, mabye the best of things. And no good thing ever dies. ",
            "Some birds are not close, because they are too bright.",
            "人生若只如初见，何事秋风悲画扇。",
            "愿得一心人，白头不相离。",
            "山无陵，江水为竭。冬雷震震，夏雨雪。天地合，乃敢与君绝。",
            "海上生明月，天涯共此时。",
            "月上柳梢头，人约黄昏后。",
            "男儿何不带吴钩，收取关山五十州。",
            "风萧萧兮易水寒，壮士一去兮不复还。",
            "落红不是无情物，化作春泥更护花。",
            "忽如一夜春风来，千树万树梨花开。",
            "天生我材必有用，千金散尽还复来。",
            "人有悲欢离合，月有阴晴圆缺，此事古难全。但愿人长久，千里共婵娟。",
            "出淤泥而不染，濯清涟而不妖，中通外直，不蔓不枝，香远益清，亭亭净植，可远观而不可亵玩焉。",
            "三十功名尘与土，八千里路云和月。",
            "不以物喜，不以己悲；居庙堂之高则忧其民；处江湖之远则忧其君。",
            "先天下之忧而忧，后天下之乐而乐。",
            "关关雎鸠，在河之洲。窈窕淑女，君子好逑。",
            "等闲识得东风面，万紫千红总是春。",
            "问君能有几多愁？恰似一江春水向东流。",
            "安得广厦千万间，大庇天下寒士俱欢颜，风雨不动安如山。",
            "众里寻他千百度。蓦然回首，那人却在，灯火阑珊处。",
            "洛阳亲友如相问，一片冰心在玉壶。",
            "老当益壮，宁移白首之心？穷且益坚，不坠青云之志。",
            "关山难越，谁悲失路之人；萍水相逢，尽是他乡之客。",
            "落霞与孤鹜齐飞，秋水共长天一色。",
            "自古逢秋悲寂寥，我言秋日胜春朝。",
            "长风破浪会有时，直挂云帆济沧海。",
            "谁道人生无再少？门前流水尚能西！休将白发唱黄鸡。",

        };

        
        protected readonly string ColorRed = "#FF2D2D";
        protected readonly string ColorBlue = "#2894FF";

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

        private string _Phrase;

        public string Phrase
        {
            get
            {
                if(_Phrase == null)
                {
                    _Phrase = "";
                }
                return _Phrase;
            }
            set
            {
                _Phrase = value;
                RaisePropertyChanged(nameof(Phrase));
            }
        }

        protected IGuestureLockService guetureLockSerivce;
        public BaseGuestureLockViewModel(IGuestureLockService guetureLockSerivce)
        {
            int day = DateTime.Now.Day;
            TodayDate = day.ToString();
            int month = DateTime.Now.Month;
            Month = GetMonthInEnglish(month);
            Phrase = GetPhrase(month,day);
            CompleteCommand = new Command((arg) => CompleteExcute(arg));
            this.guetureLockSerivce = guetureLockSerivce;
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

        private string GetPhrase(int month,int day)
        {
            month = month - 1;
            int length = PhraseArray.Length;
            int index = new Random().Next() % length;
            if (month < 0 || month >= 12 || day != 1)
            {
                return PhraseArray[index];
            }
            else
            {
                return PoemArray[month];
            }
        }

        public ICommand CompleteCommand { get; private set; }

        private void CompleteExcute(object checkList)
        {
            if (checkList is List<int>)
            {
                var datas = checkList as List<int>;
                if (!guetureLockSerivce.IsLockValid(datas))
                {
                    Message = AppResource.MsgGuesturePoint;
                    MessageColor = ColorRed;
                }
                else
                {
                    Message = "";
                    string myLock = guetureLockSerivce.GeneratePassword(datas);
                    CreateGuestureLockSuccessAsync(myLock);
                }
            }
        }

        protected abstract Task CreateGuestureLockSuccessAsync(string strLock);

    }
}
