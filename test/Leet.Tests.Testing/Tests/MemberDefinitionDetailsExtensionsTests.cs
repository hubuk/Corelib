// -----------------------------------------------------------------------
// <copyright file="MemberDefinitionDetailsExtensionsTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Reflection;
    using Leet.Testing;
    using Leet.Testing.Idioms;
    using Leet.Testing.Reflection;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="MemberDefinitionDetailsExtensions"/> class.
    /// </summary>
    [StaticSpecificationType(typeof(MemberDefinitionDetailsExtensions))]
    public class MemberDefinitionDetailsExtensionsTests : StaticSpecification
    {
        /// <summary>
        ///     Checks whether the <see cref="MemberDefinitionDetailsExtensions"/> type is an extension class.
        /// </summary>
        [Paradigm]
        public void Type_Is_ExtensionClass()
        {
            // Fixture setup
            Type sutType = typeof(MemberDefinitionDetailsExtensions);
            var assertion = new IsExtensionClassAssertion();

            // Exercise system
            // Verify outcome
            assertion.Verify(sutType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="MemberDefinitionDetailsExtensions.ToBindingFlags(MemberDefinitionDetails)"/> returns correct results.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedResult">
        ///     Expected converions result.
        /// </param>
        [Paradigm]
        [InlineData(MemberDefinitionDetails.Default, BindingFlags.Instance | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Abstract, BindingFlags.Instance | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Virtual, BindingFlags.Instance | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Protected, BindingFlags.Instance | BindingFlags.NonPublic)]
        [InlineData(MemberDefinitionDetails.Abstract | MemberDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Abstract | MemberDefinitionDetails.Protected, BindingFlags.Instance | BindingFlags.NonPublic)]
        [InlineData(MemberDefinitionDetails.Abstract | MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Protected, BindingFlags.Instance | BindingFlags.NonPublic)]
        [InlineData(MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Static, BindingFlags.Static | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Abstract, BindingFlags.Static | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Virtual, BindingFlags.Static | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Protected, BindingFlags.Static | BindingFlags.NonPublic)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Abstract | MemberDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Abstract | MemberDefinitionDetails.Protected, BindingFlags.Static | BindingFlags.NonPublic)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Abstract | MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Protected, BindingFlags.Static | BindingFlags.NonPublic)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(MemberDefinitionDetails.Static | MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        public void ToBindingFlags_Always_ReturnsCorrectResults(MemberDefinitionDetails details, BindingFlags expectedResult)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = details.ToBindingFlags();

            // Verify outcome
            Assert.Equal(expectedResult, results);

            // Teardown
        }
    }
}
