// -----------------------------------------------------------------------
// <copyright file="MemberVisibilityFlagsTests.cs" company="Leet">
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
    ///     Defines tests for <see cref="MemberVisibilityFlags"/> enum.
    /// </summary>
    public class MemberVisibilityFlagsTests : InstanceSpecification<MemberVisibilityFlags>
    {
        /// <summary>
        ///     Checks that <see cref="MemberVisibilityFlags.None"/> is a default value for the enumeration.
        /// </summary>
        [Paradigm]
        public void None_IsDefaultValue()
        {
            // Fixture setup
            int none = (int)MemberVisibilityFlags.None;

            // Exercise system
            // Verify outcome
            Assert.Equal(0, none);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the specified enumeration values are composite or not.
        /// </summary>
        /// <param name="flags">
        ///     Visibility flags to check.
        /// </param>
        /// <param name="isComposite">
        ///     <see langword="true"/> if the <paramref name="flags"/> shall be composite value;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        [Paradigm]
        [InlineData(MemberVisibilityFlags.Public, false)]
        [InlineData(MemberVisibilityFlags.Family, false)]
        [InlineData(MemberVisibilityFlags.Assembly, false)]
        [InlineData(MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(MemberVisibilityFlags.Private, false)]
        [InlineData(MemberVisibilityFlags.AnyFamily, true)]
        [InlineData(MemberVisibilityFlags.AnyAssembly, true)]
        [InlineData(MemberVisibilityFlags.PublicFamily, true)]
        [InlineData(MemberVisibilityFlags.OnlyAssembly, true)]
        [InlineData(MemberVisibilityFlags.All, true)]
        public void Values_CompositionStatus(MemberVisibilityFlags flags, bool isComposite)
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
        ///     Visibility flags to check.
        /// </param>
        /// <param name="components">
        ///     Expected value components.
        /// </param>
        [Paradigm]
        [InlineData(MemberVisibilityFlags.All, MemberVisibilityFlags.Private | MemberVisibilityFlags.Family | MemberVisibilityFlags.FamilyAndAssembly | MemberVisibilityFlags.FamilyOrAssembly | MemberVisibilityFlags.Assembly | MemberVisibilityFlags.Public)]
        [InlineData(MemberVisibilityFlags.AnyAssembly, MemberVisibilityFlags.FamilyAndAssembly | MemberVisibilityFlags.FamilyOrAssembly | MemberVisibilityFlags.Assembly)]
        [InlineData(MemberVisibilityFlags.AnyFamily, MemberVisibilityFlags.Family | MemberVisibilityFlags.FamilyAndAssembly | MemberVisibilityFlags.FamilyOrAssembly)]
        [InlineData(MemberVisibilityFlags.OnlyAssembly, MemberVisibilityFlags.FamilyAndAssembly | MemberVisibilityFlags.Assembly)]
        [InlineData(MemberVisibilityFlags.PublicFamily, MemberVisibilityFlags.Family | MemberVisibilityFlags.FamilyOrAssembly)]
        public void Values_CompositeValues(MemberVisibilityFlags flag, MemberVisibilityFlags components)
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Equal(flag, components);

            // Teardown
        }
    }
}
