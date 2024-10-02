
namespace EF_Core_01.Services.IServices
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category Insert(Category category);
        Category Update(int id, Category category);
        Category Delete(int id);
    }
}
