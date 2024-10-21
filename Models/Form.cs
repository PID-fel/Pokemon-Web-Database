using System.ComponentModel.DataAnnotations;

namespace BulbaClone.Models
{
    public class Form
    {
        [Key]
        public int Id { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public string Name { get; set; }
        public bool isBase { get; set; }
        public int Type1Id { get; set; }
        public PkmnType Type1 { get; set; }
        public int? Type2Id { get; set; }
        public PkmnType Type2 { get; set; }
        public double genderRatio { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int spa { get; set; }
        public int spd { get; set; }
        public int spe { get; set; }
        public string ability0 { get; set; }
        public string? ability1 { get; set; }
        public string? hiddenAbility { get; set; }
        public double heightm { get; set; }
        public double weightkg { get; set; }
        public string color { get; set; }
        public string eggGroup1 { get; set; }
        public string? eggGroup2 { get; set; }
        public int? PrevoFormId { get; set; }
        public Form? PrevoForm { get; set; }
        public ICollection<Form>? PostEvoForms { get; set; }
        public int? evoLevel { get; set; }
        public string? canGigantamax { get; set; }
        public string? forme { get; set; }
        public string? requiredItem { get; set; }
        public string? evoCondition { get; set; }
        public string? evoType { get; set; }
        public string? evoItem { get; set; }
        public string? evoRegion { get; set; }
        public string? evoMove { get; set; }
        public bool isDisplayed { get; set; }
        public string? specialAbility { get; set; }
        public int generation { get; set; }




        public Form()
        {

        }
    }
}
