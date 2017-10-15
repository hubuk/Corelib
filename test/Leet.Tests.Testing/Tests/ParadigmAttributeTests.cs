// -----------------------------------------------------------------------
// <copyright file="ParadigmAttributeTests.cs" company="Leet">
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
    using Ploeh.AutoFixture;
    using Xunit;
    using Xunit.Sdk;

    /// <summary>
    ///     Defines tests for <see cref="ParadigmAttribute"/> class.
    /// </summary>
    public sealed class ParadigmAttributeTests : FactAttributeSpecification<ParadigmAttribute>
    {
        /// <summary>
        ///     Checks whether the <see cref="ParadigmAttribute()"/> constructor assigns <see langword="null"/>
        ///     to the <see cref="FactAttribute.DisplayName" /> property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_AssignsNullToDisplayName()
        {
            // Fixture setup
            ParadigmAttribute sut = new ParadigmAttribute();

            // Exercise system
            // Verify outcome
            Assert.Null(sut.DisplayName);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ParadigmAttribute()"/> constructor assigns <see langword="null"/>
        ///     to the <see cref="FactAttribute.Skip" /> property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_AssignsNullToSkip()
        {
            // Fixture setup
            ParadigmAttribute sut = new ParadigmAttribute();

            // Exercise system
            // Verify outcome
            Assert.Null(sut.Skip);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ParadigmAttribute()"/> constructor assigns an empty collection
        ///     to the <see cref="ParadigmAttribute.SutModificationMethods" /> property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_AssignsEmptyCollectionToSutModificationMethods()
        {
            // Fixture setup
            ParadigmAttribute sut = new ParadigmAttribute();

            // Exercise system
            // Verify outcome
            Assert.Empty(sut.SutModificationMethods);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ParadigmAttribute(string[])"/> constructor assigns <see langword="null"/>
        ///     to the <see cref="FactAttribute.DisplayName" /> property.
        /// </summary>
        /// <param name="sutModificationMethods">
        ///     Array of names of the test class instance methods that shall be called during system-under-test
        ///     creation to modify it in a seperate test runs.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_ArrayOfString_Always_AssignsNullToDisplayName(string[] sutModificationMethods)
        {
            // Fixture setup
            ParadigmAttribute sut = new ParadigmAttribute(sutModificationMethods);

            // Exercise system
            // Verify outcome
            Assert.Null(sut.DisplayName);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ParadigmAttribute(string[])"/> constructor assigns <see langword="null"/>
        ///     to the <see cref="FactAttribute.Skip" /> property.
        /// </summary>
        /// <param name="sutModificationMethods">
        ///     Array of names of the test class instance methods that shall be called during system-under-test
        ///     creation to modify it in a seperate test runs.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_ArrayOfString_Always_AssignsNullToSkip(string[] sutModificationMethods)
        {
            // Fixture setup
            ParadigmAttribute sut = new ParadigmAttribute(sutModificationMethods);

            // Exercise system
            // Verify outcome
            Assert.Null(sut.Skip);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="ParadigmAttribute(string[])"/> constructor assigns an empty collection
        ///     to the <see cref="ParadigmAttribute.SutModificationMethods" /> property.
        /// </summary>
        /// <param name="sutModificationMethods">
        ///     Array of names of the test class instance methods that shall be called during system-under-test
        ///     creation to modify it in a seperate test runs.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_ArrayOfString_Always_AssignsEmptyCollectionToSutModificationMethods(string[] sutModificationMethods)
        {
            // Fixture setup
            ParadigmAttribute sut = new ParadigmAttribute(sutModificationMethods);

            // Exercise system
            // Verify outcome
            Assert.Equal(sutModificationMethods, sut.SutModificationMethods);

            // Teardown
        }
    }
}
