// -----------------------------------------------------------------------
// <copyright file="IComparableSpecification{TSut,T}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Leet.Testing;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IComparable{T}"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IComparable{T}"/> class.
    /// </typeparam>
    /// <typeparam name="T">
    ///     The type of objects to compare.
    /// </typeparam>
    public abstract class IComparableSpecification<TSut, T> : InstanceSpecification<TSut>
        where TSut : IComparable<T>, T
    {
        /// <summary>
        ///     The constant name of the <c>Compare</c> member.
        /// </summary>
        protected const string MemberName_Compare = "Compare";

        /// <summary>
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method called with same instance
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
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method when called with <see langword="null"/>
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
            var other = default(TSut);

            // Exercise system
            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || sut.CompareTo(other) == 1);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements also <see cref="IEquatable{T}"/> interface.
        /// </summary>
        [Paradigm]
        public void Type_Implements_IEquatable_T()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            Type iEquatableOfTType = typeof(IEquatable<T>);

            // Exercise system
            // Verify outcome
            AssertType.HierarchyDeclaresInterface(sutType, iEquatableOfTType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_GreaterThan operator.
        /// </summary>
        [Paradigm]
        public void Type_Implements_GreaterThanOperator()
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThan;
            Type[] parameterTypes = new Type[]
            {
                typeof(T), typeof(T),
            };

            // Exercise system
            // Verify outcome
            AssertType.HasMethod(typeof(TSut), MemberDefinitionDetails.Static, methodName, typeof(bool), parameterTypes);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_GreaterThanOrEqual operator.
        /// </summary>
        [Paradigm]
        public void Type_Implements_GreaterThanOrEqualOperator()
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThanOrEqual;
            Type[] parameterTypes = new Type[]
            {
                typeof(T), typeof(T),
            };

            // Exercise system
            // Verify outcome
            AssertType.HasMethod(typeof(TSut), MemberDefinitionDetails.Static, methodName, typeof(bool), parameterTypes);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_LessThan operator.
        /// </summary>
        [Paradigm]
        public void Type_Implements_LessThanOperator()
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThan;
            Type[] parameterTypes = new Type[]
            {
                typeof(T), typeof(T),
            };

            // Exercise system
            // Verify outcome
            AssertType.HasMethod(typeof(TSut), MemberDefinitionDetails.Static, methodName, typeof(bool), parameterTypes);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements op_LessThanOrEqual operator.
        /// </summary>
        [Paradigm]
        public void Type_Implements_LessThanOrEqualOperator()
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThanOrEqual;
            Type[] parameterTypes = new Type[]
            {
                typeof(T), typeof(T),
            };

            // Exercise system
            // Verify outcome
            AssertType.HasMethod(typeof(TSut), MemberDefinitionDetails.Static, methodName, typeof(bool), parameterTypes);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <typeparamref name="TSut"/> type implements static <c>Compare</c> method for
        ///     comparison of two instances of <typeparamref name="T"/> type.
        /// </summary>
        [Paradigm]
        public void Type_Implements_StaticMethod_Compare_T_T()
        {
            // Fixture setup
            string methodName = MemberName_Compare;
            Type[] parameterTypes = new Type[]
            {
                typeof(T), typeof(T),
            };

            // Exercise system
            // Verify outcome
            AssertType.HasMethod(typeof(TSut), MemberDefinitionDetails.Static, methodName, typeof(int), parameterTypes);

            // Teardown
        }

        /// <summary>
        ///     Checks whether GreaterThan operator called with same instance
        ///     returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GreaterThanOperator_CalledWithSameInstance_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThan;
            object[] arguments = new object[]
            {
                sut, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether GreaterThanOrEqual operator called with same instance
        ///     returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GreaterThanOrEqualOperator_CalledWithSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThanOrEqual;
            object[] arguments = new object[]
            {
                sut, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether LessThan operator called with same instance
        ///     returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void LessThanOperator_CalledWithSameInstance_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThan;
            object[] arguments = new object[]
            {
                sut, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether LessThanOrEqual operator called with same instance
        ///     returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void LessThanOrEqualOperator_CalledWithSameInstance_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThanOrEqual;
            object[] arguments = new object[]
            {
                sut, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the GreaterThan operator when called with <see langword="null"/> as a first parameter
        ///     reference returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GreaterThanOperator_CalledWithNullAsFirstParameter_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThan;
            object[] arguments = new object[]
            {
                null, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the GreaterThanOrEqual operator when called with <see langword="null"/> as a first parameter
        ///     reference returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GreaterThanOrEqualOperator_CalledWithNullAsFirstParameter_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThanOrEqual;
            object[] arguments = new object[]
            {
                null, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the LessThan operator when called with <see langword="null"/> as a first parameter
        ///     reference returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void LessThanOperator_CalledWithNullAsFirstParameter_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThan;
            object[] arguments = new object[]
            {
                null, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the LessThanOrEqual operator when called with <see langword="null"/> as a first parameter
        ///     reference returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void LessThanOrEqualOperator_CalledWithNullAsFirstParameter_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThanOrEqual;
            object[] arguments = new object[]
            {
                null, sut,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the GreaterThan operator when called with <see langword="null"/> as a second parameter
        ///     reference returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GreaterThanOperator_CalledWithNullAsSecondParameter_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThan;
            object[] arguments = new object[]
            {
                sut, null,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the GreaterThanOrEqual operator when called with <see langword="null"/> as a second parameter
        ///     reference returns <see langword="false"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GreaterThanOrEqualOperator_CalledWithNullAsSecondParameter_ReturnsFalse(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_GreaterThanOrEqual;
            object[] arguments = new object[]
            {
                sut, null,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.False(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the LessThan operator when called with <see langword="null"/> as a second parameter
        ///     reference returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void LessThanOperator_CalledWithNullAsSecondParameter_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThan;
            object[] arguments = new object[]
            {
                sut, null,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the LessThanOrEqual operator when called with <see langword="null"/> as a second parameter
        ///     reference returns <see langword="true"/>.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void LessThanOrEqualOperator_CalledWithNullAsSecondParameter_ReturnsTrue(TSut sut)
        {
            // Fixture setup
            string methodName = OperatorNames.op_LessThanOrEqual;
            object[] arguments = new object[]
            {
                sut, null,
            };

            // Exercise system
            bool result = (bool)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the static <c>Compare</c> method returns 0 when called with same instance as both parameters.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void StaticCompare_T_T_CalledWithSameInstance_ReturnsZero(TSut sut)
        {
            // Fixture setup
            string methodName = MemberName_Compare;
            object[] arguments = new object[]
            {
                sut, sut,
            };

            // Exercise system
            int result = (int)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.Equal(0, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method when called with <see langword="null"/>
        ///     reference as a first paramter returns 1.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void StaticCompare_T_T_CalledWithNullAsFirstParameter_ReturnsOne(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = MemberName_Compare;
            object[] arguments = new object[]
            {
                other, sut,
            };

            // Exercise system
            int result = (int)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || result == 1);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method when called with <see langword="null"/>
        ///     reference as second parameter returns 1.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void StaticCompare_T_T_CalledWithNullAsSecondParameter_ReturnsOne(TSut sut)
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = MemberName_Compare;
            object[] arguments = new object[]
            {
                sut, other,
            };

            // Exercise system
            int result = (int)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.True(!object.ReferenceEquals(other, null) || result == 1);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IComparable{T}.CompareTo(T)"/> method when called with both <see langword="null"/>
        ///     reference parameters returns 0.
        /// </summary>
        [Paradigm]
        public void StaticCompare_T_T_CalledWithBothNullParameters_ReturnsZero()
        {
            // Fixture setup
            TSut other = default(TSut);
            string methodName = MemberName_Compare;
            object[] arguments = new object[]
            {
                other, other,
            };

            // Exercise system
            int result = (int)typeof(TSut).InvokeMethod(MemberVisibilityFlags.Public, methodName, arguments);

            // Verify outcome
            Assert.Equal(1, result);

            // Teardown
        }
    }
}
