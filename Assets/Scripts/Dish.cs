using System.Globalization;

public class Dish {
    readonly string name;
    readonly string isoCode;
    int rating = 0;
    readonly string countryName;

    public Dish(string name, string isoCode) {
        this.name = name;
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

    public string GetName() {
        return name;
    }

    public string GetIsoCode() {
        return isoCode;
    }

    public int GetRating() {
        return rating;
    }

    public void SetRating(int newRating) {
        this.rating = newRating;
    }

    public override string ToString() {
        return $"{name} ({isoCode})";
    }
    
    public string GetCountryNameFormatted() {
        return countryName;
    }
}