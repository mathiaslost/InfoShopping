using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UI.Web.Models;

namespace UI.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<UI.Web.Models.ClienteModel> ClienteModel { get; set; }

        public DbSet<UI.Web.Models.CidadeModel> CidadeModel { get; set; }

        public DbSet<UI.Web.Models.EstadoModel> EstadoModel { get; set; }

        public DbSet<UI.Web.Models.LojaModel> LojaModel { get; set; }

        public DbSet<UI.Web.Models.ShoppingModel> ShoppingModel { get; set; }

        public DbSet<UI.Web.Models.EnderecoModel> EnderecoModel { get; set; }
    }
}
