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
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using AutoFixture;
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
        ///     Checks whether the <see cref="DomainFixture.CreateFor(object)"/> method throws an exception when called with object which
        ///     type is defined in an assembly with multiple domain customiztin types.
        /// </summary>
        [Paradigm]
        public void StaticCreateFor_Object_CalledFromAssemblyWithMultipleDomainCustomizations_Throws()
        {
            // Fixture setup
            var module = CreateDynamicModule();
            var firstCustomizationType = CreateDomainCustomizationType(module, "FirstCustomization");
            var secondCustomizationType = CreateDomainCustomizationType(module, "SecondCustomization");
            var firstCustomization = Activator.CreateInstance(firstCustomizationType);
            var secondCustomization = Activator.CreateInstance(secondCustomizationType);

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentException>(() =>
            {
                DomainFixture.CreateFor(firstCustomization);
            });

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

        /// <summary>
        ///     Checks whether the <see cref="DomainFixture.LoadFrom(Assembly)"/> method throws an exception when called with assembly
        ///     with multiple domain customiztin types.
        /// </summary>
        [Paradigm]
        public void StaticLoadFrom_Assembly_CalledWithAssemblyWithMultipleDomainCustomizations_Throws()
        {
            // Fixture setup
            var module = CreateDynamicModule();
            var firstCustomizationType = CreateDomainCustomizationType(module, "FirstCustomization");
            var secondCustomizationType = CreateDomainCustomizationType(module, "SecondCustomization");
            var firstCustomization = Activator.CreateInstance(firstCustomizationType);
            var secondCustomization = Activator.CreateInstance(secondCustomizationType);

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentException>(() =>
            {
                DomainFixture.LoadFrom(firstCustomizationType.Assembly);
            });

            // Teardown
        }

        /// <summary>
        ///     Creates a new unique instance of the <see cref="ModuleBuilder"/> class.
        /// </summary>
        /// <returns>
        ///     Newly created unique instance of the <see cref="ModuleBuilder"/> class.
        /// </returns>
        private static ModuleBuilder CreateDynamicModule()
        {
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
            return assemblyBuilder.DefineDynamicModule("MainModule");
        }

        /// <summary>
        ///     Creates a new type with the specified name that derives from <see cref="DomainCustomizationBase"/> class.
        /// </summary>
        /// <param name="module">
        ///     A module in which the type shall be created.
        /// </param>
        /// <param name="typeName">
        ///     Name of the type to create.
        /// </param>
        /// <returns>
        ///     A newly ceated type with the specified name that derives from <see cref="DomainCustomizationBase"/> class.
        /// </returns>
        private static Type CreateDomainCustomizationType(ModuleBuilder module, string typeName)
        {
            TypeBuilder typeBuilder = module.DefineType(
                typeName,
                TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.AutoLayout,
                typeof(DomainCustomizationBase));

            ConstructorBuilder constructor = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, Type.EmptyTypes);
            ILGenerator generator = constructor.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            Type enumerableType = typeof(Enumerable);
            MethodInfo emptyMethod = enumerableType.GetMethod("Empty", BindingFlags.Public | BindingFlags.Static);
            MethodInfo genercEmptyMethod = emptyMethod.MakeGenericMethod(typeof(ICustomization));
            Type domainCustomizationBaseType = typeof(DomainCustomizationBase);
            ConstructorInfo domainCustomizationBaseConstructor = domainCustomizationBaseType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).First();
            generator.Emit(OpCodes.Call, genercEmptyMethod);
            generator.Emit(OpCodes.Call, domainCustomizationBaseConstructor);
            generator.Emit(OpCodes.Ret);

            TypeInfo resultTypeInfo = typeBuilder.CreateTypeInfo();
            return resultTypeInfo.AsType();
        }
    }
}
