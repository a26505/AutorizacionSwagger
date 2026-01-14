using Models;

namespace RestauranteAPI.Services
{
    public interface IPlatoPrincipalService
    {
        List<PlatoPrincipal> GetAll();
        PlatoPrincipal? GetById(int id);
        PlatoPrincipal CreatePlato(PlatoPrincipal plato);
        void UpdatePlato(int id, PlatoPrincipal plato);
        void DeletePlato(int id);
    }
}
