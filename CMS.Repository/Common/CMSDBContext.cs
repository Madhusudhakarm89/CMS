namespace CMS.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using CMS.Entity;

    public partial class CMSDBContext : DbContext
    {
        public CMSDBContext()
            : base("name=DBConnection")
        {
            Database.SetInitializer<CMSDBContext>(new NullDatabaseInitializer<CMSDBContext>());

            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUsersDocumentMapping>().ToTable("tx_mapping_AspNetUsersDocument");
            modelBuilder.Entity<Alert>().ToTable("tx_Alert");
            modelBuilder.Entity<InvoiceDocumentMapping>().ToTable("tx_Mapping_InvoiceDocument");
        }
        public DbSet<Claim> Claim { get; set; }
        public DbSet<ClaimNote> ClaimNote { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TimeLogUnit> TimeLogUnit { get; set; }
        public DbSet<TimeLog> TimeLog { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Mapping_InvoiceTimelog> Mapping_InvoiceTimelog { get; set; }
        public DbSet<TaxSetting> TaxSetting { get; set; }
        public DbSet<TypeOfLoss> TypeOfLoss { get; set; }
        public DbSet<FileNameCode> FileNameCode { get; set; }
    }
}
