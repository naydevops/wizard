using System.Runtime.Versioning;
using Serilog;

namespace Wizard.Application.DirectoryManipulator;

[SupportedOSPlatform("Windows")]
public sealed class DirectoryManipulatorWindows(string directoryInstallation) : IDirectoryManipulator
{
    private const string PathToVersion = "Bin/data.dat";

    public async Task<string?> GetWizard101Version()
    {
        var fileExists = File.Exists(Path.Combine(directoryInstallation, PathToVersion));

        if (!fileExists)
        {
            Log.Warning("DMW- Unable to get Wizard101 installation.");
            return null;
        }

        ;

        var textContent = await File.ReadAllTextAsync(Path.Combine(directoryInstallation, PathToVersion));
        var version = textContent.Replace("\n", string.Empty);
        Log.Information("DMW- Found version {Version}.", version);

        return version;
    }

    public void DestroyGameData()
    {
        var gameDataFolder = Path.Combine(directoryInstallation, "Data", "GameData");
        if (Directory.Exists(gameDataFolder))
        {
            Log.Information("DMW- Deleted GameData folder.");
            Directory.Delete(gameDataFolder, true);
        }

        var objectCacheFolder = Path.Combine(directoryInstallation, "Data", "ObjectCache");
        if (!Directory.Exists(objectCacheFolder)) return;

        Log.Information("DMW- Deleted ObjectCache folder..");
        Directory.Delete(objectCacheFolder, true);
    }

    public async Task OverrideLocalPackagesListAsync(List<string> packages)
    {
        var localPackageListLocation = Path.Combine(directoryInstallation, "LocalPackagesList.txt");
        if (File.Exists(localPackageListLocation))
        {
            Log.Information("DMW- Deleted LocalPackagesList file..");
            File.Delete(localPackageListLocation);
        }

        var packageStrings = packages.Select(se => se[..se.LastIndexOf('.')]).ToList();
        packageStrings.RemoveAll(se => se.Contains("ZoneData/") || se.Contains("Shaders/"));
        packageStrings.RemoveAll(se => se.Contains(".utd"));

        await File.WriteAllLinesAsync(localPackageListLocation, packageStrings);

        Log.Information("DMW- Created and overriden LocalPackagesList file.");
    }
}