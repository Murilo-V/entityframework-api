using System;
using APIMySql.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMySql.Data
{
	public class APIDbContext : DbContext
	{
		public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }
		public DbSet<Estado> Estado { get; set; }
	}

}

