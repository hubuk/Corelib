// -----------------------------------------------------------------------
// <copyright file="AutoDomainDataDiscovererTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Reflection;
    using Leet.Specifications;
    using Leet.Testing;
    using Leet.Testing.Paradigmatic;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     Defines tests for <see cref="AutoDomainDataDiscoverer"/> class.
    /// </summary>
    public sealed class AutoDomainDataDiscovererTests : AutoDomainDataDiscovererSpecification<AutoDomainDataDiscoverer>
    {
        /// <summary>
        ///     Checks whether the <see cref="IDataDiscoverer.GetData(IAttributeInfo, IMethodInfo)"/> method
        ///     calls base implementation throws an exception when called with a <see langword="null"/> data attribute.
        /// </summary>
        /// <param name="sut">
        ///     System-under-test instance.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetData_IAttributeInfo_IMethodInfo_CalledWithNonIReflectionAttributeInfo_CallsBaseImplementation(AutoDomainDataDiscoverer sut)
        {
            // Fixture setup
            IAttributeInfo dataAttribute = Substitute.For<IAttributeInfo>();
            IMethodInfo testMethod = Substitute.For<IMethodInfo>();

            // Exercise system
            // Verify outcome
            _ = sut.GetData(dataAttribute, testMethod);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IDataDiscoverer.GetData(IAttributeInfo, IMethodInfo)"/> method
        ///     throws an exception when called with a <see langword="null"/> data attribute.
        /// </summary>
        /// <param name="sut">
        ///     System-under-test instance.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetData_IAttributeInfo_IMethodInfo_CalledWithNonAutoDomainDataAttribute_CallsBaseImplementation(AutoDomainDataDiscoverer sut)
        {
            // Fixture setup
            IReflectionAttributeInfo dataAttribute = Substitute.For<IReflectionAttributeInfo>();
            Attribute attribute = Substitute.For<Attribute>();
            dataAttribute.Attribute.Returns(attribute);
            IMethodInfo testMethod = Substitute.For<IMethodInfo>();

            // Exercise system
            // Verify outcome
            _ = sut.GetData(dataAttribute, testMethod);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IDataDiscoverer.GetData(IAttributeInfo, IMethodInfo)"/> method
        ///     throws an exception when called with a <see langword="null"/> data attribute.
        /// </summary>
        /// <param name="sut">
        ///     System-under-test instance.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetData_IAttributeInfo_IMethodInfo_CalledWithNonParadigmReflectionMethodInfo_CallsBaseImplementation(AutoDomainDataDiscoverer sut)
        {
            // Fixture setup
            IReflectionAttributeInfo dataAttribute = Substitute.For<IReflectionAttributeInfo>();
            AutoDomainDataAttribute attribute = Substitute.For<AutoDomainDataAttribute>();
            dataAttribute.Attribute.Returns(attribute);
            IMethodInfo testMethod = Substitute.For<IMethodInfo>();

            // Exercise system
            _ = sut.GetData(dataAttribute, testMethod);

            // Verify outcome
            attribute.DidNotReceive().GetData(Arg.Any<MethodInfo>(), Arg.Any<string>());

            // Teardown
        }
    }
}
