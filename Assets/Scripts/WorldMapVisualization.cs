using System;
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
        
        TestDishVisualization();
        FindFirstObjectByType<SummaryAndSuggestions>().Show(currentUser);
    }
    
    void TestDishVisualization() {
        RandomlyPopulateDishLog();
        ForceNotifyListeners();
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


    void RandomlyPopulateDishLog() {
        for (int i = 0; i < 100; ++i) {
            bool addMultiple = UnityEngine.Random.Range(0F, 1F) > 0.9F;

            Dish dish = DishCatalogue.i.GetRandomDish();

            if (addMultiple) {
                for (int c = 0; c < UnityEngine.Random.Range(10, 20); c++) {
                    currentUser.AddDishes(dish);
                }
            }
            else {
                currentUser.AddDishes(dish);
            }
        }
    }
    
    void ForceNotifyListeners() {
        int dishesFromCountryWithMostDishesInLog = 0;

        Dictionary<string, List<Dish>> numDishesFromCountries = currentUser.GetCountriesEatenFrom();
        
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