using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Perpustakaan.Api.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perpustakaan.Api.Data
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<PerpustakaanContext>())
                {
                    context.Database.Migrate();

                    AddPetugas(context);
                    AddAnggota(context);
                    AddPenngarang(context);
                    AddPenerbit(context);
                    AddRak(context);
                    AddBuku(context);

                    context.SaveChanges();
                }
            }
        }

        private static void AddPetugas(PerpustakaanContext context)
        {
            if (context.Petugas.Count() <= 0)
            {
                var list = new List<Petugas>()
                {
                    new Petugas
                    {
                        Username = "admin",
                        Password = "P@ssw0rd",
                        Nama = "Admin",
                        Email = "ridone.alfatah@gmail.com",
                        NoTlp = "085695067743",
                        Alamat = "Jl. Sirojul Munir kel. Jatisari, kec. Jatiasih Bekasi Kota",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin" },
                };

                context.Petugas.AddRange(list);
            }
        }

        private static void AddAnggota(PerpustakaanContext context)
        {
            if (context.Anggota.Count() <= 0)
            {
                var list = new List<Anggota>()
                {
                    new Anggota{ Nama = "Uti Ridwan Ali", JenisKelamin = "L", Email = "ridone.alfatah@gmail.com", NoTlp = "085695067743", Alamat = "Jl. Sirojul Munir kel. Jatisari, kec. Jatiasih Bekasi Kota", IsActive = true, CreatedAt = DateTime.Now, CreatedBy = "admin", UpdatedAt = DateTime.Now, UpdatedBy = "admin" },
                    new Anggota{ Nama = "Anastasya Devi Sagita", JenisKelamin = "P", Email = "Anastasya.Devi.Sagita@gmail.com", NoTlp = "08578980909", Alamat = "Jl. Sirojul Munir kel. Jatisari, kec. Jatiasih Bekasi Kota", IsActive = true, CreatedAt = DateTime.Now, CreatedBy = "admin", UpdatedAt = DateTime.Now, UpdatedBy = "admin" },
                };

                context.Anggota.AddRange(list);
            }
        }

        private static void AddPenngarang(PerpustakaanContext context)
        {
            if (context.Pengarang.Count() <= 0)
            {
                var list = new List<Pengarang>()
                {
                    new Pengarang
                    {
                        Nama = "Harlis Kurniawan Baskar",
                        NoTlp = "0856776876868",
                        Alamat = "",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                    new Pengarang
                    {
                        Nama = "Lina Herlina",
                        NoTlp = "0856776876868",
                        Alamat = "",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                     new Pengarang
                    {
                        Nama = "Cece Abdulwaly",
                        NoTlp = "0856776876868",
                        Alamat = "",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                };

                context.Pengarang.AddRange(list);
            }
        }

        private static void AddPenerbit(PerpustakaanContext context)
        {
            if (context.Penerbit.Count() <= 0)
            {
                var list = new List<Penerbit>()
                {
                    new Penerbit
                    {
                        Nama = "Tinta Medina",
                        NoTlp = "0856776876868",
                        Alamat = "",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                    new Penerbit
                    {
                        Nama = "Tiga Ananda",
                        NoTlp = "0856776876868",
                        Alamat = "",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                };

                context.Penerbit.AddRange(list);
            }
        }

        private static void AddRak(PerpustakaanContext context)
        {
            if (context.Rak.Count() <= 0)
            {
                var list = new List<Rak>()
                {
                    new Rak
                    {
                        Kode = "R001",
                        Lokasi = "Pojok kiri atas",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                    new Rak
                    {
                        Kode = "R002",
                        Lokasi = "Pojok tengah atas",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                    new Rak
                    {
                        Kode = "R003",
                        Lokasi = "Pojok kanan atas",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                };

                context.Rak.AddRange(list);
            }
        }

        private static void AddBuku(PerpustakaanContext context)
        {
            if (context.Buku.Count() <= 0)
            {
                var list = new List<Buku>()
                {
                    new Buku
                    {
                        Judul = "12 langkah menjadi pemuda ashabul kahfi",
                        PenerbitId = 1,
                        PengarangId = 1,
                        TahunTerbit = 2019,
                        KodeRak = "R001",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                    new Buku
                    {
                        Judul = "40 cerita indah dan doa sehari-hari",
                        PenerbitId = 2,
                        PengarangId = 2,
                        TahunTerbit = 2019,
                        KodeRak = "R002",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                    new Buku
                    {
                        Judul = "50 kesalahan dalam menghafal Al Qur'an",
                        PenerbitId = 1,
                        PengarangId = 3,
                        TahunTerbit = 2019,
                        KodeRak = "R003",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = "admin",
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = "admin"
                    },
                };

                context.Buku.AddRange(list);
            }
        }
    }
}
