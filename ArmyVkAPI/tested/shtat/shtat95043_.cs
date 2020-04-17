namespace tested.shtat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shtat95043$")]
    public partial class shtat95043_
    {
        public int id { get; set; }

        [StringLength(255)]
        public string fio { get; set; }

        public DateTime? DateBirth { get; set; }
    }
}
