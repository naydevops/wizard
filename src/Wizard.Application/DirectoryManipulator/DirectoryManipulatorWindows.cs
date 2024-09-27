using System.Runtime.Versioning;

namespace Wizard.Application.DirectoryManipulator;

[SupportedOSPlatform("Windows")]
public sealed class DirectoryManipulatorWindows(string directoryInstallation) : IDirectoryManipulator
{
    private const string PathToVersion = "Bin/data.dat";

    public async Task<string?> GetWizard101Version()
    {
        var fileExists = File.Exists(Path.Combine(directoryInstallation, PathToVersion));

        if (!fileExists) return null;

        var textContent = await File.ReadAllTextAsync(Path.Combine(directoryInstallation, PathToVersion));
        return textContent.Replace("\n", string.Empty);
    }

    public void DestroyGameData()
    {
        var gameDataFolder = Path.Combine(directoryInstallation, "Data", "GameData");
        if (Directory.Exists(gameDataFolder)) Directory.Delete(gameDataFolder, true);

        var objectCacheFolder = Path.Combine(directoryInstallation, "Data", "ObjectCache");
        if (Directory.Exists(objectCacheFolder)) Directory.Delete(objectCacheFolder, true);
    }

    public async Task OverrideLocalPackagesListAsync(List<string> packages)
    {
        var localPackageListLocation = Path.Combine(directoryInstallation, "LocalPackagesList.txt");
        if (File.Exists(localPackageListLocation)) File.Delete(localPackageListLocation);

        var packageStrings = packages.Select(se => se[..se.LastIndexOf('.')]).ToList();
        packageStrings.RemoveAll(se => se.Contains("ZoneData/") || se.Contains("Shaders/"));
        packageStrings.RemoveAll(se => se.Contains(".utd"));

        await File.WriteAllLinesAsync(localPackageListLocation, packageStrings);
    }
}