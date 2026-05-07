using System.Collections.Generic;
using System.Linq;
using DotNetKoans.Engine;
using Xunit;

namespace DotNetKoans.Koans;

public class AboutDictionary : Koan
{
	// A dictionary is a C# class.
	[Step(1)]
	public void DictionaryIsACSharpClass()
	{
		var dict = new Dictionary<string, string>
        {
            { "Value", "Key" }
        };
		var firstElement = dict.First();

		Assert.Equal("Value", firstElement.Key); // Key
		Assert.Equal("Key", firstElement.Value); // Value
	}

	// Pass keys to get their values.
	[Step(2)]
	public void UsingDictionaryKeysToGetValues()
	{
		var dict = new Dictionary<string, string>
        {
            { "Bruce", "Wayne" },
            { "United Kingdom", "London" },
            { "Poland", "Warsaw" },
            { "Japan", "Tokyo" }
        };

		var key = "Japan";
		Assert.Equal("Tokyo", dict[key]); // What is the value?            
	}

	// Check if a key exists in Dictionary.
	[Step(3)]
	public void CheckIfKeyExists()
	{
		var dict = new Dictionary<string, string>
        {
            { "Bruce", "Wayne" },
            { "United Kingdom", "London" },
            { "Poland", "Warsaw" },
            { "Japan", "Tokyo" }
        };

		var key = "Jeff";
		Assert.False(dict.ContainsKey(key)); // How to make this statement true?   
		key = "Poland";
		Assert.True(dict.ContainsKey(key));      
	}

	// Check if a value exists in Dictionary.
	[Step(4)]
	public void CheckIfValueExists()
	{
		var dict = new Dictionary<string, string>
        {
            { "Bruce", "Wayne" },
            { "United Kingdom", "London" },
            { "Poland", "Warsaw" },
            { "Japan", "Tokyo" }
        };

		var val = "Archer";
		Assert.False(dict.ContainsValue(val)); // How to make this statement true?   
		val = "London";
		Assert.True(dict.ContainsValue(val));       
	}

	// Update the value of a key in dictionary.
	[Step(5)]
	public void UpdateValueOfKey()
	{
		var dict = new Dictionary<string, string>
        {
            { "Bruce", "Wayne" },
            { "United Kingdom", "London" },
            { "Poland", "Warsaw" },
            { "Japan", "Tokyo" },
            { "India", "Mumbai" }
        };

		var key = "India";
		var expectedValue = "New Delhi";

		dict[key] = expectedValue;

		Assert.Equal(expectedValue, dict[key]);         
	}

	// Remove a key from dictionary and check its value.
	[Step(6)]
	public void RemoveKeyAndCheckIfItExists()
	{
		var dict = new Dictionary<string, string>
        {
            { "Bruce", "Wayne" },
            { "United Kingdom", "London" },
            { "Poland", "Warsaw" },
            { "Japan", "Tokyo" },
            { "India", "Mumbai" }
        };

		var keyToRemove = "Bruce";

		Assert.True(dict.ContainsKey(keyToRemove));

		if (dict.ContainsKey(keyToRemove))
			dict.Remove(keyToRemove);
            
		Assert.False(dict.ContainsKey(keyToRemove));      
	}
}