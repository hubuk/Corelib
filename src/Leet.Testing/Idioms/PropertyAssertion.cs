// -----------------------------------------------------------------------
// <copyright file="PropertyAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using AutoFixture.Idioms;
    using Leet.Testing.Properties;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The base class for idiomatic assertion that checks condition about type's property.
    /// </summary>
    public abstract class PropertyAssertion : IdiomaticAssertion
    {
        /// <summary>
        ///     Details about the property declaration.
        /// </summary>
        private readonly PropertyDefinitionDetails details;

        /// <summary>
        ///     The name of the property to locate.
        /// </summary>
        private readonly string propertyName;

        /// <summary>
        ///     Type of the property value.
        /// </summary>
        private readonly Type propertyType;

        /// <summary>
        ///     A collection of index paramter types of the property.
        /// </summary>
        private readonly Type[] indexParameters;

        /// <summary>
        ///     Read-only view of the property index parameters.
        /// </summary>
        private readonly Lazy<IReadOnlyList<Type>> readOnlyIndexParameters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyAssertion"/> class.
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
        protected PropertyAssertion(PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters)
        {
            if (object.ReferenceEquals(propertyName, null))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(propertyName));
            }

            if (object.ReferenceEquals(propertyType, null))
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (object.ReferenceEquals(indexParameters, null))
            {
                throw new ArgumentNullException(nameof(indexParameters));
            }

            if (indexParameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(indexParameters));
            }

            this.details = details;
            this.propertyName = propertyName;
            this.propertyType = propertyType;
            this.indexParameters = indexParameters.ToArray();
            this.readOnlyIndexParameters = new Lazy<IReadOnlyList<Type>>(this.InitializeReadOnlyIndexParameters, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        ///     Gets the details about the property declaration.
        /// </summary>
        public PropertyDefinitionDetails Details
        {
            get
            {
                return this.details;
            }
        }

        /// <summary>
        ///     Gets the name of the property to locate.
        /// </summary>
        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        /// <summary>
        ///     Gets the type of the property value.
        /// </summary>
        public Type PropertyType
        {
            get
            {
                return this.propertyType;
            }
        }

        /// <summary>
        ///     Gets a collection of index paramter types of the property.
        /// </summary>
        public IReadOnlyList<Type> IndexParameters
        {
            get
            {
                return this.readOnlyIndexParameters.Value;
            }
        }

        /// <summary>
        ///     Gets a constructor parameters array.
        /// </summary>
        protected Type[] IndexParametersArray
        {
            get
            {
                return this.indexParameters;
            }
        }

        /// <summary>
        ///     Initializes read-only view of the property's index parameters.
        /// </summary>
        /// <returns>
        ///     Read-only view of the property's index parameters.
        /// </returns>
        private IReadOnlyList<Type> InitializeReadOnlyIndexParameters()
        {
            return new ReadOnlyCollection<Type>(this.indexParameters);
        }
    }
}
