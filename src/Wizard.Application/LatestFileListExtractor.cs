using System.Text;
using Serilog;

namespace Wizard.Application;

public sealed class LatestFileListExtractor(string fileLocation)
{
    private const string SearchString = "Data/GameData/";

    public async Task<List<string>> ExtractStringsAsync()
    {
        var baseStrings = await ExtractBaseStringsAsync();
        baseStrings = baseStrings.Select(se => se.Replace("Data/GameData/", string.Empty)).ToList();

        Log.Information("LFLE- Extracted {StringCount}.", baseStrings.Count);

        return baseStrings;
    }

    private async Task<List<string>> ExtractBaseStringsAsync()
    {
        var extractedStrings = new List<string>();
        var buffer = new byte[1];
        var currentString = new StringBuilder();

        await using var fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);

        Log.Information("LFLE- Executing FileStream");

        while (await fs.ReadAsync(buffer.AsMemory(0, 1)) > 0)
        {
            int b = buffer[0];

            if (b is >= 32 and <= 126)
            {
                currentString.Append((char)b);
            }
            else
            {
                if (currentString.ToString().Contains(SearchString)) extractedStrings.Add(currentString.ToString());

                currentString.Clear();
            }
        }

        if (currentString.ToString().Contains(SearchString)) extractedStrings.Add(currentString.ToString());

        return extractedStrings;
    }
}