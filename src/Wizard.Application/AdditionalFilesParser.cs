namespace Wizard.Application;

public sealed class AdditionalFilesParser
{
    private readonly string _filePath;
    
    public AdditionalFilesParser(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<string>> GetAdditionalFilesAsync()
    {
        var lines = await File.ReadAllLinesAsync(_filePath);

        return lines.ToList();
    }
}