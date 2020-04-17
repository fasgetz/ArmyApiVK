namespace tested.NewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shtatNEW")]
    public partial class shtatNEW
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(255)]
        public string FIO { get; set; }

        [StringLength(255)]
        public string Пол { get; set; }

        [Column("Дата рождения")]
        public DateTime? Дата_рождения { get; set; }

        [Column("Форма допуска")]
        public double? Форма_допуска { get; set; }

        [Column("Место рождения (страна/область)")]
        [StringLength(255)]
        public string Место_рождения__страна_область_ { get; set; }

        [Column("Место рождения (райион)")]
        [StringLength(255)]
        public string Место_рождения__райион_ { get; set; }

        [Column("Место рождения (нас#пункт)")]
        [StringLength(255)]
        public string Место_рождения__нас_пункт_ { get; set; }

        [Column("Место работы")]
        [StringLength(255)]
        public string Место_работы { get; set; }

        [Column(" Должность")]
        [StringLength(255)]
        public string C_Должность { get; set; }

        [Column("Номер паспорта РФ (10 символов)")]
        [StringLength(255)]
        public string Номер_паспорта_РФ__10_символов_ { get; set; }

        [Column("Номер загранпаспорта (9 символов)")]
        [StringLength(255)]
        public string Номер_загранпаспорта__9_символов_ { get; set; }

        [Column("Служебный паспорт (при наличии)")]
        [StringLength(255)]
        public string Служебный_паспорт__при_наличии_ { get; set; }

        [Column("Дополнительный паспорт (при наличии)")]
        [StringLength(255)]
        public string Дополнительный_паспорт__при_наличии_ { get; set; }

        [Column("Паспорт моряка  (при наличии)")]
        [StringLength(255)]
        public string Паспорт_моряка___при_наличии_ { get; set; }
    }
}
