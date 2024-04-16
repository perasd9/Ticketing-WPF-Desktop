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
    public class SportskiDogadjaj : IEntity
    {
        [Select]
        [Insert]
        [Key]
        public int DogadjajId { get; set; }
        [Select]
        public string NazivDogadjaja { get; set; } = "";
        [Select]
        public string OpisDogadjaja { get; set; } = "";
        [Select]
        public double CenaKarte { get; set; }
        [Select]
        public DateTime DatumOdrzavanja { get; set; } = DateTime.Now;
        [Select]
        public int TipDogadjajaId { get; set; }
        [Insert]
        [Join]
        public TipDogadjaja TipDogadjaja { get; set; }
        [Select]
        public int AdminId { get; set; }
        [Insert]
        [Join]
        public Administrator Administrator { get; set; }
        [Select]
        public bool IsDeleted { get; set; } = false;

        [Insert]
        public string TableName => "SportskiDogadjaj";
        [Insert]
        public string Parameters => $"@NazivDogadjaja, @OpisDogadjaja, @CenaKarte, @DatumOdrzavanja, @TipDogadjajaId, @AdminId, @IsDeleted";
        public string UpdateParameters => $"NazivDogadjaja = @NazivDogadjaja, OpisDogadjaja = @OpisDogadjaja, " +
            $"CenaKarte = @CenaKarte, DatumOdrzavanja = @DatumOdrzavanja, TipDogadjajaId = @TipDogadjajaId, AdminId = @AdminId, IsDeleted = @IsDeleted";

        public override bool Equals(object obj)
        {
            return obj is SportskiDogadjaj dogadjaj &&
                   NazivDogadjaja == dogadjaj.NazivDogadjaja &&
                   OpisDogadjaja == dogadjaj.OpisDogadjaja &&
                   CenaKarte == dogadjaj.CenaKarte &&
                   DatumOdrzavanja == dogadjaj.DatumOdrzavanja &&
                   TipDogadjajaId == dogadjaj.TipDogadjajaId;
        }
        public override int GetHashCode()
        {
            int hashCode = -1811659540;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NazivDogadjaja);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OpisDogadjaja);
            hashCode = hashCode * -1521134295 + CenaKarte.GetHashCode();
            hashCode = hashCode * -1521134295 + DatumOdrzavanja.GetHashCode();
            hashCode = hashCode * -1521134295 + TipDogadjajaId.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return NazivDogadjaja;
        }
    }
}
