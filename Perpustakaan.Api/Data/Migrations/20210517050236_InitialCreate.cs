using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Perpustakaan.Api.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anggota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    JenisKelamin = table.Column<string>(type: "NVARCHAR(1)", nullable: false),
                    Alamat = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    NoTlp = table.Column<string>(type: "NVARCHAR(15)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anggota", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judul = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    TahunTerbit = table.Column<int>(type: "int", nullable: false),
                    PengarangId = table.Column<int>(type: "int", nullable: false),
                    PenerbitId = table.Column<int>(type: "int", nullable: false),
                    KodeRak = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peminjaman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TanggalPinjam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TanggalKembali = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnggotaId = table.Column<int>(type: "int", nullable: false),
                    PetugasId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjaman", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeminjamanDetail",
                columns: table => new
                {
                    PeminjamanId = table.Column<int>(type: "int", nullable: false),
                    BukuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeminjamanDetail", x => new { x.PeminjamanId, x.BukuId });
                });

            migrationBuilder.CreateTable(
                name: "Penerbit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Alamat = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    NoTlp = table.Column<string>(type: "NVARCHAR(15)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penerbit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pengarang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Alamat = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    NoTlp = table.Column<string>(type: "NVARCHAR(15)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pengarang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pengembalian",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TanggalPengembalian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Denda = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    PeminjamanId = table.Column<int>(type: "int", nullable: false),
                    AnggotaId = table.Column<int>(type: "int", nullable: false),
                    PetugasId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pengembalian", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PengembalianDetail",
                columns: table => new
                {
                    PengembalianId = table.Column<int>(type: "int", nullable: false),
                    BukuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PengembalianDetail", x => new { x.PengembalianId, x.BukuId });
                });

            migrationBuilder.CreateTable(
                name: "Petugas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Nama = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Alamat = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    NoTlp = table.Column<string>(type: "NVARCHAR(15)", nullable: false),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Petugas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rak",
                columns: table => new
                {
                    Kode = table.Column<string>(type: "NVARCHAR(10)", nullable: false),
                    Lokasi = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    CreatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rak", x => x.Kode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anggota");

            migrationBuilder.DropTable(
                name: "Buku");

            migrationBuilder.DropTable(
                name: "Peminjaman");

            migrationBuilder.DropTable(
                name: "PeminjamanDetail");

            migrationBuilder.DropTable(
                name: "Penerbit");

            migrationBuilder.DropTable(
                name: "Pengarang");

            migrationBuilder.DropTable(
                name: "Pengembalian");

            migrationBuilder.DropTable(
                name: "PengembalianDetail");

            migrationBuilder.DropTable(
                name: "Petugas");

            migrationBuilder.DropTable(
                name: "Rak");
        }
    }
}
