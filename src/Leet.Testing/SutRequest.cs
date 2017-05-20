// -----------------------------------------------------------------------
// <copyright file="SutRequest.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;

    /// <summary>
    ///     Represents a request for system-under-test object.
    /// </summary>
    public class SutRequest
    {
        /// <summary>
        ///     Type of the system-under-test object requested.
        /// </summary>
        private readonly Type sutType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SutRequest"/> class.
        /// </summary>
        /// <param name="sutType">
        ///     Type of the system-under-test object requested.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="sutType"/> is <see langword="null"/>.
        /// </exception>
        public SutRequest(Type sutType)
        {
            if (object.ReferenceEquals(sutType, null))
            {
                throw new ArgumentNullException(nameof(sutType));
            }

            this.sutType = sutType;
        }

        /// <summary>
        ///     Gets the type of the system-under-test object requested.
        /// </summary>
        public Type SutType
        {
            get
            {
                return this.sutType;
            }
        }
    }
}
