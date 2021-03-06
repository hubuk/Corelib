﻿// -----------------------------------------------------------------------
// <copyright file="DomainCustomizationBase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Collections.Generic;
    using Ploeh.AutoFixture;

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
            if (object.ReferenceEquals(customizations, null))
            {
                throw new ArgumentNullException(nameof(customizations));
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainCustomizationBase"/> class.
        /// </summary>
        /// <param name="customizations">
        ///     The customizations.
        /// </param>
        protected DomainCustomizationBase(params ICustomization[] customizations)
            : base(customizations)
        {
        }
    }
}
