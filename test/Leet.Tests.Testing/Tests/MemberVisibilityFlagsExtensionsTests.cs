// -----------------------------------------------------------------------
// <copyright file="MemberVisibilityFlagsExtensionsTests.cs" company="Leet">
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
    using NSubstitute;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="MemberVisibilityFlagsExtensions"/> class.
    /// </summary>
    [StaticSpecificationType(typeof(MemberVisibilityFlagsExtensions))]
    public class MemberVisibilityFlagsExtensionsTests : StaticSpecification
    {
        /// <summary>
        ///     Checks whether the <see cref="MemberVisibilityFlagsExtensions"/> type is an extension class.
        /// </summary>
        [Paradigm]
        public void Type_Is_ExtensionClass()
        {
            // Fixture setup
            Type sutType = typeof(MemberVisibilityFlagsExtensions);
            var assertion = new IsExtensionClassAssertion();

            // Exercise system
            // Verify outcome
            assertion.Verify(sutType);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="MemberVisibilityFlagsExtensions.IsMatch(MemberVisibilityFlags, FieldInfo)"/> throws an exception
        ///     when called with <see langword="null"/> parameter.
        /// </summary>
        /// <param name="flags">
        ///     Member visibility flags.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void IsMatch_CalledWithNullFieldInfo_Throws(MemberVisibilityFlags flags)
        {
            // Fixture setup
            FieldInfo info = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(info), () =>
            {
                MemberVisibilityFlagsExtensions.IsMatch(flags, info);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="MemberVisibilityFlagsExtensions.IsMatch(MemberVisibilityFlags, FieldInfo)"/> returns correct results
        ///     for non-null fields.
        /// </summary>
        /// <param name="visibility">
        ///     Field visibility.
        /// </param>
        /// <param name="flags">
        ///     Member visibility flags.
        /// </param>
        /// <param name="expectedMatch">
        ///     Expected match result.
        /// </param>
        [Paradigm]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.Private, true)]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.Family, false)]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.Assembly, false)]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.Public, false)]
        [InlineData(FieldAttributes.Private, MemberVisibilityFlags.All, true)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.Private, false)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.Family, true)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.Assembly, false)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.Public, false)]
        [InlineData(FieldAttributes.Family, MemberVisibilityFlags.All, true)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.Private, false)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.Family, false)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.FamilyAndAssembly, true)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.Assembly, false)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.Public, false)]
        [InlineData(FieldAttributes.FamANDAssem, MemberVisibilityFlags.All, true)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.Private, false)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.Family, false)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.FamilyOrAssembly, true)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.Assembly, false)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.Public, false)]
        [InlineData(FieldAttributes.FamORAssem, MemberVisibilityFlags.All, true)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.Private, false)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.Family, false)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.Assembly, true)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.Public, false)]
        [InlineData(FieldAttributes.Assembly, MemberVisibilityFlags.All, true)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.Private, false)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.Family, false)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.Assembly, false)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.Public, true)]
        [InlineData(FieldAttributes.Public, MemberVisibilityFlags.All, true)]
        public void ToBindingFlags_CalledWithNonNullField_ReturnsCorrectResults(FieldAttributes visibility, MemberVisibilityFlags flags, bool expectedMatch)
        {
            // Fixture setup
            FieldInfo info = Substitute.For<FieldInfo>();
            info.Attributes.Returns(visibility);

            // Exercise system
            bool result = flags.IsMatch(info);

            // Verify outcome
            Assert.Equal(expectedMatch, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="MemberVisibilityFlagsExtensions.IsMatch(MemberVisibilityFlags, MethodBase)"/> throws an exception
        ///     when called with <see langword="null"/> parameter.
        /// </summary>
        /// <param name="flags">
        ///     Member visibility flags.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void IsMatch_CalledWithNullMethodBase_Throws(MemberVisibilityFlags flags)
        {
            // Fixture setup
            MethodBase info = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(info), () =>
            {
                MemberVisibilityFlagsExtensions.IsMatch(flags, info);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="MemberVisibilityFlagsExtensions.IsMatch(MemberVisibilityFlags, MethodBase)"/> returns correct results
        ///     for non-null fields.
        /// </summary>
        /// <param name="visibility">
        ///     Field visibility.
        /// </param>
        /// <param name="flags">
        ///     Member visibility flags.
        /// </param>
        /// <param name="expectedMatch">
        ///     Expected match result.
        /// </param>
        [Paradigm]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.Private, true)]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.Family, false)]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.Assembly, false)]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.Public, false)]
        [InlineData(MethodAttributes.Private, MemberVisibilityFlags.All, true)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.Private, false)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.Family, true)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.Assembly, false)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.Public, false)]
        [InlineData(MethodAttributes.Family, MemberVisibilityFlags.All, true)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.Private, false)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.Family, false)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.FamilyAndAssembly, true)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.Assembly, false)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.Public, false)]
        [InlineData(MethodAttributes.FamANDAssem, MemberVisibilityFlags.All, true)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.Private, false)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.Family, false)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.FamilyOrAssembly, true)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.Assembly, false)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.Public, false)]
        [InlineData(MethodAttributes.FamORAssem, MemberVisibilityFlags.All, true)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.Private, false)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.Family, false)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.Assembly, true)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.Public, false)]
        [InlineData(MethodAttributes.Assembly, MemberVisibilityFlags.All, true)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.Private, false)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.Family, false)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.FamilyAndAssembly, false)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.FamilyOrAssembly, false)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.Assembly, false)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.Public, true)]
        [InlineData(MethodAttributes.Public, MemberVisibilityFlags.All, true)]
        public void ToBindingFlags_CalledWithNonNullMethodBase_ReturnsCorrectResults(MethodAttributes visibility, MemberVisibilityFlags flags, bool expectedMatch)
        {
            // Fixture setup
            MethodBase info = Substitute.For<MethodBase>();
            info.Attributes.Returns(visibility);

            // Exercise system
            bool result = flags.IsMatch(info);

            // Verify outcome
            Assert.Equal(expectedMatch, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="MemberVisibilityFlagsExtensions.ToBindingFlags(MemberVisibilityFlags, bool)"/> returns correct results.
        /// </summary>
        /// <param name="visibility">
        ///     Visibility for which the <see cref="BindingFlags"/> shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     <see langword="true"/> if the member is <see langword="static"/>;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="expectedResult">
        ///     Expected converions result.
        /// </param>
        [Paradigm]
        [InlineData(MemberVisibilityFlags.Private, false, BindingFlags.NonPublic | BindingFlags.Instance)]
        [InlineData(MemberVisibilityFlags.Family, false, BindingFlags.NonPublic | BindingFlags.Instance)]
        [InlineData(MemberVisibilityFlags.FamilyAndAssembly, false, BindingFlags.NonPublic | BindingFlags.Instance)]
        [InlineData(MemberVisibilityFlags.FamilyOrAssembly, false, BindingFlags.NonPublic | BindingFlags.Instance)]
        [InlineData(MemberVisibilityFlags.Assembly, false, BindingFlags.NonPublic | BindingFlags.Instance)]
        [InlineData(MemberVisibilityFlags.Public, false, BindingFlags.Public | BindingFlags.Instance)]
        [InlineData(MemberVisibilityFlags.Private, true, BindingFlags.NonPublic | BindingFlags.Static)]
        [InlineData(MemberVisibilityFlags.Family, true, BindingFlags.NonPublic | BindingFlags.Static)]
        [InlineData(MemberVisibilityFlags.FamilyAndAssembly, true, BindingFlags.NonPublic | BindingFlags.Static)]
        [InlineData(MemberVisibilityFlags.FamilyOrAssembly, true, BindingFlags.NonPublic | BindingFlags.Static)]
        [InlineData(MemberVisibilityFlags.Assembly, true, BindingFlags.NonPublic | BindingFlags.Static)]
        [InlineData(MemberVisibilityFlags.Public, true, BindingFlags.Public | BindingFlags.Static)]
        public void ToBindingFlags_Always_ReturnsCorrectResults(MemberVisibilityFlags visibility, bool isStatic, BindingFlags expectedResult)
        {
            // Fixture setup
            // Exercise system
            BindingFlags results = visibility.ToBindingFlags(isStatic);

            // Verify outcome
            Assert.Equal(expectedResult, results);

            // Teardown
        }
    }
}
