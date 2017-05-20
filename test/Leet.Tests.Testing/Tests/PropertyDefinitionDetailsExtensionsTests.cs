// -----------------------------------------------------------------------
// <copyright file="PropertyDefinitionDetailsExtensionsTests.cs" company="Leet">
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
    ///     Defines tests for <see cref="PropertyDefinitionDetailsExtensions"/> class.
    /// </summary>
    [StaticSpecificationType(typeof(PropertyDefinitionDetailsExtensions))]
    public class PropertyDefinitionDetailsExtensionsTests : StaticSpecification
    {
        /// <summary>
        ///     Checks whether the <see cref="PropertyDefinitionDetailsExtensions"/> type is an extension class.
        /// </summary>
        [Paradigm]
        public void Type_Is_ExtensionClass()
        {
            // Fixture setup
            Type sutType = typeof(PropertyDefinitionDetailsExtensions);
            var assertion = new IsExtensionClassAssertion();

            // Exercise system
            // Verify outcome
            assertion.Verify(sutType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="PropertyDefinitionDetailsExtensions.ToBindingFlags(PropertyDefinitionDetails)"/> returns
        ///     <see cref="BindingFlags.DeclaredOnly"/> flag only when called with <see cref="PropertyDefinitionDetails.Declared"/> parameter.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedDeclaredOnly">
        ///     <see langword="true"/> if the <see cref="BindingFlags.DeclaredOnly"/> flag shall be part of the method result;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.Default, false)]
        [InlineData(PropertyDefinitionDetails.Declared, true)]
        [InlineData(PropertyDefinitionDetails.Static, false)]
        [InlineData(PropertyDefinitionDetails.Abstract, false)]
        [InlineData(PropertyDefinitionDetails.Virtual, false)]
        [InlineData(PropertyDefinitionDetails.NoGetter, false)]
        [InlineData(PropertyDefinitionDetails.NoSetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter, false)]
        [InlineData(PropertyDefinitionDetails.PublicGetter, false)]
        [InlineData(PropertyDefinitionDetails.PublicSetter, false)]
        [InlineData(PropertyDefinitionDetails.All & ~PropertyDefinitionDetails.Declared, false)]
        [InlineData(PropertyDefinitionDetails.All, true)]
        public void ToBindingFlags_ReturnsDeclaredOnly_OnlyWhenCalledWithDeclared(PropertyDefinitionDetails details, bool expectedDeclaredOnly)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = details.ToBindingFlags();

            // Verify outcome
            Assert.Equal(results.HasFlag(BindingFlags.DeclaredOnly), expectedDeclaredOnly);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="PropertyDefinitionDetailsExtensions.ToBindingFlags(PropertyDefinitionDetails)"/> returns
        ///     <see cref="BindingFlags.Static"/> flag only when called with <see cref="PropertyDefinitionDetails.Static"/> parameter.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedStatic">
        ///     <see langword="true"/> if the <see cref="BindingFlags.Static"/> flag shall be part of the method result;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.Default, false)]
        [InlineData(PropertyDefinitionDetails.Declared, false)]
        [InlineData(PropertyDefinitionDetails.Static, true)]
        [InlineData(PropertyDefinitionDetails.Abstract, false)]
        [InlineData(PropertyDefinitionDetails.Virtual, false)]
        [InlineData(PropertyDefinitionDetails.NoGetter, false)]
        [InlineData(PropertyDefinitionDetails.NoSetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter, false)]
        [InlineData(PropertyDefinitionDetails.PublicGetter, false)]
        [InlineData(PropertyDefinitionDetails.PublicSetter, false)]
        [InlineData(PropertyDefinitionDetails.All & ~PropertyDefinitionDetails.Static, false)]
        [InlineData(PropertyDefinitionDetails.All, true)]
        public void ToBindingFlags_ReturnsStatic_OnlyWhenCalledWithStatic(PropertyDefinitionDetails details, bool expectedStatic)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = details.ToBindingFlags();

            // Verify outcome
            Assert.Equal(results.HasFlag(BindingFlags.Static), expectedStatic);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="PropertyDefinitionDetailsExtensions.ToBindingFlags(PropertyDefinitionDetails)"/> returns
        ///     <see cref="BindingFlags.Instance"/> flag only when called without <see cref="PropertyDefinitionDetails.Static"/> parameter.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedInstance">
        ///     <see langword="true"/> if the <see cref="BindingFlags.Instance"/> flag shall be part of the method result;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.Default, true)]
        [InlineData(PropertyDefinitionDetails.Declared, true)]
        [InlineData(PropertyDefinitionDetails.Static, false)]
        [InlineData(PropertyDefinitionDetails.Abstract, true)]
        [InlineData(PropertyDefinitionDetails.Virtual, true)]
        [InlineData(PropertyDefinitionDetails.NoGetter, true)]
        [InlineData(PropertyDefinitionDetails.NoSetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicGetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicSetter, true)]
        [InlineData(PropertyDefinitionDetails.All & ~PropertyDefinitionDetails.Static, true)]
        [InlineData(PropertyDefinitionDetails.All, false)]
        public void ToBindingFlags_ReturnsInstance_OnlyWhenCalledWithoutStatic(PropertyDefinitionDetails details, bool expectedInstance)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = details.ToBindingFlags();

            // Verify outcome
            Assert.Equal(results.HasFlag(BindingFlags.Instance), expectedInstance);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="PropertyDefinitionDetailsExtensions.ToBindingFlags(PropertyDefinitionDetails)"/> returns
        ///     <see cref="BindingFlags.NonPublic"/> flag when requesting protected or no accessors.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedNonPublic">
        ///     <see langword="true"/> if the <see cref="BindingFlags.NonPublic"/> flag shall be part of the method result;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.Default, true)]
        [InlineData(PropertyDefinitionDetails.Declared, true)]
        [InlineData(PropertyDefinitionDetails.Static, true)]
        [InlineData(PropertyDefinitionDetails.Abstract, true)]
        [InlineData(PropertyDefinitionDetails.Virtual, true)]
        [InlineData(PropertyDefinitionDetails.NoGetter, true)]
        [InlineData(PropertyDefinitionDetails.NoSetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicGetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicSetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicGetter | PropertyDefinitionDetails.NoSetter, false)]
        [InlineData(PropertyDefinitionDetails.PublicSetter | PropertyDefinitionDetails.NoGetter, false)]
        public void ToBindingFlags_ReturnsNonPublic_OnlyWhenRequestedProtectedOrNoAccessors(PropertyDefinitionDetails details, bool expectedNonPublic)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = details.ToBindingFlags();

            // Verify outcome
            Assert.Equal(results.HasFlag(BindingFlags.NonPublic), expectedNonPublic);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="PropertyDefinitionDetailsExtensions.ToBindingFlags(PropertyDefinitionDetails)"/> returns
        ///     <see cref="BindingFlags.Public"/> flag when requesting protected or no accessors.
        /// </summary>
        /// <param name="details">
        ///     Field definition details to convert.
        /// </param>
        /// <param name="expectedPublic">
        ///     <see langword="true"/> if the <see cref="BindingFlags.Public"/> flag shall be part of the method result;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.Default, true)]
        [InlineData(PropertyDefinitionDetails.Declared, true)]
        [InlineData(PropertyDefinitionDetails.Static, true)]
        [InlineData(PropertyDefinitionDetails.Abstract, true)]
        [InlineData(PropertyDefinitionDetails.Virtual, true)]
        [InlineData(PropertyDefinitionDetails.NoGetter, true)]
        [InlineData(PropertyDefinitionDetails.NoSetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicGetter, true)]
        [InlineData(PropertyDefinitionDetails.PublicSetter, true)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter | PropertyDefinitionDetails.NoSetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter | PropertyDefinitionDetails.NoGetter, false)]
        public void ToBindingFlags_ReturnsPublic_OnlyWhenRequestedProtectedOrNoAccessors(PropertyDefinitionDetails details, bool expectedPublic)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = details.ToBindingFlags();

            // Verify outcome
            Assert.Equal(results.HasFlag(BindingFlags.Public), expectedPublic);

            // Teardown
        }
    }
}
