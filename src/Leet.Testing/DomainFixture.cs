// -----------------------------------------------------------------------
// <copyright file="DomainFixture.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Leet.Testing.Properties;
    using Ploeh.AutoFixture;

    /// <summary>
    ///     Provides anonymous object creation services with an additional knowledge of the current problem domain.
    /// </summary>
    public class DomainFixture : Fixture
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainFixture"/> class.
        /// </summary>
        /// <param name="customization">
        ///     The customization to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="customization"/> is <see langword="null"/>.
        /// </exception>
        private DomainFixture(ICustomization customization)
        {
            if (object.ReferenceEquals(customization, null))
            {
                throw new ArgumentNullException(nameof(customization));
            }

            this.Customize(customization);
        }

        /// <summary>
        ///     Creates a <see cref="IFixture"/> that is asociated with the specified specification object.
        /// </summary>
        /// <param name="specification">
        ///     An object which specifies a behavior for a type.
        /// </param>
        /// <returns>
        ///     A domain <see cref="IFixture"/> that is defined in an assembly in which the <paramref name="specification"/>
        ///     is defined.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="specification"/> is <see langword="null"/>.
        /// </exception>
        public static IFixture CreateFor(object specification)
        {
            if (object.ReferenceEquals(specification, null))
            {
                throw new ArgumentNullException(nameof(specification));
            }

            Assembly assembly = specification.GetType().Assembly;

            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);

            var fixtures = assembly.GetTypes().Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                domainCustomizationBaseType.IsAssignableFrom(type)).Select(type =>
                    type.GetConstructor(Type.EmptyTypes)).Where(constructor => !object.ReferenceEquals(constructor, null));

            if (!fixtures.Any())
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NoDomainFixture, nameof(assembly));
            }

            if (fixtures.Skip(1).Any())
            {
                throw new ArgumentException(Resources.Exceptions_Argument_MultipleDomainFixtures, nameof(assembly));
            }

            DomainCustomizationBase customization = (DomainCustomizationBase)fixtures.Single().Invoke(null);
            CompositeCustomization composite = new CompositeCustomization(customization, new SpecimenBuilderCustomization(new SutInjectionRelay(specification)));

            return new DomainFixture(composite);
        }

        /// <summary>
        ///     Loads a <see cref="IFixture"/> declared in a specified <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">
        ///     An assembly from which the domain fixture shall be obtained.
        /// </param>
        /// <returns>
        ///     A single instance of the class declared in a specified <paramref name="assembly"/> that
        ///     represets a <see cref="IFixture"/> defined for the problem domain.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="assembly"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Specified assembly contains no class that inherits from <see cref="DomainFixture"/>.
        ///     <para>-OR-</para>
        ///     Specified assembly contains more than one class that inherits from <see cref="DomainFixture"/>.
        /// </exception>
        public static IFixture LoadFrom(Assembly assembly)
        {
            if (object.ReferenceEquals(assembly, null))
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);

            var fixtures = assembly.GetTypes().Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                domainCustomizationBaseType.IsAssignableFrom(type)).Select(type =>
                    type.GetConstructor(Type.EmptyTypes)).Where(constructor => !object.ReferenceEquals(constructor, null));

            if (!fixtures.Any())
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NoDomainFixture, nameof(assembly));
            }

            if (fixtures.Skip(1).Any())
            {
                throw new ArgumentException(Resources.Exceptions_Argument_MultipleDomainFixtures, nameof(assembly));
            }

            DomainCustomizationBase customization = (DomainCustomizationBase)fixtures.Single().Invoke(null);
            return new DomainFixture(customization);
        }
    }
}
