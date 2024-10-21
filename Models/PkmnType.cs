using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;

namespace BulbaClone.Models
{
    public class PkmnType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Pokemon>? Pokemons { get; set; }
        public string BaseHexColour { get; set; }
        public string ComplementaryHexColour { get; set; }

        public PkmnType()
        {

        }
    }
}
