
namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<Model, SaveViewModel, ViewModel>
       where Model : class
       where SaveViewModel : class
       where ViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel model);
        Task Update(SaveViewModel model, int id);
        Task Delete(int id);
        Task<List<ViewModel>> GetAll();
        Task<SaveViewModel> GetById(int id);
    }
}
