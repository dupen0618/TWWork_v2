using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TWWork_v2.Enums;

namespace TWWork_v2.Utils
{
    public class DataProcess
    {
        public static string GetDataValue(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        public static string GetStationName(string stationName)
        {
            string station = "";
            switch (stationName)
            {
                case "IN1":
                    station = "RFID01";
                    break;
                case "IN2":
                    station = "RFID02";
                    break;
                case "OUT1":
                    station = "RFID03";
                    break;
                case "OUT2":
                    station = "RFID04";
                    break;
                case "01":
                    station = "IN1";
                    break;
                case "02":
                    station = "IN2";
                    break;
                case "03":
                    station = "OUT1";
                    break;
                case "04":
                    station = "OUT2";
                    break;
            }
            return station;
        }
        
        public static List<string> SetProductionLine(int num)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < num; i++)
            {
                list.Add($"4-{i + 1}");
            }

            return list;
        }

        public static string GetEnumName(Enum en)
        {
            var type= en.GetType();//先获取这个枚举的类型
            var field=type.GetField(en.ToString());//通过这个类型获取到值
            var obj = (DisplayAttribute)field.GetCustomAttribute(typeof (DisplayAttribute));//得到特性
            return obj.Name ?? "";
        }

        public static DeviceEnum GetDviceEnum(string deviceName)
        {
            switch (deviceName)
            {
                case ("清洗制绒"):
                    return DeviceEnum.ZR01;
                case ("扩散"):
                    return DeviceEnum.KS01;
                case ("激光SE"):
                    return DeviceEnum.SE01;
                case ("刻蚀"):
                    return DeviceEnum.KS01;
                case ("退火"):
                    return DeviceEnum.TH01;
                case ("背钝化"):
                    return DeviceEnum.WD01;
                case ("背镀膜"):
                    return DeviceEnum.BM01;
                case ("正镀膜"):
                    return DeviceEnum.ZM01;
                case ("丝网"):
                    return DeviceEnum.SJ01;
                default:
                    return DeviceEnum.Default;
            }
        }

        public static OBTStationNameEnum GetStationEmun(string station)
        {
            switch (station)
            {
                case ("IN1"):
                    return OBTStationNameEnum.RFID01;
                case ("IN2"):
                    return OBTStationNameEnum.RFID02;
                case ("OUT1"):
                    return OBTStationNameEnum.RFID03;
                case ("OUT2"):
                    return OBTStationNameEnum.RFID04;
                default:
                    return OBTStationNameEnum.Default;
            }
        }
    }
}