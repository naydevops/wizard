namespace Wizard.Application.DirectoryManipulator;

public interface IDirectoryManipulator
{
    Task<string?> GetWizard101Version();
    void DestroyGameData();
    void DestroyLocalPackagesList();
}