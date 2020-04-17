namespace tested.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoundMaterials
    {
        public int Id { get; set; }

        public int IdMaterial { get; set; }

        [StringLength(150)]
        public string WebAddress { get; set; }

        public DateTime? DateOfEntry { get; set; }

        public byte[] ScreenShot { get; set; }

        public DateTime? DateOfLoading { get; set; }

        public virtual Materials Materials { get; set; }
    }
}
