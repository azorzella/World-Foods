using System.Collections.Generic;
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
		ParseIsoCodes();
		ParseDishes();
	}
	
	static readonly List<Dish> dishes = new();
	static readonly Dictionary<string, string> isoCodes = new();

	static void ParseIsoCodes() {
		string contents = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "", "iso_codes.csv"));

		string[] lines = contents.Split('\n');

		for (int i = 0; i < lines.Length; i++) {
			string line = lines[i];
			string[] split = line.Split('\t');

			string countryName = split[0];
			string isoCode = split[1];

			countryName = countryName.ToLower();
			countryName = Regex.Replace(countryName, @"\s+", "");
			
			isoCodes.Add(countryName, isoCode);
		}
	}
	
	static void ParseDishes() {
		string contents = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "", "global_street_food.csv"));

		string[] lines = contents.Split('\n');

		for (int i = 0; i < lines.Length; i++) {
			string line = lines[i];
			string[] split = line.Split('\t');

			string dishName = split[0];
			string dishCountry = split[1];

			string isoCode = "";

			if (isoCodes.ContainsKey(dishCountry)) {
				isoCode = isoCodes[dishCountry];
			} else {
				continue;
			}

			Dish newDish = new Dish(dishName, isoCode);
			dishes.Add(newDish);
		}
	}
}