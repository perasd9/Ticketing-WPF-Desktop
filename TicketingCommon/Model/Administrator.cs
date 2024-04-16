using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model.Attributes;

namespace TicketingCommon.Model
{
    [Serializable]
    public class Administrator : IEntity
    {
        [Key]
        [Select]
        [Insert]
        public int AdminId { get; set; }
        [Select]
        public string Ime { get; set; }
        [Select]
        public string Prezime { get; set; }
        [Select]
        public string Email { get; set; } = "";
        [Select]
        public byte[] Sifra { get; set; }

        [Insert]
        public string TableName => "Administrator";
        [Insert]
        public string Parameters => $"@Ime, @Prezime, @Email, @Sifra";

        public string UpdateParameters => $"Jmbg = @Jmbg, Ime = @Ime, Prezime = @Prezime, Email = @Email, Sifra = @Sifra, DatumRodjenja = @DatumRodjenja, AdminId = @AdminId, MestoId = @MestoId";

        public override string ToString()
        {
            return Ime;
        }
    }
}
