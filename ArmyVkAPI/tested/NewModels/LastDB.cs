namespace tested.NewModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LastDB : DbContext
    {
        public LastDB()
            : base("name=LastDB")
        {
        }

        public virtual DbSet<Intersections> Intersections { get; set; }
        public virtual DbSet<NewShtat> NewShtat { get; set; }
        public virtual DbSet<shtat> shtat { get; set; }
        public virtual DbSet<shtatNEW> shtatNEW { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<usAllData> usAllData { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasMany(e => e.Intersections)
                .WithOptional(e => e.Users)
                .WillCascadeOnDelete();
        }
    }
}
