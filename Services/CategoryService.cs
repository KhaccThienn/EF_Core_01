using EF_Core_01.Models;
using EF_Core_01.Services.IRepositories;
using EF_Core_01.Services.IServices;

namespace EF_Core_01.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public List<Category> GetCategories()
        {
            try
            {
                var data = _repository.GetCategories();
                return data;
            }
            catch
            {
                throw;
            }
        }
        public Category Delete(int id)
        {
            try
            {
                var data = _repository.Delete(id);
                return data;
            }
            catch
            {
                throw;
            }
        }

        public Category Insert(Category category)
        {
            try
            {
                var data = _repository.Insert(category);
                return data;
            }
            catch
            {
                throw;
            }
            
        }

        public Category Update(int id, Category category)
        {
            try
            {
                var data = _repository.Update(id, category);
                return data;
            }
            catch
            {
                throw;
            }
        }
    }
}
