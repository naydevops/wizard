namespace Wizard.Application;

public sealed class AdditionalFilesParser(string filePath)
{
    public async Task<List<string>> GetAdditionalFilesAsync()
    {
        var lines = await File.ReadAllLinesAsync(filePath);

        return lines.ToList();
    }
}