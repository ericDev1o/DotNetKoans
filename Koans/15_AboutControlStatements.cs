using Xunit;
using System.Collections.Generic;
using System;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutControlStatements : Koan
{
	[Step(1)]
	public static void IfThenElseStatementsWithBrackets()
	{
		bool b;
		if (true)
		{
			b = true;
		}
		else
		{
			b = false;
		}

		Assert.True(b);
	}

	[Step(2)]
	public static void IfThenElseStatementsWithoutBrackets()
	{
		bool b;
		if (true)
			b = true;
		else
			b = false;

		Assert.True(b);

	}

	[Step(3)]
	public static void IfThenStatementsWithBrackets()
	{
        bool b;
        if (true)
		{
			b = true;
		}

		Assert.True(b);
	}

	[Step(4)]
	public static void IfThenStatementsWithoutBrackets()
	{
        bool b;
        if (true)
			b = true;

		Assert.True(b);
	}

	[Step(5)]
	public static void WhyItsWiseToAlwaysUseBrackets()
	{
		bool b1 = false;
        short counter = 1;

		if (counter == 0)
			b1 = true;
        bool b2 = true;

        Assert.False(b1);
		Assert.True(b2);
	}

	[Step(6)]
	public static void TernaryOperators()
	{
		Assert.Equal(1, true ? 1 : 0);
		Assert.Equal(0, false ? 1 : 0);
	}

	// This is out of place for control statements, but necessary for Koan 8
	[Step(7)]
	public static void NullableTypes()
	{
		short i = 0;
		//i = null; // You can't do this.
		short j; // It's not advised to do that.

		short? nullableShort = null; // but you can do this
		//Assert.NotNull(i); // Don"t assert for Null reference on value types.
		//Assert.Null(j);
		Assert.Equal(0, i);
		Assert.Null(nullableShort);
	}

	[Step(8)]
	public static void AssignIfNullOperator()
	{
		short? nullableShort = null;

		short x = nullableShort ?? 42;

		Assert.Equal(42, x);
	}

	[Step(9)]
	public void IsOperators()
	{
		bool isKoan = false;
		bool isAboutControlStatements = false;
		bool isAboutMethods = false;

		var myType = this;

		if (myType is not null) {
			if (myType is Koan)
				isKoan = true;

			if (myType is AboutControlStatements)
				isAboutControlStatements = true;

			if (myType is AboutMethods)
				isAboutMethods = true;
		}

		Assert.True(isKoan);
		Assert.True(isAboutControlStatements);
		Assert.False(isAboutMethods);
	}

	[Step(10)]
	public static void WhileStatement()
	{
		short i = 1;
		short result = 1;
		
		while (i <= 3)
		{
			result += i;
			i += 1;
		}

		Assert.Equal(7, result);
	}

	[Step(11)]
	public static void BreakStatement()
	{
		short i = 1;
		short result = 1;

		while (true)
		{
			if (i > 3) 
				break;
			result += i;
			i += 1;
		}

		Assert.Equal(7, result);
	}

	[Step(12)]
	public static void ContinueStatement()
	{
		short i = 0;
		var result = new List<short>();

		while (i < 10)
		{
			i += 1;
			if ((i % 2) == 0) { continue; }
			result.Add(i);
		}
		Assert.Equal([1, 3, 5, 7, 9], result);
	}

	[Step(13)]
	public static void ForStatement()
	{
		var list = new List<string> { "fish", "and", "chips" };

		for (short i = 0; i < list.Count; i++)
		{
			list[i] = list[i].ToUpper();
		}
		
		Assert.Equal(["FISH", "AND", "CHIPS"], list);
	}

	[Step(14)]
	public static void ForEachStatement()
	{
		var list = new List<string> { "fish", "and", "chips" };
		var finalList = new List<string>();

		foreach (string item in list)
		{
			finalList.Add(item.ToUpper());
		}

		Assert.Equal(["fish", "and", "chips"], list);
		Assert.Equal(["FISH", "AND", "CHIPS"], finalList);
	}

	[Step(15)]
	public static void ModifyingACollectionDuringForEach()
	{
		var list = new List<string> { "fish", "and", "chips" };

		try
		{
			foreach (string item in list)
			{
				list.Add(item.ToUpper());
			}
		}
		catch (Exception ex)
		{
			Assert.Equal(typeof(InvalidOperationException), ex.GetType());
		}
	}

	[Step(16)]
	public static void CatchingModificationExceptions()
	{
		string whoCaughtTheException = "No one";

		var list = new List<string> { "fish", "and", "chips" };
		try
		{
			foreach (string item in list)
			{
				try
				{
					list.Add(item.ToUpper());
				}
				catch
				{
					whoCaughtTheException = "When we tried to Add it";
				}
			}
		}
		catch
		{
			whoCaughtTheException = "When we tried to move to the next item in the list";
		}

		Assert.Equal("When we tried to move to the next item in the list", whoCaughtTheException);
	}
}