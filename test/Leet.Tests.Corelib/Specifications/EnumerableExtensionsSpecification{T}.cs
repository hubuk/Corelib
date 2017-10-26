// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensionsSpecification{T}.cs" company="Leet">
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
    using AutoFixture;
    using Leet.Testing;
    using NSubstitute;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="EnumerableExtensions"/> class.
    /// </summary>
    /// <typeparam name="T">
    ///     Type of the enumerable collection items.
    /// </typeparam>
    [StaticSpecificationType(typeof(EnumerableExtensions))]
    public abstract class EnumerableExtensionsSpecification<T> : StaticSpecification
    {
        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Iterate{T}(IEnumerable{T})"/> method throws an <see cref="ArgumentNullException"/>
        ///     when called on <see langword="null"/> reference.
        /// </summary>
        [Paradigm]
        public void Iterate_IEnumerableOfT_ForNullCollection_Throws()
        {
            // Fixture setup
            IEnumerable<T> source = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(source), () =>
            {
                source.Iterate();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Iterate{T}(IEnumerable{T})"/> method is using
        ///     <see cref="IEnumerable{T}.GetEnumerator"/> method.
        /// </summary>
        [Paradigm]
        public void Iterate_IEnumerableOfT_Always_GetsEnumerator()
        {
            // Fixture setup
            IEnumerable<T> enumerable = Substitute.For<IEnumerable<T>>();

            // Exercise system
            enumerable.Iterate();

            // Verify outcome
            enumerable.Received(1).GetEnumerator();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Iterate{T}(IEnumerable{T})"/> method iterates all the source's collection elements.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the collection under test.
        /// </param>
        [Paradigm]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Iterate_IEnumerableOfT_Always_VisitsAllElements(int collectionSize)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            IEnumerator<T> sourceEnumerator = source.GetEnumerator();
            IEnumerator<T> enumeratorMock = Substitute.For<IEnumerator<T>>();
            enumeratorMock.MoveNext().Returns(x => sourceEnumerator.MoveNext());
            IEnumerable<T> enumerable = Substitute.For<IEnumerable<T>>();
            enumerable.GetEnumerator().Returns(enumeratorMock);

            // Exercise system
            enumerable.Iterate();

            // Verify outcome
            enumeratorMock.Received(collectionSize + 1).MoveNext();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Iterate{T}(IEnumerable{T})"/> method calls <see cref="IDisposable.Dispose"/> method on
        ///     accessed enumerator.
        /// </summary>
        [Paradigm]
        public void Iterate_IEnumerableOfT_Always_CallDispose()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> source = fixture.CreateMany<T>();
            IEnumerator<T> sourceEnumerator = source.GetEnumerator();
            IEnumerator<T> enumeratorMock = Substitute.For<IEnumerator<T>>();
            enumeratorMock.MoveNext().Returns(x => sourceEnumerator.MoveNext());
            IEnumerable<T> enumerable = Substitute.For<IEnumerable<T>>();
            enumerable.GetEnumerator().Returns(enumeratorMock);

            // Exercise system
            enumerable.Iterate();

            // Verify outcome
            enumeratorMock.Received(1).Dispose();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method throws an <see cref="ArgumentNullException"/>
        ///     when called on <see langword="null"/> reference.
        /// </summary>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void Insert_IEnumerableOfT_Int32_T_ForNullCollection_Throws(int insertAt)
        {
            // Fixture setup
            IEnumerable<T> source = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(source), () =>
            {
                source.Insert(0, default(T));
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method throws
        ///     an <see cref="ArgumentOutOfRangeException"/> when called with index less than zero.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, -1)]
        [InlineData(1, int.MinValue)]
        [InlineData(2, -1)]
        [InlineData(3, int.MinValue)]
        public void Insert_IEnumerableOfT_Int32_T_CalledWithIndexLessThanZero_Throws(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(nameof(insertAt), () =>
            {
                source.Insert(insertAt, newItem);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method deffers throw
        ///     of an <see cref="ArgumentOutOfRangeException"/> when called with index from outside of the collection.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, int.MaxValue)]
        [InlineData(3, int.MaxValue)]
        public void Insert_IEnumerableOfT_Int32_T_CalledWithIndexGreaterThanCollectionSizePlusOne_DeffersThrow(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            T newItem = fixture.Create<T>();
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(nameof(insertAt), () =>
            {
                result.Iterate();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method returns a lazily
        ///     computed collection.
        /// </summary>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        /// <param name="newItem">
        ///     A new item to insert at the specified location.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Insert_IEnumerableOfT_Int32_T_Always_EvaluatesLazily(int insertAt, T newItem)
        {
            // Fixture setup
            IEnumerable<T> source = Substitute.For<IEnumerable<T>>();

            // Exercise system
            source.Insert(insertAt, newItem);

            // Verify outcome
            source.DidNotReceive().GetEnumerator();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method returns a non-<see langword="null"/>
        ///     collection.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IEnumerableOfT_Int32_T_IntoCollection_ReturnsNotNullCollection(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method returns a new collection
        ///     each time.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IEnumerableOfT_Int32_T_IntoCollection_CreatesNewCollection(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> firstResult = source.Insert(insertAt, newItem);
            IEnumerable<T> secondResult = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.NotSame(firstResult, secondResult);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method adds only one item into
        ///     a specified collection.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IEnumerableOfT_Int32_T_IntoCollection_AddsOnlyOneItem(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.Equal(expectedCollectionSize, result.Count());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method does not
        ///     change item before insertion index.
        ///     with a specified item added.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IEnumerableOfT_Int32_T_IntoCollection_DoesNotChangeItemsBeforeInsertIndex(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = new Fixture().Customize(new DomainCustomization());
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.True(result.Take(insertAt).SequenceEqual(source.Take(insertAt)));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method insert specified item
        ///     at specified index.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IEnumerableOfT_Int32_T_IntoCollection_AddsItemAtTheSpecifiedIndex(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = new Fixture().Customize(new DomainCustomization());
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.Equal(newItem, result.ElementAt(insertAt));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.Insert{T}(IEnumerable{T}, int, T)"/> method does not change items
        ///     after the insertion index.
        /// </summary>
        /// <param name="collectionSize">
        ///     Initial number of the elements in the collection.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item should be added.
        /// </param>
        [Paradigm]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        public void Insert_IEnumerableOfT_Int32_T_IntoCollection_DoesNotChangeItemsAfterInsertIndex(int collectionSize, int insertAt)
        {
            // Fixture setup
            IFixture fixture = new Fixture().Customize(new DomainCustomization());
            IEnumerable<T> source = fixture.CreateMany<T>(collectionSize);
            T newItem = fixture.Create<T>();
            int expectedCollectionSize = collectionSize + 1;

            // Exercise system
            IEnumerable<T> result = source.Insert(insertAt, newItem);

            // Verify outcome
            Assert.True(result.Skip(insertAt + 1).SequenceEqual(source.Skip(insertAt)));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> collection.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForNullCollections_Throws()
        {
            // Fixture setup
            IEnumerable<IEnumerable<T>> collection = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.CartesianProduct();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method throws
        ///     an <see cref="ArgumentNullException"/> when called with collection with <see langword="null"/> item.
        /// </summary>
        /// <param name="items">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForNullItem_Throws(IEnumerable<IEnumerable<T>> items)
        {
            // Fixture setup
            var collections = items.Insert(1, null);

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentException>(nameof(collections), () =>
            {
                collections.CartesianProduct();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method always returns
        ///     non-<see langword="null"/> collection.
        /// </summary>
        /// <param name="collection">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_Always_ReturnsNotNull(IEnumerable<IEnumerable<T>> collection)
        {
            // Fixture setup

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method iterates collection eagerly.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of item collection for the cartesian product.
        /// </param>
        [Paradigm]
        [InlineData(1)]
        [InlineData(3)]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_Always_EvaluatesEagerly(int itemCount)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int enumeratorsGet = 0;
            List<IEnumerable<T>> collection = new List<IEnumerable<T>>(itemCount);

            for (int i = 0; i < itemCount; ++i)
            {
                var sourcItem = fixture.Create<IEnumerable<T>>();
                var item = Substitute.For<IEnumerable<T>>();
                item.GetEnumerator().Returns(x =>
                {
                    ++enumeratorsGet;
                    return sourcItem.GetEnumerator();
                });

                collection.Add(item);
            }

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.Equal(itemCount, enumeratorsGet);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method
        ///     iterates collections till empty collection.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of item collection for the cartesian product.
        /// </param>
        [Paradigm]
        [InlineData(1)]
        [InlineData(3)]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_Always_EvaluatesTillEmpty(int itemCount)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int enumeratorsGet = 0;
            List<IEnumerable<T>> collection = new List<IEnumerable<T>>(itemCount);

            for (int i = 0; i < itemCount; ++i)
            {
                var sourcItem = fixture.Create<IEnumerable<T>>();
                var item = Substitute.For<IEnumerable<T>>();
                item.GetEnumerator().Returns(x =>
                {
                    ++enumeratorsGet;
                    return sourcItem.GetEnumerator();
                });

                collection.Add(item);
            }

            collection.Insert(0, Enumerable.Empty<T>());

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.Equal(0, enumeratorsGet);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method iterates
        ///     each collection item eagerly.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfIEnumerableIfT_Always_EvaluatesEagerlyForEachCollection()
        {
            // Fixture setup
            var collection = Substitute.For<IEnumerable<IEnumerable<T>>>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.All(collection, item => item.Received(1).GetEnumerator());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method returns collection
        ///     which iteration is not using source collection enumerator.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_Always_ReturnsIndependentCollection()
        {
            // Fixture setup
            var collection = Substitute.For<IEnumerable<IEnumerable<T>>>();
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();
            collection.ClearReceivedCalls();

            // Exercise system
            result.Iterate();

            // Verify outcome
            collection.DidNotReceive().GetEnumerator();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method always returns
        ///     a new collection each time called.
        /// </summary>
        /// <param name="collection">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_Always_ReturnsNewCollection(IEnumerable<IEnumerable<T>> collection)
        {
            // Fixture setup

            // Exercise system
            IEnumerable<IEnumerable<T>> first = collection.CartesianProduct();
            IEnumerable<IEnumerable<T>> second = collection.CartesianProduct();

            // Verify outcome
            Assert.NotSame(first, second);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method returns
        ///     an empty collection when called with empty collection.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForEmptyCollection_ReturnsEmptyCollection()
        {
            // Fixture setup
            IEnumerable<IEnumerable<T>> empty = Enumerable.Empty<IEnumerable<T>>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = empty.CartesianProduct();

            // Verify outcome
            Assert.True(!result.Any());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method returns
        ///     an empty collection when at least one source collection is empty.
        /// </summary>
        /// <param name="collectionSize">
        ///     Size of the collection to be tested.
        /// </param>
        /// <param name="emptyCollectionIndex">
        ///     Index in the collection at which an empty collection item shall be located.
        /// </param>
        [Paradigm]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        [InlineData(3, 1)]
        [InlineData(3, 2)]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_WithEmptyCollectionElement_ReturnsEmptyCollection(int collectionSize, int emptyCollectionIndex)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<IEnumerable<T>> collection = fixture.CreateMany<IEnumerable<T>>(collectionSize - 1);
            IEnumerable<IEnumerable<T>> collectionWithEmptyCollectionElement = collection.Insert(emptyCollectionIndex, Enumerable.Empty<T>());

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collectionWithEmptyCollectionElement.CartesianProduct();

            // Verify outcome
            Assert.True(!result.Any());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method returns
        ///     a collection of singletons (<see cref="IEnumerable{T}"/> with one element) when called with one collection.
        /// </summary>
        /// <param name="collectionElement">
        ///     A collection to produce cartesian product from.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForOneCollection_ReturnsSingletonsOfElements(IEnumerable<T> collectionElement)
        {
            // Fixture setup
            var collection = new[] { collectionElement };

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.True(result.Select(item => item.Single()).SequenceEqual(collectionElement));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method always returns
        ///     a same collection.
        /// </summary>
        /// <param name="collection">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_Always_ReturnsSameResult(IEnumerable<IEnumerable<T>> collection)
        {
            // Fixture setup
            SequenceEqualityComparer<T> itemComparer = new SequenceEqualityComparer<T>();

            // Exercise system
            IEnumerable<IEnumerable<T>> first = collection.CartesianProduct();
            IEnumerable<IEnumerable<T>> second = collection.CartesianProduct();
            IEnumerable<IEnumerable<T>> third = collection.CartesianProduct();

            // Verify outcome
            Assert.True(first.SequenceEqual(second, itemComparer));
            Assert.True(second.SequenceEqual(third, itemComparer));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method always returns
        ///     a collection with a size equal to multiplication of all source collection sizes.
        /// </summary>
        /// <param name="collectionCount">
        ///     Number of collections to produce cartesian product.
        /// </param>
        [Paradigm]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForNonEmptyCollection_ReturnsCorrectNumberOfItemsInCollection(int collectionCount)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            fixture.Customizations.Add(new RandomMultipleRelay());
            var collection = fixture.CreateMany<IEnumerable<T>>(collectionCount);
            var expectedNumberOfItems = collection.Aggregate(1, (acc, item) => acc * item.Count());

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.Equal(expectedNumberOfItems, result.Count());

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method always returns
        ///     a collection with items of size equal to source collection's count.
        /// </summary>
        /// <param name="collectionCount">
        ///     Number of collections to produce cartesian product.
        /// </param>
        [Paradigm]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForNonEmptyCollection_ReturnsCollectionWithItemsOfCorrectSize(int collectionCount)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            fixture.Customizations.Add(new RandomMultipleRelay());
            var collection = fixture.CreateMany<IEnumerable<T>>(collectionCount);

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct();

            // Verify outcome
            Assert.True(result.All(item => item.Count() == collectionCount));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method always add each item of a new
        ///     source collection to each item of previous result.
        /// </summary>
        /// <param name="collections">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        /// <param name="newCollection">
        ///     A new collection item which addition shall be examined.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForNonEmptyCollection_AddsEachNewItemToEachPreviousResultItems(IEnumerable<IEnumerable<T>> collections, IEnumerable<T> newCollection)
        {
            // Fixture setup
            IEnumerable<IEnumerable<T>> baseResult = collections.CartesianProduct();
            var expectedResult = baseResult.SelectMany(baseResultItem =>
                newCollection.Select(newItem =>
                    baseResultItem.Insert(baseResultItem.Count(), newItem)));
            var appendedCollection = collections.Insert(collections.Count(), newCollection);

            // Exercise system
            IEnumerable<IEnumerable<T>> appendedResult = appendedCollection.CartesianProduct();

            // Verify outcome
            Assert.True(appendedResult.SequenceEqual(expectedResult, new SequenceEqualityComparer<T>()));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> method returns correct result.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfIEnumerableOfT_ForNonEmptyCollection_ReturnsCorrectResult()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            T[][] collections = new[]
            {
                fixture.CreateMany<T>(2).ToArray(),
                fixture.CreateMany<T>(1).ToArray(),
                fixture.CreateMany<T>(3).ToArray(),
            };
            T[][] expectedResult = new[]
            {
                new[]
                {
                    collections[0][0], collections[1][0], collections[2][0],
                },
                new[]
                {
                    collections[0][0], collections[1][0], collections[2][1],
                },
                new[]
                {
                    collections[0][0], collections[1][0], collections[2][2],
                },
                new[]
                {
                    collections[0][1], collections[1][0], collections[2][0],
                },
                new[]
                {
                    collections[0][1], collections[1][0], collections[2][1],
                },
                new[]
                {
                    collections[0][1], collections[1][0], collections[2][2],
                },
            };

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collections.CartesianProduct();

            // Verify outcome
            Assert.True(expectedResult.SequenceEqual(result, new SequenceEqualityComparer<T>()));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> as first argument.
        /// </summary>
        /// <param name="second">
        ///     A collection that shall be passed as a second parameter to <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/>
        ///     method.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_ForFirstNullCollection_Throws(IEnumerable<T> second)
        {
            // Fixture setup
            IEnumerable<T> first = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(first), () =>
            {
                first.CartesianProduct(second);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> as second argument.
        /// </summary>
        /// <param name="first">
        ///     A collection that shall be passed as a first parameter to <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/>
        ///     method.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_ForSecondNullCollection_Throws(IEnumerable<T> first)
        {
            // Fixture setup
            IEnumerable<T> second = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(second), () =>
            {
                first.CartesianProduct(second);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method returns
        ///     non-<see langword="null"/> result.
        /// </summary>
        /// <param name="first">
        ///     A collection that shall be passed as a first parameter to <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/>
        ///     method.
        /// </param>
        /// <param name="second">
        ///     A collection that shall be passed as a second parameter to <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/>
        ///     method.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_Always_ReturnsNotNull(IEnumerable<T> first, IEnumerable<T> second)
        {
            // Fixture setup

            // Exercise system
            IEnumerable<IEnumerable<T>> result = first.CartesianProduct(second);

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method always lazily enumerates
        ///     first collection.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_Always_EnumeratesFirstCollectionLazily()
        {
            // Fixture setup
            var first = Substitute.For<IEnumerable<T>>();
            var second = Substitute.For<IEnumerable<T>>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = first.CartesianProduct(second);

            // Verify outcome
            first.DidNotReceive().GetEnumerator();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method always lazily enumerates
        ///     second collection.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_Always_EnumeratesSecondCollectionLazily()
        {
            // Fixture setup
            var first = Substitute.For<IEnumerable<T>>();
            var second = Substitute.For<IEnumerable<T>>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = first.CartesianProduct(second);

            // Verify outcome
            second.DidNotReceive().GetEnumerator();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method always returns
        ///     a new collection instance.
        /// </summary>
        /// <param name="first">
        ///     A first enumerable collection for cartesian product.
        /// </param>
        /// <param name="second">
        ///     A second enumerable collection for cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_Always_ReturnsNewCollection(IEnumerable<T> first, IEnumerable<T> second)
        {
            // Fixture setup

            // Exercise system
            IEnumerable<IEnumerable<T>> firstResult = first.CartesianProduct(second);
            IEnumerable<IEnumerable<T>> secondResult = first.CartesianProduct(second);

            // Verify outcome
            Assert.NotSame(first, second);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method returns
        ///     an empty collection when called with empty collection as first argument.
        /// </summary>
        /// <param name="second">
        ///     A second enumerable collection for cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_ForFirstEmptyCollection_ReturnsEmptyResult(IEnumerable<T> second)
        {
            // Fixture setup
            IEnumerable<T> first = Enumerable.Empty<T>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = first.CartesianProduct(second);

            // Verify outcome
            Assert.Empty(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method returns
        ///     an empty collection when called with empty collection as second argument.
        /// </summary>
        /// <param name="first">
        ///     A second enumerable collection for cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_ForSecondEmptyCollection_ReturnsEmptyResult(IEnumerable<T> first)
        {
            // Fixture setup
            IEnumerable<T> second = Enumerable.Empty<T>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = first.CartesianProduct(second);

            // Verify outcome
            Assert.Empty(result);
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},IEnumerable{T})"/> method returns
        ///     same collection as <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> overload.
        /// </summary>
        /// <param name="first">
        ///     A first enumerable collection for cartesian product.
        /// </param>
        /// <param name="second">
        ///     A second enumerable collection for cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_IEnumerableOfT_Always_ConformsWithIEnumerableOfIEnumerableOfT(IEnumerable<T> first, IEnumerable<T> second)
        {
            // Fixture setup
            IEnumerable<IEnumerable<T>> collections = new[] { first, second };
            IEnumerable<IEnumerable<T>> expectedResult = collections.CartesianProduct();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = first.CartesianProduct(second);

            // Verify outcome
            Assert.True(expectedResult.SequenceEqual(result, new SequenceEqualityComparer<T>()));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method throws
        ///     an <see cref="ArgumentNullException"/> when called with <see langword="null"/> collection.
        /// </summary>
        /// <param name="power">
        ///     A power of the cartesian product to request.
        /// </param>
        [Paradigm]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void CartesianProduct_IEnumerableOfT_Int32_ForNullCollection_ThrowsException(int power)
        {
            // Fixture setup
            IEnumerable<T> collection = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
            {
                collection.CartesianProduct(power);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method throws
        ///     an <see cref="ArgumentOutOfRangeException"/> when called with negative power.
        /// </summary>
        /// <param name="power">
        ///     A power of the cartesian product to request.
        /// </param>
        [Paradigm]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void CartesianProduct_IEnumerableOfT_Int32_ForNegativePower_ThrowsException(int power)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            var collection = fixture.CreateMany<T>();

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                collection.CartesianProduct(power);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method always return
        ///     non-<see langword="null"/> collection.
        /// </summary>
        /// <param name="power">
        ///     A power of the cartesian product to request.
        /// </param>
        [Paradigm]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CartesianProduct_IEnumerableOfT_Int32_Always_ReturnsNotNull(int power)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            var collection = fixture.CreateMany<T>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct(power);

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method always calculates
        ///     result eagerly.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfT_Int32_Always_EvaluatesEagerly()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            var collection = Substitute.For<IEnumerable<T>>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct(fixture.RepeatCount);

            // Verify outcome
            collection.Received(1).GetEnumerator();

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method always
        ///     returns new collection instance.
        /// </summary>
        /// <param name="collection">
        ///     A source collection for the cartesian product.
        /// </param>
        /// <param name="power">
        ///     A power of the cartesian product to request.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_Int32_Always_ReturnsNewCollection(IEnumerable<T> collection, int power)
        {
            // Fixture setup

            // Exercise system
            IEnumerable<IEnumerable<T>> first = collection.CartesianProduct(power);
            IEnumerable<IEnumerable<T>> second = collection.CartesianProduct(power);

            // Verify outcome
            Assert.NotSame(first, second);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method returns empty collection
        ///     when called with zero power.
        /// </summary>
        [Paradigm]
        public void CartesianProduct_IEnumerableOfT_Int32_ForZeroPower_ReturnsEmptyCollection()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            var collection = fixture.CreateMany<T>();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct(0);

            // Verify outcome
            Assert.Empty(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method returns empty collection
        ///     when called with empty collection.
        /// </summary>
        /// <param name="power">
        ///     A power of the cartesian product to request.
        /// </param>
        [Paradigm]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CartesianProduct_IEnumerableOfT_Int32_ForEmptyCollection_ReturnsEmptyCollection(int power)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            var collection = fixture.CreateMany<T>(0);

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct(power);

            // Verify outcome
            Assert.Empty(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{T},int)"/> method returns
        ///     same collection as <see cref="EnumerableExtensions.CartesianProduct{T}(IEnumerable{IEnumerable{T}})"/> overload.
        /// </summary>
        /// <param name="collection">
        ///     A source collection for the cartesian product.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CartesianProduct_IEnumerableOfT_Int32_Always_ConformsWithGeneralOverload(IEnumerable<T> collection)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IEnumerable<IEnumerable<T>> collections = Enumerable.Repeat(collection, fixture.RepeatCount);
            IEnumerable<IEnumerable<T>> expectedResult = collections.CartesianProduct();

            // Exercise system
            IEnumerable<IEnumerable<T>> result = collection.CartesianProduct(fixture.RepeatCount);

            // Verify outcome
            Assert.True(expectedResult.SequenceEqual(result, new SequenceEqualityComparer<T>()));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="EnumerableExtensions.AsArray{T}(IEnumerable{T})"/> method throws <see cref="ArgumentNullException"/>
        ///     when called with <see langword="null"/> collection parameter.
        /// </summary>
        [Paradigm]
        public void AsArray_IEnumerableOfT_ForNullCollection_Throws()
        {
            // Fixture setup
            IEnumerable<T> collection = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(collection), () =>
            {
                collection.AsArray();
            });

            // Teardown
        }
    }
}
