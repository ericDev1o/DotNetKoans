using Xunit;
using System.Reflection;
using System;
using System.Text;
using System.Linq;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

public class AboutDelegates : Koan
{
	// A delegate is a user defined type just like a class. 
	// A delegate lets you reference methods with the same signature and return type.
	// Once you have the reference to the method, pass them as parameters or call it via the delegate.
	// In other languages this is known as functions as first class citizens.

	// Here is a delegate declaration
	delegate int BinaryOp(int lhs, int rhs);

	/**
	 * Here int is used instead of memory-lighter short due to
	 * implicit conversion to int during math requiring for example a (short)(lhs + rhs).
	 * 2 additional casts short -> int -> short only to process simple math is an overhead.
	 * The process time gain isn't significant in most usual cases.
	 * Moreover it makes the code more complex than needed. It's overkill here.
	 *
	 * Be aware to adapt to the context.
	 * If you do data-heavy processing, using short may be far more efficient.
	 * Those use cases include:
	 *    image processing,
	 *    audio,
	 *    video games,
	 *    matrix calculations,
	 *    AI,
	 *    network buffering,
	 *    Big data.
	 * In those cases a CPU cache gain coming with 2 times less memory usage can be crucial for performance.
	 */
	private class MyMath
	{
		// Add has the same signature as BinaryOp
		public static int Add(int lhs, int rhs)
		{
			return lhs + rhs;
		}
		public static int Subtract(int lhs, int rhs)
		{
			return lhs - rhs;
		}
	}
	[Step(1)]
	public static void DelegatesAreReferenceTypes()
	{
		// If you don't initialize a delegate it will be a null value, just as any other reference type.
		// Be aware that
		// BinaryOp op;
		// doesn't default to null, even if a delegate is a reference type.
		// This makes it different from properties or class fields like
		// private BinaryOp op; which is automatically null.
		BinaryOp op = default;

		Assert.Null(op);
	}
	[Step(2)]
	public static void DelegatesCanBeInstantiated()
	{
		BinaryOp op = new(MyMath.Add);

		Assert.Equal("Add", op.GetMethodInfo().Name);
	}
	[Step(3)]
	public static void DelegatesCanBeAssigned()
	{
		BinaryOp op = MyMath.Subtract;

		Assert.Equal("Subtract", op.GetMethodInfo().Name);
	}
	[Step(4)]
	public static void MethodsCalledViaDelegate()
	{
		BinaryOp op = MyMath.Add;

		Assert.Equal(6, op(3, 3));
	}
	private static void PassMeTheDelegate(BinaryOp passed)
	{
		Assert.Equal(6, passed(3, 3));
	}
	[Step(5)]
	public static void DelegatesCanBePassed()
	{
		BinaryOp op = MyMath.Add;

		PassMeTheDelegate(op);
	}
	[Step(6)]
	public static void MethodCanBePassedDirectly()
	{
		PassMeTheDelegate(MyMath.Add);
	}
	[Step(7)]
	public static void DelegatesAreImmutable()
	{
		// Like strings it looks like you can change what a delegate references, but really they are immutable objects
		BinaryOp a = MyMath.Add;
		BinaryOp original = a;

		Assert.Same(a, original);

		a = MyMath.Subtract;
		// a is now a different instance

		Assert.NotSame(a, original);
	}
	delegate int Curry(int val);
	public static class FunctionalTricks
	{
		public static int Add5(int x)
		{
			return x + 5;
		}
		public static int Add10(int x)
		{
			return x + 10;
		}
	}
	[Step(8)]
	public static void DelegatesHaveAnInvocationList()
	{
		Curry adding = FunctionalTricks.Add5;

		// So far we've only seen one method attached to a delegate. 
		Assert.Equal(1, adding.GetInvocationList().Length);

		// However, you can attach multiple methods to a delegate 
		adding += FunctionalTricks.Add10;

		Assert.Equal(2, adding.GetInvocationList().Length);
	}
	[Step(9)]
	public static void OnlyLastResultReturned()
	{
		Curry adding = FunctionalTricks.Add5;
		adding += FunctionalTricks.Add10;
		// Delegates may have more than one method attached, but only the result of the last method is returned.

		Assert.Equal(15, adding(5));
	}
	[Step(10)]
	public static void RemovingMethods()
	{
		Curry adding = FunctionalTricks.Add5;
		adding += FunctionalTricks.Add10;

		Assert.Equal(2, adding.GetInvocationList().Length);

		adding -= FunctionalTricks.Add5;
		
		Assert.Equal(1, adding.GetInvocationList().Length);
		Assert.Equal("Add10", adding.GetMethodInfo().Name);
	}

