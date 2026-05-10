using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutTuples : Koan
{

	#region 1: Tuple can group multiple elements together

	// A tuple is a C# class
	[Step(1)]
	public static void TupleIsACSharpClass()
	{
		Tuple<string, string> batman = new("Bruce", "Wayne");

		Assert.Equal("Bruce", batman.Item1); // FirstName
		Assert.Equal("Wayne", batman.Item2); // LastName
	}

	// with some syntax sugar
	[Step(2)]
	public static void WithSomeSyntaxSugar()
	{
		(string, string) batman = ("Bruce", "Wayne");

		Assert.Equal("Bruce", batman.Item1); // FirstName
		Assert.Equal("Wayne", batman.Item2); // LastName
	}

	// You can name values in the tuple
	[Step(3)]
	public static void YouCanNameValuesInTuple()
	{
		string lastName = "Wayne";
		(string firstName, string lastName) batman = (firstName: "Bruce", lastName);

		Assert.Equal("Bruce", batman.firstName);
		Assert.Equal("Wayne", batman.lastName);
	}

	// A tuple can be used as a function parameter
	[Step(4)]
	public static void TupleCanBeUsedInFunction()
	{
		(string firstName, string lastName) batman = (firstName: "Bruce", lastName: "Wayne");

		Assert.Equal("Bruce Wayne", GetFullName(batman));
	}

	public static string GetFullName((string firstName, string lastName) data)
	{
		return $"{data.firstName} {data.lastName}";
	}

	// A tuple can contain different types
	[Step(5)]
	public static void TupleCanContainDifferentTypes()
	{
		List<string> enemy = ["Joker", "Penguin", "Riddler", "Catwoman"];
		(string firstName, string lastName, List<string> enemy) batman1966 = (firstName: "Bruce", lastName: "Wayne", enemy);

		Assert.Equal(typeof(string), batman1966.firstName.GetType());
		Assert.Equal(typeof(List<string>), batman1966.enemy.GetType());

	}
	#endregion

	#region 2: equality

	// Two tuples are equal when they have the same values
	[Step(6)]
	public static void TwoTupleAreEquaWhenHaveSameValuesInSameOrder()
	{
		(string firstName, string lastName) batman = (firstName: "Bruce", lastName: "Wayne");
		(string, string) bruceWayne = ("Bruce", "Wayne");

		Assert.True(batman == bruceWayne);

		(string, string) wayneBruce = ("Wayne", "Bruce");
		Assert.False(batman == wayneBruce);

		(string firstName, string lastName) azrael = (firstName: "Jean-Paul", lastName: "Valley");
		Assert.False(batman == azrael);
	}

	// Two lists in a tuple are compared by reference
	[Step(7)]
	public static void ButListStillUsedReferenceEquality()
	{
		List<string> enemy1966 = ["Joker", "Penguin", "Riddler", "Catwoman"];
		(string firstName, string lastName, List<string> enemy) batman1966 = (firstName: "Bruce", lastName: "Wayne"
			, enemy: enemy1966);

		(string firstName, string lastName, List<string> enemy) aDud = (firstName: "Bruce", lastName: "Wayne"
			, enemy: enemy1966);

		Assert.True(batman1966 == aDud);

		(string firstName, string lastName, List<string> enemy) newBatman1966 = (firstName: "Bruce", lastName: "Wayne"
			, enemy: new List<string>() { "Joker", "Penguin", "Riddler", "Catwoman" });

		Assert.False(batman1966 == newBatman1966); //this one is tricky
	}

	#endregion

	#region 3: Usage

	// A Tuple can replace out parameter
	[Step(8)]
	public static void TupleReplaceOutParameter()
	{
        /// When your method needs to return more than one value, you have to use out parameter
        /// Now, we can use tuples
        string mainEnemy = ExtractMainEnemyWithOut("Joker,Penguin,Riddler,Catwoman", out List<string> otherEnemies);

        Assert.Equal("Joker", mainEnemy);
		Assert.Equal("Penguin,Riddler,Catwoman", string.Join(",", otherEnemies));

		(string mainEnemy, List<string> otherEnemies) extract = ExtractMainEnemyWithTuple("Joker,Penguin,Riddler,Catwoman");

		Assert.Equal("Joker", extract.mainEnemy);
		Assert.Equal("Penguin,Riddler,Catwoman", string.Join(",", extract.otherEnemies));

		// What syntax do you prefer?
	}

	private static string ExtractMainEnemyWithOut(string enemies, out List<string> othersEnemies)
	{
		string[] listEnemies = enemies.Split(",");
		string mainEnemy = listEnemies.First();
		othersEnemies = [.. listEnemies.Skip(1)];

		return mainEnemy;
	}

	private static (string mainEnemy, List<string> otherEnemies) ExtractMainEnemyWithTuple(string enemies)
	{
		string[] listEnemies = enemies.Split(",");
		string mainEnemy = listEnemies.First();
		List<string> otherEnemies = [.. listEnemies.Skip(1)];

		return (mainEnemy, otherEnemies);
	}

	// Tuple with extension can replace class 
	[Step(9)]
	public static void TupleWithExtensionCanReplaceClass()
	{
		Movie batman1966Class = new("Bruce", "Wayne");
		batman1966Class.AddMainEnemy("Joker");
		batman1966Class.AddAlso("Penguin");
		batman1966Class.AddAlso("Riddler");
		batman1966Class.AddAlso("Catwoman");
		string titleClass = batman1966Class.GetTitle();

		Assert.Equal("A movie with Bruce Wayne against Joker, Penguin, Riddler, Catwoman", titleClass);

		// You can know more on extension with koan AboutMethods
		string titleTuple = ("Bruce", "Wayne")
			.WithMainEnemy("Joker")
			.AndAlso("Penguin")
			.AndAlso("Riddler")
			.AndAlso("Catwoman")
			.GetTitle();

		Assert.Equal("A movie with Bruce Wayne against Joker, Penguin, Riddler, Catwoman", titleTuple);
		/* 
		 What's syntax do you prefer?
		If you want to know more on tuple + extension advantages, look at : https://github.com/MostlyAdequate/mostly-adequate-guide/blob/master/ch03.md
		*/
	}
	#endregion
}

