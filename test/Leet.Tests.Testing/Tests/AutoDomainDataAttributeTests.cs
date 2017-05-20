// -----------------------------------------------------------------------
// <copyright file="AutoDomainDataAttributeTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using Leet.Specifications;
    using Leet.Testing;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     Defines tests for <see cref="AutoDomainDataAttribute"/> class.
    /// </summary>
    public sealed class AutoDomainDataAttributeTests : AutoDomainDataAttributeSpecification<AutoDomainDataAttribute>
    {
        /// <summary>
        ///     Checks whether the <see cref="AutoDomainDataAttribute"/> has parameterless constructor available.
        /// </summary>
        [Paradigm]
        public void Type_Declares_ParameterlessConstructor()
        {
            // Fixture setup
            Type autoDomainDataAttributeType = typeof(AutoDomainDataAttribute);

            // Exercise system
            // Verify outcome
            AssertType.HasParameterlessConstructor(autoDomainDataAttributeType, MemberVisibilityFlags.Public);

            // Teardown
        }
    }
}
