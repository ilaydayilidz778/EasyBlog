namespace EasyBlog.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Image { get; set; } 

        // Navigation Property : Category (one-to-many relationship)
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}