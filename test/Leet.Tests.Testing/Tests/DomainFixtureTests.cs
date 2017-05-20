// -----------------------------------------------------------------------
// <copyright file="DomainFixtureTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Reflection;
    using Leet.Specifications;
    using Leet.Testing;
    using Leet.Testing.Reflection;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="DomainFixture"/> class.
    /// </summary>
    public class DomainFixtureTests : ObjectSpecification<DomainFixture>
    {
        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.CreateFor(object)"/> method throws an exception when called with null object.
        /// </summary>
        [Paradigm]
        public void StaticCreateFor_Object_CalledWithNullObject_Throws()
        {
            // Fixture setup
            SpecificationBase specification = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(specification), () =>
            {
                DomainFixture.CreateFor(specification);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.CreateFor(object)"/> method throws an exception when called with object
        ///     of type from non-domain assembly.
        /// </summary>
        [Paradigm]
        public void StaticCreateFor_Object_CalledWithObjectFromNonDomainAssembly_Throws()
        {
            // Fixture setup
            SpecificationBase specification = (SpecificationBase)typeof(StaticSpecification).InvokeConstructorViaProxy();

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentException>(() =>
            {
                DomainFixture.CreateFor(specification);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.CreateFor(object)"/> method returns an instance of <see cref="DomainFixture"/>
        ///     when called with correct object.
        /// </summary>
        [Paradigm]
        public void StaticCreateFor_Object_CalledWithObjectFromDomainAssembly_ReturnsFixture()
        {
            // Fixture setup
            SpecificationBase specification = this;

            // Exercise system
            // Verify outcome
            Assert.NotNull(DomainFixture.CreateFor(specification));

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.LoadFrom(Assembly)"/> method throws an exception when called with null assembly.
        /// </summary>
        [Paradigm]
        public void StaticLoadFrom_Assembly_CalledWithNullAssembly_Throws()
        {
            // Fixture setup
            Assembly assembly = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(assembly), () =>
            {
                DomainFixture.LoadFrom(assembly);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.LoadFrom(Assembly)"/> method throws an exception when called with assembly
        ///     without domain customization defined.
        /// </summary>
        [Paradigm]
        public void StaticLoadFrom_Assembly_CalledWithNonDomainAssembly_Throws()
        {
            // Fixture setup
            Assembly assembly = typeof(object).Assembly;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentException>(nameof(assembly), () =>
            {
                DomainFixture.LoadFrom(assembly);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.LoadFrom(Assembly)"/> method returns <see cref="DomainFixture"/> when called
        ///     with doamin assembly.
        /// </summary>
        [Paradigm]
        public void StaticLoadFrom_Assembly_CalledWithDomainAssembly_ReturnsFixture()
        {
            // Fixture setup
            Assembly assembly = this.GetType().Assembly;

            // Exercise system
            // Verify outcome
            Assert.NotNull(DomainFixture.LoadFrom(assembly));

            // Teardown
        }
    }
}
