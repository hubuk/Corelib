// -----------------------------------------------------------------------
// <copyright file="IFixtureSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Leet.Testing;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Dsl;
    using Ploeh.AutoFixture.Kernel;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IFixture"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IFixture"/> interface.
    /// </typeparam>
    public abstract class IFixtureSpecification<TSut> : ISpecimenBuilderSpecification<TSut>
        where TSut : IFixture
    {
        /// <summary>
        ///     Checks whether <see cref="IFixture.Customize(ICustomization)"/> method throws
        ///     exception when called with <see langword="null"/> customization.
        /// </summary>
        [Paradigm]
        public void Customize_ICustomization_CalledWithNullCustomization_Throws()
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            TSut sut = testFixture.Create<TSut>();
            ICustomization customization = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(customization), () =>
            {
                sut.Customize(customization);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws
        ///     exception when called with <see langword="null"/> composer transformation when <c>T</c> is <see cref="object"/>.
        /// </summary>
        [Paradigm]
        public void Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws_ForObject()
        {
            this.Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws<object>();
        }

        /// <summary>
        ///     Checks whether <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws
        ///     exception when called with <see langword="null"/> composer transformation when <c>T</c> is <see cref="int"/>.
        /// </summary>
        [Paradigm]
        public void Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws_ForInt32()
        {
            this.Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws<int>();
        }

        /// <summary>
        ///     Checks whether <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws
        ///     exception when called with <see langword="null"/> composer transformation when <c>T</c> is <see cref="IDisposable"/>.
        /// </summary>
        [Paradigm]
        public void Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws_ForIDisposable()
        {
            this.Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws<IDisposable>();
        }

        /// <summary>
        ///     Checks whether <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws
        ///     exception when called with <see langword="null"/> composer transformation.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of specimen to customize.
        /// </typeparam>
        private void Customize_FuncOfICustomizationComposerOfTAndOfISpecimenBuilder_CalledWithNullComposerTransformation_Throws<T>()
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            TSut sut = testFixture.Create<TSut>();
            Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(composerTransformation), () =>
            {
                sut.Customize(composerTransformation);
            });

            // Teardown
        }
    }
}
