using System.Collections.Generic;
using UnityEngine;

public class WorldMapVisualization : MonoBehaviour {
    UserData june = new("June December", "june");
    UserData amelia = new("Amelia Qux", "ameliia");
    UserData mona = new("Mona Lisa", "monalisa");
    UserData alex = new("Alex Zorzella", "azorzella");

    UserData currentUser = new("Amy", "ratlover");

    readonly Dictionary<string, List<VisListener>> listeners = new();
    readonly Dictionary<string, float> values = new();

    /// <summary>
    /// Adds the passed dish to the current user's dish log and force notifies
    /// all its listeners
    /// </summary>
    /// <param name="dish"></param>
    public void LogDish(Dish dish) {
        currentUser.AddDishes(dish);
        ForceNotifyListeners();
    }

    /// <summary>
    /// Registers a listener to the list of listeners for the passed ISO code
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="isoCode"></param>
    public void RegisterListener(VisListener listener, string isoCode) {
        if (!listeners.ContainsKey(isoCode)) {
            listeners.Add(isoCode, new List<VisListener>());
        }

        listeners[isoCode].Add(listener);

        if (values.ContainsKey(isoCode)) {
            listener.OnValueChanged(values[isoCode]);
        }
        else {
            listener.OnValueChanged(0);
        }
    }

    /// <summary>
    /// Notifies all the listeners registered to the passed ISO code
    /// </summary>
    /// <param name="isoCode"></param>
    void NotifyListeners(string isoCode) {
        if (!listeners.ContainsKey(isoCode)) {
            return;
        }

        foreach (var listener in listeners[isoCode]) {
            if (values.ContainsKey(isoCode)) {
                listener.OnValueChanged(values[isoCode]);
            }
            else {
                listener.OnValueChanged(0);
            }
        }
    }

    void Start() {
        PopulateCurrentUserWithTestData();

        ForceNotifyListeners();
    }

    /// <summary>
    /// Populates the current user with test data for testing
    /// </summary>
    void PopulateCurrentUserWithTestData() {
        june.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        amelia.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        mona.AddDishes(DishCatalogue.i.GetXRandomDishes(10));
        alex.AddDishes(DishCatalogue.i.GetXRandomDishes(10));

        currentUser.AddDishes(DishCatalogue.i.GetXRandomDishes(15));

        currentUser.AddFriends(june, amelia, mona, alex);
    }

    /// <summary>
    /// Sets the passed ISO code's value to the passed value and then
    /// notifies all of that ISO code's listeners of the change
    /// </summary>
    /// <param name="isoCode"></param>
    /// <param name="value"></param>
    void SetValueForCountryVisualization(string isoCode, float value) {
        if (!values.ContainsKey(isoCode)) {
            values.Add(isoCode, value);
        }
        else {
            values[isoCode] = value;
        }

        NotifyListeners(isoCode);
    }

    /// <summary>
    /// Notifies every listener registered to every ISO code that the
    /// values have changed
    /// </summary>
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

            if (numDishesFromCountries.ContainsKey(isoCode)) {
                numDishes = numDishesFromCountries[isoCode].Count;
            }

            if (numDishes > 0) {
                percentage = (float)numDishes / dishesFromCountryWithMostDishesInLog;
            }

            SetValueForCountryVisualization(isoCode, percentage);
        }
    }

    /// <summary>
    /// Returns the list of dishes the current user has eaten from the country with the passed ISO code
    /// </summary>
    /// <param name="isoCode"></param>
    /// <returns></returns>
    public List<Dish> GetLoggedDishesFromCountry(string isoCode) {
        List<Dish> result = new();

        if (currentUser.GetCountriesEatenFrom().ContainsKey(isoCode)) {
            result = currentUser.GetCountriesEatenFrom()[isoCode];
        }

        return result;
    }

    /// <summary>
    /// Returns the current user
    /// </summary>
    /// <returns></returns>
    public UserData GetCurrentUser() {
        return currentUser;
    }
}