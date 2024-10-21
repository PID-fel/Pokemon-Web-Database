using BulbaClone.Models;
using System.Linq;


public static class CommonMethodsHelper
{
    public static (string pokemonNum, string formNum, string genderCode, string backupGenderCode, string gigantamax) GetImageInput(Pokemon pokemon, Form form)
    {
        var pokemonNum = pokemon.Id.ToString("D4");

        var genderCode = form.genderRatio switch
        {
            1 => "fo",
            0 => "mo",
            -1 => "uk",
            _ => "mf"
        };

        var backupGenderCode = form.genderRatio switch
        {
            1 => "fd",
            0 => "md",
            _ => "fd"
        };

        var orderedForms = pokemon.Forms.OrderBy(f => f.Id).ToList();

        var formNum = orderedForms.Select((alternateForm, index) => new { alternateForm, index })
            .FirstOrDefault(x => x.alternateForm.Id == form.Id)?.index.ToString("D3");

        var gigantamax = "n";

        if (form.forme == "Gmax")
        {
            formNum = "000";
            gigantamax = "g";
        }
        else if (form.forme == "Gmax1")
        {
            formNum = "001";
            gigantamax = "g";
        }

        return (pokemonNum, formNum, genderCode, backupGenderCode, gigantamax);
    }

    public static int GetUrlFormIdFromPokemon(Pokemon pokemonToTest, Form formToTest)
    {
        var orderedForms = pokemonToTest.Forms.OrderBy(f => f.Id).ToList();

        var formIndexCount = 0;
        foreach (var comparisonForm in orderedForms) {
            if (comparisonForm.Id == formToTest.Id){
                return formIndexCount;
            }
            formIndexCount = formIndexCount + 1;
        }
        return 0;
    }


}