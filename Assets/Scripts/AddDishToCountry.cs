using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using TMPro;

public class AddDishToCountry : MonoBehaviour {
    TextMeshProUGUI Button;

    readonly List<Dish> filteredResults = new();
    readonly List<string> countryNames = new();

    Dish dish;
    int country_index = -1;

    public TMP_InputField dishNameInput;
    public TMP_Dropdown dropdown;
    public TMP_Dropdown countryDropdown;

    /// <summary>
    /// sets the country dropdown to false at start
    /// </summary>
    private void Start() {
        countryDropdown.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// shows filtered dish results based on text entry. Toggles custom dish add if no dish matches found.
    /// </summary>
    /// <param name="entry"></param>
    public void EntryFilter(string entry) {
        countryDropdown.gameObject.SetActive(false);

        filteredResults.Clear();

        if (entry.Length < 3) {
            return;
        }

        List<string> filteredDishNames = new();

        foreach (var d in DishCatalogue.dishes) {
            if (d.GetName().ToLower().Contains(entry.ToLower()) || d.GetAlternativeName().ToLower().Contains(entry.ToLower())) {
                filteredResults.Add(d);
                filteredDishNames.Add(d.GetName());
            }
        }

        if (filteredResults.Count == 0) {
            countryDropdown.gameObject.SetActive(true);

            PopulateCountryDropdown();
        }
        else {
            dish = filteredResults[0];
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(filteredDishNames);
    }
    
    /// <summary>
    /// populates country dropdown
    /// </summary>
    void PopulateCountryDropdown() {
        countryDropdown.ClearOptions();

        foreach (var pair in DishCatalogue.isoCodes) {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string countryName = textInfo.ToTitleCase(pair.Key);
                
            countryNames.Add(countryName);
        }

        countryDropdown.AddOptions(countryNames);
    }
    
    /// <summary>
    /// saves selected dish from filtered results
    /// </summary>
    /// <param name="index"></param>
    public void DishSelected(int index) {
        dish = filteredResults[index];
    }

    /// <summary>
    /// saves selected country
    /// </summary>
    /// <param name="index"></param>
    public void countrySelected(int index) {
        country_index = index;
    }

    /// <summary>
    /// logs a dish
    /// </summary>
    public void LogDish() {
        if (dish == null) {
            return;
        }
            
        if (country_index < 0) {
            FindFirstObjectByType<WorldMapVisualization>().LogDish(dish);
        } else {
            Dish newDish = new(dishNameInput.text, DishCatalogue.isoCodes.ElementAt(country_index).Value);
            FindFirstObjectByType<WorldMapVisualization>().LogDish(newDish);
            DishCatalogue.i.AddDishToCatalogue(newDish);
        }
    }
}