// -----------------------------------------------------------------------
// <copyright file="TestDataProviderBaseSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Leet.Testing;
    using Leet.Testing.Reflection;
    using NSubstitute;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="TestDataProviderBase"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="TestDataProviderBase"/> class.
    /// </typeparam>
    public abstract class TestDataProviderBaseSpecification<TSut> : ObjectSpecification<TSut>
        where TSut : TestDataProviderBase
    {
        /// <summary>
        ///     Checks whether <see cref="TestDataProviderBase(IEnumerable{object[]})"/> constructor throws exception
        ///     when called with <see langword="null"/> parameter.
        /// </summary>
        [Paradigm]
        public void Constructor_ISpecimenBuilder_CalledWithNullBuilder_Throws()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            IEnumerable<object[]> data = null;
            Type[] parameterTypes = new Type[] { typeof(IEnumerable<object[]>) };
            object[] parameters = new object[] { data };

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(data), new Action(() =>
            {
                throw sutType.InvokeConstructorViaProxyWithException<ArgumentNullException>(parameterTypes, parameters);
            }));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="TestDataProviderBase(IEnumerable{object[]})"/> constructor initializes
        ///     properties when called with non-<see langword="null"/> parameter.
        /// </summary>
        /// <param name="data">
        ///     Test data parameter to pass to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_ISpecimenBuilder_Always_SetsExtension(IEnumerable<object[]> data)
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            TSut sut = (TSut)sutType.InvokeConstructorViaProxy(new object[] { data });

            // Verify outcome
            Assert.Same(sut.Data, data);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="TestDataProviderBase"/> interpreted as <see cref="IEnumerable{T}">IEnumerable&lt;object[]></see>
        ///     returns same enumerator from a parameter passed to its constructor.
        /// </summary>
        [Paradigm]
        public void TestDataProviderBase_AsIEnumerableOfArrayOfObject_Always_ReturnsSameEnumeratorAsConstructorParameter()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            IEnumerator<object[]> enumerator = Substitute.For<IEnumerator<object[]>>();
            IEnumerable<object[]> enumerable = Substitute.For<IEnumerable<object[]>>();
            enumerable.GetEnumerator().Returns(enumerator);
            TSut sut = (TSut)sutType.InvokeConstructorViaProxy(new object[] { enumerable });
            IEnumerable<object[]> sutAsEnumerable = sut as IEnumerable<object[]>;

            // Exercise system
            var returnedEnumerator = sutAsEnumerable.GetEnumerator();

            // Verify outcome
            Assert.Same(enumerator, returnedEnumerator);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="TestDataProviderBase"/> interpreted as <see cref="IEnumerable"/>
        ///     returns same enumerator from a parameter passed to its constructor.
        /// </summary>
        [Paradigm]
        public void TestDataProviderBase_AsIEnumerable_Always_ReturnsSameEnumeratorAsConstructorParameter()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            IEnumerator<object[]> enumerator = Substitute.For<IEnumerator<object[]>>();
            IEnumerable<object[]> enumerable = Substitute.For<IEnumerable<object[]>>();
            enumerable.GetEnumerator().Returns(enumerator);
            TSut sut = (TSut)sutType.InvokeConstructorViaProxy(new object[] { enumerable });
            IEnumerable sutAsEnumerable = sut as IEnumerable;

            // Exercise system
            var returnedEnumerator = sutAsEnumerable.GetEnumerator();

            // Verify outcome
            Assert.Same(enumerator, returnedEnumerator);

            // Teardown
        }
    }
}
