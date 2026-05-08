using Xunit;
using System.Collections.Generic;
using System;
using System.Collections;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutGenericContainers : Koan
{
	[Step(1)]
	public static void ArrayListSizeIsDynamic()
	{
		// When you worked with Array, the fact that Array is fixed size was glossed over.
		// The size of an array cannot be changed after you allocate it. To get around that
		// you need a class from the System.Collections namespace such as ArrayList.
		ArrayList list = [];

		Assert.Equal(0, list.Count);

		list.Add(42);
		
		Assert.Equal(1, list.Count);
	}

	[Step(2)]
	public static void ArrayListHoldsObjects()
	{
		ArrayList list = [];

		System.Reflection.MethodInfo method = list.GetType().GetMethod("Add");
		
		Assert.Equal(typeof(object), method.GetParameters()[0].ParameterType);
	}

	[Step(3)]
	public static void MustCastWhenRetrieving()
	{
		// There are a few problems with ArrayList holding object references. The first 
		// is that you must cast the items you fetch back to the original type.
		ArrayList list = [(short)42];

		short x = (short)list[0];

		Assert.Equal((short)42, x);
	}

	[Step(4)]
	public static void ArrayListIsNotStronglyTyped()
	{
		// Having to cast everywhere is tedious. But there is also another issue lurking.
		// ArrayList can hold more than one type. 
		ArrayList list = [42, "forty two"];

		Assert.Equal(42, list[0]);
		Assert.Equal("forty two", list[1]);

		// While there are a few cases where it could be nice, instead what it means is that 
		// anytime your code works with an array list you have to check that the element is 
		// of the type you expect.
	}

	[Step(5)]
	public static void Boxing()
	{
		short s = 5;

		object os = s;

		Assert.Equal(s.GetType(), os.GetType());
		Assert.Equal(s, os);

		// While it is true that everything is an object and all the above passes, not everything is quite as it seems.
		// Under the covers .NET allocates memory for all value type objects (int, double, bool,...) on the stack. This is 
		// considerably more efficient than a heap allocation. .NET also has the ability to put a value type onto the heap.
		// (for calling methods and other reasons). The process of putting stack data into the heap is called "boxing". The 
		// process of taking the value type off the heap is called "unboxing". We won't go into the details (see Jeffrey 
		// Richter's book if you want details). This subject comes up because every time you put a value type into an 
		// ArrayList it must be boxed. Every time you read it from the ArrayList it must be unboxed. This can be a significant
		// cost.
	}

	[Step(6)]
	public static void ABetterDynamicSizeContainer()
	{
		// ArrayList is a .NET 1.0 container. With .NET 2.0 generics were introduced and with it a new set of collections in
		// System.Collections.Generic The array like container is List<T>. List<T> (read "list of T") is a generic class. 
		// The "T" in the definition of List<T> is the type argument. You cannot declare an instance of List<T> without also
		// supplying a type in place of T.
		List<short> list = [];

		Assert.Equal(0, list.Count);

		list.Add(42);

		Assert.Equal(1, list.Count);

		// Now just like short[], you can have a type safe dynamic sized container
		// list.Add("forty two"); // <--Unlike ArrayList this is illegal.

		// List<T> also solves the boxing/unboxing issues of ArrayList. Unfortunately, you'll have to take Microsoft's word for it
		// as I can't find a way to prove it without some ugly MSIL beyond the scope of these Koans.
	}

	public class Widget
	{
	}

	[Step(7)]
	public static void ListWorksWithAnyType()
	{
		// Just as with Array, list will work with any type
		List<Widget> list = [new Widget()];

		Assert.Equal(1, list.Count);
	}

	[Step(8)]
	public static void InitializingWithValues()
	{
		// Like array you can create a list with an initial set of values easily
		List<short> list = [1, 2, 3];

		Assert.Equal(3, list.Count);
	}

	[Step(9)]
	public static void AddMultipleItems()
	{
		//You can add multiple items to a list at once
		List<short> list = [];

		list.AddRange([1, 2, 3]);

		Assert.Equal(3, list.Count);
	}

	[Step(10)]
	public static void RandomAccess()
	{
		// Just as with array, you can use the subscript notation to access any element in a list.
		List<short> list = [5, 6, 7];

		Assert.Equal((short)7, list[2]);
	}

	[Step(11)]
	public static void BeyondTheLimits()
	{
		List<short> list = [1, 2, 3];
		// You cannot attempt to get data that doesn't exist

		Assert.Throws<ArgumentOutOfRangeException>(() => list[3]);
	}

	[Step(12)]
	public static void ConvertingToFixedSize()
	{
		List<short> list = [1, 2, 3];

		Assert.Equal([(short)1, (short)2, (short)3], [.. list]); // [.. list] equals list.ToArray()
	}

	[Step(13)]
	public static void InsertingInTheMiddle()
	{
		List<short> list = [1, 2, 3];

		list.Insert(1, 6);

		Assert.Equal([(short)1, (short)6, (short)2, (short)3], [.. list]);
	}

	[Step(14)]
	public static void RemovingItems()
	{
		List<short> list = [2, 1, 2, 3];

		list.Remove(2);

		Assert.Equal([(short)1, (short)2, (short)3], [.. list]);
	}

	[Step(15)]
	public static void StackPushPop()
	{
		Stack<short> stack = new();

		Assert.Equal(0, stack.Count);

		stack.Push(42);

		Assert.Equal(1, stack.Count);

		short x = stack.Pop();

		Assert.Equal((short)42, x);

		Assert.Equal(0, stack.Count);
	}

	[Step(16)]
	public static void StackOrder()
	{
		Stack<short> stack = new();

		stack.Push(1);
		stack.Push(2);
		stack.Push(3);

		Assert.Equal([(short)3, (short)2, (short)1], [.. stack]);
	}

	[Step(17)]
	public static void PeekingIntoAQueue()
	{
		Queue<string> queue = new();
		queue.Enqueue("one");

		Assert.Equal("one", queue.Peek());
		Assert.Equal(1, queue.Count);

		queue.Enqueue("two");

		Assert.Equal("one", queue.Peek());
		Assert.Equal(2, queue.Count);
	}

	[Step(18)]
	public static void RemovingItemsFromTheQueue()
	{
		Queue<string> queue = new();
		queue.Enqueue("one");
		queue.Enqueue("two");

		Assert.Equal(2, queue.Count);
		Assert.Equal("one", queue.Dequeue());
		Assert.Equal(1, queue.Count);
	}

	[Step(19)]
	public static void AddingToADictionary()
	{
		// Dictionary<TKey, TValue> is .NET's key value store. The key and the value do not need to be the same types.
		Dictionary<short, string> dictionary = [];

		Assert.Equal(0, dictionary.Count);

		dictionary[1] = "one";
		
		Assert.Equal(1, dictionary.Count);
	}

	[Step(20)]
	public static void AccessingData()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno",
            ["two"] = "dos"
        };
		
		// The most common way to locate data is with the subscript notation.
		Assert.Equal("uno", dictionary["one"]);
		Assert.Equal("dos", dictionary["two"]);
	}

	[Step(21)]
	public static void AccessingDataNotAdded()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno"
        };

		Assert.Throws<KeyNotFoundException>(() => dictionary["two"]);
	}

	[Step(22)]
	public static void CatchingMissingData()
	{
		// To deal with the throw when data is not there, you could wrap the data access in a try/catch block...
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno"
        };
        string result;
		
        try
		{
			result = dictionary["two"];
		}
		catch (Exception)
        {
			result = "dos";
		}

		Assert.Equal("dos", result);
	}

	[Step(23)]
	public static void PreCheckForMissingData()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno"
        };

        if (!dictionary.TryGetValue("two", out string result))
        {
            result = "dos";
        }

        Assert.Equal("dos", result);
	}

	[Step(24)]
	public static void TryGetValueForMissingData()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno"
        };

        if (!dictionary.TryGetValue("two", out string result))
        {
            result = "dos";
        }

        Assert.Equal("dos", result);
	}

	[Step(25)]
	public static void InitializingADictionary()
	{
		// Although it is not common, you can initialize a dictionary...
		Dictionary<string, string> dictionary = new() 
		{ 
			{ "one", "uno" }, 
			{ "two", "dos" } 
		};

		Assert.Equal("uno", dictionary["one"]);
		Assert.Equal("dos", dictionary["two"]);
	}

	[Step(26)]
	public static void ModifyingData()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno",
            ["two"] = "dos",
            ["one"] = "ein"
        };

		Assert.Equal("ein", dictionary["one"]);
	}

	[Step(27)]
	public static void KeyExists()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno"
        };

		Assert.True(dictionary.ContainsKey("one"));
		Assert.False(dictionary.ContainsKey("two"));
	}

	[Step(28)]
	public static void ValueExists()
	{
		Dictionary<string, string> dictionary = new()
        {
            ["one"] = "uno"
        };

		Assert.True(dictionary.ContainsValue("uno"));
		Assert.False(dictionary.ContainsValue("dos"));
	}

	[Step(29)]
	public static void AddingDataViaSubscript()
	{
		// The Dictionary also has some smarts built-in... You can use
		// the subscript operator, [], to add data to it. Consider
		// carefully the foreach loop below.
		Dictionary<string, short> one = new()
        {
            ["jim"] = 53,
            ["amy"] = 20,
            ["dan"] = 23
        };
		Dictionary<string, short> two = new()
        {
            ["jim"] = 54,
            ["jenny"] = 26
        };

		foreach (KeyValuePair<string, short> item in two)
		{
			one[item.Key] = item.Value;
		}

		Assert.Equal((short)54, one["jim"]);
		Assert.Equal((short)26, one["jenny"]);
		Assert.Equal((short)20, one["amy"]);
	}
}