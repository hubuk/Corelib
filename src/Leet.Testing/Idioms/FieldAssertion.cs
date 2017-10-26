// -----------------------------------------------------------------------
// <copyright file="FieldAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using AutoFixture.Idioms;
    using Leet.Testing.Properties;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The base class for idiomatic assertion that checks condition about type's field.
    /// </summary>
    public abstract class FieldAssertion : IdiomaticAssertion
    {
        /// <summary>
        ///     Details about the field declaration.
        /// </summary>
        private readonly FieldDefinitionDetails details;

        /// <summary>
        ///     The name of the field to locate.
        /// </summary>
        private readonly string fieldName;

        /// <summary>
        ///     Type of the field value.
        /// </summary>
        private readonly Type fieldType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FieldAssertion"/> class.
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
        protected FieldAssertion(FieldDefinitionDetails details, string fieldName, Type fieldType)
        {
            if (object.ReferenceEquals(fieldName, null))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(fieldName));
            }

            if (object.ReferenceEquals(fieldType, null))
            {
                throw new ArgumentNullException(nameof(fieldType));
            }

            this.details = details;
            this.fieldName = fieldName;
            this.fieldType = fieldType;
        }

        /// <summary>
        ///     Gets the details about the field declaration.
        /// </summary>
        public FieldDefinitionDetails Details
        {
            get
            {
                return this.details;
            }
        }

        /// <summary>
        ///     Gets the name of the field to locate.
        /// </summary>
        public string FieldName
        {
            get
            {
                return this.fieldName;
            }
        }

        /// <summary>
        ///     Gets the type of the field value.
        /// </summary>
        public Type FieldType
        {
            get
            {
                return this.fieldType;
            }
        }
    }
}
