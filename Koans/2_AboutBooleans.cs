using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutBooleans : Koan
{
	// The bool type represents boolean logical quantities.
	// The only possible values of bool are true and false.
	// No standard conversions exists between bool and other types.
	// bool is a simple type and is a Alias of System.Boolean, these
	// can be used interchangeably.

	[Step(1)]
	public static void TrueIsTreatedAsTrue()
	{
		// true is true
		Assert.True(true);
	}

	[Step(2)]
	public static void FalseIsTreatedAsFalse()
	{
		// false is false
		Assert.False(false);
	}

	[Step(3)]
	public static void TrueIsNotFalse()
	{
		// true is not false
		Assert.True( ! false);
	}

	[Step(4)]
	public static void BoolIsAReservedWordOfSystemBoolean()
	{
		// bool is a Alias of System.Boolean
		Assert.Equal(typeof(System.Boolean), typeof(bool));
	}

	[Step(5)]
	public static void NoOtherTypeConvertsToBool()
	{
		object[] otherTypes = new object[]
		{
			"not a bool",
			1, 0,
			null,
			new object[0]
		};

		foreach (object otherType in otherTypes)
		{
			Assert.False(otherType is bool); // no other type can cast to bool
		}
	}
}