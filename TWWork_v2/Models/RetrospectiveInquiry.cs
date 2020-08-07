using System;
using System.ComponentModel.DataAnnotations;

namespace TWWork_v2.Models
{
    public class RetrospectiveInquiry
    {
        [Key]
        public int? No{get;set;}
        public string DeviceName{get;set;}
        public string BoxId{get;set;}
        public string RFIDStationName {get;set;}
        public string TraceID {get;set;}
        public DateTime? CreateDate{get;set;}
        public int? MyPassNum{get;set;}
        public DateTime? LateTime{get;set;}
        // public string MyTraceId{get;set;}
        // public DateTime? MyLateTime	{get;set;}
        public string MyDeviceName{get;set;}
        public string PushOrPop{get;set;}

    }
}