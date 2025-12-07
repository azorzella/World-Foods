using System.Collections.Generic;
using System.Linq;

public class UserData {
    readonly string fullName;
    readonly string firstName;
    
    readonly string username;
    // readonly string birthday;
    readonly List<UserData> friends = new();
    
    readonly Dictionary<Dish, int> uniqueDishes = new();
    readonly Dictionary<string, List<Dish>> uniqueCountries = new();

    Dish favoriteDish;
    
    public UserData(string fullName, string username, params UserData[] friends) {
        this.fullName = fullName;
        this.username = username;
        
        firstName = fullName.Split(' ')[0];

        AddFriends(friends);
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

    public string GetUsername() {
        return username;
    }

    public string GetFirstName() {
        return firstName;
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

    public List<UserData> GetFriends() {
        return friends;
    }

    public Dish FamiliarSuggestion() {
        if (uniqueDishes.Count == 0) {
            return null;
        }
        
        Dish result = uniqueDishes.ElementAt(UnityEngine.Random.Range(0, uniqueDishes.Count)).Key;
        
        return result;
    }

    const int maxIterations = 1000;
    
    public Dish UnfamiliarSuggestion() {
        Dish result = null;

        int i = 0;
        while ((result == null || uniqueDishes.ContainsKey(result)) && i < maxIterations) {
            result = DishCatalogue.i.GetRandomDish();
            i++;
        }
        
        return result;
    }
}