public class Dish {
    readonly string name;
    readonly string isoCode;

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

    public override string ToString() {
        return $"{name} ({isoCode})";
    }
}