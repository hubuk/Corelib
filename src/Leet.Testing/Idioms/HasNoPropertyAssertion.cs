// -----------------------------------------------------------------------
// <copyright file="HasNoPropertyAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using System.Collections.Generic;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The idiomatic assertion that checks whether the specified type does not have specified property defined.
    /// </summary>
    public class HasNoPropertyAssertion : PropertyAssertion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoPropertyAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <param name="indexParameters">
        ///     A collection of index paramter types of the property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        public HasNoPropertyAssertion(PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters)
            : base(details, propertyName, propertyType, indexParameters)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoPropertyAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <param name="indexParameters">
        ///     A collection of index paramter types of the property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        public HasNoPropertyAssertion(PropertyDefinitionDetails details, string propertyName, Type propertyType, params Type[] indexParameters)
            : base(details, propertyName, propertyType, indexParameters)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoPropertyAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        public HasNoPropertyAssertion(PropertyDefinitionDetails details, string propertyName, Type propertyType)
            : base(details, propertyName, propertyType, Type.EmptyTypes)
        {
        }

        /// <summary>
        ///     Verifies whether the specified type has requested field defined.
        /// </summary>
        /// <param name="type">
        ///     Type to examine.
        /// </param>
        public override void Verify(Type type)
        {
            AssertType.HasNoProperty(type, this.Details, this.PropertyName, this.PropertyType, this.IndexParametersArray);
        }
    }
}
