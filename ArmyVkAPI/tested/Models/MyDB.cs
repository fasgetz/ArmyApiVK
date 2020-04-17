namespace tested.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDB : DbContext
    {
        public MyDB()
            : base("name=MyDB")
        {
        }

        public virtual DbSet<FoundMaterials> FoundMaterials { get; set; }
        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<SocialNetworkUsers> SocialNetworkUsers { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Materials>()
                .HasMany(e => e.FoundMaterials)
                .WithRequired(e => e.Materials)
                .HasForeignKey(e => e.IdMaterial);

            modelBuilder.Entity<users>()
                .HasMany(e => e.SocialNetworkUsers)
                .WithOptional(e => e.users)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete();
        }
    }
}
