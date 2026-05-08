using Xunit;
using System;
using System.Globalization;
using DotNetKoans.Engine;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetKoans.Koans;

public class AboutStrings : Koan
{
	// Note: This is one of the longest katas and, perhaps, one
	// of the most important. String behavior in .NET is not
	// always what you expect it to be, especially when it comes
	// to concatenation and newlines, and is one of the biggest
	// causes of memory leaks in .NET applications

	static readonly CultureInfo culture = new("fr-FR");

	[Step(1)]
	public static void DoubleQuotedStringsAreStrings()
	{
		string str = "Hello, World";
		Assert.Equal(typeof(string), str.GetType());
	}

	[Step(2)]
	public static void SingleQuotedStringsAreNotStrings()
	{
		var str = 'H';
		Assert.Equal(typeof(char), str.GetType());
	}

	[Step(3)]
	public static void CreateAStringWhichContainsDoubleQuotes()
	{
		string str = "Hello, \"World\"";
		Assert.Equal(14, str.Length);
	}

	[Step(4)]
	public static void AnotherWayToCreateAStringWhichContainsDoubleQuotes()
	{
		// The @ symbol creates a 'verbatim string literal'. 
		// Here's one thing you can do with it:
		string str = @"Hello, ""World""";
		Assert.Equal(14, str.Length);
	}

	[Step(5)]
	public static void VerbatimStringsCanHandleFlexibleQuoting()
	{
		string strA = @"Verbatim Strings can handle both ' and "" characters (when escaped)";
		string strB = "Verbatim Strings can handle both ' and \" characters (when escaped)";
		Assert.Equal(strA,strB);
	}

	[Step(6)]
	public static void VerbatimStringsCanHandleMultipleLinesToo()
	{
		// Tip: What you create for the literal string will have to 
		// escape the newline characters. For Windows, that would be
		// \r\n. If you are on non-Windows, that would just be \n.
		// We'll show a different way next.
		string verbatimString = @"I
am a
broken line";

		// Make sure to use a literal string.
		// Escaped characters in verbatim strings are covered later.
		string literalString = "I\nam a\nbroken line";
		Assert.Equal(literalString.Length, verbatimString.Length);

		// For verbatim strings, the newline character used will depend on
		// whether the source file uses a \r\n or a \n ending and they have
		// to match the ones on the literal string
		// If you are using Visual Studio Code, you can see which line ending is
		// in use at the bottom right of the screen

		Assert.Equal(literalString, verbatimString);
	}

	[Step(7)]
	public static void ACrossPlatformWayToHandleLineEndings()
	{
		// Since line endings are different on different platforms
		// (\r\n for Windows, \n for Linux) you shouldn't just type in
		// the hardcoded escape sequence. A much better way
		// (We'll handle concatenation and better ways of that in a bit)
		string literalString = "I" + System.Environment.NewLine + "am a" + System.Environment.NewLine + "broken line";
		string verbatimString =  @"I
am a
broken line";
		Assert.Equal(literalString, verbatimString);
	}

	[Step(8)]
	public static void PlusWillConcatenateTwoStrings()
	{
		string str = "Hello, " + "World";
		Assert.Equal("Hello, World", str);
	}

	[Step(9)]
	public static void PlusConcatenationWillNotModifyOriginalStrings()
	{
		string strA = "Hello, ";
		string strB = "World";
		string fullString = strA + strB;
		Assert.Equal("Hello, ", strA);
		Assert.Equal("World", strB);
	}

	[Step(10)]
	public static void PlusEqualsWillModifyTheTargetString()
	{
		string strA = "Hello, ";
		string strB = "World";
		strA += strB;
		Assert.Equal("Hello, World", strA);
		Assert.Equal("World", strB);
	}

	[Step(11)]
	public static void StringsAreReallyImmutable()
	{
		// So here's the thing. Concatenating strings is cool
		// and all. But if you think you are modifying the original
		// string, you'd be wrong. 

		string strA = "Hello, ";
		string originalString = strA;
		string strB = "World";
		strA += strB;
		
		Assert.Equal("Hello, ", originalString);
		Assert.False(Object.ReferenceEquals(strA, originalString));
		// What just happened? Well, the string concatenation actually
		// takes strA and strB and creates a *new* string in memory
		// that has the new value. It does *not* modify the original
		// string. This is a very important point - if you do this kind
		// of string concatenation in a tight loop, you'll use a lot of memory
		// because the original string will hang around in memory until the
		// garbage collector picks it up. Let's look at a better way
		// when dealing with lots of concatenation
	}

	[Step(12)]
	public static void ABetterWayToConcatenateLotsOfStrings()
	{
		// Concatenating lots of strings is a Bad Idea(tm). If you need to do that, then consider StringBuilder.
		StringBuilder strBuilder = new System.Text.StringBuilder();
		strBuilder.Append("The ");
		strBuilder.Append("quick ");
		strBuilder.Append("brown ");
		strBuilder.Append("fox ");
		strBuilder.Append("jumped ");
		strBuilder.Append("over ");
		strBuilder.Append("the ");
		strBuilder.Append("lazy ");
		strBuilder.Append("dog.");
		string str = strBuilder.ToString();
		Assert.Equal("The quick brown fox jumped over the lazy dog.", str);

		// When doing lots and lots of concatenation in a loop, 
		// StringBuilder will be more efficient than concatenation using the +-operator.
		// However, even in the above example simple concatenation would actually be more efficient.
	}
	
