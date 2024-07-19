using Domain.Entities.Base;

namespace Domain.Entities;

public class App : BaseEntity
{
    public string AppKey { get; set; } = String.Empty;
}
