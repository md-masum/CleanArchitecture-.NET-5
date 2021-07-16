using System;

namespace Application.Interfaces.Services
{
    public interface IDateTimeService : IDisposable
    {
        DateTime Now { get; }
    }
}