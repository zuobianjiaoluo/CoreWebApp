using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeJobDemo.Models
{

    /// <summary>
    /// 通话记录详情
    /// </summary>
    public class CallInstance
    {
        public int code { get; set; }
        public DataA data { get; set; }
        public string resultMsg { get; set; }
        public object errorStackTrace { get; set; }
    }

    public class DataA
    {
        public string dataType { get; set; }
        public DataB data { get; set; }
    }

    public class DataB
    {
        public Sceneinstance sceneInstance { get; set; }
        public Taskresult [] taskResult { get; set; }
        public Phonelog phoneLog { get; set; }
        public bool jobFinished { get; set; }
        public object sign { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Sceneinstance
    {
        public int callInstanceId { get; set; }
        public int companyId { get; set; }
        public int callUserId { get; set; }
        public int callJobId { get; set; }
        public int customerId { get; set; }
        public string customerTelephone { get; set; }
        public string customerName { get; set; }
        public int status { get; set; }
        public int finishStatus { get; set; }
        public int duration { get; set; }
        public int chatRound { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string callerPhone { get; set; }
        public string luyinOssUrl { get; set; }
        public string userLuyinOssUrl { get; set; }
        public string properties { get; set; }
        public string handlePerson { get; set; }
        public int callType { get; set; }
        public int callIndex { get; set; }
        public int readStatus { get; set; }
        public string jobName { get; set; }
        public int robotDefId { get; set; }
        public int sceneDefId { get; set; }
        public int sceneRecordId { get; set; }
        public string corpName { get; set; }
        public string industry { get; set; }
        //public object trackResult { get; set; }
        //public object bugType { get; set; }
        public int hangUp { get; set; }
        public string secondaryCallTime { get; set; }
        public int secondaryCallTimes { get; set; }
        public int cost { get; set; }
        public int callbacked { get; set; }
        public string gmtCreate { get; set; }
        public string gmtModified { get; set; }
        public Propertiesmap propertiesMap { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Propertiesmap
    {
        public string building_name { get; set; }
        public string room_name { get; set; }
        public string construction_area { get; set; }
        public string community_name { get; set; }
        public string price { get; set; }
        public string name { get; set; }
        public string trade_type { get; set; }
        //public string 客户名称 { get; set; }
        //public string 联系方式 { get; set; }
        public string appellation { get; set; }
        public string work_order_num { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Phonelog
    {
        public PhoneLogs [] phoneLogs { get; set; }
        public string luyinOssUrl { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PhoneLogs
    {
        public int sceneInstanceLogId { get; set; }
        public int sceneInstanceId { get; set; }
        public int companyId { get; set; }
        public int robotDefId { get; set; }
        public int decisionId { get; set; }
        public string speaker { get; set; }
        public string content { get; set; }
        public string userMean { get; set; }
        //public object userMeanDetail { get; set; }
        public bool aiUnknown { get; set; }
        public int? answerStatus { get; set; }
        //public object studyStatus { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string gmtCreate { get; set; }
        public string gmtModified { get; set; }
        //public object knowledgeBaseId { get; set; }
        //public object correctionContent { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Taskresult
    {
        public int sceneInstanceResultId { get; set; }
        public int companyId { get; set; }
        public int sceneInstanceId { get; set; }
        public string resultName { get; set; }
        public string resultValue { get; set; }
        public string artificialResultValue { get; set; }
        //public object artificialChanged { get; set; }
        public string resultDesc { get; set; }
        public string extra { get; set; }
        public string analyzeType { get; set; }
        public object gmtCreate { get; set; }
        public object gmtModified { get; set; }
        public string resultValueAlias { get; set; }
    }


    //public class TaskresultExtra
    //{
    //    public object [] cells { get; set; }
    //    public Resultcell [] resultCell { get; set; }
    //    public string userLevel { get; set; }
    //}

    //public class Resultcell
    //{
    //    public string value { get; set; }
    //    public bool _checked { get; set; }
    //}
}
