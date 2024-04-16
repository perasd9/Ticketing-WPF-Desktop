using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingCommon
{
    [Serializable]
    public enum Operation
    {
        PrijavaNaSistem,
        DodajKorisnika,
        UcitajMesta,
        UcitajKorisnike,
        PronadjiKorisnike,
        UcitajKorisnika,
        ObrisiKorisnika,
        UcitajTipoveDogadjaja,
        KreirajDogadjaj,
        UcitajDogadjaje,
        PronadjiDogadjaje,
        ObrisiDogadjaj,
        SacuvajRezervaciju,
        UcitajRezervacije,
        PronadjiRezervacije,
        UcitajRezervaciju,
        IzmeniRezervaciju,
        OdjavaSaSistema,
    }
}
