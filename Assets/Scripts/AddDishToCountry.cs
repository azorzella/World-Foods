using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddDishToCountry : MonoBehaviour
{
    TextMeshProUGUI Button;
    
    readonly List<Dish> filteredResults = new();
    
    Dish dish;
    
    public TMP_Dropdown dropdown;
    public void EntryFilter(string entry)
    {
        filteredResults.Clear();

        if (entry.Length < 3)
        {
            return;
        }
        
        List<string> filteredDishNames = new();
        
        foreach (var d in DishCatalogue.dishes)
        {
            if(d.GetName().ToLower().Contains(entry.ToLower()))
            {
                filteredResults.Add(d);
                filteredDishNames.Add(d.GetName());
            }
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(filteredDishNames);
    }

    public void DishSelected(int index)
    {
        dish = filteredResults[index];
    }
    
    public void LogDish()
    {
        if (dish != null)
        {
            FindFirstObjectByType<WorldMapVisualization>().LogDish(dish);
        }
    }
}