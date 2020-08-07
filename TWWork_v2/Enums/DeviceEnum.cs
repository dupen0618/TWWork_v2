using System.ComponentModel.DataAnnotations;

namespace TWWork_v2.Enums
{
    public enum DeviceEnum
    {
        [Display(Name = "--请选择机台--")]
        Default,
        [Display(Name = "清洗制绒")]
        ZR01,
        [Display(Name = "扩散")]
        KS01,
        [Display(Name = "激光SE")]
        SE01,
        [Display(Name = "刻蚀")]
        KES01,
        [Display(Name = "退火")]
        TH01,
        [Display(Name = "背钝化")]
        WD01,
        [Display(Name = "背镀膜")]
        BM01,
        [Display(Name = "正镀膜")]
        ZM01,
        [Display(Name = "丝网")]
        SJ01
    }
    
    public enum OBTStationNameEnum
    {
        [Display(Name = "--请选择光电站点--")]
        Default,
        [Display(Name = "IN1")]
        RFID01,
        [Display(Name = "IN2")]
        RFID02,
        [Display(Name = "OUT1")]
        RFID03,
        [Display(Name = "OUT2")]
        RFID04
    }
    public enum RFIDStationNameEnum
    {
        [Display(Name = "--请选择RFID站点--")]
        Default,
        [Display(Name = "IN1")]
        RFID01,
        [Display(Name = "IN2")]
        RFID02,
        [Display(Name = "OUT1")]
        RFID03,
        [Display(Name = "OUT2")]
        RFID04
    }
    
    public enum StationCategoryEnum
    {
        [Display(Name ="--请选择类别--")]
        Default,
        [Display(Name ="光电")]
        OBT,
        [Display(Name = "RFID")]
        RFID
    }

    public enum PushOrPopEnum
    {
        [Display(Name="--请选择进/出站--")]
        Default,
        IN,
        OUT
    }
}