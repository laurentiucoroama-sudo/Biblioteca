namespace Biblioteca.Models
{
    public class Persoana
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }
        public int NrCartiImprumutate { get; set; }

        public Persoana(int id, string nume, string prenume, string cnp)
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            CNP = cnp;
            NrCartiImprumutate = 0;
        }

        public override string ToString()
        {
            return $"[{Id}] {Prenume} {Nume} | Carti imprumutate: {NrCartiImprumutate}";
        }
    }
}
