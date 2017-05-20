// -----------------------------------------------------------------------
// <copyright file="FieldDefinitionDetailsExtensionsTests.cs" company="Leet">
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
    ///     Defines tests for <see cref="FieldDefinitionDetailsExtensions"/> class.
    /// </summary>
    [StaticSpecificationType(typeof(FieldDefinitionDetailsExtensions))]
    public class FieldDefinitionDetailsExtensionsTests : StaticSpecification
    {
        /// <summary>
        ///     Checks whether the <see cref="FieldDefinitionDetailsExtensions"/> type is an extension class.
        /// </summary>
        [Paradigm]
        public void Type_Is_ExtensionClass()
        {
            // Fixture setup
            Type sutType = typeof(FieldDefinitionDetailsExtensions);
            var assertion = new IsExtensionClassAssertion();

            // Exercise system
            // Verify outcome
            assertion.Verify(sutType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="FieldDefinitionDetailsExtensions.ToBindingFlags(FieldDefinitionDetails)"/> returns correct results.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedResult">
        ///     Expected converions result.
        /// </param>
        [Paradigm]
        [InlineData(FieldDefinitionDetails.Default, BindingFlags.Instance | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Const, BindingFlags.Instance | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.ReadOnly, BindingFlags.Instance | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Protected, BindingFlags.Instance | BindingFlags.NonPublic)]
        [InlineData(FieldDefinitionDetails.Const | FieldDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Const | FieldDefinitionDetails.Protected, BindingFlags.Instance | BindingFlags.NonPublic)]
        [InlineData(FieldDefinitionDetails.Const | FieldDefinitionDetails.Protected | FieldDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.ReadOnly | FieldDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.ReadOnly | FieldDefinitionDetails.Protected, BindingFlags.Instance | BindingFlags.NonPublic)]
        [InlineData(FieldDefinitionDetails.ReadOnly | FieldDefinitionDetails.Protected | FieldDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Protected | FieldDefinitionDetails.Declared, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Static, BindingFlags.Static | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Const, BindingFlags.Static | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.ReadOnly, BindingFlags.Static | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Protected, BindingFlags.Static | BindingFlags.NonPublic)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Const | FieldDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Const | FieldDefinitionDetails.Protected, BindingFlags.Static | BindingFlags.NonPublic)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Const | FieldDefinitionDetails.Protected | FieldDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.ReadOnly | FieldDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.ReadOnly | FieldDefinitionDetails.Protected, BindingFlags.Static | BindingFlags.NonPublic)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.ReadOnly | FieldDefinitionDetails.Protected | FieldDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        [InlineData(FieldDefinitionDetails.Static | FieldDefinitionDetails.Protected | FieldDefinitionDetails.Declared, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)]
        public void ToBindingFlags_Always_ReturnsCorrectResults(FieldDefinitionDetails details, BindingFlags expectedResult)
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
