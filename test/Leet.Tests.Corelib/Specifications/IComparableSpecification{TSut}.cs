// -----------------------------------------------------------------------
// <copyright file="IComparableSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Leet.Testing;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IComparable"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IComparable"/> class.
    /// </typeparam>
    public abstract class IComparableSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : IComparable
    {
        /// <summary>
        ///     Checks whether <see cref="IComparable.CompareTo(object)"/> method called with same instance
        ///     returns 0.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CompareTo_TSut_CalledWithSameInstance_ReturnsZero(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            Assert.Equal(0, sut.CompareTo(sut));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IComparable.CompareTo(object)"/> method when called with <see langword="null"/>
        ///     reference returns 1.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CompareTo_TSut_CalledWithNull_ReturnsOne(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            int expectedResult = object.ReferenceEquals(other, null) ? 1 : 0;

            // Exercise system
            // Verify outcome
            Assert.Equal(expectedResult, sut.CompareTo(other));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IComparable.CompareTo(object)"/> method when called with object of diferent type
        ///     throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CompareTo_TSut_CalledWithObjectOfDifferentType_ThrowsArgumentException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentException>(() =>
            {
                sut.CompareTo(new object());
            });

            // Teardown
        }
    }
}
