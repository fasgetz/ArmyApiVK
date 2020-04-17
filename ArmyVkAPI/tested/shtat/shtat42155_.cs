namespace tested.shtat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shtat42155$")]
    public partial class shtat42155_
    {
        public int id { get; set; }

        [StringLength(255)]
        public string famiy { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string surname { get; set; }

        public DateTime? birth { get; set; }
    }
}
