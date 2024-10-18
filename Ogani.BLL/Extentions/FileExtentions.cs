using Microsoft.AspNetCore.Http;

namespace Ogani.BLL.Extentions;

public static class FileExtentions
{
    public static bool IsImage(this IFormFile file)
    {
        return file.ContentType.Contains("image",StringComparison.OrdinalIgnoreCase);
    }
    public static bool AllowedSize(this IFormFile file, int mb)
    {
        return file.Length <= mb * 1024 * 1024;
    }
    public static async Task<string> GenerateFile(this IFormFile file, string path)
    {
        var name = Guid.NewGuid().ToString() + file.FileName;
        path = Path.Combine(path, name);

        using(var fs = new FileStream(path, FileMode.CreateNew))
           await file.CopyToAsync(fs);

        return name;
    }
}
