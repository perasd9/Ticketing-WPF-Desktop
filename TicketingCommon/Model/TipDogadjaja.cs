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
    public class TipDogadjaja : IEntity
    {
        [Select]
        [Insert]
        [Key]
        public int TipDogadjajaId { get; set; }
        [Select]
        public string Naziv { get; set; }

        [Insert]
        public string TableName => "TipDogadjaja";
        [Insert]
        public string Parameters => $"@Naziv";

        public string UpdateParameters => $"TipDogadjajaId = @TipDogadjajaId, Naziv = @Naziv";

        public override string ToString()
        {
            return Naziv;
        }
    }
}
