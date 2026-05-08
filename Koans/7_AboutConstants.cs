using DotNetKoans.Engine;
using Xunit;

namespace DotNetKoans.Koans;

public class AboutConstants : Koan
{
	[Step(1)]
	public static void ConstantsMustBeInitalizedAsDeclared()
	{
		const short Months = 12;

		Assert.Equal(12, Months);
	}

	[Step(2)]
	public static void ConstantsCannotBeChanged()
	{
		// Since C# inserts literal values into compiled
		// code, you will not achieve zen when attempting
		// to change them after definition.
		const short Days = 365;
		// Days = Days + 1; // Compilation error

		Assert.Equal(365, Days);
	}

	private static short Days;

	public AboutConstants() { Days = 365; }

	[Step(3)]
	public static void ReadonlyValuesCanBeAssignedOnlyOnce()
	{
		// Days += 1; // Compilation error

		Assert.Equal(365, Days);
	}

	[Step(4)]
	public static void ConstantsOfTheSameTypeCanBeDeclaredAtTheSameTime()
	{
		// You can achieve zen (and save keystrokes) by defining
		// constants of the same type as one.
		const short Months = 12, Weeks = 52, Days = 365;

		Assert.Equal(typeof(short), Months.GetType());
		Assert.Equal(typeof(short), Weeks.GetType());
		Assert.Equal(typeof(short), Days.GetType());

		Assert.Equal(12, Months);
		Assert.Equal(52, Weeks);
		Assert.Equal(365, Days);
	}

	[Step(5)]
	public static void ConstantsCanBeUsedInExpressionsToInitializeOtherConstants()
	{
		const short Months = 12;
		const short Weeks = 52;
		const short Days = 365;

		const double DaysPerWeek = Days / Weeks;
		const double DaysPerMonth = Days / Months;

		Assert.Equal(7d, DaysPerWeek);
		Assert.Equal(30,5d, DaysPerMonth);

		// Constants can be used in arithmetic to set other constant values.
		// They can also initialize each other.
	}

	class Animal
	{
		public const short Legs = 4;

		public static short LegsInAnimal()
		{
			return Legs;
		}

		public class NestedAnimal
		{
			public static short LegsInNestedAnimal()
			{
				return Legs;
			}
		}
	}

	[Step(6)]
	public static void NestedClassesInheritConstantsFromEnclosingClasses()
	{
		Assert.Equal(4, Animal.NestedAnimal.LegsInNestedAnimal());

		// Nested classes have access to their parent's scope.
		// This includes private or static or constant members.
		// But nested classes don't inherit their parent type.
	}

	class Reptile : Animal
	{
		public static short LegsInReptile()
		{
			return Legs;
		}
	}

	[Step(7)]
	public static void SubclassesInheritConstantsFromParentClasses()
	{
		// If a Reptile is an Animal, zen is achieved
		// when you realize they too will have legs.
		Assert.Equal(4, Reptile.LegsInReptile());
	}

	class MyAnimals
	{
		public const short Legs = 2;

		public class Bird : Animal
		{
			public static short LegsInBird()
			{
				return Legs;
			}
		}
	}

	[Step(8)]
	public static void WhoWinsWithBothNestedAndInheritedConstants()
	{
		Assert.Equal(4, MyAnimals.Bird.LegsInBird());

		// The constant from the inheritance hierarchy
		// has precedence over 
		// the constant in the lexical scope.
	}
}