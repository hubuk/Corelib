// -----------------------------------------------------------------------
// <copyright file="SequenceEqualityComparerSpecification{TSut,T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet.Testing;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="SequenceEqualityComparer{T}"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="SequenceEqualityComparer{T}"/> class.
    /// </typeparam>
    /// <typeparam name="T">
    ///     The type of objects contained in the <see cref="IEnumerable{T}"/> collection being compared
    ///     by the <see cref="SequenceEqualityComparer{T}"/> class.
    /// </typeparam>
    public abstract class SequenceEqualityComparerSpecification<TSut, T> : ObjectSpecification<TSut>
        where TSut : SequenceEqualityComparer<T>
    {
        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method returns
        ///     <see langword="true"/> if called with two collection of different type, but same values.
        /// </summary>
        /// <param name="items">
        ///     Enumerable collection of items to compare.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForEnumerableAndArrayWithSameElements_ReturnsTrue(IEnumerable<T> items)
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>();
            T[] array = items.ToArray();

            // Exercise system
            // Verify outcome
            Assert.True(sut.Equals(items, array));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the larger collection.
        /// </param>
        /// <param name="subcollectionSize">
        ///     Size of the smaller collection.
        /// </param>
        [Paradigm]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForFirstCollectionBeingSubcolectionOfSecond_ReturnsFalse(int collectionSize, int subcollectionSize)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            SequenceEqualityComparer<T> sut = fixture.Create<SequenceEqualityComparer<T>>();
            IEnumerable<T> collection = fixture.CreateMany<T>(collectionSize);
            IEnumerable<T> subcollection = collection.Take(subcollectionSize);

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(subcollection, collection));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the larger collection.
        /// </param>
        /// <param name="subcollectionSize">
        ///     Size of the smaller collection.
        /// </param>
        [Paradigm]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSecondCollectionBeingSubcolectionOfFirst_ReturnsFalse(int collectionSize, int subcollectionSize)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            SequenceEqualityComparer<T> sut = fixture.Create<SequenceEqualityComparer<T>>();
            IEnumerable<T> collection = fixture.CreateMany<T>(collectionSize);
            IEnumerable<T> subcollection = collection.Take(subcollectionSize);

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(collection, subcollection));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality using item comparer.
        /// </summary>
        /// <param name="collectionsSize">
        ///     Size of the collections to compare.
        /// </param>
        /// <param name="differentItemsIndex">
        ///     Index at which a different item shall be located.
        /// </param>
        [Paradigm]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_Always_EvaluatesBasedOnItemComparer(int collectionsSize, int differentItemsIndex)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> first = fixture.CreateMany<T>(collectionsSize);
            IEnumerable<T> second = first.ToArray();
            int itemIndex = 0;
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => itemIndex++ != differentItemsIndex);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(first, second));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collectionsSize">
        ///     Size of the collections to compare.
        /// </param>
        /// <param name="differentItemsIndex">
        ///     Index at which a different item shall be located.
        /// </param>
        [Paradigm]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public void Equals_IEnumerableOfT_IEnumerableOfT_Always_EvaluatesTillFirstDifference(int collectionsSize, int differentItemsIndex)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> first = fixture.CreateMany<T>(collectionsSize);
            IEnumerable<T> second = first.ToArray();
            int itemIndex = 0;
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => itemIndex++ != differentItemsIndex);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            sut.Equals(first, second);

            // Verify outcome
            Assert.Equal(differentItemsIndex + 1, itemIndex);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collection">
        ///     A collection instance to compare to itself.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSameReferences_ReturnsTrue(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => false);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            // Verify outcome
            Assert.True(sut.Equals(collection, collection));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collection">
        ///     A collection instance to compare to itself.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSameInstances_NeverComparesItems(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => false);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            sut.Equals(collection, collection);

            // Verify outcome
            itemsComparer.DidNotReceive().Equals(Arg.Any<T>(), Arg.Any<T>());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.Equals(IEnumerable{T}, IEnumerable{T})"/> method
        ///     always evaluates items equality till first diferent items.
        /// </summary>
        /// <param name="collection">
        ///     A collection instance to compare to itself.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_IEnumerableOfT_IEnumerableOfT_ForSameSequences_ReturnsAccordingToItemComparer(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEqualityComparer<T> itemsComparer = fixture.Create<IEqualityComparer<T>>();
            itemsComparer.Equals(Arg.Any<T>(), Arg.Any<T>()).Returns(y => false);
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);
            T[] first = collection.ToArray();
            T[] second = collection.ToArray();

            // Exercise system
            // Verify outcome
            Assert.False(sut.Equals(first, second));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.GetHashCode(IEnumerable{T})"/> method returns
        ///     <see langword="true"/> if called with two collection of different type, but same values.
        /// </summary>
        /// <param name="items">
        ///     Enumerable collection of items to compare.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetHashCode_IEnumerableOfT_ForEnumerableAndArrayWithSameElements_ReturnsSameHashCode(IEnumerable<T> items)
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>();
            T[] array = items.ToArray();

            // Exercise system
            var first = sut.GetHashCode(items);
            var second = sut.GetHashCode(array);

            // Verify outcome
            Assert.Equal(first, second);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.GetHashCode(IEnumerable{T})"/> method
        ///     thrown an exception when called with <see langword="null"/> parameter.
        /// </summary>
        [Paradigm]
        public void GetHashCode_IEnumerableOfT_CalledWithNullObject_ThrowsException()
        {
            // Fixture setup
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>();
            IEnumerable<T> obj = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(obj), () =>
            {
                sut.GetHashCode(obj);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SequenceEqualityComparer{T}.GetHashCode(IEnumerable{T})"/> method
        ///     always evaluates using item comparer.
        /// </summary>
        /// <param name="items">
        ///     Enumerable collection of items to compare.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetHashCode_IEnumerableOfT_Always_EvaluatesBasedOnItemComparer(IEnumerable<T> items)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEqualityComparer<T> itemsComparer = Substitute.For<IEqualityComparer<T>>();
            SequenceEqualityComparer<T> sut = new SequenceEqualityComparer<T>(itemsComparer);

            // Exercise system
            sut.GetHashCode(items);

            // Verify outcome
            itemsComparer.Received().GetHashCode(Arg.Any<T>());

            // Teardown
        }
    }
}
