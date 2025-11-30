using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

public class DishCatalogue {
	static DishCatalogue _i;
	
	public static DishCatalogue i {
		get {
			if (_i == null) {
				_i = new DishCatalogue();
			}
			return _i;
		}
	}

	DishCatalogue() {
		ParseData();
	}
	
	static readonly List<Dish> dishes = new();
	public static readonly Dictionary<string, string> isoCodes = new();

	public Dish GetDishCalled(string dishName) {
		Dish result = Array.Find(dishes.ToArray(), dish => dish.GetName().ToLower() == dishName.ToLower());
		
		return result;
	}

	public List<Dish> GetDishesWhichContain(string contains) {
		List<Dish> result = new();

		foreach (Dish dish in dishes) {
			if (dish.GetName().ToLower() == contains.ToLower()) {
				result.Add(dish);
			}
		}

		return result;
	}

	public void AddDishToCatalogue(Dish dish) {
		dishes.Add(dish);
	}

	public Dish GetRandomDish() {
		return dishes[UnityEngine.Random.Range(0, dishes.Count)];
	}

	static void ParseData() {
		ParseIsoCodes();

		ParseDishesFromFile("dishes.tsv", 2, 5);
		ParseDishesFromFile("wikipedia_dishes.tsv", 1, 3);
	}
	
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
	
	static void ParseDishesFromFile(string filename, int dishNameIndex, int countryNameIndex) {
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
			
			string dishName = split[dishNameIndex];
			
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
			
				Dish newDish = new Dish(dishName, isoCode);
				dishes.Add(newDish);
			}
		}
	}
}