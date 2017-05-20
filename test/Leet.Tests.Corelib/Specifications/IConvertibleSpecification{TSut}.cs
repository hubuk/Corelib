// -----------------------------------------------------------------------
// <copyright file="IConvertibleSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Globalization;
    using Leet.Testing;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IConvertible"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IConvertible"/> class.
    /// </typeparam>
    public abstract class IConvertibleSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : IConvertible
    {
        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToBoolean(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToBoolean_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToBoolean(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToByte(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToByte_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToByte(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToChar(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToChar_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToChar(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToDateTime(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToDateTime_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToDateTime(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToDecimal(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToDecimal_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToDecimal(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToDouble(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToDouble_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToDouble(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToInt16(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToInt16_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToInt16(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToInt32(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToInt32_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToInt32(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToInt64(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToInt64_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToInt64(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToSByte(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToSByte_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToSByte(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToSingle(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToSingle_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToSingle(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToString(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToString_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToString(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToUInt16(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToUInt16_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToUInt16(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToUInt32(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToUInt32_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToUInt32(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToUInt64(IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToUInt64_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToUInt64(null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToType(Type, IFormatProvider)"/> method throws
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> type.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToType_Type_IFormatProvider_CalledWithNullType_ThrowsArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.ToType(null, CultureInfo.InvariantCulture);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IConvertible.ToType(Type, IFormatProvider)"/> method does not throw
        ///     <see cref="ArgumentNullException"/> when called with <see langword="null"/> format provider.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void ToType_IFormatProvider_CalledWithNullFormatProvider_DoesNotThrowArgumentNullException(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            try
            {
                sut.ToType(sut.GetType(), null);
            }
            catch (Exception e) when (!(e is ArgumentNullException))
            {
            }

            // Teardown
        }
    }
}
