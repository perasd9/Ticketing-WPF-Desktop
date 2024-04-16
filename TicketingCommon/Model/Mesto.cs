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
    public class Mesto : IEntity
    {
        [Key]
        [Select]
        [Insert]
        public int MestoId { get; set; }
        [Select]
        public string Naziv { get; set; }

        [Insert]
        public string TableName => "Mesto";
        [Insert]
        public string Parameters => $"@Naziv";

        public string UpdateParameters => $"MestoId = @MestoId, Naziv = @Naziv";

        public override string ToString()
        {
            return Naziv;
        }
    }
}
