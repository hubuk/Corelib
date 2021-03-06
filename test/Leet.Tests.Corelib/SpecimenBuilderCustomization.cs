﻿// -----------------------------------------------------------------------
// <copyright file="SpecimenBuilderCustomization.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    ///     Represents a customization of an <see cref="IFixture"/> that extends its functionality
    ///     by adding associated <see cref="ISpecimenBuilder"/>.
    /// </summary>
    public class SpecimenBuilderCustomization : ICustomization
    {
        /// <summary>
        ///     Holds a read-only reference to the speciment builder that shall extend a fixture.
        /// </summary>
        private readonly ISpecimenBuilder extension;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpecimenBuilderCustomization"/> class.
        /// </summary>
        /// <param name="extension">
        ///     A speciment builder that shall extend a fixture.
        /// </param>
        public SpecimenBuilderCustomization(ISpecimenBuilder extension)
        {
            if (object.ReferenceEquals(extension, null))
            {
                throw new ArgumentNullException(nameof(extension));
            }

            this.extension = extension;
        }

        /// <summary>
        ///     Gets a speciment builder that shall extend a fixture.
        /// </summary>
        public ISpecimenBuilder Extension
        {
            get
            {
                return this.extension;
            }
        }

        /// <summary>
        ///     Customizes the specified fixture.
        /// </summary>
        /// <param name="fixture">
        ///     The fixture to customize.
        /// </param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(this.extension);
        }
    }
}
