using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayId { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(300)]
        public string Aciklama { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string TakipKodu { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Personel { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Alici { get; set; }

        public DateTime Tarih { get; set; }

    }
}