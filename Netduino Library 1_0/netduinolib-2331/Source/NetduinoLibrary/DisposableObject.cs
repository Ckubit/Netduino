using System;
using Microsoft.SPOT;

namespace NetduinoLibrary
{
    /// <summary>
    /// Serves as a base class for disposable objects. It implements the <see cref="IDisposable"/> and lets derived classes override the 
    /// <see cref="Dispose"/> method.
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        #region Constructors
        public DisposableObject()
        {
        }
        #endregion
        protected bool disposed;

        /// <summary>
        /// Override this method to implement disposing in derived class.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            disposed = true;
        }

        public bool IsDisposed
        {
            get { return disposed; }
        }

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

}
