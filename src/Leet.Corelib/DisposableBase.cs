// -----------------------------------------------------------------------
// <copyright file="DisposableBase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;

    /// <summary>
    ///     An <see langword="abstract"/> class that provides basic implementation of <see cref="IDisposable"/> interface.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        /// <summary>
        ///     Holds a value that indicates whether current instance has been disposed.
        /// </summary>
        private int isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisposableBase"/> class.
        /// </summary>
        protected DisposableBase()
        {
            this.isDisposed = 0;
        }

        /// <summary>
        ///     Gets a value indicating whether current instance has been disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get
            {
                return this.isDisposed == 1;
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1063:ImplementIDisposableCorrectly",
            Justification = "Pattern maintained with a value that tracks the disposition state.")]
        public void Dispose()
        {
            if (!this.MarkAsDisposed())
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        ///     Called upon application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        ///     Value of this parameter is set to <see langword="true"/> if the method is called from <see cref="IDisposable.Dispose"/> method.
        ///     Value of this parameter set to <see langword="false"/> indicates that the method is called from finalizer.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        ///     Throws an <see cref="ObjectDisposedException"/> if current instance is disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     Current instance is disposed.
        /// </exception>
        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                this.ThrowObjectDisposedException();
            }
        }

        /// <summary>
        ///     Marks current instance as disposed.
        /// </summary>
        /// <returns>
        ///     A value that indicates whether current instance has been already marked as disposed.
        /// </returns>
        private bool MarkAsDisposed()
        {
            return Interlocked.Exchange(ref this.isDisposed, 1) == 1;
        }

        /// <summary>
        ///     Throws an <see cref="ObjectDisposedException"/>  object.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        ///     Current object is disposed.
        /// </exception>
        private void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException(this.GetType().FullName);
        }
    }
}
