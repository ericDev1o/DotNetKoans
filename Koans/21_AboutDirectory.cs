using System.IO;
using DotNetKoans.Engine;
using Xunit;
using IOPath = System.IO.Path;

namespace DotNetKoans.Koans;

public class AboutDirectory : Koan
{
	// Directory is a class that provides static methods for creating, 
	// moving, and enumerating through directories and subdirectories.

	private static readonly string directoryName = "tempDirectory";       
	private static readonly string fullPath = IOPath.Combine(IOPath.GetTempPath(), directoryName); 
	// GetTempPath() Returns the path of the current user's temporary folder.

	[Step(1)]
	public static void CreatingAndDeletingDirectory()
	{            
		Directory.CreateDirectory(fullPath);

		Assert.True(Directory.Exists(fullPath));

		Directory.Delete(fullPath);

		Assert.False(Directory.Exists(fullPath));
	}
        
	[Step(2)]
	public static void GetDirectoryInfo()
	{
		DirectoryInfo directoryInfo = new(fullPath);
		directoryInfo.Create();

		Assert.True(directoryInfo.Exists);
		Assert.Equal("tempDirectory", directoryInfo.Name);

		directoryInfo.Delete(false);
	}
        
	[Step(3)]
	public static void CreateSubDirectory()
	{
		DirectoryInfo directoryInfo = new(fullPath);
		directoryInfo.Create();
		directoryInfo.CreateSubdirectory("subdirectory1");
		directoryInfo.CreateSubdirectory("subdirectory2");

		Assert.Equal(2, directoryInfo.GetDirectories().Length); // what is the number of subdirectories?

		directoryInfo.Delete(true);
	}
        
	[Step(4)]
	public static void GetFilesInDirectory()
	{
		DirectoryInfo directoryInfo = new(fullPath);
		directoryInfo.Create();
            
		using (File.Create(IOPath.Combine(fullPath, "file1")))
		using (File.Create(IOPath.Combine(fullPath, "file2")))
		{
		}

		Assert.Equal(2, directoryInfo.GetFiles().Length); 
		// what is the number of files that exist in this directory?

		directoryInfo.Delete(true);

	}

}