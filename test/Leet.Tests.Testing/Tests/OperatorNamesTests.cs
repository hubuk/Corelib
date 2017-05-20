// -----------------------------------------------------------------------
// <copyright file="OperatorNamesTests.cs" company="Leet">
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
    using Leet.Testing.Reflection;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="OperatorNames"/> class.
    /// </summary>
    [StaticSpecificationType(typeof(OperatorNames))]
    public sealed class OperatorNamesTests : StaticSpecification
    {
        /// <summary>
        ///     Checks whether the <see cref="OperatorNames"/> is a <see langword="static"/> class.
        /// </summary>
        [Paradigm]
        public void Type_IsStatic()
        {
            // Fixture setup
            Type sutType = typeof(OperatorNames);

            // Exercise system
            // Verify outcome
            Assert.True(sutType.IsAbstract && sutType.IsSealed);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="OperatorNames"/> declares fields only.
        /// </summary>
        [Paradigm]
        public void Type_DeclaresFieldsOnly()
        {
            // Fixture setup
            Type sutType = typeof(OperatorNames);
            MemberInfo[] members = sutType.GetMembers(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);

            // Exercise system
            // Verify outcome
            Assert.All(members, member => member.GetType().Equals(typeof(FieldInfo)));

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="OperatorNames"/> declares public static fields only.
        /// </summary>
        [Paradigm]
        public void Type_DeclaresPublicStaticFieldsOnly()
        {
            // Fixture setup
            Type sutType = typeof(OperatorNames);
            FieldInfo[] fields = sutType.GetFields();

            // Exercise system
            // Verify outcome
            Assert.All(fields, field => Assert.True(field.IsPublic && field.IsStatic && field.IsLiteral));

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="OperatorNames"/> declares fields only.
        /// </summary>
        [Paradigm]
        public void Type_DeclaresFieldsWithValueSameAsFielNames()
        {
            // Fixture setup
            Type sutType = typeof(OperatorNames);
            FieldInfo[] fields = sutType.GetFields();

            // Exercise system
            // Verify outcome
            Assert.All(fields, field => Assert.Equal(field.Name, field.GetValue(null)));

            // Teardown
        }
    }
}
