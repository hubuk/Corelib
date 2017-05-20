// -----------------------------------------------------------------------
// <copyright file="ISpecimenBuilderSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Leet.Testing;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="ISpecimenBuilder"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="ISpecimenBuilder"/> interface.
    /// </typeparam>
    public abstract class ISpecimenBuilderSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : ISpecimenBuilder
    {
        /// <summary>
        ///     Checks whether <see cref="ISpecimenBuilder.Create(object, ISpecimenContext)"/> method throws
        ///     exception when called with <see langword="null"/> speciment context.
        /// </summary>
        [Paradigm]
        public void Create_Object_ISpecimenContext_CalledWithNullContext_Throws()
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            TSut sut = testFixture.Create<TSut>();
            ISpecimenContext context = null;
            object request = null;

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(nameof(context), () =>
            {
                sut.Create(request, context);
            });

            // Teardown
        }
    }
}
