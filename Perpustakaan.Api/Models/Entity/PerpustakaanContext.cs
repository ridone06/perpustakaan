using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Perpustakaan.Api.Models.Entity
{
    public partial class PerpustakaanContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
       
        public PerpustakaanContext(DbContextOptions<PerpustakaanContext> options)
            : base(options)
        {

        }
       
        public PerpustakaanContext(DbContextOptions<PerpustakaanContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }


        //Master Data
        public virtual DbSet<Anggota> Anggota { get; set; }
        public virtual DbSet<Buku> Buku { get; set; }
        public virtual DbSet<Penerbit> Penerbit { get; set; }
        public virtual DbSet<Pengarang> Pengarang { get; set; }
        public virtual DbSet<Petugas> Petugas { get; set; }
        public virtual DbSet<Rak> Rak { get; set; }

        //Transaksi
        public virtual DbSet<Peminjaman> Peminjaman { get; set; }
        public virtual DbSet<PeminjamanDetail> PeminjamanDetail { get; set; }
        public virtual DbSet<Pengembalian> Pengembalian { get; set; }
        public virtual DbSet<PengembalianDetail> PengembalianDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            //.EnableSensitiveDataLogging();
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeminjamanDetail>().HasKey(table => new
            {
                table.PeminjamanId,
                table.BukuId
            });

            modelBuilder.Entity<PengembalianDetail>().HasKey(table => new
            {
                table.PengembalianId,
                table.BukuId
            });

            OnModelCreatingPartial(modelBuilder);
        }
       
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
