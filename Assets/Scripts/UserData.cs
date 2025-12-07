using System.Collections.Generic;
using System.Linq;

public class UserData {
    readonly string username;
    // readonly string birthday;
    readonly List<UserData> friends = new();
    
    readonly Dictionary<Dish, int> uniqueDishes = new();
    readonly Dictionary<string, List<Dish>> uniqueCountries = new();

    Dish favoriteDish;
    
    public UserData(string username) {
        this.username = username;
    }

    public Dictionary<Dish, int> GetUniqueDishesEaten() {
        return uniqueDishes;
    }

    public Dictionary<string, List<Dish>> GetCountriesEatenFrom() {
        return uniqueCountries;
    }

    public void AddDishes(params Dish[] dishes) {
        AddDishes(dishes.ToList());
    }
    
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
                uniqueCountries.Add(isoCode, new List<Dish>());
            }

            uniqueCountries[isoCode].Add(dish.Key);
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

    public void AddFriends(params UserData[] users) {
        AddFriends(users.ToList());
    }

    public void AddFriends(List<UserData> users) {
        friends.AddRange(users);
    }
}