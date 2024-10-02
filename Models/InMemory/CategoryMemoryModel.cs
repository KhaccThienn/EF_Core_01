namespace EF_Core_01.Models.InMemory
{
    public class CategoryMemoryModel
    {
        public Dictionary<string, Category> CategoryMem { get; set; }

        public CategoryMemoryModel()
        {
            CategoryMem = new Dictionary<string, Category>();
        }
    }
}
