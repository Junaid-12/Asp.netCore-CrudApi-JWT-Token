﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;

namespace WebApplicationApi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Option) : base(Option)   
        {
                
        }
        public DbSet<Employee> employees { get; set; }
    }
}
