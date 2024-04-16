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
    public class Rezervacija : IEntity
    {
        [Select]
        [Insert]
        [Key]
        public int RezervacijaId { get; set; }
        [Select]
        public double UkupnaCena { get; set; }
        [Select]
        public DateTime DatumRezervacije { get; set; } = DateTime.Now;
        [Select]
        public double PDVStopa { get; set; } = 20;
        [Select]
        public string Jmbg { get; set; }
        [Insert]
        [Join]
        public Korisnik Korisnik { get; set; }

        [Insert]
        public List<KomponentaRezervacije> ListaKomponenti { get; set; }

        [Insert]
        public string TableName => "Rezervacija";
        [Insert]
        public string Parameters => $"@UkupnaCena, @DatumRezervacije, @PDVStopa, @Jmbg";
        [Insert]
        public string UpdateParameters => $"UkupnaCena = @UkupnaCena, DatumRezervacije = @DatumRezervacije, PDVStopa = @PDVStopa, Jmbg = @Jmbg";

        public override string ToString()
        {
            return RezervacijaId + "";
        }
    }
}
