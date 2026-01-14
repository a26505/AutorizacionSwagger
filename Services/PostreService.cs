using RestauranteAPI.Repositories;
using Models;

namespace RestauranteAPI.Services
{
    public class PostreService : IPostreService
    {
        private readonly IPostreRepository _repo;

        public PostreService(IPostreRepository repo)
        {
            _repo = repo;
        }

        public List<Postre> GetAll()
        {
            var task = _repo.GetAllAsync();
            task.Wait();
            return task.Result;
        }

        public Postre? GetById(int id)
        {
            var task = _repo.GetByIdAsync(id);
            task.Wait();
            return task.Result;
        }

        public Postre CreatePostre(Postre postre)
        {
            var task = _repo.AddAsync(postre);
            task.Wait();
            return postre;
        }

        public void UpdatePostre(int id, Postre postre)
        {
            postre.Id = id;
            var task = _repo.UpdateAsync(postre);
            task.Wait();
        }

        public void DeletePostre(int id)
        {
            var task = _repo.DeleteAsync(id);
            task.Wait();
        }
    }
}
