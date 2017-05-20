// -----------------------------------------------------------------------
// <copyright file="AutoDomainDataDiscoverer.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System.Collections.Generic;
    using Leet.Testing.Paradigmatic;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit.Abstractions;

    /// <summary>
    ///     Allows passing system-under-test modification method name from paradigm tests to the domain fixture.
    /// </summary>
    public class AutoDomainDataDiscoverer : NoPreDiscoveryDataDiscoverer
    {
        /// <summary>
        ///     Returns the data to be used to test the theory.
        /// </summary>
        /// <param name="dataAttribute">
        ///     The data attribute being discovered.
        /// </param>
        /// <param name="testMethod">
        ///     The method that is being tested/discovered.
        /// </param>
        /// <returns>
        ///     The theory data (or null during discovery, if not enough information is available to enumerate the data).
        /// </returns>
        public override IEnumerable<object[]> GetData(IAttributeInfo dataAttribute, IMethodInfo testMethod)
        {
            if (dataAttribute is IReflectionAttributeInfo reflectionDataAttribute &&
                reflectionDataAttribute.Attribute is AutoDomainDataAttribute attribute &&
                testMethod is ParadigmReflectionMethodInfo reflectionTestMethod)
            {
                return attribute.GetData(reflectionTestMethod.MethodInfo, reflectionTestMethod.SutModificationMethod);
            }

            return base.GetData(dataAttribute, testMethod);
        }
    }
}
