// -----------------------------------------------------------------------
// <copyright file="SequenceEqualityComparerTests{T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Collections.Generic;
    using Leet.Specifications;
    using Leet.Testing;
    using Xunit;

    /// <summary>
    ///     A class that tests <see cref="SequenceEqualityComparer{T}"/> class for a specified type parameter.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects contained in the sequence.
    /// </typeparam>
    public abstract class SequenceEqualityComparerTests<T> : SequenceEqualityComparerSpecification<SequenceEqualityComparer<T>, T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SequenceEqualityComparerTests{T}"/> class.
        /// </summary>
        internal SequenceEqualityComparerTests()
        {
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}(IEqualityComparer{T})"/> constructor throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> item's comparer.
        /// </summary>
        [Paradigm]
        public void Constructor_IEqualityComparerOfT_ForNullItemComparer_Throws()
        {
            // Fixture setup
            IEqualityComparer<T> itemComparer = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(itemComparer), () =>
            {
                new SequenceEqualityComparer<T>(itemComparer);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}(IEqualityComparer{T})"/> constructor assigns its parameter to
        ///     <see cref="SequenceEqualityComparer{T}.ItemComparer"/> property.
        /// </summary>
        /// <param name="itemComparer">
        ///     A collection item's comparer.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_IEqualityComparerOfT_Always_AssignsItemComparer(IEqualityComparer<T> itemComparer)
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemComparer);

            // Exercise system
            IEqualityComparer<T> storedComparer = sut.ItemComparer;

            // Verify outcome
            Assert.Same(itemComparer, storedComparer);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}()"/> constructor assigns default
        ///     comparer for <typeparamref name="T"/> type to <see cref="SequenceEqualityComparer{T}.ItemComparer"/> property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_AssignsItemComparer()
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>();

            // Exercise system
            IEqualityComparer<T> storedComparer = sut.ItemComparer;

            // Verify outcome
            Assert.Same(EqualityComparer<T>.Default, storedComparer);

            // Teardown
        }
    }
}
