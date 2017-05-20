// -----------------------------------------------------------------------
// <copyright file="SpecimenBuilderCustomizationTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Collections.Generic;
    using Leet.Specifications;
    using Leet.Testing;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="SpecimenBuilderCustomization"/> class.
    /// </summary>
    public class SpecimenBuilderCustomizationTests : ObjectSpecification<SpecimenBuilderCustomization>
    {
        /// <summary>
        ///     Checks whether <see cref="SpecimenBuilderCustomization(ISpecimenBuilder)"/> constructor throws exception
        ///     when called with <see langword="null"/> parameter.
        /// </summary>
        [Paradigm]
        public void Constructor_ISpecimenBuilder_CalledWithNullBuilder_Throws()
        {
            // Fixture setup
            ISpecimenBuilder extension = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(extension), () =>
            {
                new SpecimenBuilderCustomization(extension);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SpecimenBuilderCustomization(ISpecimenBuilder)"/> constructor initializes
        ///     <see cref="SpecimenBuilderCustomization.Extension"/> property.
        /// </summary>
        /// <param name="extension">
        ///     Extension parameter to pass to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_ISpecimenBuilder_Always_SetsExtension(ISpecimenBuilder extension)
        {
            // Fixture setup

            // Exercise system
            SpecimenBuilderCustomization sut = new SpecimenBuilderCustomization(extension);
            ISpecimenBuilder result = sut.Extension;

            // Verify outcome
            Assert.Same(extension, result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="SpecimenBuilderCustomization.Customize(IFixture)"/> method adds
        ///     the extension to the fixture customizations.
        /// </summary>
        /// <param name="extension">
        ///     Extension parameter to pass to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Customize_IFixture_CalledWithNonNullFixture_AddExtension(ISpecimenBuilder extension)
        {
            // Fixture setup
            SpecimenBuilderCustomization sut = new SpecimenBuilderCustomization(extension);
            ISpecimenBuilder result = sut.Extension;
            IFixture fixture = Substitute.For<IFixture>();
            List<ISpecimenBuilder> customizatios = new List<ISpecimenBuilder>();
            fixture.Customizations.Returns(customizatios);

            // Exercise system
            sut.Customize(fixture);

            // Verify outcome
            Assert.Contains(customizatios, (item) => object.ReferenceEquals(item, extension));

            // Teardown
        }
    }
}
