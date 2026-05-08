using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

/**
 * Extension methods can be used by "using" the namespace they're in.
 * Static methods, they can be used, after "using" their namespace, 
 * as instance methods of the local class are.
 *
 * "this Koan koan" arguments could be omitted but are kept here for learning purpose.
 */
public static class ExtensionMethods
{
	public static string HelloWorld(this Koan koan)
	{
		return "Hello!";
	}

	public static string SayHello(
		this Koan koan, 
		string name)
	{
		return string.Format("Hello, {0}!", name);
	}

	public static string[] MethodWithVariableArguments(
		this Koan koan, 
		params string[] names)
	{
		return names;
	}

	public static string SayHi(this string str)
	{
		return "Hi, " + str;
	}

	public static string ExtensionMethodAndLocalMethodShareSameName()
	{
		return "Extension method is resolved.";
	}
}

/**
 * Most methods that don't access instance data became static.
 * This remains compatible with learning purpose and
 * avoids unnecessary object creation in memory
 * in this simple case.
 */
public class AboutMethods : Koan
{
	// Extension Methods allow us to "add" methods to any class
	// without having to recompile. You only have to reference the
	// namespace the methods are in to use them. Here, since both the
	// ExtensionMethods class and the AboutMethods class are in the
	// DotNetKoans.CSharp namespace, AboutMethods can automatically
	// find them.
	[Step(1)]
	public void ExtensionMethodsShowUpInTheCurrentClass()
	{
		Assert.Equal("Hello!", this.HelloWorld());
	}

	[Step(2)]
	public void ExtensionMethodsWithParameters()
	{
		Assert.Equal("Hello, Cory!", this.SayHello("Cory"));
	}

	[Step(3)]
	public void ExtensionMethodsWithVariableParameters()
	{
		Assert.Equal(["Cory","Will","Corey"], this.MethodWithVariableArguments("Cory", "Will", "Corey"));
	}

	// Extension methods can extend any class by referencing 
	// the name of the class they are extending. For example, 
	// we can "extend" the string class like so:
	[Step(4)]
	public static void ExtendingCoreClasses()
	{
		Assert.Equal("Hi, Cory", "Cory".SayHi());
	}

	// Of course, any of the parameter things you can do with 
	// extension methods you can also do with local methods
	private static string[] LocalMethodWithVariableParameters(params string[] names)
	{
		return names;
	}

	// Note how we called the extension method by saying "this.LocalMethodWithVariableParameters"
	// That isn't necessary for local methods:
	[Step(5)]
	public static void LocalMethodsWithoutExplicitReceiver()
	{
		Assert.Equal(["Cory", "Will", "Corey"], LocalMethodWithVariableParameters("Cory", "Will", "Corey"));
	}
	// But it is required for Extension Methods, since it needs
	// an instance variable. So this wouldn't work, giving a
	// compile-time error:
	// Assert.Equal(["Cory", "Will", "Corey"], MethodWithVariableArguments("Cory", "Will", "Corey"));

	public static string ExtensionMethodAndLocalMethodShareSameName()
	{
		return "Local method is resolved.";
	}

	/**
	 * In the edge case of naming a local method and an extension method with the same name,
	 * the extension method would never be "found".
	 * The local method takes precedence. 
	 */
	[Step(6)]
	public static void LocalMethodsWithSameNameAsExtensionMethod()
	{
		Assert.Equal("Local method is resolved.", ExtensionMethodAndLocalMethodShareSameName());
	}

	class InnerSecret
	{
		public static string Key() { return "Key"; }
		public string Secret() { return "Secret"; }
		protected static string SuperSecret() { return "This is secret"; }
		private string SooperSeekrit() { return "No one will find me!"; }
	}

	class StateSecret : InnerSecret
	{
		public static string InformationLeak() { return SuperSecret(); }
	}

	// Static methods don't require an instance of the object
	// in order to be called. 
	[Step(7)]
	public static void CallingStaticMethodsWithoutAnInstance()
	{
		Assert.Equal("Key", InnerSecret.Key());
	}

	// In fact, you can't call it on an instance variable
	// of the object. So this wouldn't compile:
	// InnerSecret secret = new InnerSecret();
	// Assert.Equal("Key", secret.Key());
	[Step(8)]
	public static void CallingPublicMethodsOnAnInstance()
	{
		InnerSecret innerSecret = new();

		Assert.Equal("Secret", innerSecret.Secret());
	}

	// Protected methods can only be called by a subclass
	// We're going to call the public method called
	// InformationLeak of the StateSecret class which returns
	// the value from the protected method SuperSecret
	[Step(9)]
	public static void CallingProtectedStaticMethods()
	{
		Assert.Equal("This is secret", StateSecret.InformationLeak());
	}

	// But, we can't call the private methods of InnerSecret
	// either through an instance, or through a subclass. It
	// just isn't available.

	// Ok, well, that isn't entirely true. Reflection can get
	// you just about anything, and though it's way out of scope
	// for this...
	[Step(10)]
	public static void SubvertPrivateMethods()
	{
		InnerSecret secret = new();

		string superSecretMessage = secret.GetType()
			.GetMethod("SooperSeekrit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
			.Invoke(secret, null) as string;

		Assert.Equal("No one will find me!", superSecretMessage);
	}

	// Up till now we've had explicit return types. It's also
	// possible to create methods which dynamically shift
	// the type based on the input. These are referred to
	// as generics
	public static T GiveMeBack<T>(T p1)
	{
		return p1;
	}

	[Step(11)]
	public static void CallingGenericMethods()
	{
		Assert.Equal(typeof(short), GiveMeBack<short>(1).GetType());

		Assert.Equal("Hi!", GiveMeBack("Hi!"));
	}
}