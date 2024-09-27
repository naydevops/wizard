using System.Runtime.Versioning;
using System.Text.RegularExpressions;

namespace Wizard.Application.DirectoryManipulator;

[SupportedOSPlatform("Windows")]
public sealed class DirectoryManipulatorWindows(string directoryInstallation) : IDirectoryManipulator
{
    private readonly string _directoryInstallation = directoryInstallation;
    private const string PathToVersion = "Bin/data.dat";

    public async Task<string?> GetWizard101Version()
    {
        var fileExists = File.Exists(Path.Combine(_directoryInstallation, PathToVersion));

        if (!fileExists) return null;
        
        var textContent = await File.ReadAllTextAsync(Path.Combine(_directoryInstallation, PathToVersion));
        return textContent.Replace("\n", string.Empty);
    }

    public void DestroyGameData()
    {
        var gameDataFolder = Path.Combine(_directoryInstallation, "Data", "GameData");
        if (Directory.Exists(gameDataFolder))
        {
            Directory.Delete(gameDataFolder, true);
        }
        
        var objectCacheFolder = Path.Combine(_directoryInstallation, "Data", "ObjectCache");
        if (Directory.Exists(objectCacheFolder))
        {
            Directory.Delete(objectCacheFolder, true);
        }
    }

    public void DestroyLocalPackagesList()
    {
        var localPackageListLocation = Path.Combine(_directoryInstallation, "LocalPackagesList.txt");
        if (File.Exists(localPackageListLocation))
        {
            File.Delete(localPackageListLocation);
        }
}
}