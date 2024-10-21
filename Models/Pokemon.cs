using System.ComponentModel.DataAnnotations;

namespace BulbaClone.Models
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Tags { get; set; }
        public int generation { get; set; }
        public ICollection<Form> Forms { get; set; }
        public Pokemon()
        {

        }
    }
}