	private static void AssertIntEqualsFortyTwo(int x)
	{
		Assert.Equal(42, x);
	}
	private static void AssertStringEqualsFortyTwo(string s)
	{
		Assert.Equal("42", s);
	}
	private static void AssertAddEqualsFortyTwo(int x, string s)
	{
        if(int.TryParse(s, out int y))
			Assert.Equal(42, x + y);
		else
			throw new ArgumentException("s must contain a valid integer.", nameof(s));
	}
	[Step(11)]
	public static void BuiltInActionDelegateTakesInt()
	{
		// With the release of generics in .NET 2.0 we got some delegates which will cover most of our needs. 
		// You will see them in the base class libraries, so knowing about them will be helpful. 
		// The first is Action<>. Action<> can take a variety of parameters and has a void return type.
		// public delegate void Action<T>(T obj);
		Action<int> i = AssertIntEqualsFortyTwo;

		i(42);
	}
	[Step(12)]
	public static void BuiltInActionDelegateTakesString()
	{
		// Because the delegate is a template, it also works with any other type. 
		Action<string> s = AssertStringEqualsFortyTwo;
		
		s("42");
	}
	[Step(13)]
	public static void BuiltInActionDelegateIsOverloaded()
	{
		// Action is an overloaded delegate so it can take more than one parameter
		Action<int, string> a = AssertAddEqualsFortyTwo;

		a(12, "30");
	}
	public class Seen
	{
		private static readonly StringBuilder _letters = new();
		public static string Letters
		{
			get { return _letters.ToString(); }
		}
		public static void Look(char letter)
		{
			_letters.Append(letter);
		}
	}
	[Step(14)]
	public static void ActionInTheBcl()
	{
		// You will find Action used within the BCL, often when iterating over a container
		string greeting = "Hello world";

		Array.ForEach(greeting.ToCharArray(), Seen.Look);

		Assert.Equal("Hello world", Seen.Letters);
	}

	private static bool IntEqualsFortyTwo(int x)
	{
		return 42 == x;
	}
	private static bool StringEqualsFortyTwo(string s)
	{
		return "42" == s;
	}
	[Step(15)]
	public static void BuiltInPredicateDelegateIntSatisfied()
	{
		// The Predicate<T> delegate 
		// public delegate bool Predicate<T>(T obj);
		// Predicate allows you to codify a condition and pass it around. 
		// You use it to determine if an object satisfies some criteria. 

		Predicate<int> i = IntEqualsFortyTwo;

		Assert.True(i(42));
	}
	[Step(16)]
	public static void BuiltInPredicateDelegateStringSatisfied()
	{
		// Because it is a template, you can work with any type
		Predicate<string> s = StringEqualsFortyTwo;

		Assert.True(s("42"));

		// Predicate is not overloaded, so unlike Action<> you cannot do this...
		// Predicate<int, string> a = (Predicate<int, string>)FILL_ME_IN;
		// Assert.True(a(42, "42"));
	}

	private static bool StartsWithS(string country)
	{
		return country.StartsWith('S');
	}
	[Step(17)]
	public static void FindingWithPredicate()
	{
		// Predicate can be used to find an element in an array
		string[] countries = ["Greece", "Spain", "Uruguay", "Japan"];

		Assert.Equal("Spain", Array.Find(countries, StartsWithS));
	}

	private static bool IsInSouthAmerica(string country)
	{
		string[] countries = [
			"Argentina", 
			"Bolivia", 
			"Brazil", 
			"Chile", 
			"Colombia", 
			"Ecuador", 
			"French Guiana", 
			"Guyana", 
			"Paraguay", 
			"Peru", 
			"Suriname", 
			"Uruguay", 
			"Venezuela"
		];

		return countries.Contains(country);
	}
	[Step(18)]
	public static void ValidationWithPredicate()
	{
		// Predicate can also be used when verifying 
		string[] countries = ["Greece", "Spain", "Uruguay", "Japan"];

		Assert.False(Array.TrueForAll(countries, IsInSouthAmerica));
	}

	private static string FirstMonth()
	{
		return "January";
	}
	private static int Add(int x, int y)
	{
		return x + y;
	}
	[Step(19)]
	public static void FuncWithNoParameters()
	{
		// The Func<> delegate 
		// public delegate TResult Func<T, TResult>(T arg);
		// Is very similar to the Action<> delegate. However, Func<> does not require any parameters, 
		// while it does require to return a value.
		// The last type parameter specifies the return type. If you only specify a single 
		// type, Func<int>, then the method takes no parameters and returns an int.
		// If you specify more than one parameter, then you are specifying the parameter types as well.

		Func<string> d = FirstMonth;

		Assert.Equal("January", d());
	}
	[Step(20)]
	public static void FunctionReturnsInt()
	{
		// Like Action<>, Func<> is overloaded and can take a variable number of parameters.
		// The first type parameters define the parameter types and the last one is the return type. So the following matches
		// a method which takes two int parameters and returns a int.
		Func<int, int, int> a = Add;

		Assert.Equal(2, a(1, 1));
	}

	public class Car(string make, string model, int year)
    {
        public string Make { get; set; } = make;
        public string Model { get; set; } = model;
        public int Year { get; set; } = year;
    }
	private static int SortByModel(Car lhs, Car rhs)
	{
		return lhs.Model.CompareTo(rhs.Model);
	}
	[Step(21)]
	public static void SortingWithComparison()
	{
		// You could make classes sortable by implementing IComparable or IComparer. But the Comparison<> delegate makes it easier
		// public delegate int Comparison<T>(T x, T y);
		// All you need is a method which takes two of the same type and returns -1, 0, or 1 depending upon what order they should go in.
		Car[] cars = [new Car("BMC", "Mini", 1959), new Car("Alfa Romero", "GTV-6", 1986)];

		Comparison<Car> by = SortByModel;
		Array.Sort(cars, by);

		Assert.Equal("GTV-6", cars[0].Model);
	}

	private static string Stringify(int x)
	{
		return x.ToString();
	}
	[Step(22)]
	public static void ChangingTypesWithConverter()
	{
		// The Converter<> delegate
		// public delegate U Converter<T, U>(T from);
		// Can be used to change an object from one type to another
		int[] numbers = [1, 2, 3, 4];
		Converter<int, string> c = Stringify;

		string[] result = Array.ConvertAll(numbers, c);

		Assert.Equal(["1", "2", "3", "4"], result);
	}
}