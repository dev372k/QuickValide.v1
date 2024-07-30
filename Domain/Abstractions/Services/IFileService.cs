using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Services;

public interface IFileService
{
    Task<string> SaveAsync(byte[] imageData);
}
