// -----------------------------------------------------------------------
// <copyright file="SpecimenBuilderCustomization.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
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
        ///     Holds a read-only reference to the specimen builder that shall extend a fixture.
        /// </summary>
        private readonly ISpecimenBuilder extension;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpecimenBuilderCustomization"/> class.
        /// </summary>
        /// <param name="extension">
        ///     A specimen builder that shall extend a fixture.
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
        ///     Gets a specimen builder that shall extend a fixture.
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="fixture"/> is <see langword="null"/>.
        /// </exception>
        public void Customize(IFixture fixture)
        {
            if (object.ReferenceEquals(fixture, null))
            {
                throw new ArgumentNullException(nameof(fixture));
            }

            fixture.Customizations.Add(this.extension);
        }
    }
}
