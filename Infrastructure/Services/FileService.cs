using Domain.Repositories.Services;
using Shared.Commons;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    public async Task<string> SaveAsync(byte[] imageData)
    {
        var filename = Path.Combine(MiscilenousConstants.IMAGEPATH, $"{Guid.NewGuid()}.jpeg");
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);

        await File.WriteAllBytesAsync(filePath, imageData);

        return filename;
    }
}
