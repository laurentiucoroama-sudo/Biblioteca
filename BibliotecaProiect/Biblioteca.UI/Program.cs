using Biblioteca.DataAccess;
using Biblioteca.Models;
using Biblioteca.Models.Enums;

namespace Biblioteca.UI
{
    class Program
    {
        static Configurari config = new Configurari(limitaCarti: 3, zileMax: 30);
        static BibliotecaRepository biblioteca = new BibliotecaRepository(config);
        static int nextIdAutor = 1;
        static int nextIdCarte = 1;
        static int nextIdPersoana = 1;

        static void Main(string[] args)
        {
            bool rulare = true;
            while (rulare)
            {
                Console.WriteLine("\n===== BIBLIOTECA =====");
                Console.WriteLine("1. Autori");
                Console.WriteLine("2. Carti");
                Console.WriteLine("3. Persoane");
                Console.WriteLine("4. Imprumuturi");
                Console.WriteLine("5. Cautare");
                Console.WriteLine("0. Iesire");
                Console.Write("Alegere: ");
                string opt = Console.ReadLine();

                switch (opt)
                {
                    case "1": MeniuAutori(); break;
                    case "2": MeniuCarti(); break;
                    case "3": MeniuPersoane(); break;
                    case "4": MeniuImprumuturi(); break;
                    case "5": MeniuCautare(); break;
                    case "0": rulare = false; break;
                    default: Console.WriteLine("Optiune invalida."); break;
                }
            }
        }

        static void MeniuAutori()
        {
            Console.WriteLine("\n-- AUTORI --");
            Console.WriteLine("1. Adauga autor");
            Console.WriteLine("2. Afiseaza autori");
            Console.WriteLine("3. Sterge autor");
            Console.Write("Alegere: ");
            string opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    Console.Write("Nume: "); string nume = Console.ReadLine();
                    Console.Write("Prenume: "); string prenume = Console.ReadLine();
                    Console.Write("Nationalitate: "); string nat = Console.ReadLine();
                    biblioteca.AdaugaAutor(new Autor(nextIdAutor++, nume, prenume, nat));
                    Console.WriteLine("Autor adaugat!");
                    break;
                case "2":
                    biblioteca.AfiseazaAutori();
                    break;
                case "3":
                    Console.Write("ID autor de sters: ");
                    int id = int.Parse(Console.ReadLine());
                    biblioteca.StergeAutor(id);
                    Console.WriteLine("Autor sters!");
                    break;
            }
        }

