namespace Biblioteca.Models
{
    public class Configurari
    {
        public int LimitaCartiPerPersoana { get; set; }
        public int ZileMaxImprumut { get; set; }

        public Configurari(int limitaCarti = 3, int zileMax = 30)
        {
            LimitaCartiPerPersoana = limitaCarti;
            ZileMaxImprumut = zileMax;
        }
    }
}
