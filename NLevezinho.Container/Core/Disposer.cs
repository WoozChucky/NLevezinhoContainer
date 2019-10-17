using System;
using System.Collections.Generic;
using System.Text;

namespace NLevezinho.Container.Core
{
    internal class Disposer : Disposable, IDisposer
    {
        /// <summary>
        /// Contents all implement IDisposable
        /// </summary>
        private Stack<IDisposable> _items = new Stack<IDisposable>();

        private readonly object _syncRoot = new object();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_syncRoot)
                {
                    while (_items.Count > 0)
                    {
                        var item = _items.Pop();
                        item.Dispose();
                    }

                    _items = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Adds an object to the disposer. When the disposer is
        /// disposed, so will the object be.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void AddInstanceForDisposal(IDisposable instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            lock (_syncRoot)
            {
                _items.Push(instance);
            }
        }
    }
}
