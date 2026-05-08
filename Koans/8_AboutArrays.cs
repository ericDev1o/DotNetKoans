using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutArrays : Koan
{
	[Step(1)]
	public static void CreatingArrays()
	{
		object[] empty_array = [];
		Assert.Equal(typeof(object[]), empty_array.GetType());

		// Note that you have to explicitly check for subclasses
		Assert.True(typeof(Array).IsAssignableFrom(empty_array.GetType()));

		Assert.Equal(0, empty_array.Length);
	}

	[Step(2)]
	public static void ArrayLiterals()
	{
		// You don't have to specify a type if the arguments can be inferred
		var array = new[] { 42 };
		Assert.Equal(typeof(int[]), array.GetType());
		Assert.Equal([42], array);

		// Are arrays 0-based or 1-based?
		Assert.Equal(42, array[0]);

		// This is important because...
		Assert.True(array.IsFixedSize);

		// ...it means we can't do this: array[1] = 13;
		Assert.Throws<IndexOutOfRangeException>(() => array[1] = 13);

		// This is because the array is fixed at length 1. You could write a function
		// which created a new array bigger than the last, copied the elements over, and
		// returned the new array. Or you could do this:
		List<int> dynamicArray = [42];
		Assert.Equal(array, dynamicArray.ToArray());

		dynamicArray.Add(13);
		Assert.Equal([42, 13], [.. dynamicArray]); // [.. dynamicArray] equals dynamicArray.ToArray()
	}

	[Step(3)]
	public static void AccessingArrayElements()
	{
		string[] array = ["peanut", "butter", "and", "jelly"];

		Assert.Equal("peanut", array[0]);
		Assert.Equal("jelly", array[3]);

		// This doesn't work: Assert.Equal("jelly", array[-1]);
	}

	[Step(4)]
	public static void SlicingArrays()
	{
		string[] array = ["peanut", "butter", "and", "jelly"];

		Assert.Equal(["peanut", "butter"], array.Take(2).ToArray());
		Assert.Equal(["butter", "and"], [.. array.Skip(1).Take(2)]);
	}

	[Step(5)]
	public static void PushingAndPopping()
	{
		short[] array = [1, 2];
		Stack stack = new(array);
		stack.Push("last");
		Assert.Equal(["last", (short)2, (short)1], stack.ToArray());
		var poppedValue = stack.Pop();
		Assert.Equal("last", poppedValue);
		Assert.Equal([(short)2, (short)1], stack.ToArray());
	}

	[Step(6)]
	public static void Shifting()
	{
		// Shift == Remove First Element
		// Unshift == Insert Element at Beginning
		// C# doesn't provide this natively. You have a couple
		// of options, but we'll use the LinkedList<T> to implement
		string[] array = ["Hello", "World"];
		LinkedList<string> list = new(array);

		list.AddFirst("Say");
		Assert.Equal(["Say", "Hello", "World"], [.. list]);

		list.RemoveLast();
		Assert.Equal(["Say", "Hello"], [.. list]);

		list.RemoveFirst();
		Assert.Equal(["Hello"], [.. list]);

		list.AddAfter(list.Find("Hello"), "World");
		Assert.Equal(["Hello", "World"], [.. list]);
	}

}