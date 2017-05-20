// -----------------------------------------------------------------------
// <copyright file="IEqualityComparerSpecification{TSut,T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections.Generic;
    using Leet.Testing;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IEqualityComparer{T}"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IEqualityComparer{T}"/> class.
    /// </typeparam>
    /// <typeparam name="T">
    ///     The type of objects to compare.
    /// </typeparam>
    public abstract class IEqualityComparerSpecification<TSut, T> : InstanceSpecification<TSut>
        where TSut : IEqualityComparer<T>
    {
        /// <summary>
        ///     Checks whether <see cref="IEqualityComparer{T}.Equals(T, T)"/> method returns <see langword="true"/>
        ///     for both default instance parameters.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_T_T_ForBothDefaultInstanceParameters_ReturnsTrue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(default(T), default(T));

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEqualityComparer{T}.Equals(T, T)"/> method returns <see langword="false"/>
        ///     for default instance as first parameter and non-default instance as second parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="second">
        ///     Second parameter instance.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_T_T_ForFirstDefaultInstanceParametersAndSecondNonDefault_ReturnsFalse(TSut sut, T second)
        {
            // Fixture setup
            var defaultInstance = default(T);

            // Exercise system
            bool result = (sut as IEqualityComparer<T>).Equals(defaultInstance, second);

            // Verify outcome
            Assert.False(result && object.ReferenceEquals(defaultInstance, null));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEqualityComparer{T}.Equals(T, T)"/> method returns <see langword="false"/>
        ///     for non-default instance as first parameter and default instance as second parameter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="first">
        ///     First parameter instance.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_T_T_ForSecondDefaultInstanceParametersAndSecondNonDefault_ReturnsFalse(TSut sut, T first)
        {
            // Fixture setup
            var defaultInstance = default(T);

            // Exercise system
            bool result = (sut as IEqualityComparer<T>).Equals(first, defaultInstance);

            // Verify outcome
            Assert.False(result && object.ReferenceEquals(defaultInstance, null));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEqualityComparer{T}.Equals(T, T)"/> method returns <see langword="true"/>
        ///     for both parameters as same instance of an object.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="instance">
        ///     A parameter instance to compare.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_T_T_ForBothSameInstances_ReturnsTrue(TSut sut, T instance)
        {
            // Fixture setup

            // Exercise system
            bool result = (sut as IEqualityComparer<T>).Equals(instance, instance);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IEqualityComparer{T}.GetHashCode(T)"/> method returns same values upon subsequent calls.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="instance">
        ///     Instance of the object which has code shall be obtained.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetHashCode_T_Always_ReturnsSameValue(TSut sut, T instance)
        {
            // Fixture setup

            // Exercise system
            int firstResult = sut.GetHashCode(instance);
            int secondResult = sut.GetHashCode(instance);
            int thirdResult = sut.GetHashCode(instance);

            // Verify outcome
            Assert.Equal(firstResult, secondResult);
            Assert.Equal(secondResult, thirdResult);

            // Teardown
        }
    }
}
