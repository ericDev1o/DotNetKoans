using Xunit;
using System.Collections.Generic;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutIteration : Koan
{
	// We can use several C# constructs to iterate over items in a collection

	[Step(1)]
	public static void ForLoop()
	{
		// Let's make a list with some numbers
		List<short> numbers =
        [
            42,
			68,
			12
		];

		short sum = 0;
		// A for loop has three parts: something to run before the loop starts for the first time,
		// a condition that will decide whether to keep iterating, and something to do after each iteration

		for (short i = 0; i < numbers.Count; i++)
		{
			sum += numbers[i];
		}

		Assert.Equal(122, sum);
	}


	[Step(2)]
	public static void ForBreak()
	{
		// We can interrupt a for loop with "break;"

		string[] animals = ["Cats", "Dogs", "Sharks"];
		string lastAnimal = "";

		for (short i = 0; i < animals.Length; i++)
		{
			lastAnimal = animals[i];
			if (animals[i] == "Dogs")
			{
				break;
			}
		}

		Assert.Equal("Dogs", lastAnimal);
	}

	[Step(3)]
	public static void ForContinue()
	{
		// We can ignore the rest of the current iteration using "continue;"

		List<string> colors = ["Blue", "Red", "Pink", "Green"];
		List<string> new_colors = [];

        for (short i = 0; i < colors.Count; i++)
		{
			if (colors[i] == "Blue")
			{
				continue;
			}
			new_colors.Add(colors[i]);
		}

		Assert.Equal("Red", new_colors[0]);
	}


	[Step(4)]
	public static void WhileLoop()
	{
		// This loop is sort of like the for loop, but only requires the middle part
		List<short> numbers = 
		[
			42,
			68,
			12
		];
		short sum = 0;

		// A while loop will keep repeating until the condition at the start is false.
		// So we need to initialize any variables the loop needs before it, and to change those variables inside the loop itself.
		// Let's do it backwards, just for fun.

		short i = 2;
		while (i >= 0)
		{
			sum += numbers[i];
			i--;
		}

		Assert.Equal(122, sum);
	}


	[Step(5)]
	public static void ForeachLoop()
	{
		// What if we had a way to iterate over any sort of collection that does not require us
		// to have to deal with an index and risk making a mistake that makes our program crash?

		// A foreach loop will iterate through a collection all by itself, assigning the current iteration's value to a variable.
		// No more dealing with index variables.

		List<string> sharkSpecies = [
			"Great white shark",
			"Tiger shark",
			"Whale shark",
			"Leopard shark"
		];
		string lastShark = "";

		foreach (string shark in sharkSpecies)
		{
			lastShark = shark;
		}

		// Best for last
		Assert.Equal("Leopard shark", lastShark);
	}
}