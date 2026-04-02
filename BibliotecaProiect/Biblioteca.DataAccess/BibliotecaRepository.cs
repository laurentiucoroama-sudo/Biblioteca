using Biblioteca.Models;
using Biblioteca.Models.Enums;

namespace Biblioteca.DataAccess
{
    public class BibliotecaRepository
    {
        private List<Autor> autori = new List<Autor>();
        private List<Carte> carti = new List<Carte>();
        private List<Persoana> persoane = new List<Persoana>();
        private List<Imprumut> imprumuturi = new List<Imprumut>();
        private Configurari configurari;
        private int nextId = 1;

        public BibliotecaRepository(Configurari config)
        {
            configurari = config;
        }

        // ── AUTORI ──
        public void AdaugaAutor(Autor a) { autori.Add(a); }
        public void StergeAutor(int id) { autori.RemoveAll(a => a.Id == id); }
        public void AfiseazaAutori() { autori.ForEach(a => Console.WriteLine(a)); }

        public Autor GasesteAutor(int id)
        {
            return autori.FirstOrDefault(a => a.Id == id);
        }

        // ── CARTI ──
        public void AdaugaCarte(Carte c) { carti.Add(c); }
        public void StergeCarte(int id) { carti.RemoveAll(c => c.Id == id); }
        public void AfiseazaCarti() { carti.ForEach(c => Console.WriteLine(c)); }

        public void VerificaDisponibilitate(int idCarte)
        {
            var carte = carti.FirstOrDefault(c => c.Id == idCarte);
            if (carte == null) { Console.WriteLine("Cartea nu a fost gasita."); return; }
            Console.WriteLine(carte.EsteDisponibila()
                ? $"'{carte.Titlu}' este DISPONIBILA ({carte.NrExemplareDisponibile} exemplare)."
                : $"'{carte.Titlu}' NU este disponibila.");
        }

        // ── PERSOANE ──
        public void AdaugaPersoana(Persoana p) { persoane.Add(p); }
        public void StergePersoana(int id) { persoane.RemoveAll(p => p.Id == id); }
        public void AfiseazaPersoane() { persoane.ForEach(p => Console.WriteLine(p)); }

        public void AfiseazaCartiImprumutatePersoana(int idPersoana)
        {
            var persoana = persoane.FirstOrDefault(p => p.Id == idPersoana);
            if (persoana == null) { Console.WriteLine("Persoana nu a fost gasita."); return; }

            var imprPersoane = imprumuturi
                .Where(i => i.Persoana.Id == idPersoana && !i.Returnat)
                .ToList();

            Console.WriteLine($"{persoana.Prenume} {persoana.Nume} are {imprPersoane.Count} carti imprumutate:");
            imprPersoane.ForEach(i => Console.WriteLine($"  - {i.Carte.Titlu}"));

            if (persoana.NrCartiImprumutate >= configurari.LimitaCartiPerPersoana)
                Console.WriteLine("ATENTIE: Limita de imprumuturi atinsa!");
        }

        // ── IMPRUMUTURI ──
        public void ImprumutaCarte(int idPersoana, int idCarte)
        {
            var persoana = persoane.FirstOrDefault(p => p.Id == idPersoana);
            var carte = carti.FirstOrDefault(c => c.Id == idCarte);

            if (persoana == null || carte == null) { Console.WriteLine("Persoana sau cartea nu a fost gasita."); return; }
            if (!carte.EsteDisponibila()) { Console.WriteLine("Cartea nu este disponibila."); return; }
            if (persoana.NrCartiImprumutate >= configurari.LimitaCartiPerPersoana)
            {
                Console.WriteLine($"ATENTIE: {persoana.Prenume} {persoana.Nume} a atins limita de {configurari.LimitaCartiPerPersoana} carti.");
                return;
            }

            carte.NrExemplareDisponibile--;
            persoana.NrCartiImprumutate++;
            imprumuturi.Add(new Imprumut(nextId++, persoana, carte));
            Console.WriteLine($"Imprumut realizat: '{carte.Titlu}' -> {persoana.Prenume} {persoana.Nume}");
        }

        public void ReturneazaCarte(int idImprumut)
        {
            var imprumut = imprumuturi.FirstOrDefault(i => i.Id == idImprumut);
            if (imprumut == null || imprumut.Returnat) { Console.WriteLine("Imprumut invalid."); return; }

            imprumut.Returnat = true;
            imprumut.DataReturnare = DateTime.Now;
            imprumut.Status = StatusImprumut.Returnat;

            if (DateTime.Now > imprumut.DataImprumut.AddDays(configurari.ZileMaxImprumut))
                imprumut.Status = StatusImprumut.Returnat | StatusImprumut.Intarziat | StatusImprumut.Penalizat;

            imprumut.Carte.NrExemplareDisponibile++;
            imprumut.Persoana.NrCartiImprumutate--;
            Console.WriteLine($"Cartea '{imprumut.Carte.Titlu}' returnata. Status: {imprumut.Status}");
        }

        public void AfiseazaImprumuturi() { imprumuturi.ForEach(i => Console.WriteLine(i)); }

        public void AfiseazaImprumututiIntarziate()
        {
            var intarziate = imprumuturi
                .Where(i => !i.Returnat && DateTime.Now > i.DataImprumut.AddDays(configurari.ZileMaxImprumut))
                .ToList();
            if (intarziate.Count == 0) { Console.WriteLine("Nu exista imprumuturi intarziate."); return; }
            Console.WriteLine("=== INTARZIATE ===");
            intarziate.ForEach(i => Console.WriteLine(i));
        }

        // ── CAUTARE CU LINQ ──
        public void CautaCarteDupaTitlu(string titlu)
        {
            var rezultate = carti.Where(c => c.Titlu.ToLower().Contains(titlu.ToLower())).ToList();
            if (rezultate.Count == 0) { Console.WriteLine("Nicio carte gasita."); return; }
            rezultate.ForEach(c => Console.WriteLine(c));
        }

        public void CautaCarteDupaAutor(string numeAutor)
        {
            var rezultate = carti.Where(c => c.Autor.Nume.ToLower().Contains(numeAutor.ToLower())).ToList();
            if (rezultate.Count == 0) { Console.WriteLine("Nicio carte gasita."); return; }
            rezultate.ForEach(c => Console.WriteLine(c));
        }

        public void CautaCarteDupaGen(GenLiterar gen)
        {
            var rezultate = carti.Where(c => c.Gen == gen).ToList();
            if (rezultate.Count == 0) { Console.WriteLine("Nicio carte gasita pentru acest gen."); return; }
            rezultate.ForEach(c => Console.WriteLine(c));
        }

        public void CautaPersoana(string nume)
        {
            var rezultate = persoane.Where(p => p.Nume.ToLower().Contains(nume.ToLower())).ToList();
            if (rezultate.Count == 0) { Console.WriteLine("Nicio persoana gasita."); return; }
            rezultate.ForEach(p => Console.WriteLine(p));
        }
    }
}
