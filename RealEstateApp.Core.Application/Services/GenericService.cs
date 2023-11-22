

using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Core.Application.Services
{
    public class GenericService<Model, SaveViewModel, ViewModel> : IGenericService<Model, SaveViewModel, ViewModel>
         where Model : class
         where ViewModel : class
         where SaveViewModel : class
    {
        private readonly IGenericRepository<Model> _genericRepository;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<Model> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel model)
        {
            var entity = _mapper.Map<Model>(model);
            entity = await _genericRepository.AddAsync(entity);
            SaveViewModel entityvm = _mapper.Map<SaveViewModel>(entity);
            return entityvm;
        }
        public virtual async Task Update(SaveViewModel model, int id)
        {
            Model entity = _mapper.Map<Model>(model);
            await _genericRepository.UpdateAsync(entity, id);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            await _genericRepository.DeleteAsync(entity);
        }

        public virtual async Task<SaveViewModel> GetById(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            SaveViewModel saveViewModel = _mapper.Map<SaveViewModel>(entity);
            return saveViewModel;
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entities = await _genericRepository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }

    }
}
