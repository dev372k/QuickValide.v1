using Shared.DTOs.Services;

namespace Domain.Repositories.Services;

public interface IGPTService
{
    Task<string> ChatCompletion(string input);
}
