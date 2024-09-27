namespace Wizard.Application;

public sealed class PatchClientDownloader
{
    private readonly HttpClient _httpClient;

    public PatchClientDownloader(string wizard101Version)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress =
            new Uri($"http://versionec.us.wizard101.com/WizPatcher/V_{wizard101Version}/");
        _httpClient.Timeout = TimeSpan.FromHours(12);
    }

    public async Task DownloadLatestFileListAsync(string filePath)
    {
        Directory.CreateDirectory(filePath);

        var response =
            await _httpClient.GetAsync("Windows/LatestFileList.bin", HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        await using var contentStream = await response.Content.ReadAsStreamAsync();

        await using var fileStream = new FileStream(Path.Combine(filePath, "LatestFileList.bin"), FileMode.Create,
            FileAccess.Write, FileShare.None, 8192, true);

        await contentStream.CopyToAsync(fileStream);
    }

    public async Task<long> GetFileSizeAsync(string fileName)
    {
        using var response =
            await _httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Head, $"LatestBuild/Data/GameData/{fileName}"));

        return response is { IsSuccessStatusCode: true, Content.Headers.ContentLength: not null }
            ? response.Content.Headers.ContentLength.Value
            : 0;
    }

    public async Task DownloadFileAsync(string fileName, string filePath)
    {
        Directory.CreateDirectory(Path.Combine(filePath, Path.GetDirectoryName(fileName)!));

        var response =
            await _httpClient.GetAsync($"LatestBuild/Data/GameData/{fileName}",
                HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        await using var contentStream = await response.Content.ReadAsStreamAsync();

        await using var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create,
            FileAccess.Write, FileShare.None, 8192, true);

        await contentStream.CopyToAsync(fileStream);
    }
}