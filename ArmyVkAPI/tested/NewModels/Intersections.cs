namespace tested.NewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Intersections
    {
        public int ID { get; set; }

        public int? IdUser { get; set; }

        [StringLength(100)]
        public string TypeIntersect { get; set; }

        [StringLength(50)]
        public string ViewIntersect { get; set; }

        [StringLength(100)]
        public string DateIntersect { get; set; }

        [StringLength(100)]
        public string TargetIntersect { get; set; }

        [StringLength(100)]
        public string DocumentType { get; set; }

        [StringLength(100)]
        public string EndDateDocument { get; set; }

        [StringLength(100)]
        public string CountryDestination { get; set; }

        [StringLength(100)]
        public string AddressDestination { get; set; }

        [StringLength(100)]
        public string ChekPoint { get; set; }

        [StringLength(50)]
        public string BorderWith { get; set; }

        public virtual Users Users { get; set; }
    }
}
