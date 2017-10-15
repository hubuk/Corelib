// -----------------------------------------------------------------------
// <copyright file="FactAttributeSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using Leet.Testing;
    using Leet.Testing.Reflection;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="FactAttribute"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="FactAttribute"/> class.
    /// </typeparam>
    public abstract class FactAttributeSpecification<TSut> : ObjectSpecification<TSut>
        where TSut : FactAttribute
    {
        /// <summary>
        ///     Checks whether the <see cref="FactAttribute.DisplayName"/> property sets value in setter.
        /// </summary>
        /// <param name="value">
        ///     Value for the property.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void DisplayName_Setter_Always_SetsPropertyValue(string value)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            TSut sut = fixture.CreateSut<TSut>();

            // Exercise system
            sut.DisplayName = value;

            // Verify outcome
            Assert.Same(value, sut.DisplayName);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="FactAttribute.DisplayName"/> property sets value in setter
        ///     when called with <see langword="null"/>.
        /// </summary>
        /// <param name="value">
        ///     Initial value for the property.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void DisplayName_Setter_CalledWithNull_SetsPropertyValue(string value)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            TSut sut = fixture.CreateSut<TSut>();
            sut.DisplayName = value;

            // Exercise system
            sut.DisplayName = null;

            // Verify outcome
            Assert.Null(sut.DisplayName);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="FactAttribute.Skip"/> property sets value in setter.
        /// </summary>
        /// <param name="value">
        ///     Value for the property.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Skip_Setter_Always_SetsPropertyValue(string value)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            TSut sut = fixture.CreateSut<TSut>();

            // Exercise system
            sut.Skip = value;

            // Verify outcome
            Assert.Same(value, sut.Skip);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="FactAttribute.Skip"/> property sets value in setter
        ///     when called with <see langword="null"/>.
        /// </summary>
        /// <param name="value">
        ///     Initial value for the property.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Skip_Setter_CalledWithNull_SetsPropertyValue(string value)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            TSut sut = fixture.CreateSut<TSut>();
            sut.Skip = value;

            // Exercise system
            sut.Skip = null;

            // Verify outcome
            Assert.Null(sut.Skip);

            // Teardown
        }
    }
}
