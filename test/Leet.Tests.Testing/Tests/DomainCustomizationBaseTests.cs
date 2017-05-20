// -----------------------------------------------------------------------
// <copyright file="DomainCustomizationBaseTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet.Specifications;
    using Leet.Testing;
    using Leet.Testing.Reflection;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="DomainCustomizationBase"/> class.
    /// </summary>
    public class DomainCustomizationBaseTests : ObjectSpecification<DomainCustomizationBase>
    {
        /// <summary>
        ///     Checks whether the <see cref="DomainCustomizationBase(IEnumerable{ICustomization})"/> constructor throws an
        ///     exception when called with <see langword="null"/> collection.
        /// </summary>
        [Paradigm]
        public void Constructor_IEnumerableOfICustomization_CalledWithNullCollection_Throws()
        {
            // Fixture setup
            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);
            IEnumerable<ICustomization> source = null;

            // Exercise system
            ArgumentNullException exception = domainCustomizationBaseType.InvokeConstructorViaProxyWithException<ArgumentNullException>(
                new Type[] { typeof(IEnumerable<ICustomization>), },
                new object[] { source, });

            // Verify outcome
            Assert.Equal(nameof(source), exception.ParamName);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainCustomizationBase(IEnumerable{ICustomization})"/> constructor assigns passed collection.
        /// </summary>
        /// <param name="customizations">
        ///     Domain customizations to pass to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_IEnumerableOfICustomization_Always_AssignsCustomizations(IEnumerable<ICustomization> customizations)
        {
            // Fixture setup
            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);

            // Exercise system
            DomainCustomizationBase custimization = (DomainCustomizationBase)domainCustomizationBaseType.InvokeConstructorViaProxy(
                new Type[] { typeof(IEnumerable<ICustomization>), },
                new object[] { customizations, });

            // Verify outcome
            Assert.True(customizations.SequenceEqual(custimization.Customizations));

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainCustomizationBase(ICustomization[])"/> constructor throws an
        ///     exception when called with <see langword="null"/> collection.
        /// </summary>
        [Paradigm]
        public void Constructor_ArrayOfICustomization_CalledWithNullCollection_Throws()
        {
            // Fixture setup
            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);
            ICustomization[] customizations = null;

            // Exercise system
            ArgumentNullException exception = domainCustomizationBaseType.InvokeConstructorViaProxyWithException<ArgumentNullException>(
                    new Type[] { typeof(ICustomization[]), },
                    new object[] { customizations, });

            // Verify outcome
            Assert.Equal(nameof(customizations), exception.ParamName);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainCustomizationBase(IEnumerable{ICustomization})"/> constructor assigns passed collection.
        /// </summary>
        /// <param name="customizations">
        ///     Domain customizations to pass to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_ArrayOfICustomization_Always_AssignsCustomizations(IEnumerable<ICustomization> customizations)
        {
            // Fixture setup
            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);

            // Exercise system
            DomainCustomizationBase custimization = (DomainCustomizationBase)domainCustomizationBaseType.InvokeConstructorViaProxy(
                new Type[] { typeof(ICustomization[]), },
                new object[] { customizations.ToArray(), });

            // Verify outcome
            Assert.True(customizations.SequenceEqual(custimization.Customizations));

            // Teardown
        }
    }
}
