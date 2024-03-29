﻿using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Models;

namespace WebApiMinimal.Contexts
{
    public class Contexto:DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) => Database.EnsureCreated();

        public DbSet<RequestIdTotem> RequestIdTotem { get; set; }
    }
}
