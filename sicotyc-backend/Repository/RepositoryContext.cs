﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LookupCodeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new LookupCodeConfiguration());
        }

        public DbSet<LookupCodeGroup>? LookupCodeGroups { get; set; }
        public DbSet<LookupCode>? LookupCodes { get; set; }
    }
}