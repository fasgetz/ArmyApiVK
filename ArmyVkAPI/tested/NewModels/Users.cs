namespace tested.NewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Intersections = new HashSet<Intersections>();
        }

        [Key]
        public int IdUser { get; set; }

        [StringLength(100)]
        public string FIO { get; set; }

        [StringLength(100)]
        public string DateBirth { get; set; }

        [StringLength(100)]
        public string nationality { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Intersections> Intersections { get; set; }
    }
}
