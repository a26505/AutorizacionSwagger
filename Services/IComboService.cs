using Models;

namespace RestauranteAPI.Services
{
    public interface IComboService
    {
        List<Combo> GetAll();
        Combo? GetById(int id);
        Combo CreateCombo(Combo combo);
        void UpdateCombo(int id, Combo combo);
        void DeleteCombo(int id);
    }
}
