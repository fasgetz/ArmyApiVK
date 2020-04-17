namespace tested.shtat
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class shtatDB : DbContext
    {
        public shtatDB()
            : base("name=shtatDB")
        {
        }

        public virtual DbSet<shtat01907> shtat01907 { get; set; }
        public virtual DbSet<shtat42155_> shtat42155_ { get; set; }
        public virtual DbSet<shtat49289_> shtat49289_ { get; set; }
        public virtual DbSet<shtat95043_> shtat95043_ { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
