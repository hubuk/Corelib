// -----------------------------------------------------------------------
// <copyright file="DisposableBaseTests.cs" company="Leet">
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
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;
    using NSubstitute;
    using Xunit;

    /// <summary>
    ///     A class that tests <see cref="DisposableBase"/> class in a conformance to
    ///     its specified behavior.
    /// </summary>
    public sealed class DisposableBaseTests : DisposableBaseSpecification<DisposableBase>
    {
        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> type is class.
        /// </summary>
        [Paradigm]
        public void Type_Is_Class()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            bool isClass = sutType.IsClass;

            // Verify outcome
            Assert.True(isClass);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> type is publicly visible.
        /// </summary>
        [Paradigm]
        public void Type_Is_Public()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            bool isPublic = sutType.IsPublic;

            // Verify outcome
            Assert.True(isPublic);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> class is abstract.
        /// </summary>
        [Paradigm]
        public void Type_Is_Abstract()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            bool isAbstract = sutType.IsAbstract;

            // Verify outcome
            Assert.True(isAbstract);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> has public non-virtual <see cref="IDisposable.Dispose"/> method.
        /// </summary>
        [Paradigm]
        public void Type_Has_PublicVirtualDisposeVoidMethod()
        {
            // Fixture setup
            Type disposableBaseType = typeof(DisposableBase);

            // Exercise system
            // Verify outcome
            AssertType.HasMethod(disposableBaseType, MemberDefinitionDetails.Default, MemberName_Dispose, typeof(void));

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> has implicit implementation of <see cref="IDisposable.Dispose"/> method.
        /// </summary>
        [Paradigm]
        public void Type_Has_ImplicitIDisposableDisposeVoidMethodImplementation()
        {
            // Fixture setup
            Type disposableBaseType = typeof(DisposableBase);
            Type iDisposableType = typeof(IDisposable);

            // Exercise system
            InterfaceMapping iDisposableMapping = disposableBaseType.GetInterfaceMap(iDisposableType);
            MethodInfo disposeMethod = disposableBaseType.GetMethod(MemberDefinitionDetails.Default, MemberName_Dispose, typeof(void));

            // Verify outcome
            Assert.Same(iDisposableMapping.TargetMethods[0], disposeMethod);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> class has protected virtual implementation of <see cref="DisposableBase.Dispose(bool)"/> method.
        /// </summary>
        [Paradigm]
        public void Type_Has_ProtectedVirtualDisposeBooleanMethod()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            bool methodFound = sutType.IsMethodDeclared(
                MemberDefinitionDetails.Protected | MemberDefinitionDetails.Virtual,
                MemberName_Dispose,
                typeof(void),
                typeof(bool));

            // Verify outcome
            Assert.True(methodFound);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> class has protected non-virtual implementation of <see cref="DisposableBase.ThrowIfDisposed"/> method.
        /// </summary>
        [Paradigm]
        public void Type_Has_ProtectedNonVirtualThrowIfDisposedVoidMethod()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            bool methodFound = sutType.IsMethodDeclared(MemberDefinitionDetails.Protected, MemberName_ThrowIfDisposed, typeof(void), Type.EmptyTypes);

            // Verify outcome
            Assert.True(methodFound);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> class has non-virtual of <see cref="DisposableBase.IsDisposed"/> property with protected getter and no setter.
        /// </summary>
        [Paradigm]
        public void Type_Has_ProtectedNonVirtualIsDisposedPropertyWithGetter()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            bool propertyFound = sutType.IsPropertyDeclared(
                PropertyDefinitionDetails.NoSetter | PropertyDefinitionDetails.ProtectedGetter,
                MemberName_IsDisposed,
                typeof(bool));

            // Verify outcome
            Assert.True(propertyFound);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> class has protected parameterless constructor.
        /// </summary>
        [Paradigm]
        public void Type_Has_ProtectedParameterlessConstructor()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            // Verify outcome
            AssertType.HasParameterlessConstructor(sutType, MemberVisibilityFlags.Family);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DisposableBase"/> class has no finalizer declared.
        /// </summary>
        [Paradigm]
        public void Type_Has_NoFinalizer()
        {
            // Fixture setup
            Type sutType = typeof(DisposableBase);

            // Exercise system
            // Verify outcome
            AssertType.HasNoMethod(
                sutType,
                MemberDefinitionDetails.Virtual | MemberDefinitionDetails.Protected | MemberDefinitionDetails.Declared,
                MemberName_Finalize,
                typeof(void));

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="DisposableBase"/> constructor does not sets <see cref="DisposableBase.IsDisposed"/>
        ///     property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Never_CallsSetIsDisposed()
        {
            // Fixture setup
            // Exercise system
            DisposableBase sut = Substitute.For<DisposableBase>();

            // Verify outcome
            Assert.False((bool)sut.GetPropertyValue(MemberVisibilityFlags.Family, MemberName_IsDisposed));

            // Teardown
        }
    }
}
