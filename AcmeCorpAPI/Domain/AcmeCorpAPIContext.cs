using Microsoft.EntityFrameworkCore;
using AcmeCorpAPI.Models;
using System;
using System.Collections.Generic;

namespace AcmeCorpAPI.Models;

public class AcmeCorpAPIContext : DbContext
{
    public string DbPath { get; }
    public AcmeCorpAPIContext(DbContextOptions<AcmeCorpAPIContext> options)
        : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "acmecorp.db");
        Console.WriteLine("******************************"+DbPath);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public DbSet<AcmeCorpAPI.Domain.Customer> Customer { get; set; } = default!;

    public DbSet<AcmeCorpAPI.Domain.Order> Order { get; set; } = default!;
}