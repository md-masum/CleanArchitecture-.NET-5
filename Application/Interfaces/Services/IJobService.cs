using System;

namespace Application.Interfaces.Services
{
    public interface IJobService : IDisposable
    {
        void FireAndForgetJob();
        void ReccuringJob();
        void DelayedJob();
        void ContinuationJob();
    }
}
