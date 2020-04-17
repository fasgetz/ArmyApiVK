namespace tested.DataBases
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewDB : DbContext
    {
        public NewDB()
            : base("name=NewDB1")
        {
        }

        public virtual DbSet<ZigHailVkUsers> ZigHailVkUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
