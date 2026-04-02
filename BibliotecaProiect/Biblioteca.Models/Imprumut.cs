using Biblioteca.Models.Enums;

namespace Biblioteca.Models
{
    public class Imprumut
    {
        public int Id { get; set; }
        public Persoana Persoana { get; set; }
        public Carte Carte { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime? DataReturnare { get; set; }
        public bool Returnat { get; set; }

        // ── ENUM 2 (cu Flag): Statusul imprumutului (pot fi combinate) ──
        public StatusImprumut Status { get; set; }

        public Imprumut(int id, Persoana persoana, Carte carte)
        {
            Id = id;
            Persoana = persoana;
            Carte = carte;
            DataImprumut = DateTime.Now;
            DataReturnare = null;
            Returnat = false;
            Status = StatusImprumut.Activ;
        }

        public override string ToString()
        {
            string statusText = Returnat
                ? $"Returnat: {DataReturnare:dd/MM/yyyy}"
                : "Neretornat";
            return $"[{Id}] {Persoana.Prenume} {Persoana.Nume} - {Carte.Titlu} | {DataImprumut:dd/MM/yyyy} | {statusText} | Status: {Status}";
        }
    }
}
