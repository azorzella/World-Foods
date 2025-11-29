using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Dish {
	readonly string name;
	readonly string isoCode;
}

public class DishParser {
	static DishParser _i;
	
	public static DishParser i {
		get {
			if (_i == null) {
				_i = new DishParser();
			}
			return _i;
		}
	}

	DishParser() {
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

			string dishName = split[0];
			string dishCountry = split[1];
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
		}
	}
}