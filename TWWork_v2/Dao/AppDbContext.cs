using Microsoft.EntityFrameworkCore;
using TWWork_v2.Models;
using TWWwork.Models;

namespace TWWork_v2.Dao
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		//public DbSet<OBTRFIDRecordCount> OBTRFIDRecordCounts { get; set; }
		public DbSet<OBTRFIDCountModel> ObtrfidCountModels { get; set; }
		public DbSet<DataChangeModel> DataChangeModels { get; set; }

		public DbSet<OBTRFIDRecord> ObtrfidRecords { get; set; }
		public DbSet<OBTRecord> ObtRecords { get; set; }
		public DbSet<RFIDRecord> RfidRecords { get; set; }

		public DbSet<SuccessRateModel> SuccessRateModels { get; set; }

		public DbSet<SiteReadQuality> SiteReadQualities { get; set; }

		public DbSet<MissingRecord> MissingRecords { get; set; }

		public DbSet<RFIDReadCounts> RfidReadCountses { get; set; }

		public DbSet<InOutCompareDataModel> InOutCompareDataModels { get; set; }

		public DbSet<DeviceWorkRecord> DeviceWorkRecords { get; set; }
		public DbSet<TableInfo> TableInfos { get; set; }

		public DbSet<RetrospectiveInquiry> RetrospectiveInquiries { get; set; }
		//public DbSet<dev_MissingRecord> dev_MissingRecords { get; set; }
		public DbSet<dev_TraceRecord> dev_TraceRecords { get; set; }
	}
}