namespace tested.NewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewShtat")]
    public partial class NewShtat
    {
        [Key]
        public int ID_column { get; set; }

        [StringLength(255)]
        public string doljnost { get; set; }

        [StringLength(255)]
        public string zvanie { get; set; }

        [StringLength(255)]
        public string FIO { get; set; }

        [StringLength(255)]
        public string DateBirth { get; set; }

        public double? FormaDopyska { get; set; }
    }
}
