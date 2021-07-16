using System;
using Application.Interfaces.Services;

namespace Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;

        public void Dispose()
        {
        }
    }
}
