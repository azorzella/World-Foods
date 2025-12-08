using System.Globalization;

public class Dish {
    readonly string name;
    readonly string countryName;
    readonly string alternativeName;
    
    readonly string isoCode;
    
    int rating = 0;
    
    /// <summary>
    /// Sets the passed values and caches the country's formatted name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isoCode"></param>
    /// <param name="alternativeName"></param>
    public Dish(string name, string isoCode, string alternativeName = "") {
        this.name = name;
        this.alternativeName = alternativeName;
        
        this.isoCode = isoCode;
        
        foreach (var entry in DishCatalogue.isoCodes) {
            if (entry.Value == isoCode) {
                countryName = entry.Key;
                break;
            }
        }
        
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        countryName = textInfo.ToTitleCase(countryName);
    }

    // Getters
    
    public string GetName() {
        return name;
    }

    public string GetAlternativeName() {
        return alternativeName;
    }
    
    public string GetIsoCode() {
        return isoCode;
    }

    public int GetRating() {
        return rating;
    }
    
    public string GetCountryNameFormatted() {
        return countryName;
    }
    
    // Setter
    
    public void SetRating(int newRating) {
        rating = newRating;
    }
    
    /// <summary>
    /// Returns the country's name and ISO code formatted as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return $"{name} ({isoCode})";
    }
}