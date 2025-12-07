using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldMapVisualization : MonoBehaviour {
    UserData june = new("June December", "june");
    UserData amelia = new("Amelia Qux", "ameliia");
    UserData mona = new("Mona Lisa", "monalisa");
    UserData alex = new("Alex Zorzella", "azorzella");
    
    UserData currentUser = new("Amy", "ratlover");
    
    readonly Dictionary<string, List<VisListener>> listeners = new();
    readonly Dictionary<string, float> values = new();

    public void LogDish(Dish dish)
    {
        currentUser.AddDishes(dish);
        ForceNotifyListeners();
    }
    
    public void RegisterListener(VisListener listener, string isoCode) {
        if (!listeners.ContainsKey(isoCode)) {
            listeners.Add(isoCode, new List<VisListener>());
        }
        
        listeners[isoCode].Add(listener);
        
        if (values.ContainsKey(isoCode)) {
            listener.OnValueChanged(values[isoCode]);
        } else {
            listener.OnValueChanged(0);
        }
    }

    void NotifyListeners(string isoCode) {
        if (!listeners.ContainsKey(isoCode)) {
            return;
        }
        
        foreach (var listener in listeners[isoCode]) {
            if (values.ContainsKey(isoCode)) {
                listener.OnValueChanged(values[isoCode]);
            } else {
                listener.OnValueChanged(0);
            }
        }
    }
    
    void Start() {
        june.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        amelia.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        mona.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        alex.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        
        currentUser.AddDishes(DishCatalogue.i.GetXRandomDishes(15));
        
        currentUser.AddFriends(june, amelia, mona, alex);
        
        ForceNotifyListeners();
        // FindFirstObjectByType<SummaryAndSuggestions>().Show(currentUser);
    }
    
    void SetValueForCountryVisualization(string isoCode, float value) {
        if (!values.ContainsKey(isoCode)) {
            values.Add(isoCode, value);
        }
        else {
            values[isoCode] = value;
        }

        NotifyListeners(isoCode);        
    }
    
    void ForceNotifyListeners() {
        int dishesFromCountryWithMostDishesInLog = 0;

        Dictionary<string, List<Dish>> numDishesFromCountries = currentUser.GetCountriesEatenFrom();

        foreach (var pair in numDishesFromCountries) {
            if (pair.Value.Count > dishesFromCountryWithMostDishesInLog) {
                dishesFromCountryWithMostDishesInLog = pair.Value.Count;
            }
        }
        
        foreach (var pair in DishCatalogue.isoCodes) {
            string isoCode = pair.Value;
            float percentage = 0;

            int numDishes = 0;
            
            if(numDishesFromCountries.ContainsKey(isoCode)) {
                numDishes = numDishesFromCountries[isoCode].Count;
            }

            if (numDishes > 0) {
                percentage = (float)numDishes / dishesFromCountryWithMostDishesInLog;
            }
            
            SetValueForCountryVisualization(isoCode, percentage);
        }
    }

    public List<Dish> GetLoggedDishesFromCountry(string isoCode) {
        List<Dish> result = new();

        if (currentUser.GetCountriesEatenFrom().ContainsKey(isoCode)) {
            result = currentUser.GetCountriesEatenFrom()[isoCode];
        }

        return result;
    }
}