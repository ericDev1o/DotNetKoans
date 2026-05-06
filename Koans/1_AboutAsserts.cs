using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutAsserts : Koan
{
	// We shall contemplate truth by testing reality, via asserts.
	[Step(1)]
	public void AssertTruth()
	{
		Assert.True(true && "true" == "true" && 1 + 1 == 2);
	}

	// Enlightenment may be more easily achieved with appropriate messages
	[Step(2)]
	public void AssertTruthWithMessage()
	{
		Assert.True(true && "true" == "true" && 1 + 1 == 2, "This is true");
	}

	// To understand reality, we must compare our expectations against reality
	[Step(3)]
	public void AssertEquality()
	{
		int expectedValue = 3;
		int actualValue = 1 + 1 + 1;
		Assert.True(expectedValue == actualValue);
	}

	// Some ways of asserting equality are better than others
	[Step(4)]
	public void ABetterWayOfAssertingEquality()
	{
		int expectedValue = 3;
		int actualValue = 1 + 1 + 1;
		Assert.Equal(expectedValue, actualValue);
	}

	// Sometimes we will ask you to fill in the values
	[Step(5)]
	public void FillInValues()
	{
		Assert.Equal(2, 1 + 1);
	}
}