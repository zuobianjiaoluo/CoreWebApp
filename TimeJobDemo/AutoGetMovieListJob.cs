
using Microsoft.IdentityModel.Logging;
using Pomelo.AspNetCore.TimedJob;
using TimeJobDemo.Services;

namespace TimeJobDemo
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class AutoJob : Job
    {
        IBook iBook;
        public AutoJob(IBook book)
        {
            iBook = book;
        }

        //Begin 起始时间；Interval执行时间间隔，单位是毫秒，建议使用以下格式，3小时(Interval =1000 * 3600 * 3)；
        //SkipWhileExecuting是否等待上一个执行完成，true为等待；
        [Invoke(Begin = "2018-9-12 22:10", Interval = 3000, SkipWhileExecuting = true)]
        public string Run()
        {
            //Job要执行的逻辑代码
            string date=iBook.GetDate();
            return date;
        }
    }
}
