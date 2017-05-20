// -----------------------------------------------------------------------
// <copyright file="AutoDomainDataDiscovererSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Reflection;
    using Leet.Testing;
    using Leet.Testing.Paradigmatic;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     A class that specifies behavior for <see cref="AutoDomainDataDiscoverer"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="AutoDomainDataDiscoverer"/> interface.
    /// </typeparam>
    public abstract class AutoDomainDataDiscovererSpecification<TSut> : ObjectSpecification<TSut>
        where TSut : AutoDomainDataDiscoverer
    {
        /// <summary>
        ///     Checks whether the <see cref="IDataDiscoverer.GetData(IAttributeInfo, IMethodInfo)"/> method
        ///     throws an exception when called with a <see langword="null"/> data attribute.
        /// </summary>
        /// <param name="sut">
        ///     System-under-test instance.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void GetData_IAttributeInfo_IMethodInfo_CalledWithRightParameters_CallsAttributeImplementation(TSut sut)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            IReflectionAttributeInfo dataAttribute = Substitute.For<IReflectionAttributeInfo>();
            AutoDomainDataAttribute attribute = Substitute.For<AutoDomainDataAttribute>();
            dataAttribute.Attribute.Returns(attribute);
            IReflectionMethodInfo inner = Substitute.For<IReflectionMethodInfo>();
            string sutModificationMethod = fixture.Create<string>();
            ParadigmReflectionMethodInfo testMethod = Substitute.ForPartsOf<ParadigmReflectionMethodInfo>(inner, sutModificationMethod);
            MethodInfo methodInfo = Substitute.For<MethodInfo>();
            testMethod.MethodInfo.Returns(methodInfo);

            // Exercise system
            _ = sut.GetData(dataAttribute, testMethod);

            // Verify outcome
            attribute.Received(1).GetData(methodInfo, sutModificationMethod);

            // Teardown
        }
    }
}
