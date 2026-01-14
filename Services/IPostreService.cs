using Models;

namespace RestauranteAPI.Services
{
    public interface IPostreService
    {
        List<Postre> GetAll();
        Postre? GetById(int id);
        Postre CreatePostre(Postre postre);
        void UpdatePostre(int id, Postre postre);
        void DeletePostre(int id);
    }
}
