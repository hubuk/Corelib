// -----------------------------------------------------------------------
// <copyright file="ICustomizationSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using AutoFixture;
    using Leet.Testing;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="ICustomization"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="ICustomization"/> interface.
    /// </typeparam>
    public abstract class ICustomizationSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : ICustomization
    {
        /// <summary>
        ///     Checks whether <see cref="ICustomization.Customize(IFixture)"/> method throws
        ///     exception when called with <see langword="null"/> fixture.
        /// </summary>
        [Paradigm]
        public void Customize_IFixture_CalledWithNullFixture_Throws()
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            TSut sut = testFixture.Create<TSut>();
            IFixture fixture = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(fixture), () =>
            {
                sut.Customize(fixture);
            });

            // Teardown
        }
    }
}
