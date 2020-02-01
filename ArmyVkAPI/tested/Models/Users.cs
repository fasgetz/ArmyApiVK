namespace tested.Models
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
            SocialNetworkUser = new HashSet<SocialNetworkUser>();
            UserSoldierService = new HashSet<UserSoldierService>();
        }

        public int Id { get; set; }

        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string Family { get; set; }

        [StringLength(25)]
        public string Surname { get; set; }

        public byte[] Photo { get; set; }

        public DateTime? DateBirth { get; set; }

        [StringLength(150)]
        public string AddressResidence { get; set; }

        public string Characteristic { get; set; }

        public bool? IsMonitoring { get; set; }

        public byte? SocialStatusID { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(15)]
        public string phone { get; set; }

        public DateTime? DateOfEntry { get; set; }

        public int? CityBirth_Id { get; set; }

        public int? CurrentCityResience_Id { get; set; }

        public byte? CountryBirth_Id { get; set; }

        public byte? CountryResidence_Id { get; set; }

        public virtual City City { get; set; }

        public virtual City City1 { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual Countries Countries1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocialNetworkUser> SocialNetworkUser { get; set; }

        public virtual SocialStatuses SocialStatuses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSoldierService> UserSoldierService { get; set; }
    }
}
