// -----------------------------------------------------------------------
// <copyright file="PropertyDefinitionDetailsTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System.Linq;
    using Leet.Testing;
    using Leet.Testing.Reflection;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="PropertyDefinitionDetails"/> enum.
    /// </summary>
    public class PropertyDefinitionDetailsTests : InstanceSpecification<PropertyDefinitionDetails>
    {
        /// <summary>
        ///     Checks that <see cref="PropertyDefinitionDetails.Default"/> is a default value for the enumeration.
        /// </summary>
        [Paradigm]
        public void None_IsDefaultValue()
        {
            // Fixture setup
            int none = (int)PropertyDefinitionDetails.Default;

            // Exercise system
            // Verify outcome
            Assert.Equal(0, none);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the specified enumeration values are composite or not.
        /// </summary>
        /// <param name="flags">
        ///     Details flags to check.
        /// </param>
        /// <param name="isComposite">
        ///     <see langword="true"/> if the <paramref name="flags"/> shall be composite value;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.Declared, false)]
        [InlineData(PropertyDefinitionDetails.Static, false)]
        [InlineData(PropertyDefinitionDetails.Abstract, false)]
        [InlineData(PropertyDefinitionDetails.Virtual, false)]
        [InlineData(PropertyDefinitionDetails.PublicGetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedGetter, false)]
        [InlineData(PropertyDefinitionDetails.NoGetter, false)]
        [InlineData(PropertyDefinitionDetails.PublicSetter, false)]
        [InlineData(PropertyDefinitionDetails.ProtectedSetter, false)]
        [InlineData(PropertyDefinitionDetails.NoSetter, false)]
        [InlineData(PropertyDefinitionDetails.All, true)]
        public void Values_CompositionStatus(PropertyDefinitionDetails flags, bool isComposite)
        {
            // Fixture setup
            int value = (int)flags;

            // Exercise system
            int bitCount = Enumerable.Range(0, 31).Count(shiftCount => ((1 << shiftCount) & value) != 0);

            // Verify outcome
            Assert.True(bitCount > 0);
            Assert.Equal(isComposite, bitCount > 1);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the specified composite enumeration values are composed of the expected values.
        /// </summary>
        /// <param name="flag">
        ///     Details flags to check.
        /// </param>
        /// <param name="components">
        ///     Expected value components.
        /// </param>
        [Paradigm]
        [InlineData(PropertyDefinitionDetails.All, PropertyDefinitionDetails.Declared | PropertyDefinitionDetails.Static | PropertyDefinitionDetails.Abstract | PropertyDefinitionDetails.Virtual | PropertyDefinitionDetails.PublicGetter | PropertyDefinitionDetails.ProtectedGetter | PropertyDefinitionDetails.NoGetter | PropertyDefinitionDetails.PublicSetter | PropertyDefinitionDetails.ProtectedSetter | PropertyDefinitionDetails.NoSetter)]
        public void Values_CompositeValues(PropertyDefinitionDetails flag, PropertyDefinitionDetails components)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Equal(flag, components);

            // Teardown
        }
    }
}
