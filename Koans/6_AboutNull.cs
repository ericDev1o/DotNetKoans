using Xunit;
using DotNetKoans.Engine;
using System;

namespace DotNetKoans.Koans;

public class AboutNull : Koan
{
	[Step(1)]
	public static void NullIsNotAnObject()
	{
		Assert.False(null is object);

		// The `is` operator returns false if the object (first parameter)
		// is null, no matter what the type (second parameter) is.
	}

	[Step(2)]
	public static void YouGetNullPointerErrorsWhenCallingMethodsOnNull()
	{
		// What is the Exception that is thrown when you call a method on a null object?
		// Don't be confused by the code below. It is using Anonymous Delegates which we will
		// cover later on. 
		object nothing = null;
		Assert.Throws<NullReferenceException>(() => nothing.ToString());

		// What's the message of the exception? What substring or pattern could you test
		// against in order to have a good idea of what the string is?
		try
		{
			nothing.ToString();
		}
		catch (Exception ex)
		{
			Assert.Contains("Object reference not set to an instance of an object", ex.Message);
		}
	}

	[Step(3)]
	public static void CheckingThatAnObjectIsNull()
	{
		object obj = null;

		Assert.Null(obj);
	}

	[Step(4)]
	public static void ABetterWayToCheckThatAnObjectIsNull()
	{
		object obj = null;

		Assert.Null(obj);
	}

	[Step(5)]
	public static void AWayNotToCheckThatAnObjectIsNull()
	{
		object obj = null;
		
		Assert.Throws<NullReferenceException>(() => obj.Equals(null));
	}
}