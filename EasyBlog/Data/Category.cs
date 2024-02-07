namespace EasyBlog.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigation Property : Post (one-to-many relationship)
        public List<Post> Posts { get; set; } = new();
    }
}
