using RestauranteAPI.Repositories;
using Models;

namespace RestauranteAPI.Services
{
    public class ComboService : IComboService
    {
        private readonly IComboRepository _repo;

        public ComboService(IComboRepository repo)
        {
            _repo = repo;
        }

        public List<Combo> GetAll()
        {
            var task = _repo.GetAllAsync();
            task.Wait();
            return task.Result;
        }

        public Combo? GetById(int id)
        {
            var task = _repo.GetByIdAsync(id);
            task.Wait();
            return task.Result;
        }

        public Combo CreateCombo(Combo combo)
        {
            var task = _repo.AddAsync(combo);
            task.Wait();
            return combo;
        }

        public void UpdateCombo(int id, Combo combo)
        {
            combo.Id = id;
            var task = _repo.UpdateAsync(combo);
            task.Wait();
        }

        public void DeleteCombo(int id)
        {
            var task = _repo.DeleteAsync(id);
            task.Wait();
        }
    }
}
