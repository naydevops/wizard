using System.Reflection;
using System.Runtime.InteropServices;
using Wizard.Application;
using Wizard.Application.DirectoryManipulator;
using Wizard.Application.InstallFinder;

if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Console.WriteLine("The author of this software is lazy and hasn't allowed systems other than Windows yet. :(");
    Environment.Exit(1);
}

Console.WriteLine("wizard - the little helper");

var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
var tempPatchFileDirectory = Path.Combine(applicationDirectory, "TempPatchFiles");

var installFinder = GetInstallFinder();
var installPath = installFinder.GetInstallationPath();

var directoryManipulator = GetDirectoryManipulator(installPath);
var wizard101Version = await directoryManipulator.GetWizard101Version();

Console.WriteLine($"Found Wizard101 installation at location {installPath}");
Console.WriteLine($"Wizard101 data version is: {wizard101Version}");

directoryManipulator.DestroyGameData();
directoryManipulator.DestroyLocalPackagesList();

Console.WriteLine("Deleted previously installed LocalPackagesList for a fresh installation.");
Console.WriteLine("Deleted previously installed GameData and ObjectCache for a fresh installation.");
Console.WriteLine("Downloading LatestFileList.bin to discover all Wizard101 files.");

var patchClientDownloader = new PatchClientDownloader(wizard101Version);
await patchClientDownloader.DownloadLatestFileListAsync(tempPatchFileDirectory);

Console.WriteLine("Downloaded LatestFileList.bin successfully!");

var latestFileListExtractor = new LatestFileListExtractor(Path.Combine(tempPatchFileDirectory, "LatestFileList.bin"));
var filesDiscovered = await latestFileListExtractor.ExtractStringsAsync();

Console.WriteLine($"Discovered {filesDiscovered.Count} downloadable objects from the LatestFileList.bin file.");

var additionalFilesParser = new AdditionalFilesParser(Path.Combine(applicationDirectory, "AdditionalFiles.txt"));
var additionalFiles = await additionalFilesParser.GetAdditionalFilesAsync();

Console.WriteLine($"Discovered {additionalFiles.Count} downloadable objects from the AdditionalFiles.txt file.");

var allFiles = new List<string>();
allFiles.AddRange(filesDiscovered);
allFiles.AddRange(additionalFiles);

allFiles = allFiles.Distinct().ToList();
allFiles.Sort();

Console.WriteLine($"Discovered a total of {allFiles.Count} files to download after merging and removing duplicates.");

var currentFile = 1;
foreach (var file in allFiles)
{
    Console.WriteLine($"[{currentFile:D4}/{allFiles.Count:D4}] BEGIN {file}");

    var fileSize = await patchClientDownloader.GetFileSizeAsync(file);
    
    Console.WriteLine($"[{currentFile:D4}/{allFiles.Count:D4}] BEGIN {file} | File size: {fileSize / (1024.0 * 1024.0):F2}MB");

    await patchClientDownloader.DownloadFileAsync(file, Path.Combine(installPath, "Data", "GameData"));
    
    currentFile++;
}

Console.WriteLine("The files were successfully downloaded to the Wizard101 client. Please start up your game and enjoy!~");

return;

IInstallFinder GetInstallFinder()
{
    return RuntimeInformation.OSDescription switch
    {
        _ when RuntimeInformation.IsOSPlatform(OSPlatform.Windows) => new InstallFinderWindows(),
        _ => throw new PlatformNotSupportedException()
    };
}

IDirectoryManipulator GetDirectoryManipulator(string directoryPath)
{
    return RuntimeInformation.OSDescription switch
    {
        _ when RuntimeInformation.IsOSPlatform(OSPlatform.Windows) => new DirectoryManipulatorWindows(directoryPath),
        _ => throw new PlatformNotSupportedException()
    };
}