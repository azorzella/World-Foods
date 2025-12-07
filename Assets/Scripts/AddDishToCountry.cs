using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddDishToCountry : MonoBehaviour {
    TextMeshProUGUI Button;

    readonly List<Dish> filteredResults = new();
    readonly List<string> countryNames = new();

    Dish dish;
    int country_index = 0;

    public TMP_InputField dishNameInput;
    public TMP_Dropdown dropdown;
    public TMP_Dropdown countryDropdown;

    private void Start() {
        countryDropdown.gameObject.SetActive(false);
    }

    public void EntryFilter(string entry) {
        countryDropdown.gameObject.SetActive(false);

        filteredResults.Clear();

        if (entry.Length < 3) {
            return;
        }

        List<string> filteredDishNames = new();

        foreach (var d in DishCatalogue.dishes) {
            if (d.GetName().ToLower().Contains(entry.ToLower())) {
                filteredResults.Add(d);
                filteredDishNames.Add(d.GetName());
            }
        }

        if (filteredResults.Count == 0) {
            countryDropdown.gameObject.SetActive(true);

            foreach (var pair in DishCatalogue.isoCodes) {
                countryNames.Add(pair.Key);
            }

            countryDropdown.AddOptions(countryNames);
            ;
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(filteredDishNames);
    }

    public void DishSelected(int index) {
        dish = filteredResults[index];
        Debug.Log($"Selected {dish}");
    }

    public void countrySelected(int index) {
        country_index = index;
    }

    public void LogDish() {
        Debug.Log(dish);
        
        if (dish == null) {
            return;
        }

        FindFirstObjectByType<WorldMapVisualization>().LogDish(dish);
        
        // if (country_index == 0) {
        //     FindFirstObjectByType<WorldMapVisualization>().LogDish(dish);
        // }
        // else {
        //     Dish d = new(dishNameInput.text, DishCatalogue.isoCodes.ElementAt(country_index).Value);
        //     FindFirstObjectByType<WorldMapVisualization>().LogDish(d);
        // }
    }
}