// -----------------------------------------------------------------------
// <copyright file="TestDataProviderBase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    ///     A basic implementation of the test data provider that may be pointed by <see cref="ClassDataAttribute"/>.
    /// </summary>
    public abstract class TestDataProviderBase : IEnumerable<object[]>
    {
        /// <summary>
        ///     A collection of the arguments for the test method.
        /// </summary>
        private readonly IEnumerable<object[]> data;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestDataProviderBase"/> class.
        /// </summary>
        /// <param name="data">
        ///     The collection of the arguments for the test method.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        protected TestDataProviderBase(IEnumerable<object[]> data)
        {
            if (object.ReferenceEquals(data, null))
            {
                throw new ArgumentNullException(nameof(data));
            }

            this.data = data;
        }

        /// <summary>
        ///     Gets a collection of the arguments for the test method.
        /// </summary>
        public IEnumerable<object[]> Data
        {
            get
            {
                return this.data;
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
