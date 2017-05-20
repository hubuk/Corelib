// -----------------------------------------------------------------------
// <copyright file="ObjectSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using Leet.Testing;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="object"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="object"/> class.
    /// </typeparam>
    public abstract class ObjectSpecification<TSut> : InstanceSpecification<TSut>
    {
        /// <summary>
        ///     Name of the <c>Finalize</c> method.
        /// </summary>
        protected const string MemberName_Finalize = "Finalize";

        /// <summary>
        ///     Checks whether <see cref="object.ToString()"/> method returns non-<see langword="null"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToString_Void_Always_ReturnsNotNull(TSut sut)
        {
            // Fixture setup

            // Exercise system
            string result = sut.ToString();

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.Equals(object)"/> method returns <see langword="false"/> when called with <see langword="null"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_Object_CalledWithNull_ReturnsFalse(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(null);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.Equals(object)"/> method returns <see langword="true"/> when called with <see langword="this"/> reference.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_Object_CalledWithThis_ReturnsTrue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            bool result = sut.Equals(sut);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.Equals(object)"/> method returns same results no matter for which object it is beeing invoked.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        /// <param name="other">
        ///     Other instance to compare ro.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_Object_ForSwappedInstances_ReturnsSameResult(TSut sut, TSut other)
        {
            // Fixture setup
            bool expectedResult = other.Equals(sut);

            // Exercise system
            bool result = sut.Equals(other);

            // Verify outcome
            Assert.Equal(expectedResult, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.Equals(object)"/> returns <see langword="false"/> if called with plain object as a paremter.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Equals_Object_ForDifferentType_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            object other = new object();

            // Exercise system
            bool result = sut.Equals(other);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="object.GetHashCode"/> method returns always the same value upon subsequent calls.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetHashCode_Void_Alwyas_ReturnsSameValue(TSut sut)
        {
            // Fixture setup

            // Exercise system
            int firstValue = sut.GetHashCode();
            int secondValue = sut.GetHashCode();

            // Verify outcome
            Assert.Equal(firstValue, secondValue);

            // Teardown
        }
    }
}
