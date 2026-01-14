using RestauranteAPI.Repositories;
using Models;

namespace RestauranteAPI.Services
{
    public class PlatoPrincipalService : IPlatoPrincipalService
    {
        private readonly IPlatoPrincipalRepository _repo;

        public PlatoPrincipalService(IPlatoPrincipalRepository repo)
        {
            _repo = repo;
        }

        public List<PlatoPrincipal> GetAll()
        {
            var task = _repo.GetAllAsync();
            task.Wait();
            return task.Result;
        }

        public PlatoPrincipal? GetById(int id)
        {
            var task = _repo.GetByIdAsync(id);
            task.Wait();
            return task.Result;
        }

        public PlatoPrincipal CreatePlato(PlatoPrincipal plato)
        {
            var task = _repo.AddAsync(plato);
            task.Wait();
            return plato;
        }

        public void UpdatePlato(int id, PlatoPrincipal plato)
        {
            plato.Id = id;
            var task = _repo.UpdateAsync(plato);
            task.Wait();
        }

        public void DeletePlato(int id)
        {
            var task = _repo.DeleteAsync(id);
            task.Wait();
        }
    }
}
