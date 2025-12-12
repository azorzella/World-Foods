using System.IO;
using UnityEngine;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DishCatalogue {
	static DishCatalogue _i;
	
	/// <summary>
	/// Returns the static instance of DishCatalogue. If the instance
	/// has not been set, then it creates a new instance of DishCatalogue,
	/// caches it, and returns it
	/// </summary>
	public static DishCatalogue i {
		get {
			if (_i == null) {
				_i = new DishCatalogue();
			}
			
			return _i;
		}
	}

	/// <summary>
	/// The constructor is private and can only be called by the getter i
	/// </summary>
	DishCatalogue() {
		ParseData();
	}

	public static readonly List<Dish> dishes = new();
	public static readonly Dictionary<string, string> isoCodes = new();

	const string latinPattern = @"^{Script=Latin}*$";

	/// <summary>
	/// Returns the first dish in dishes that the function finds with the name 'dishName'
	/// </summary>
	/// <param name="dishName"></param>
	/// <returns></returns>
	public Dish GetDishByName(string dishName) {
		Dish result = null;

		return result;
	}
	
	/// <summary>
	/// Adds the passed dish to the catalogue
	/// </summary>
	/// <param name="dish"></param>
	public void AddDishToCatalogue(Dish dish) {
		dishes.Add(dish);
	}
	
	/// <summary>
	/// Returns a random dish from the catalogue
	/// </summary>
	/// <returns></returns>
	public Dish GetRandomDish() {
		return dishes[Random.Range(0, dishes.Count)];
	}
	
	/// <summary>
	/// Returns X random dishes from the catalogue
	/// </summary>
	/// <param name="count"></param>
	/// <returns></returns>
	public List<Dish> GetXRandomDishes(int count) {
		List<Dish> result = new();

		for (int n = 0; n < count; n++) {
			result.Add(GetRandomDish());
		}
		
		return result;
	}
	
	/// <summary>
	/// Parses the ISO codes and dishes from the passed files
	/// </summary>
	static void ParseData() {
		ParseIsoCodes();

		ParseDishesFromFile("dishes.tsv", 1, 5, 2);
		ParseDishesFromFile("wikipedia_dishes.tsv", 0, 1);
	}
	
	/// <summary>
	/// Parses the ISO codes from iso_codes.csv and populates the Dictionary of countries and ISO codes
	/// with its results 
	/// </summary>
	static void ParseIsoCodes() {
		string contents = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "", "iso_codes.csv"));

		string[] lines = contents.Split('\n');

		for (int i = 1; i < lines.Length; i++) {
			string line = lines[i];

			if (string.IsNullOrWhiteSpace(line)) {
				continue;
			}
			
			string[] split = line.Split(',');

			string countryName = split[0];
			string isoCode = split[1];

			countryName = countryName.ToLower();
			countryName = Regex.Replace(countryName, @"\s+", "");
			
			isoCodes.Add(countryName, isoCode);
		}
	}
	
	/// <summary>
	/// Parses the dishes from the passed filename using the passed indices to reference
	/// what column the main name, alternative name, and country name will be found, then
	/// populates the list of dishes with dishes created using the parsed data
	/// </summary>
	/// <param name="filename"></param>
	/// <param name="dishNameIndex"></param>
	/// <param name="countryNameIndex"></param>
	/// <param name="alternativeNameIndex"></param>
	static void ParseDishesFromFile(string filename, int dishNameIndex, int countryNameIndex, int alternativeNameIndex = -1) {
		string contents = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "", filename));

		string[] lines = contents.Split('\n');

		for (int i = 1; i < lines.Length; i++) {
			string line = lines[i];

			if (string.IsNullOrWhiteSpace(line)) {
				continue;
			}

			string[] split = line.Split('\t');

			if (split.Length < (countryNameIndex > dishNameIndex ? countryNameIndex : dishNameIndex) + 1) {
				Debug.LogWarning($"Skipping line '{line}' as it seems to be incorrectly formatted or not have enough data.");
				continue;
			}
			
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

			string dishName = split[dishNameIndex];
			textInfo.ToTitleCase(dishName);

			string alternativeName = "";
			
			if (alternativeNameIndex >= 0) {
				alternativeName = split[alternativeNameIndex];
				alternativeName = textInfo.ToTitleCase(alternativeName);
			}
			
			string dishCountries = split[countryNameIndex];

			string[] dishCountriesSplit = dishCountries.Split(',');

			foreach (var country in dishCountriesSplit) {
				string dishCountry = country.ToLower();
				dishCountry = Regex.Replace(dishCountry, @"\s+", "");
			
				string isoCode = "";

				if (!isoCodes.ContainsKey(dishCountry)) {
					continue;
				}

				isoCode = isoCodes[dishCountry];

				if (!Regex.IsMatch(dishName, latinPattern)) {
					if (string.IsNullOrWhiteSpace(alternativeName)) {
						continue;
					}

					(dishName, alternativeName) = (alternativeName, dishName);
				}
				
				Dish newDish = new Dish(dishName, isoCode, alternativeName);
				dishes.Add(newDish);
			}
		}
	}
}