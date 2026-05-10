using Xunit;
using System;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

class AboutDestructuring : Koan
{
	/// Destructuring is a language feature that lets you extract a property inside a data structure

	#region 1: Destructuring with tuples

	// Tuples can be destructured
	[Step(1)]
	public static void TupleCanBeDestructured()
	{
		// If you don't know tuples, look at AboutTuples.cs
		Tuple<string, string> batman = new("Bruce", "Wayne");

		(string firstName, string lastName) = batman; // This is destructuring

		Assert.Equal("Bruce", firstName);
		Assert.Equal("Wayne", lastName);
	}

	// you can avoid destructuring a property
	[Step(2)]
	public static void AvoidDestructuringAProperty()
	{
		// Use _ when you don't need to extract a property
		Tuple<string, string> batman = new("Bruce", "Wayne");

		(_, string lastName) = batman;

		Assert.Equal("Wayne", lastName);
	}


	#endregion

	#region 2: Destructuring with object

	// Object can be destructured
	[Step(3)]
	public static void ObjectCanBeDestructured()
	{
		Batman batman = new("Bruce", "Wayne");

		(string firstName, string lastName) = batman; // uses Deconstruct(out string firstName, out string lastName)

		Assert.Equal("Bruce", firstName);
		Assert.Equal("Wayne", lastName);
	}


	// you can avoid destructuring a property
	[Step(4)]
	public static void ObjectAvoidDestructuringAProperty()
	{
		// Use _ when you don't need to extract a property
		Batman batman = new("Bruce", "Wayne");

		(_, string lastName) = batman; // uses Deconstruct(out string firstName, out string lastName)

		Assert.Equal("Wayne", lastName);
	}


	// You can "configure" object destructuring
	[Step(5)]
	public static void ObjectDestructuringCanBeConfigured()
	{
		// Use _ when you don't need to extract a property
		Batman batman = new("Bruce", "Wayne");
        (_, _, string heroName) = batman; // uses Deconstruct(out string firstName, out string lastName, out string heroName)

		Assert.Equal("Batman", heroName);

		// Do you think it is a good practice ?
	}

	class Batman(string firstName, string lastName)
    {
		private readonly string firstName = firstName;
		private readonly string lastName = lastName;

        public void Deconstruct(out string firstName, out string lastName)
		{
			firstName = this.firstName;
			lastName = this.lastName;
		}

		public void Deconstruct(out string firstName, out string lastName, out string heroName)
		{
			Deconstruct(out firstName, out lastName);
			heroName = "Batman";
		}
	}
	#endregion
}