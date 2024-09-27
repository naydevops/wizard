using System.Runtime.Versioning;
using Microsoft.Win32;

namespace Wizard.Application.InstallFinder;

[SupportedOSPlatform("Windows")]
public sealed class InstallFinderWindows : IInstallFinder
{
    private const string WindowsUninstallRegistryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
    private const string DefaultInstallationDirectory = @"C:\ProgramData\KingsIsle Entertainment\Wizard101";
    private const string Wizard101ExecutableName = "Wizard101.exe";
    private const string Wizard101RegistryName = "Wizard101";

    public string? GetInstallationPath()
    {
        if (Directory.Exists(DefaultInstallationDirectory) && File.Exists(Path.Combine(DefaultInstallationDirectory, Wizard101ExecutableName)))
        {
            return DefaultInstallationDirectory;
        }
        
        return FindRegistryLocation();
    }
    
    private static string? FindRegistryLocation()
    {
        var uninstallRegistryKey = Registry.CurrentUser.OpenSubKey(WindowsUninstallRegistryKey);
        
        return uninstallRegistryKey is null
            ? null
            : (from uninstallKey in uninstallRegistryKey.GetSubKeyNames()
                select uninstallRegistryKey.OpenSubKey(uninstallKey)
                into regKey
                let regKeyDisplayNameValue = regKey?.GetValue("DisplayName")?.ToString()
                where regKeyDisplayNameValue == Wizard101RegistryName
                select regKey?.GetValue("InstallLocation")?.ToString()).FirstOrDefault(regKeyInstallLocationValue =>
                !string.IsNullOrEmpty(regKeyInstallLocationValue));
    }
}