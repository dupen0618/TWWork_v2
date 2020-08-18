using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TWWork_v2
{
	public class LogManager
	{
		private static string logPath = string.Empty;  //用来保存程序的基目录
		public static bool isLog = true;  //
		public static int LogLevel = 1;   //logLevel =1 为所有
		public static int critical_LV = 5; //出错信息与起始信息
		public static int normal_LV = 1; //正常级别，用来重置的level
		private static string logFielPrefix = string.Empty;

		//logLevel =1 为所有； logLevel = 5 为只有出错信息 与起始信息；
		/// <summary>     
		/// 保存日志的文件夹     
		/// </summary>     
		public static string LogPath
		{
			get
			{
				if (logPath == string.Empty)
				{
					//if (System.Web.HttpContext.Current == null)                     
					// Windows Forms 应用                     
					logPath = AppDomain.CurrentDomain.BaseDirectory + "LogFiles//";//获取程序的基目录
																				   //else                      
																				   // Web 应用                      
																				   // logPath = AppDomain.CurrentDomain.BaseDirectory + @"bin\";             
				}
				return logPath;
			}
			set
			{
				logPath = value;
			}
		}

		/// <summary>     
		/// 日志文件前缀     
		/// </summary>      
		public static string LogFielPrefix
		{
			get
			{
				return logFielPrefix;
			}
			set
			{
				logFielPrefix = value;
			}
		}

		/// <summary>     
		/// 写日志     
		/// </summary> 
		/// 
		public static void WriteLog(string logFile, string msg)
		{
			int Level = normal_LV;   //初始level为1，默认是输出所有信息
			_WriteLog(Level, logFile, msg);
		}
		public static void WriteLog_Critical(string logFile, string msg)
		{
			int Level = critical_LV; //出错信息与起始信息
			_WriteLog(Level, logFile, msg);
		}

		public static void _WriteLog(int level, string logFile, string msg)
		{
			if (!isLog)
				return;
			if (LogLevel > level)
				return;
			if (!Directory.Exists(LogPath)) //是否存在该文件路径
			{
				Directory.CreateDirectory(LogPath); //不存在，新建文件目录
			}

			try
			{
				StreamWriter sw = File.AppendText(
					LogPath + LogFielPrefix + logFile + " " +
					DateTime.Now.ToString("yyyy-MM-dd-HH") + ".Log");      //生成日志文件

				sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff ")
					+ msg);
				sw.Close();
			}
			catch
			{ }
		}

		/// <summary>     
		/// 写日志     
		/// </summary>      
		public static void WriteLog(LogFile logFile, string msg) //前一个LogFile是枚举方法名， 后一个是日志信息
		{
			WriteLog(logFile.ToString(), msg);
		}
	}
	/// <summary> 
	/// 日志类型 
	/// </summary> 
	public enum LogFile { Trace, Warning, Error, SQL }

	/*
     使用方法： 
        LogManager.LogFielPrefix = "ERP ";
        LogManager.LogPath = @"C:\"; 
        LogManager.WriteLog(LogFile.Trace, "A test Msg.");    
    */
}
