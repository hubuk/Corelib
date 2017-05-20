// -----------------------------------------------------------------------
// <copyright file="DeferredFixtureTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using Leet.Specifications;
    using Leet.Testing;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Dsl;
    using Ploeh.AutoFixture.Kernel;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="DeferredFixture"/> class.
    /// </summary>
    public class DeferredFixtureTests : ObjectSpecification<DeferredFixture>
    {
        /// <summary>
        ///     Checks whether the <see cref="DeferredFixture.DeferredFixture"/> constructor assigns <see langword="null"/> to the
        ///     <see cref="DeferredFixture.Fixture"/> property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_AssignsNullToFixture()
        {
            // Fixture setup

            // Exercise system
            DeferredFixture sut = new DeferredFixture();

            // Verify outcome
            Assert.Null(sut.Fixture);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DeferredFixture.AssignFixture"/> method assigns specified fixture to the
        ///     <see cref="DeferredFixture.Fixture"/> property.
        /// </summary>
        [Paradigm]
        public void AssignFixture_IFixture_Always_AssignsFixture()
        {
            // Fixture setup
            DeferredFixture sut = new DeferredFixture();
            IFixture fixture = Substitute.For<IFixture>();

            // Exercise system
            sut.AssignFixture(fixture);

            // Verify outcome
            Assert.Same(fixture, sut.Fixture);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DeferredFixture.AssignFixture"/> method throws an exception when called twice.
        /// </summary>
        [Paradigm]
        public void AssignFixture_IFixture_CalledTwice_Throws()
        {
            // Fixture setup
            DeferredFixture sut = new DeferredFixture();
            IFixture fixture = Substitute.For<IFixture>();

            // Exercise system
            sut.AssignFixture(fixture);

            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.AssignFixture(fixture);
            });
            Assert.Same(fixture, sut.Fixture);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DeferredFixture.AssignFixture"/> method throws an exception when second time with
        ///     another fixture.
        /// </summary>
        [Paradigm]
        public void AssignFixture_IFixture_CalledSecondTimeWithAnotherFixture_Throws()
        {
            // Fixture setup
            DeferredFixture sut = new DeferredFixture();
            IFixture first = Substitute.For<IFixture>();
            IFixture second = Substitute.For<IFixture>();

            // Exercise system
            sut.AssignFixture(first);

            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.AssignFixture(second);
            });
            Assert.Same(first, sut.Fixture);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Behaviors"/> property throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void Behaviors_CalledWhenNotAssigned_Throws()
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Behaviors;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Customizations"/> property throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void Customizations_CalledWhenNotAssigned_Throws()
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Customizations;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.OmitAutoProperties"/> property throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void OmitAutoProperties_GetWhenNotAssigned_Throws()
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.OmitAutoProperties;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.OmitAutoProperties"/> property throws an exception when set without fixture assigned.
        /// </summary>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void OmitAutoProperties_SetWhenNotAssigned_Throws(bool value)
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.OmitAutoProperties = value;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.RepeatCount"/> property throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void RepeatCount_GetWhenNotAssigned_Throws()
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.RepeatCount;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.RepeatCount"/> property throws an exception when set without fixture assigned.
        /// </summary>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void RepeatCount_SetWhenNotAssigned_Throws(int value)
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.RepeatCount = value;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.ResidueCollectors"/> property throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void ResidueCollectors_CalledWhenNotAssigned_Throws()
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.ResidueCollectors;
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Build{T}"/> method throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void BuildOfObject_CalledWhenNotAssigned_Throws()
        {
            this.BuildOfT_CalledWhenNotAssigned_Throws<object>();
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Build{T}"/> method throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void BuildOfInt32_CalledWhenNotAssigned_Throws()
        {
            this.BuildOfT_CalledWhenNotAssigned_Throws<int>();
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Build{T}"/> method throws an exception when called without fixture assigned.
        /// </summary>
        [Paradigm]
        public void BuildOfIDisposable_CalledWhenNotAssigned_Throws()
        {
            this.BuildOfT_CalledWhenNotAssigned_Throws<IDisposable>();
        }

        /// <summary>
        ///     Checks whether the <see cref="ISpecimenBuilder.Create(object, ISpecimenContext)"/> method throws an exception
        ///     when called without fixture assigned.
        /// </summary>
        /// <param name="request">
        ///     The request that describes what to create.
        /// </param>
        /// <param name="context">
        ///     A context that can be used to create other specimens.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Create_CalledWhenNotAssigned_Throws(object request, ISpecimenContext context)
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Create(request, context);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Customize(ICustomization)"/> method throws an exception when called without fixture assigned.
        /// </summary>
        /// <param name="customization">
        ///     The customization to apply.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Customize_CalledWhenNotAssigned_Throws(ICustomization customization)
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Customize(customization);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws an exception when called without fixture assigned.
        /// </summary>
        /// <param name="composerTransformation">
        ///     A function that customizes a given <see cref="ICustomizationComposer{T}"/> and returns
        ///     the modified composer.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CustomizeOfObject_CalledWhenNotAssigned_Throws(Func<ICustomizationComposer<object>, ISpecimenBuilder> composerTransformation)
        {
            this.CustomizeOfT_CalledWhenNotAssigned_Throws(composerTransformation);
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws an exception when called without fixture assigned.
        /// </summary>
        /// <param name="composerTransformation">
        ///     A function that customizes a given <see cref="ICustomizationComposer{T}"/> and returns
        ///     the modified composer.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CustomizeOfInt32_CalledWhenNotAssigned_Throws(Func<ICustomizationComposer<int>, ISpecimenBuilder> composerTransformation)
        {
            this.CustomizeOfT_CalledWhenNotAssigned_Throws(composerTransformation);
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Customize{T}(Func{ICustomizationComposer{T}, ISpecimenBuilder})"/> method throws an exception when called without fixture assigned.
        /// </summary>
        /// <param name="composerTransformation">
        ///     A function that customizes a given <see cref="ICustomizationComposer{T}"/> and returns
        ///     the modified composer.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void CustomizeOfIDisposable_CalledWhenNotAssigned_Throws(Func<ICustomizationComposer<IDisposable>, ISpecimenBuilder> composerTransformation)
        {
            this.CustomizeOfT_CalledWhenNotAssigned_Throws(composerTransformation);
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Build{T}"/> method throws an exception when called without fixture assigned.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of object for which the algorithm should be customized.
        /// </typeparam>
        private void BuildOfT_CalledWhenNotAssigned_Throws<T>()
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Build<T>();
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="IFixture.Build{T}"/> method throws an exception when called without fixture assigned.
        /// </summary>
        /// <param name="composerTransformation">
        ///     A function that customizes a given <see cref="ICustomizationComposer{T}"/> and returns
        ///     the modified composer.
        /// </param>
        /// <typeparam name="T">
        ///     The type of object for which the algorithm should be customized.
        /// </typeparam>
        private void CustomizeOfT_CalledWhenNotAssigned_Throws<T>(Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation)
        {
            // Fixture setup
            IFixture sut = new DeferredFixture();

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                sut.Customize(composerTransformation);
            });

            // Teardown
        }
    }
}
