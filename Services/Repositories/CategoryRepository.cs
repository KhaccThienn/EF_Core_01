using EF_Core_01.Models;
using EF_Core_01.Models.InMemory;
using EF_Core_01.Services.IRepositories;

namespace EF_Core_01.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ModelContext _dbContext;
        private readonly CategoryMemoryModel _inMemory;

        public CategoryRepository(ModelContext dbContext, CategoryMemoryModel inMemo)
        {
            _dbContext = dbContext;
            _inMemory = inMemo;
        }

        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();

            try
            {
                list = _inMemory.CategoryMem.Values.ToList();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public Category Insert(Category category)
        {
            try
            {
                //Insert into db
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();

                // insert into Memory
                _inMemory.CategoryMem.Add(category.Id.ToString(), category);
            } catch
            {
                throw;
            }
            return category;
        }

        public Category Update(int id, Category category)
        {
            //Category c = new Category();
            //var item = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            //if (item != null)
            //{
            //    try
            //    {
            //        // Update data in database
            //        item.Name = category.Name;
            //        item.Status = category.Status;
            //        _dbContext.Update(item);
            //        _dbContext.SaveChanges();
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //}
            //return c;

            // Find record from Memory:
            var old_category = _inMemory.CategoryMem.FirstOrDefault(x => x.Key.Equals(id));
            Category c = new Category();

            c.Id = id;
            c.Name = category.Name;
            c.Status = category.Status;

            try
            {
                _inMemory.CategoryMem.Remove(c.Id.ToString());
                _inMemory.CategoryMem.Add(c.Id.ToString(), c);

                _dbContext.Update(c);
                _dbContext.SaveChanges();
            } catch(Exception ex)
            {
                throw;
            }

            return c;

        }

        public Category Delete(int id)
        {
            var category = new Category();
            var cate = _inMemory.CategoryMem.FirstOrDefault(x => x.Value.Id == id);
            if (cate.Value != null)
            {
                try
                {
                    category = cate.Value;
                    _inMemory.CategoryMem.Remove(cate.Key);
                    return cate.Value;
                }
                catch
                {
                    throw;
                }
            }

            return category;
        }
    }
}
