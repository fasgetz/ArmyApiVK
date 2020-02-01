namespace tested.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForeignFriends
    {
        public int Id { get; set; }

        public int SocialNetworkUserID { get; set; }

        [Required]
        [StringLength(60)]
        public string WebAddress { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string Family { get; set; }

        [StringLength(25)]
        public string Surname { get; set; }

        public int? IdCity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        public byte[] Photo { get; set; }

        public byte? CountryId { get; set; }

        public virtual City City { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual SocialNetworkUser SocialNetworkUser { get; set; }
    }
}
