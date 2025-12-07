using System.Collections.Generic;

public class UserData {
    readonly string username;
    // readonly string birthday;

    public UserData(string username, List<Dish> dishes) {
        this.username = username;
    }

    public Dictionary<Dish, int> GetDishes() {
        return uniqueDishes;
    }

    readonly Dictionary<string, int> uniqueCountries = new();
    readonly Dictionary<Dish, int> uniqueDishes = new();

    Dish favoriteDish;

    public void AddDishes(List<Dish> dishes) {
        foreach (var dish in dishes) {
            if (!uniqueDishes.ContainsKey(dish)) {
                uniqueDishes.Add(dish, 1);
            }

            uniqueDishes[dish]++;
        }
        
        UpdateCachedValues();
    }
    
    void UpdateCachedValues() {
        uniqueCountries.Clear();
        uniqueDishes.Clear();
        
        foreach (var dish in uniqueDishes) {
            string isoCode = dish.Key.GetIsoCode();
                
            if (!uniqueCountries.ContainsKey(isoCode)) {
                uniqueCountries.Add(isoCode, 1);
            }

            uniqueCountries[isoCode]++;
        }

        int mostTimesEatenOneDish = 0;

        foreach (var dish in uniqueDishes) {
            if (dish.Value > mostTimesEatenOneDish) {
                favoriteDish = dish.Key;
                mostTimesEatenOneDish = dish.Value;
            }
        }
    }
    
    public int NumUniqueCountriesEatenFrom() {
        return uniqueCountries.Count;
    }

    public int NumUniqueDishesEaten() {
        return uniqueDishes.Count;
    }

    public Dish FavoriteDish() {
        return favoriteDish;
    }
}