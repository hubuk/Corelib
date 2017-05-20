// -----------------------------------------------------------------------
// <copyright file="HasNoFieldAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The idiomatic assertion that checks whether the specified type does not have specified field defined.
    /// </summary>
    public class HasNoFieldAssertion : FieldAssertion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoFieldAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the field declaration.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <param name="fieldType">
        ///     Type of the field value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        public HasNoFieldAssertion(FieldDefinitionDetails details, string fieldName, Type fieldType)
            : base(details, fieldName, fieldType)
        {
        }

        /// <summary>
        ///     Verifies whether the specified type does not have requested field defined.
        /// </summary>
        /// <param name="type">
        ///     Type to examine.
        /// </param>
        public override void Verify(Type type)
        {
            AssertType.HasNoField(type, this.Details, this.FieldName, this.FieldType);
        }
    }
}
