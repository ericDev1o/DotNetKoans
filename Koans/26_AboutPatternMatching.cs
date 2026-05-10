using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

class AboutPatternMatching : Koan
{
	#region 1: Syntax

	#region 1.1: Test object type
	// Pattern matching can test an object's type
	[Step(1)]
	public static void PatternMatchingTestObjectType()
	{
		Hero hero = new Superman();

		string message = GetHeroHelloMessageWithIf(hero);
		Assert.Equal("I'm Superman", message);


		string message2 = GetHeroHelloMessageWithCase(hero);
		Assert.Equal("I'm Superman", message2);
	}

	private static string GetHeroHelloMessageWithIf(Hero hero)
	{
		if (hero is Superman)
		{
			return "I'm Superman";
		}
		else if (hero is Batman)
		{
			return "I'm the Dark Knight, you know me as Batman";
		}
		else
		{
			return "Nobody knows me :'(";
		}
	}

	private static string GetHeroHelloMessageWithCase(Hero hero)
	{
        return hero switch
        {
            Batman => "I'm the Dark Knight, you know me as Batman",
            Superman => "I'm Superman",
            _ => "Nobody knows me :'(",
        };
    }
	#endregion

	#region 1.2: Cast object
	// Pattern matching can cast object
	[Step(2)]
	public static void PatternMatchingCastObject()
	{
		Hero hero = new Batman();

		string[] gadgets = GetGadgetsWithIf(hero);
		Assert.Equal("Batarang,Batgyro,Batsuit,Batmobile,Belt", string.Join(",", gadgets));

		string[] gadgets2 = GetGadgetsWithCase(hero);
		Assert.Equal("Batarang,Batgyro,Batsuit,Batmobile,Belt", string.Join(",", gadgets2));
	}

	private static string[] GetGadgetsWithIf(Hero hero)
	{
        // return null if hero is not a batman
        if (hero is Batman batman)
        {
            return batman.gadget; // gadget is not in hero class but in batman
        }

        else return [];
	}

	private static string[] GetGadgetsWithCase(Hero hero)
	{
        return hero switch
        {
            Batman batman => batman.gadget,// gadget is not in hero class but in batman
            _ => [],
        };
    }
	#endregion

	// Pattern matching case with sugar syntax
	[Step(3)]
	public static void PatternMatchingCaseSugarSyntax()
	{
		/// In the two previous examples, each switch case always returns a value.
		/// There is a sugar syntax for that
		/// Let's refactor the second one.
		Hero hero = new Batman();

		string[] gadgets = hero switch
		{
			Batman batman => batman.gadget,
			_ => [] // default case
		};

		Assert.Equal("Batarang,Batgyro,Batsuit,Batmobile,Belt", string.Join(",", gadgets));
	}

	#endregion

	#region 2: Special case

	/// Pattern Matching let you make choices based on object/tuple properties.
	/// It's called Special Case.

	// Special case with when
	[Step(4)]
	public static void SpecialCaseWithWhenClause()
	{
		Hero hero = new Batman();
		hero.ReplaceBy("Jean-Paul", "Valley");
        string message = hero switch
		{
			Batman batman when batman.LastName == "Wayne" => "Sure, you are Batman", // Special case
            Batman => "You look like batman, but I don't think you are",
			_ => "I don't know you" // default case
		};

		Assert.Equal("You look like batman, but I don't think you are", message);
	}

	// Special case with destructuring on tuples
	[Step(5)]
	public static void SpecialCaseWithDestructuringTuple()
	{
		(string, string, string) hero = ("Batman", "Valley", "Jean-Paul");

		string message = hero switch
		{
			("Batman", "Wayne", _) => "Sure, you are Batman",
			("Batman", _, _) => "You look like Batman, but I don't think you are",
			_ => "I don't know you" // default case
		};

		Assert.Equal("You look like Batman, but I don't think you are", message);
	}

	// Special case with destructuring on object
	[Step(6)]
	public static void SpecialCaseWithDestructuringObject()
	{
		Hero hero = new Batman();

		string message = hero switch
		{
			{ LastName: "Wayne" } => "Sure, you're Batman",
			Batman => "You look like Batman, but I don't think you are",
			_ => "I don't know you" // default case
		};

		Assert.Equal("Sure, you're Batman", message);
	}

	#endregion

	#region 3: some warning

	// Evaluation order in pattern matching
	[Step(7)]
	public static void PatternMatchingOrder()
	{
		/// Pattern matching is evaluated from top to bottom
		Hero hero = new Batman();

		string message = hero switch
		{
			Batman batman when batman.LastName != "Wayne" => "You look like Batman, but I don't think you are",
			{ LastName: "Wayne" } => "Sure, you're Batman",
			_ => "I don't know you" // default case
		};

		Assert.Equal("Sure, you're Batman", message);
	}


	// Pattern matching with null values
	[Step(8)]
	public static void PatternMatchingWithNull()
	{
		// Pattern matching doesn't throw NullReferenceException
		Hero hero = null;

		string message = hero switch
		{
			Batman batman when batman.LastName != "Wayne" => "You look like Batman, but I don't think you are",
			Batman => "Sure, you are Batman",
			_ => "I don't know you" // default case
		};

		Assert.Equal("I don't know you", message);
	}
	#endregion
}

class Hero(string firstName, string lastName)
{
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;

    public void ReplaceBy(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}
}

class Batman : Hero
{
	public string[] gadget =
    [
        "Batarang", 
		"Batgyro", 
		"Batsuit", 
		"Batmobile", 
		"Belt"
	];

	public Batman() : base("Bruce", "Wayne")
	{ }
}

class Superman : Hero
{
	public Superman() : base("Clark Joseph", "Kent")
	{ }
}