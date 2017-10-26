// -----------------------------------------------------------------------
// <copyright file="DomainCustomizationBase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Collections.Generic;
    using AutoFixture;

    /// <summary>
    ///     Represents a base class for customization related to a problem domain.
    /// </summary>
    public abstract class DomainCustomizationBase : CompositeCustomization
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainCustomizationBase"/> class.
        /// </summary>
        /// <param name="customizations">
        ///     The customizations.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="customizations"/> is <see langword="null"/>.
        /// </exception>
        protected DomainCustomizationBase(IEnumerable<ICustomization> customizations)
            : base(customizations)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainCustomizationBase"/> class.
        /// </summary>
        /// <param name="customizations">
        ///     The customizations.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="customizations"/> is <see langword="null"/>.
        /// </exception>
        protected DomainCustomizationBase(params ICustomization[] customizations)
            : base(customizations)
        {
        }
    }
}