        static void MeniuCarti()
        {
            Console.WriteLine("\n-- CARTI --");
            Console.WriteLine("1. Adauga carte");
            Console.WriteLine("2. Afiseaza carti");
            Console.WriteLine("3. Sterge carte");
            Console.WriteLine("4. Verifica disponibilitate");
            Console.Write("Alegere: ");
            string opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    Console.Write("Titlu: "); string titlu = Console.ReadLine();
                    Console.Write("ID Autor: "); int idAutor = int.Parse(Console.ReadLine());
                    Console.Write("Nr exemplare: "); int nrEx = int.Parse(Console.ReadLine());
                    Console.WriteLine("Gen:");
                    foreach (GenLiterar g in Enum.GetValues(typeof(GenLiterar)))
                        Console.WriteLine($"  {(int)g}. {g}");
                    Console.Write("Alegere gen: ");
                    GenLiterar gen = (GenLiterar)int.Parse(Console.ReadLine());
                    var autor = biblioteca.GasesteAutor(idAutor);
                    if (autor == null) { Console.WriteLine("Autorul nu exista!"); break; }
                    biblioteca.AdaugaCarte(new Carte(nextIdCarte++, titlu, autor, nrEx, gen));
                    Console.WriteLine("Carte adaugata!");
                    break;
                case "2":
                    biblioteca.AfiseazaCarti();
                    break;
                case "3":
                    Console.Write("ID carte de sters: ");
                    int id = int.Parse(Console.ReadLine());
                    biblioteca.StergeCarte(id);
                    Console.WriteLine("Carte stearsa!");
                    break;
                case "4":
                    Console.Write("ID carte: ");
                    int idC = int.Parse(Console.ReadLine());
                    biblioteca.VerificaDisponibilitate(idC);
                    break;
            }
        }

        static void MeniuPersoane()
        {
            Console.WriteLine("\n-- PERSOANE --");
            Console.WriteLine("1. Adauga persoana");
            Console.WriteLine("2. Afiseaza persoane");
            Console.WriteLine("3. Sterge persoana");
            Console.WriteLine("4. Carti imprumutate de o persoana");
            Console.Write("Alegere: ");
            string opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    Console.Write("Nume: "); string nume = Console.ReadLine();
                    Console.Write("Prenume: "); string prenume = Console.ReadLine();
                    Console.Write("CNP: "); string cnp = Console.ReadLine();
                    biblioteca.AdaugaPersoana(new Persoana(nextIdPersoana++, nume, prenume, cnp));
                    Console.WriteLine("Persoana adaugata!");
                    break;
                case "2":
                    biblioteca.AfiseazaPersoane();
                    break;
                case "3":
                    Console.Write("ID persoana de sters: ");
                    int id = int.Parse(Console.ReadLine());
                    biblioteca.StergePersoana(id);
                    Console.WriteLine("Persoana stearsa!");
                    break;
                case "4":
                    Console.Write("ID persoana: ");
                    int idP = int.Parse(Console.ReadLine());
                    biblioteca.AfiseazaCartiImprumutatePersoana(idP);
                    break;
            }
        }

        static void MeniuImprumuturi()
        {
            Console.WriteLine("\n-- IMPRUMUTURI --");
            Console.WriteLine("1. Imprumuta carte");
            Console.WriteLine("2. Returneaza carte");
            Console.WriteLine("3. Afiseaza toate imprumuturile");
            Console.WriteLine("4. Afiseaza imprumuturi intarziate");
            Console.Write("Alegere: ");
            string opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    Console.Write("ID persoana: "); int idP = int.Parse(Console.ReadLine());
                    Console.Write("ID carte: "); int idC = int.Parse(Console.ReadLine());
                    biblioteca.ImprumutaCarte(idP, idC);
                    break;
                case "2":
                    Console.Write("ID imprumut: "); int idI = int.Parse(Console.ReadLine());
                    biblioteca.ReturneazaCarte(idI);
                    break;
                case "3":
                    biblioteca.AfiseazaImprumuturi();
                    break;
                case "4":
                    biblioteca.AfiseazaImprumututiIntarziate();
                    break;
            }
        }

        static void MeniuCautare()
        {
            Console.WriteLine("\n-- CAUTARE --");
            Console.WriteLine("1. Cauta carte dupa titlu");
            Console.WriteLine("2. Cauta carte dupa autor");
            Console.WriteLine("3. Cauta carte dupa gen");
            Console.WriteLine("4. Cauta persoana dupa nume");
            Console.Write("Alegere: ");
            string opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    Console.Write("Titlu: ");
                    biblioteca.CautaCarteDupaTitlu(Console.ReadLine());
                    break;
                case "2":
                    Console.Write("Autor: ");
                    biblioteca.CautaCarteDupaAutor(Console.ReadLine());
                    break;
                case "3":
                    Console.WriteLine("Gen:");
                    foreach (GenLiterar g in Enum.GetValues(typeof(GenLiterar)))
                        Console.WriteLine($"  {(int)g}. {g}");
                    Console.Write("Alegere gen: ");
                    biblioteca.CautaCarteDupaGen((GenLiterar)int.Parse(Console.ReadLine()));
                    break;
                case "4":
                    Console.Write("Nume: ");
                    biblioteca.CautaPersoana(Console.ReadLine());
                    break;
            }
        }
    }
}
