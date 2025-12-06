public class Dish {
    readonly string name;
    readonly string isoCode;
    int rating = 0;

    public Dish(string name, string isoCode) {
        this.name = name;
        this.isoCode = isoCode;
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
}