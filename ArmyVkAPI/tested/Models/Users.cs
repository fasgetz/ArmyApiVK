namespace tested.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            SocialNetworkUsers = new HashSet<SocialNetworkUsers>();
        }

        public int id { get; set; }

        [StringLength(30)]
        public string Family { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Surname { get; set; }

        public DateTime? DateBirth { get; set; }

        public bool? IsFinded { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialNetworkUsers> SocialNetworkUsers { get; set; }
    }
}
