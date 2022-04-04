using System;
using System.Collections.ObjectModel;

namespace MyGL.Models
{
	public class Constants
	{
		public static readonly List<string> DateFormatList = new()
		{
			"d/M/yyyy",
			"d/M/yy",
			"yyyy/M/d",
			"M/d/yyyy",
			"M/d/yy"
		};

		public static readonly ReadOnlyCollection<(string, string)> DefaultCategories = new(new[]
		{
			("Bill", "Electricity"),
			("Bill", "Internet"),
			("Bill", "Phone"),
			("Car", "Car Insurance"),
			("Car", "Fuel"),
			("Car", "Parking"),
			("Groceries", "Groceries"),
			("Income", "Interest"),
			("Income", "Wage"),
			("Lifestyle", "Drinks"),
			("Lifestyle", "Entertainment"),
			("Lifestyle", "Food"),
			("Medical", "Medical"),
			("Rent", "Bond"),
			("Rent", "Rent")
		});

		public static readonly ReadOnlyCollection<(string, string)> DefaultCategoryConditions = new(new[]
		{
			("TPG INTERNET", "Internet"),
			("AMAYSIM", "Phone"),
			("BELONG", "Phone"),
			("TELSTRA", "Phone"),
			("BOOST PREPAID", "Phone"),
			("NRMA", "Car Insurance"),
			("7 ELEVEN", "Fuel"),
			("7-ELEVEN", "Fuel"),
			("AMPOL", "Fuel"),
			("CALTEX", "Fuel"),
			("PARKING", "Parking"),
			("ALDI", "Groceries"),
			("BI-LO", "Groceries"),
			("COLES", "Groceries"),
			("SAFEWAY", "Groceries"),
			("WOOLWORTHS", "Groceries"),
			("1ST CHOICE", "Drinks"),
			("BWS", "Drinks"),
			("DAN MURPHY", "Drinks"),
			("LIQUORLAND", "Drinks"),
			("TAVERN", "Drinks"),
			("TICKETEK", "Entertainment"),
			("NETFLIX", "Entertainment"),
			("FOOD", "Food"),
			("SUBWAY", "Food"),
			("SUSHI TRAIN", "Food"),
			("AMCAL", "Medical"),
			("CHEMIST", "Medical"),
			("CHEMMART", "Medical"),
			("TERRYWHITE", "Medical")
		});
	}
}

