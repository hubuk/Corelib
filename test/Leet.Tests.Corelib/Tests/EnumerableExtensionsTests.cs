// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensionsTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using Leet.Testing;
    using Leet.Testing.Idioms;

    /// <summary>
    ///     A class that tests <see cref="EnumerableExtensions"/> class in a conformance to
    ///     its specified behavior.
    /// </summary>
    [StaticSpecificationType(typeof(EnumerableExtensions))]
    public sealed class EnumerableExtensionsTests : StaticSpecification
    {
        /// <summary>
        ///     Checks whether the <see cref="EnumerableExtensions"/> type is an extension class.
        /// </summary>
        [Paradigm]
        public void Type_Is_ExtensionClass()
        {
            // Fixture setup
            Type sutType = typeof(EnumerableExtensions);
            var assertion = new IsExtensionClassAssertion();

            // Exercise system
            // Verify outcome
            assertion.Verify(sutType);

            // Teardown
        }
    }
}
