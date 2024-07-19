using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Shared.Helpers;

public class CacheHelper
{
    public static MemoryCacheEntryOptions CacheOptions()
    {
        Appsettings appsettings = Appsettings.Instance; 

        int SlidingExpiration = ConversionHelper.ConvertTo<int>(appsettings.GetValue("Cache:SlidingExpiration"));
        int AbsoluteExpiration = ConversionHelper.ConvertTo<int>(appsettings.GetValue("Cache:AbsoluteExpiration"));
        int Size = ConversionHelper.ConvertTo<int>(appsettings.GetValue("Cache:Size"));

        return new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(SlidingExpiration))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(AbsoluteExpiration))
            .SetPriority(CacheItemPriority.Normal)
            .SetSize(Size);
    }
}