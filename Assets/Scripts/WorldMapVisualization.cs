using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapVisualization : MonoBehaviour {
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

    public void NotifyListeners(string isoCode) {
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

    public void SetValue(string isoCode, float value) {
        if (!values.ContainsKey(isoCode)) {
            values.Add(isoCode, value);
        }
        else {
            values[isoCode] = value;
        }

        NotifyListeners(isoCode);        
    }

    int CountDishesFromCountryInLog(string isoCode) {
        int result = 0;

        foreach (var dish in dishLog) {
            if (dish.GetIsoCode().ToLower() == isoCode.ToLower()) {
                result++;
            }
        }
        
        return result;
    }
    
    readonly List<Dish> dishLog = new();

    void RandomlyPopulateDishLog() {
        for (int i = 0; i < 100; ++i) {
            bool addMultiple = UnityEngine.Random.Range(0F, 1F) > 0.9F;

            Dish dish = DishCatalogue.i.GetRandomDish();

            if (addMultiple) {
                for (int c = 0; c < UnityEngine.Random.Range(10, 20); c++) {
                    dishLog.Add(dish);
                }
            }
            else {
                dishLog.Add(dish);
            }
        }
    }
    
    void ForceNotifyListeners() {
        int dishesFromCountryWithMostDishesInLog = 0;

        Dictionary<string, int> numDishesFromCountries = new();
        
        foreach (var pair in DishCatalogue.isoCodes) {
            string isoCode = pair.Value;
            int numDishesFromCountry = CountDishesFromCountryInLog(isoCode);

            if (numDishesFromCountry > dishesFromCountryWithMostDishesInLog) {
                dishesFromCountryWithMostDishesInLog = numDishesFromCountry;
            }

            numDishesFromCountries.Add(isoCode, numDishesFromCountry);
        }
        
        foreach (var pair in DishCatalogue.isoCodes) {
            string isoCode = pair.Value;
            float percentage = (float)numDishesFromCountries[isoCode] / dishesFromCountryWithMostDishesInLog;
            
            SetValue(isoCode, percentage);
        }
    }
    
    void Start() {
        RandomlyPopulateDishLog();
        ForceNotifyListeners();
    }
}