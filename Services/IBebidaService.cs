using Models;

namespace RestauranteAPI.Services
{
    public interface IBebidaService
    {
        List<Bebida> GetAll();
        Bebida? GetById(int id);
        Bebida CreateBebida(Bebida bebida);
        void UpdateBebida(int id, Bebida bebida);
        void DeleteBebida(int id);
    }
}
