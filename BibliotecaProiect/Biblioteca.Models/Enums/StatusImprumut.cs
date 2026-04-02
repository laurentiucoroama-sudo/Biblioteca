namespace Biblioteca.Models.Enums
{
    [Flags]
    public enum StatusImprumut
    {
        Niciuna   = 0,
        Activ     = 1,
        Returnat  = 2,
        Intarziat = 4,
        Penalizat = 8
    }
}
