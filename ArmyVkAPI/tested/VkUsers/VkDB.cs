namespace tested.VkUsers
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VkDB : DbContext
    {
        public VkDB()
            : base("name=VkDB")
        {
        }

        public virtual DbSet<VkUsers> VkUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
