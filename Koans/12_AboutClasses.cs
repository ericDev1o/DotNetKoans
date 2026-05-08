using System;
using DotNetKoans.Engine;
using Xunit;

namespace DotNetKoans.Koans;

public class AboutClasses : Koan
{
	// You can create your own custom types by using
	// struct, class, interface, and enum constructs.
	// C# is an object-oriented language. Classes and objects are
	// the two main aspects of object-oriented programming.
	// A class is a template for objects, and an object is an
	// instance of a class. Classes are declared by using the
	// class keyword followed by a unique identifier.

	class Foo1
	{

	}

	[Step(1)]
	public void InstancesOfAClassesCanBeCreatedWithNew()
	{
		// A type that is defined as a class is a reference type.
		// when you declare a variable of a reference type, the variable
		// contains the value null until you explicitly create an instance
		Foo1 foo = null;
		
		Assert.Null(foo);

		foo = new();

		Assert.NotNull(foo);
	}

	class Foo2
	{
		public int Int { get; set; }
		internal string _str;

		private DateTime _canNotSeeMe = DateTime.Now;
	}

	[Step(2)]
	public void InstanceMembersCanBeSetByAssigningToThem()
	{
        // Try to assign visible class members
        Foo2 foo = new()
        {
            Int = 1,
            _str = "Bar"
        };

        Assert.Equal(1, foo.Int);
		Assert.Equal("Bar", foo._str);
	}

	class Foo3
	{
		private bool _boom = true;
		public bool Internal { get => _boom; set { _boom = value; } }

		public void Do()
		{
			if (_boom)
			{
				throw new InvalidOperationException(nameof(Do));
			}
		}
	}

	[Step(3)]
	public void UseAccessorsToReturnInstanceVariables()
	{
		Foo3 foo = new();

		Assert.Throws<InvalidOperationException>(foo.Do);

		// make sure it won't explode
		foo.Internal = false;

		foo.Do();
	}

	class Foo4
	{
		public string Bar { get; }
		public Foo4(string value = default) => Bar = value;
	}

	[Step(4)]
	public void UseConstructorsToDefineInitialValues()
	{
		Foo4 foo = default;

		Assert.Null(foo);

		foo = new();

		Assert.Null(foo.Bar);

		foo = new("BarCustom");

		Assert.Equal("BarCustom", foo.Bar);
	}

	[Step(5)]
	public void DifferentObjectsHasDifferentInstanceVariables()
	{
		Foo4 foo4_1 = new();
		Foo4 foo4_2 = new();
		Assert.Equal(foo4_1.Bar, foo4_2.Bar);

		foo4_1 = new("1");
		foo4_2 = new("2");
		Assert.NotEqual(foo4_1.Bar, foo4_2.Bar);
	}

	class Foo5(int val = 0)
    {
        public int Val { get; } = val;

        public Foo5 Self() => this;

		public override string ToString()
		{
			return base.ToString();
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Val);
		}
	}

	[Step(6)]
	public void MemberMethodSelfRefersToContainingObject()
	{
		Foo5 foo = new();

		Assert.Equal(foo, foo.Self());
	}

	[Step(7)]
	public void ToStringProvidesStringRepresentationOfAnObject()
	{
		Foo5 foo = new();

		Assert.Equal("DotNetKoans.Koans.AboutClasses+Foo5", foo.ToString());
	}

	[Step(8)]
	public void EqualsDeterminesObjectComparison()
	{
		Foo5 foo5_1 = new(3);
		Foo5 foo5_2 = new(3);
		// you can define how objects are compared

		Assert.NotEqual(foo5_1, foo5_2);
		Assert.Equal(foo5_1.Val, foo5_2.Val);
		// references are different
		Assert.False(ReferenceEquals(foo5_1, foo5_2));
	}

}