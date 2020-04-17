namespace tested.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SocialNetworkUsers
    {
        public int Id { get; set; }

        public int? IdUser { get; set; }

        [StringLength(60)]
        public string WebAddress { get; set; }

        public virtual users users { get; set; }
    }
}
