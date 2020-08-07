using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using TWWork_v2.Models;

namespace TWWork_v2.Controllers
{
    public class ComtrolLogController : Controller
    {
        
        //private string logFolderPath = @"C:\Users\cisco\Desktop\controlLog\Debug\log";
        //private string logFolderPath = @"C:\Users\Administrator\Desktop\Release\log_back\";
        private string logFolderPath = @"C:\Users\cisco\Desktop\controlLog\Debug\log\";
        private string copyFolderPath = $"{AppDomain.CurrentDomain.BaseDirectory}log\\";
        
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        public JsonResult GetData()
        {
            DirectoryInfo[] logFolderArr = new DirectoryInfo(logFolderPath).GetDirectories();
            //Array.Sort(logFolderArr);
            List<LayUiDataModel> layUiDataModels = new List<LayUiDataModel>();

            int count = logFolderArr.Length;
            
            for (var i = 0;i < logFolderArr.Length;i ++)
            {
                List<LayUiDataModel> logList = new List<LayUiDataModel>();
                
                FileInfo[] logFileArr = new DirectoryInfo($"{logFolderArr[i]}").GetFiles();
                for (int j = 0;j< logFileArr.Length;j ++)
                {
                    logList.Add(new LayUiDataModel
                    {
                        title = logFileArr[j].Name,
                        id= j + count,
                        field = logFileArr[j].Name,
                        @checked = false,
                        spread = false
                    });
                    count++;
                }
                
                layUiDataModels.Add(new LayUiDataModel
                {
                    title = logFolderArr[i].Name,
                    id= i,
                    field = logFolderArr[i].Name,
                    @checked = false,
                    spread = false,
                    children = logList
                });
            }
            
            layUiDataModels.Sort((x,y)=>y.title.CompareTo(x.title));
            string json = JsonConvert.SerializeObject(layUiDataModels);
            return Json(json);
            
            //return Json(new JsonResult(layUiDataModels));
        }

        [HttpPost]
        public JsonResult CompressionLogToZip([FromForm] List<LayUiDataModel> list)
        {
            if (!Directory.Exists(copyFolderPath))
            {
                Directory.CreateDirectory(copyFolderPath);
            }

            DateTime dateTime = DateTime.Now;
            string zipPath =
                $@"{copyFolderPath}{dateTime.Year}-{dateTime.Month}-{dateTime.Day}_{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}.zip";

            ZipOutputStream outputStream = new ZipOutputStream(System.IO.File.Create(zipPath));
            
            for (int i = 0; i < list.Count; i++)
                {
                    if (!Directory.Exists($@"{copyFolderPath}\{list[i].title}"))
                    {
                        Directory.CreateDirectory($@"{copyFolderPath}\{list[i].title}");
                    }
                    else
                    {
                        foreach (var file in Directory.GetFiles($@"{copyFolderPath}\{list[i].title}"))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    
                    foreach (var file in list[i].children)
                    {
                        string localPath = $@"{logFolderPath}{list[i].title}\{file.title}"; //创建文件流
                        string savePath = $@"{copyFolderPath}\{list[i].title}\{file.title}";
                        
                        

                        // if (!System.IO.File.Exists(savePath))
                        // {
                        //     System.IO.File.Create(savePath);
                        // }

                        System.IO.File.Copy(localPath, savePath, true);

                    }

                    CreateZipFiles($@"{copyFolderPath}\{list[i].title}", outputStream,
                        $@"{copyFolderPath}\{list[i].title}");
                }
            outputStream.Close();
           string json = JsonConvert.SerializeObject(zipPath);
           return Json(json);
            
                // return File(outputStream, 
                //     "application/x-zip-compressed", 
                //     "{dateTime.Year}-{dateTime.Month}-{dateTime.Day}_{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}.zip");
        }
        
         /// <summary>
        /// 递归压缩文件
        /// </summary>
        /// <param name="sourceFilePath">待压缩的文件或文件夹路径</param>
        /// <param name="zipStream">打包结果的zip文件路径（类似 D:\WorkSpace\a.zip）,全路径包括文件名和.zip扩展名</param>
        /// <param name="staticFile"></param>
        private static void CreateZipFiles(string sourceFilePath, ZipOutputStream zipStream, string staticFile)
        {
              Crc32 crc = new Crc32();
              string[] filesArray = Directory.GetFileSystemEntries(sourceFilePath);
              foreach (string file in filesArray)
              {
                   //如果当前是文件夹，递归
                   if (Directory.Exists(file))
                   {
                        CreateZipFiles(file, zipStream, staticFile);
                   }
                   else
                   {
                        //如果是文件，开始压缩
                        FileStream fileStream = System.IO.File.OpenRead(file);
         
                        byte[] buffer = new byte[fileStream.Length];
                        fileStream.Read(buffer, 0, buffer.Length);
                        string tempFile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(tempFile);
         
                        entry.DateTime = DateTime.Now;
                        entry.Size = fileStream.Length;
                        fileStream.Close();
                        crc.Reset();
                        crc.Update(buffer);
                        entry.Crc = crc.Value;
                        zipStream.PutNextEntry(entry);
         
                        zipStream.Write(buffer, 0, buffer.Length);
                   }
              }
        }
        
        public FileResult Download(string file)
        {
            //var addrUrl = @"D:\TWWwork\TWWwork\bin\Debug\netcoreapp3.0\log\2020-1-21_12-55-15.zip";
            var addrUrl =  file.Trim('"');
            var stream = System.IO.File.OpenRead(addrUrl);
            string fileExt = ".zip";
            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(stream, memi, Path.GetFileName(addrUrl));
        }
    }
}