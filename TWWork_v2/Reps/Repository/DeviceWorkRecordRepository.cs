using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TWWork_v2.Dao;
using TWWork_v2.Reps.IRepository;
using TWWwork.Models;

namespace TWWork_v2.Reps.Repository
{
    public class DeviceWorkRecordRepository:IDeviceWorkRecordRepository
    {
        private readonly AppDbContext _context;

        public DeviceWorkRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DeviceWorkRecord> GetDeviceWorkRecords()
        {
            List<DeviceWorkRecord> list = new List<DeviceWorkRecord>();

            try
            {
                string sql = " select * from PF_DEVICEWORKRECORD ";
                list = _context.DeviceWorkRecords.FromSql(sql).
                    OrderBy(item=>item.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDeviceWorkRecords出错:{ex.Message}");
            }
            return list;
        }

        public int AddDeviceWorkRecord(DeviceWorkRecord model)
        {
            int result = 0;

            try
            {
                //string sql = GetInsertSql(model);
                _context.DeviceWorkRecords.Add(model);
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddDeviceWorkRecord出错:{ex.Message}");
            }
            return result;
        }

        public int DelDeviceWorkRecord(int id)
        {
            int result = 0;

            try
            {
                var list = _context.DeviceWorkRecords
                    .Where(item =>item.Id == id).ToList();
                _context.DeviceWorkRecords.Remove(list[0]);
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DelDeviceWorkRecord出错:{ex.Message}");
            }
            return result;
        }

        public int EditDeviceWorkRecord(DeviceWorkRecord model)
        {
            int result = 0;

            try
            {
                var record = _context.DeviceWorkRecords
                    .Where(item => item.Id == model.Id).ToList()[0];
                record.DeviceName = model.DeviceName;
                record.CraftName = model.CraftName;
                record.StationName = model.StationName;
                record.Description = model.Description;
                record.CreateDate = model.CreateDate;
                record.SubmitPerson = model.SubmitPerson;
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DelDeviceWorkRecord出错:{ex.Message}");
            }
            return result;
        }
    }
}