namespace tested.DataBases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ZigHailVkUsers
    {
        public int Id { get; set; }

        public int? VkIdUser { get; set; }
    }
}
