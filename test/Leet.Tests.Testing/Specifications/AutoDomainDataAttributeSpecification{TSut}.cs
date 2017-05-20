// -----------------------------------------------------------------------
// <copyright file="AutoDomainDataAttributeSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using Leet.Testing;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="AutoDomainDataAttribute"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="AutoDomainDataAttribute"/> class.
    /// </typeparam>
    public abstract class AutoDomainDataAttributeSpecification<TSut> : DataAttributeSpecification<TSut>
        where TSut : AutoDomainDataAttribute
    {
        /// <summary>
        ///     Checks whether the <see cref="AutoDomainDataAttribute"/> calls base class constructor with non-null fixture.
        /// </summary>
        [Paradigm]
        public void Construction_CallsBaseConstructor_WithNonNullFixture()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);

            // Exercise system
            AutoDomainDataAttribute sut = fixture.Create<AutoDomainDataAttribute>();
            IFixture result = sut.Fixture;

            // Verify outcome
            Assert.NotNull(result);

            // Teardown
        }
    }
}
