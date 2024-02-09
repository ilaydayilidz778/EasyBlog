using System.ComponentModel.DataAnnotations;

namespace EasyBlog.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name="Category Name")]
        public string Name { get; set; } = null!;

        // Navigation Property : Post (one-to-many relationship)
        public List<Post> Posts { get; set; } = new(); // Default değer belirtilmiş olması pst olmadan yeni bir kategory eklenmesini sağladı 
    }
}
