namespace Biblioteca.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Nationalitate { get; set; }

        public Autor(int id, string nume, string prenume, string nationalitate)
        {
            Id = id;
            Nume = nume;
            Prenume = prenume;
            Nationalitate = nationalitate;
        }

        public override string ToString()
        {
            return $"[{Id}] {Prenume} {Nume} ({Nationalitate})";
        }
    }
}
