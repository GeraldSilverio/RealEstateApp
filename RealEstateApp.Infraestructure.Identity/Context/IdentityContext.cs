using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using RealEstateApp.Infraestructure.Identity.Entities;
using System.Reflection.Emit;

namespace RealEstateApp.Infraestructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            #region UsersConfiguration
            //Esta configuracion era leve, por eso decidi hacerla aqui mismo, en vez de realizar la configuracion aparte - Christopher Peguero
            //Se necesita que el campo imagen acepte valores nulos, para que al momento de insertar a los administradores o desarrolladores no de error por que la imagen sea requerida.
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(x => x.ImageUser)
                .IsRequired(false);
            });
            #endregion

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogin");
            });
        }
    }
}
