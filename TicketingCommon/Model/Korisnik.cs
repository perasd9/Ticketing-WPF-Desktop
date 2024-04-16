using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model.Attributes;

namespace TicketingCommon.Model
{
    [Serializable]
    public class Korisnik : IEntity
    {
        [Select]
        [Key]
        public string Jmbg { get; set; } = "";
        [Select]
        public string Ime { get; set; } = "";
        [Select]
        public string Prezime { get; set; } = "";
        [Select]
        public string Email { get; set; } = "";
        [Select]
        public byte[] Sifra { get; set; } = new byte[64];
        [Select]
        public DateTime DatumRodjenja { get; set; } = DateTime.Now;
        [Select]
        [ForeignKey("admin")]
        public int AdminId { get; set; }
        [Insert]
        [Join]
        public Administrator Admin { get; set; }
        [Select]
        [ForeignKey("mesto")]
        public int MestoId { get; set; }
        [Insert]
        [Join]
        public Mesto Mesto { get; set; }

        [Insert]
        public string TableName => "Korisnik";
        [Insert]
        public string Parameters => $"@Jmbg, @Ime, @Prezime, @Email, @Sifra, @DatumRodjenja, @AdminId, @MestoId";

        public string UpdateParameters => $"Jmbg = @Jmbg, Ime = @Ime, Prezime = @Prezime, Email = @Email, Sifra = @Sifra, DatumRodjenja = @DatumRodjenja, AdminId = @AdminId, MestoId = @MestoId";

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }


    }
}
