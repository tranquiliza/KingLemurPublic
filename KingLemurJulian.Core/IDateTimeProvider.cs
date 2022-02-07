using System;

namespace KingLemurJulian.Core
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
