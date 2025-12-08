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
    
    /// <summary>
    /// Caches the user's full name and username, determines their
    /// first name based on their full name, and then adds the passed friends
    /// to their friends list
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="username"></param>
    /// <param name="friends"></param>
    public UserData(string fullName, string username, params UserData[] friends) {
        this.fullName = fullName;
        this.username = username;
        
        firstName = fullName.Split(' ')[0];

        AddFriends(friends);
    }

    /// <summary>
    /// Returns the dictionary of unique dishes to times eaten
    /// </summary>
    /// <returns></returns>
    public Dictionary<Dish, int> GetUniqueDishesEaten() {
        return uniqueDishes;
    }

    /// <summary>
    /// Returns the dictionary of countries to list of dishes eaten from that country
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, List<Dish>> GetCountriesEatenFrom() {
        return uniqueCountries;
    }

    /// <summary>
    /// Adds the passed dishes to the user's dictionary of dishes
    /// </summary>
    /// <param name="dishes"></param>
    public void AddDishes(params Dish[] dishes) {
        AddDishes(dishes.ToList());
    }
    
    /// <summary>
    /// Checks if the passed dish is present in the dictionary of dishes the user has eaten and adds an entry if
    /// it isn't. It then increases the number of times that user has eaten that dish by one
    /// </summary>
    /// <param name="dishes"></param>
    public void AddDishes(List<Dish> dishes) {
        foreach (var dish in dishes) {
            if (!uniqueDishes.ContainsKey(dish)) {
                uniqueDishes.Add(dish, 0);
            }

            uniqueDishes[dish]++;
        }
        
        UpdateCachedValues();
    }
    
    /// <summary>
    /// Updates the list of dishes eaten from each country using the dishes from the unique dishes
    /// </summary>
    void UpdateCachedValues() {
        foreach (var dish in uniqueDishes) {
            string isoCode = dish.Key.GetIsoCode();
                
            if (!uniqueCountries.ContainsKey(isoCode)) {
                uniqueCountries.Add(isoCode, new List<Dish>());
            }

            if (!uniqueCountries[isoCode].Contains(dish.Key)) {
                uniqueCountries[isoCode].Add(dish.Key);
            }
        }

        int mostTimesEatenOneDish = 0;

        foreach (var dish in uniqueDishes) {
            if (dish.Value > mostTimesEatenOneDish) {
                favoriteDish = dish.Key;
                mostTimesEatenOneDish = dish.Value;
            }
        }
    }
    
    // Getters

    public string GetUsername() {
        return username;
    }

    public string GetFirstName() {
        return firstName;
    }
    
    public Dish GetFavoriteDish() {
        return favoriteDish;
    }
    
    public List<UserData> GetFriends() {
        return friends;
    }
    
    /// <summary>
    /// Returns the number of unique countries the user has eaten from
    /// </summary>
    /// <returns></returns>
    public int NumUniqueCountriesEatenFrom() {
        return uniqueCountries.Count;
    }

    /// <summary>
    /// Returns the number of unique dishes the user has eaten
    /// </summary>
    /// <returns></returns>
    public int NumUniqueDishesEaten() {
        return uniqueDishes.Count;
    }

    /// <summary>
    /// Adds the passed users to the user's friends list
    /// </summary>
    /// <param name="users"></param>
    public void AddFriends(params UserData[] users) {
        AddFriends(users.ToList());
    }

    /// <summary>
    /// Adds the passed list of users to the user's friends list
    /// </summary>
    /// <param name="users"></param>
    public void AddFriends(List<UserData> users) {
        friends.AddRange(users);
    }

    /// <summary>
    /// Returns a random dish that the user has eaten already
    /// </summary>
    /// <returns></returns>
    public Dish FamiliarSuggestion() {
        if (uniqueDishes.Count == 0) {
            return null;
        }
        
        Dish result = uniqueDishes.ElementAt(UnityEngine.Random.Range(0, uniqueDishes.Count)).Key;
        
        return result;
    }

    const int maxIterations = 1000;
    
    /// <summary>
    /// Returns a random dish that the user hasn't eaten. If it doesn't find a new dish, it returns
    /// a random dish the user has already eaten
    /// </summary>
    /// <returns></returns>
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