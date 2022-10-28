﻿using AppModel.Models;
using Microsoft.EntityFrameworkCore;

namespace AppModel.Data
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options)
        : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
