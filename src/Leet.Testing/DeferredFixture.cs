// -----------------------------------------------------------------------
// <copyright file="DeferredFixture.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Collections.Generic;
    using Leet.Testing.Properties;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Dsl;
    using Ploeh.AutoFixture.Kernel;
    using Ploeh.AutoFixture.Xunit2;

    /// <summary>
    ///     Prvides a mechanism for deferred <see cref="IFixture"/> initialization in <see cref="AutoDataAttribute"/>.
    /// </summary>
    public sealed class DeferredFixture : IFixture
    {
        /// <summary>
        ///     Holds a reference to the fixture assigned to this object.
        /// </summary>
        private IFixture fixture = null;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeferredFixture"/> class.
        /// </summary>
        public DeferredFixture()
        {
        }

        /// <summary>
        ///     Gets a reference to the currently assigned fixture.
        /// </summary>
        public IFixture Fixture
        {
            get
            {
                return this.fixture;
            }
        }

        /// <summary>
        ///     Gets the behaviors that are applied as Decorators around other parts of a Fixture.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        IList<ISpecimenBuilderTransformation> IFixture.Behaviors
        {
            get
            {
                this.ThrowIfNotAssigned();

                return this.fixture.Behaviors;
            }
        }

        /// <summary>
        ///     Gets customizations.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        IList<ISpecimenBuilder> IFixture.Customizations
        {
            get
            {
                this.ThrowIfNotAssigned();

                return this.fixture.Customizations;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether writable properties should generally be assigned
        ///     a value when  generating an anonymous object.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        bool IFixture.OmitAutoProperties
        {
            get
            {
                this.ThrowIfNotAssigned();

                return this.fixture.OmitAutoProperties;
            }

            set
            {
                this.ThrowIfNotAssigned();

                this.fixture.OmitAutoProperties = value;
            }
        }

        /// <summary>
        ///     Gets or sets a number that controls how many objects are created when a <see cref="IFixture"/>
        ///     creates more than one anonymous objects.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        int IFixture.RepeatCount
        {
            get
            {
                this.ThrowIfNotAssigned();

                return this.fixture.RepeatCount;
            }

            set
            {
                this.ThrowIfNotAssigned();

                this.fixture.RepeatCount = value;
            }
        }

        /// <summary>
        ///     Gets the residue collectors.
        /// </summary>
        /// <remarks>
        ///     It is expected that residue collectors provide fallback mechanisms if no earlier
        ///     <see cref="ISpecimenBuilder"/> can handle a request.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        IList<ISpecimenBuilder> IFixture.ResidueCollectors
        {
            get
            {
                this.ThrowIfNotAssigned();

                return this.fixture.ResidueCollectors;
            }
        }

        /// <summary>
        ///     Assignes a specified <see cref="IFixture"/> object to this instance.
        /// </summary>
        /// <param name="fixture">
        ///     A reference to the <see cref="IFixture"/> to be assigned to this object.
        /// </param>
        /// <remarks>
        ///     Fixture assignment can be performed only once for particualar instance
        ///     of the <see cref="DeferredFixture"/> class. This object cannot be used as a
        ///     <see cref="IFixture"/> instance unless a <see cref="IFixture"/> is assigned to it.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     A fixture has been already assigned.
        /// </exception>
        public void AssignFixture(IFixture fixture)
        {
            this.ThrowIfAssigned();

            this.fixture = fixture;
        }

        /// <summary>
        ///     Customizes the creation algorithm for a single object, effectively turning off
        ///     all Customizations on the <see cref="IFixture"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of object for which the algorithm should be customized.
        /// </typeparam>
        /// <returns>
        ///     A <see cref="ICustomizationComposer{T}"/> that can be used to customize the creation
        ///     algorithm before creating the object.
        /// </returns>
        /// <remarks>
        ///     The Build method kicks off a Fluent API which is usually completed by invoking
        ///     <see cref="SpecimenFactory.Create{T}(IPostprocessComposer{T})"/> on the method chain.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        ICustomizationComposer<T> IFixture.Build<T>()
        {
            this.ThrowIfNotAssigned();

            return this.fixture.Build<T>();
        }

        /// <summary>
        ///     Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">
        ///     The request that describes what to create.
        /// </param>
        /// <param name="context">
        ///     A context that can be used to create other specimens.
        /// </param>
        /// <returns>
        ///     The requested specimen if possible; otherwise a <see cref="NoSpecimen"/> instance.
        /// </returns>
        /// <remarks>
        /// The <paramref name="request"/> can be any object, but will often be a
        /// <see cref="Type"/> or other <see cref="System.Reflection.MemberInfo"/> instances.
        /// <para/>
        /// Note to implementers: Implementations are expected to return a
        /// <see cref="NoSpecimen"/> instance if they can't satisfy the request.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        object ISpecimenBuilder.Create(object request, ISpecimenContext context)
        {
            this.ThrowIfNotAssigned();

            return this.fixture.Create(request, context);
        }

        /// <summary>
        ///     Applies a customization.
        /// </summary>
        /// <param name="customization">
        ///     The customization to apply.
        /// </param>
        /// <returns>
        ///     An <see cref="IFixture"/> where the customization is applied.
        /// </returns>
        /// <remarks>
        ///     Note to implementers: the returned <see cref="IFixture"/> is expected to have
        ///     <paramref name="customization"/> applied. Whether the return value is the same
        ///     instance as the current instance, or a copy is unspecified.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        IFixture IFixture.Customize(ICustomization customization)
        {
            this.ThrowIfNotAssigned();

            return this.fixture.Customize(customization);
        }

        /// <summary>
        ///     Customizes the creation algorithm for all objects of a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of object to customize.
        /// </typeparam>
        /// <param name="composerTransformation">
        ///     A function that customizes a given <see cref="ICustomizationComposer{T}"/> and returns
        ///     the modified composer.
        /// </param>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        void IFixture.Customize<T>(Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation)
        {
            this.ThrowIfNotAssigned();

            this.fixture.Customize(composerTransformation);
        }

        /// <summary>
        ///     Throws the <see cref="InvalidOperationException"/> when a fixture has been already assigned to the
        ///     current object.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     A fixture has been already assigned.
        /// </exception>
        private void ThrowIfAssigned()
        {
            if (!object.ReferenceEquals(this.fixture, null))
            {
                throw new InvalidOperationException(Resources.Exception_InvalidOperation_FixtureAlreadyAssigned);
            }
        }

        /// <summary>
        ///     Throws the <see cref="InvalidOperationException"/> when no fixture has been assigned to the
        ///     current object yet.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Cannot use this objecct if no fixture is assigned to it.
        /// </exception>
        private void ThrowIfNotAssigned()
        {
            if (object.ReferenceEquals(this.fixture, null))
            {
                throw new InvalidOperationException(Resources.Exception_InvalidOperation_NoFixtureAssigned);
            }
        }
    }
}
