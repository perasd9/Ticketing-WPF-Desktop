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
    public class KomponentaRezervacije : IEntity
    {
        [Key]
        [Select]
        public int RezervacijaId { get; set; }
        [Select]
        [Key]
        public int RbKomponente { get; set; }
        [Insert]
        [Join]
        public Rezervacija Rezervacija { get; set; }
        [Select]
        public int BrojKarata { get; set; } = 1;
        [Select]
        public double UkupanIznos { get; set; }
        [Select]
        public int DogadjajId { get; set; }
        [Insert]
        [Join]
        public SportskiDogadjaj SportskiDogadjaj { get; set; }
        [Insert]
        public Status Status { get; set; } = Status.Unchanged;

        [Insert]
        public string TableName => "KomponentaRezervacije";
        [Insert]
        public string Parameters => $"@RezervacijaId, @RbKomponente, @BrojKarata, @UkupanIznos, @DogadjajId";

        public string UpdateParameters => $"RezervacijaId = @RezervacijaId, RbKomponente = @RbKomponente, BrojKarata = @BrojKarata, UkupanIznos = @UkupanIznos, DogadjajId = @DogadjajId";

        public override string ToString()
        {
            return "Rezervacija: " + RezervacijaId + ", redni broj kompoenente: " + RbKomponente;
        }
    }
}
