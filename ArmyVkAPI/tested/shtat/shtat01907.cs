namespace tested.shtat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class shtat01907
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string zvanie { get; set; }

        [StringLength(200)]
        public string fio { get; set; }

        public DateTime? dateBirth { get; set; }
    }
}
