using System;
using System.Threading;

namespace NLevezinho.Container.Core
{
    /// <summary>
    /// Base class for disposable objects.
    /// </summary>
    public class Disposable : IDisposable
    {
        private const int DisposeFlag = 1;
        private int _isDisposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            var wasDisposed = Interlocked.Exchange(ref _isDisposed, DisposeFlag);
            if (wasDisposed == DisposeFlag)
                return;
            
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the current instance has been disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get
            {
                Interlocked.MemoryBarrier();
                return _isDisposed == DisposeFlag;
            }
        }
    }
}
