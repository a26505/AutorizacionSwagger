using RestauranteAPI.Repositories;
using Models;

namespace RestauranteAPI.Services
{
    public class BebidaService : IBebidaService
    {
        private readonly IBebidaRepository _repo;

        public BebidaService(IBebidaRepository repo)
        {
            _repo = repo;
        }

        public List<Bebida> GetAll()
        {
            var task = _repo.GetAllAsync();
            task.Wait();
            return task.Result;
        }

        public Bebida? GetById(int id)
        {
            var task = _repo.GetByIdAsync(id);
            task.Wait();
            return task.Result;
        }

        public Bebida CreateBebida(Bebida bebida)
        {
            var task = _repo.AddAsync(bebida);
            task.Wait();
            return bebida;
        }

        public void UpdateBebida(int id, Bebida bebida)
        {
            bebida.Id = id;
            var task = _repo.UpdateAsync(bebida);
            task.Wait();
        }

        public void DeleteBebida(int id)
        {
            var task = _repo.DeleteAsync(id);
            task.Wait();
        }
    }
}
