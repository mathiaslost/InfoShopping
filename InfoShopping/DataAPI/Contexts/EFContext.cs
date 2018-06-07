using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataAPI.Models.InfoShoppingModels;

namespace DataAPI.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base("Asp_Net_MVC_CS")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EFContext>());
        }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<ClienteModel> Empresas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<DataAPI.Models.Administracao> Administracaos { get; set; }
    }
}