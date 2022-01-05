﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Data
{
    public class MyGLContext : DbContext
    {
        public MyGLContext (DbContextOptions<MyGLContext> options)
            : base(options)
        {
        }

        public DbSet<MyGL.Models.Account> Account { get; set; }
    }
}