class Movie(string firstName, string lastName)
{
	private readonly string firstName = firstName;
	private readonly string lastName = lastName;
	private string mainEnemy;

	private readonly List<string> enemies = [];

    public void AddMainEnemy(string name)
	{
		mainEnemy = name;
	}

	public void AddAlso(string name)
	{
		enemies.Add(name);
	}

	public string ToStringEnemies()
	{
		string result = mainEnemy;
		if (enemies.Count > 0)
		{
			result += ", " + string.Join(", ", enemies);
		}

		return result == "" ? "himself" : result; // If you don't understand, please look in AboutControlStatements.cs > TernaryOperators
	}

	public string GetTitle()
	{
		return $"A movie with {firstName} {lastName} against {ToStringEnemies()}";
	}
}


public static class MovieExtension
{
	public static string GetTitle(this (string firstName, string lastName, List<string> enemies) movie)
	{
		string strEnemies = movie.enemies.Count > 0
			? string.Join(", ", movie.enemies)
			: "himself"; // If you don't understand, please look in AboutControlStatements.cs > TernaryOperators

		return $"A movie with {movie.firstName} {movie.lastName} against {strEnemies}";
	}

	public static (string firstName, string lastName, List<string> enemies) WithMainEnemy(this (string firstName, string lastName) movie, string enemyName)
	{
		return (movie.firstName, movie.lastName, new List<string>() { enemyName });
	}

	public static (string firstName, string lastName, List<string> enemies) AndAlso(this (string firstName, string lastName, List<string> enemies) movie, string enemyName)
	{
		List<string> enemies = [.. movie.enemies, enemyName];
		return (movie.firstName, movie.lastName, enemies);
	}
}