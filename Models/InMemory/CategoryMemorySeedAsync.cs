namespace EF_Core_01.Models.InMemory
{
    public class CategoryMemorySeedAsync
    {
        public async Task SeedAsync(CategoryMemoryModel memory, ModelContext dbContext)
        {
            var basketItems = await dbContext.Categories.ToListAsync();
            if (basketItems.Count > 0)
            {
                foreach (var item in basketItems)
                {
                    memory.CategoryMem.Add(item.Id.ToString(), item);
                }
            }
        }
    }
}
