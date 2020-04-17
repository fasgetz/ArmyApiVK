namespace tested.NewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usAllData")]
    public partial class usAllData
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string fio { get; set; }

        public double? dataBirth { get; set; }

        [StringLength(255)]
        public string Grajdanstvo { get; set; }

        [StringLength(255)]
        public string CountryBirth { get; set; }

        [StringLength(255)]
        public string CityBirth { get; set; }

        [StringLength(255)]
        public string FormaDopyska { get; set; }

        [StringLength(255)]
        public string Zvanie { get; set; }

        [StringLength(255)]
        public string Doljnost { get; set; }

        [StringLength(255)]
        public string MestoRab { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        [StringLength(255)]
        public string vid { get; set; }

        [StringLength(255)]
        public string TargetVisit { get; set; }

        public DateTime? DateVisit { get; set; }

        [StringLength(255)]
        public string PynktPropyska { get; set; }

        [StringLength(255)]
        public string BorderPropyska { get; set; }

        [StringLength(255)]
        public string CountryVisit { get; set; }

        [StringLength(255)]
        public string AddressVisit { get; set; }

        [StringLength(255)]
        public string TypeDocument { get; set; }

        public DateTime? DateEndDocument { get; set; }
    }
}
