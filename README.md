# Application Perpustakaan with .Net Core 3.1 and ReactJS 

Aplikasi perpustakaan yang mencakup proses peminjaman dan pengembalian buku menggunakan teknologi **.NET Core, SQL Server** dan **ReactJS**.

# Compatibility #

|ASP.NET Core|SQL Server|ReactJS|Node.js|
|----------|----------|----------|----------|
|Aps .Net Core 3.1|MSSQL Server 2016 or higher|v17.0.2|v12.13.0|

## Table of Contents

* [Web API](#web-api)
  * [Installation](#installation-web-api)
  * [Database](#database)
  * [Swagger](#swagger)
* [Web UI](#web-ui)
  * [Installation](#installation-web-ui)
* [Module](#module)

## Web API
Web API yang digunakan untuk backend aplikasi **Perpustakaan** di buat menggunakan **[ASP .Net Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)** dan **[SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15)**

### Installation Web API

#### Clone repo

``` bash
# clone the repo
$ git clone https://github.com/ridone06/perpustakaan.git my-project

# go into app's directory
$ cd Perpustakaan.Api

# restore app's dependencies
$ dotnet restore

# builid app
$ dotnet build

# Make sure the ConnectionStrings in appsettings.json must be setup because there is an automatic database migration. 
"ConnectionStrings": {
   "DefaultConnection": "Server=localhost;Initial Catalog=Perpustakaan;User Id=sa;password=P@ssw0rd;MultipleActiveResultSets=true;"
 },
  
# running app
$ dotnet run
```
Buka http://localhost:5000 kemudian akan di arahkan ke halaman swagger documentations api berikut : 

![swagger](https://github.com/ridone06/perpustakaan/blob/70e70948edab221161d505e9964994ffe42983f7/capture/1.%20Swager%20API.png)

### Database
Database aplikasi perpustakaan menggunakan **SQL Server** dan databse akan otomatis dibuat pada saat running aplikasi web api `dotnet run` menggunakan **[Entity Frmawork Core Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)**.
Pastikan `ConnectionStrings` pada `appsettings.json` sesuai dengan server databse yang valid, berikut contoh nya :

``` bash
 "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Initial Catalog=Perpustakaan;User Id=sa;password=P@ssw0rd;MultipleActiveResultSets=true;"
  },
```

### Swagger
Untuk kebutuhan dokumentasi dan testing yang lebih fleksibel dapat menggunakan sawgger.
Swagger yang digunakan dapat dilihat di **[Swashbuckle AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)**.

Untuk testing swagger ini menggunakan Basic Authentication, berikut info credential nya :

``` bash
 username : admin
 password : P@ssw0rd
```

## Web UI
Web UI yang digunakan untuk frontend aplikasi **Perpustakaan** di buat menggunakan **[ReactJS](https://github.com/facebook/create-react-app)**

### Installation Web UI

#### Clone repo

``` bash
# clone the repo
$ git clone https://github.com/ridone06/perpustakaan.git my-project

# go into app's directory
$ cd perpustakaan-ui

# install app's dependencies
$ npm install

# running app (dev server with hot reload at http://localhost:3000)
$ npm start
```
Buka http://localhost:3000 kemudian akan di arahkan ke halaman aplikasi perpustakaan berikut contoh nya: 

![aplikasi-perpustakaan](https://github.com/ridone06/perpustakaan/blob/70e70948edab221161d505e9964994ffe42983f7/capture/2.%20Aplikasi%20Perpustakaan.png)


## Module
Aplikasi **Perpustakaan** ini mempunyai beberapa module sebagai berikut :

|Module|Description|
|----------|----------|
|Master Anggota|Digunakan untuk mengelola data anggota|
|Master Penerbit|Digunakan untuk mengelola data penerbit|
|Master Pengarang|Digunakan untuk mengelola data pengarang|
|Master Rak|Digunakan untuk mengelola data rak|
|Master Buku|Digunakan untuk mengelola data buku|
|Peminjaman|Digunakan untuk mengelola data peminjaman dan pengmebalian buku|