	[Step(13)]
	public static void YouCouldAlsoUseStringFormatToConcatenate()
	{
		string world = "World";
		string str = String.Format("Hello, {0}", world);
		Assert.Equal("Hello, World", str);
		// Note that string concatenation and interpolation is more efficient than string.Format
	}

	[Step(14)]
	public static void AnyExpressionCanBeUsedInFormatString()
	{
		string str = String.Format("The square root of 9 is {0}", Math.Sqrt(9));
		Assert.Equal("The square root of 9 is 3", str);
	}

	[Step(15)]
	public static void StringsCanBePaddedToTheLeft()
	{
		// You can modify the value inserted into the result
		string str = string.Format("{0,3:}", "x");
		Assert.Equal("  x", str);
	}

	[Step(16)]
	public static void StringsCanBePaddedToTheRight()
	{
		string str = string.Format("{0,-3:}", "x");
		Assert.Equal("x  ", str);
	}

	[Step(17)]
	public static void SeparatorsCanBeAdded()
	{
		string str = string.Format(culture, "{0:n}", 123456);
		
		Assert.Equal("123\u202F456,000", str);
	}

	[Step(18)]
	public static void CurrencyDesignatorsCanBeAdded()
	{
		string str = string.Format(culture, "{0:c}", 123456);
		Assert.Equal("123\u202F456,00 €", str);
	}

	[Step(19)]
	public static void NumberOfDisplayedDecimalsCanBeControlled()
	{
		string str = string.Format(culture, "{0:.##}", 12.3456);
		Assert.Equal("12,35", str);
	}

	[Step(20)]
	public static void MinimumNumberOfDisplayedDecimalsCanBeControlled()
	{
		string str = string.Format(culture, "{0:.00}", 12.3);
		Assert.Equal("12,30", str);
	}

	[Step(21)]
	public static void BuiltInDateFormatters()
	{
		string str = string.Format(
			culture, 
			"{0:t}", 
			DateTime.Parse("12/16/2011 2:35:02 PM", CultureInfo.InvariantCulture)
		);
		Assert.Equal("14:35", str);
	}

	[Step(22)]
	public static void CustomDateFormatters()
	{
		string str = string.Format("{0:t m}", DateTime.Parse("12/16/2011 2:35:02 PM", CultureInfo.InvariantCulture));
		Assert.Equal("P 35", str);
	}
	// These are just a few of the formatters available. Dig some and you may find what you need.


	[Step(23)]
	public static void StringBuilderCanUseFormatAsWell()
	{
		StringBuilder strBuilder = new();
		strBuilder.AppendFormat("{0} {1} {2}", "The", "quick", "brown");
		strBuilder.AppendFormat("{0} {1} {2}", "jumped", "over", "the");
		strBuilder.AppendFormat("{0} {1}.", "lazy", "dog");
		string str = strBuilder.ToString();
		Assert.Equal("The quick brownjumped over thelazy dog.", str);
	}

	[Step(24)]
	public static void LiteralStringsInterpretsEscapeCharacters()
	{
		string str = "\n";
		Assert.Equal(1, str.Length);
	}

	[Step(25)]
	public static void VerbatimStringsDoNotInterpretEscapeCharacters()
	{
		string str = @"\n";
		Assert.Equal(2, str.Length);
	}

	[Step(26)]
	public static void VerbatimStringsStillDoNotInterpretEscapeCharacters()
	{
		string str = @"\\\";
		Assert.Equal(3, str.Length);
	}

	[Step(27)]
	public static void YouCanGetASubstringFromAString()
	{
		string str = "Bacon, lettuce and tomato";
		Assert.Equal("tomato", str.Substring(19));
		Assert.Equal("let", str.Substring(7, 3));
	}

	[Step(28)]
	public static void YouCanGetASingleCharacterFromAString()
	{
		string str = "Bacon, lettuce and tomato";
		Assert.Equal('B', str[0]);
	}

	[Step(29)]
	public static void SingleCharactersAreRepresentedByIntegers()
	{
		Assert.Equal(97, 'a');
		Assert.Equal(98, 'b');
		Assert.Equal('b', 'a' + 1);
	}

	[Step(30)]
	public static void StringsCanBeSplit()
	{
		string str = "Sausage Egg Cheese";
		string[] words = str.Split();
		Assert.Equal(new[] { "Sausage", "Egg", "Cheese" }, words);
	}

	[Step(31)]
	public static void StringsCanBeSplitUsingCharacters()
	{
		string str = "the:rain:in:spain";
		string[] words = str.Split(':');
		Assert.Equal(new[] { "the", "rain", "in", "spain" }, words);
	}

	[Step(32)]
	public static void StringsCanBeSplitUsingRegularExpressions()
	{
		string str = "the:rain:in:spain";
		Regex regex = new(":");
		string[] words = regex.Split(str);
		Assert.Equal(new[] { "the", "rain", "in", "spain" }, words);

		// A full treatment of regular expressions is beyond the scope
		// of this tutorial. The book "Mastering Regular Expressions"
		// is highly recommended to be on your bookshelf
	}

	[Step(33)]
	public static void YouCanInterpolateVariablesIntoAString()
	{
		string name = "John Doe";
		short age = 33;
		string str = $"Mr. {name} is {age} years old";
		Assert.Equal("Mr. John Doe is 33 years old", str);
	}
	
	[Step(34)]
	public static void InterpolationSupportsFormatAsWell()
	{
		string str = $"{DateTime.Parse("12/16/2011 2:35:02 PM", CultureInfo.InvariantCulture):t m}";
		Assert.Equal("P 35", str);
	}
}