using Biblioteca.Models.Enums;

namespace Biblioteca.Models
{
    public class Carte
    {
        public int Id { get; set; }
        public string Titlu { get; set; }
        public Autor Autor { get; set; }
        public int NrExemplareTotal { get; set; }
        public int NrExemplareDisponibile { get; set; }

        // ── ENUM 1: Genul literar al cartii ──
        public GenLiterar Gen { get; set; }

        public Carte(int id, string titlu, Autor autor, int nrExemplare, GenLiterar gen)
        {
            Id = id;
            Titlu = titlu;
            Autor = autor;
            NrExemplareTotal = nrExemplare;
            NrExemplareDisponibile = nrExemplare;
            Gen = gen;
        }

        public bool EsteDisponibila()
        {
            return NrExemplareDisponibile > 0;
        }

        public override string ToString()
        {
            return $"[{Id}] {Titlu} - {Autor.Prenume} {Autor.Nume} | Gen: {Gen} | Disponibile: {NrExemplareDisponibile}/{NrExemplareTotal}";
        }
    }
}
