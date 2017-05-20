// -----------------------------------------------------------------------
// <copyright file="IDisposableSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using Leet.Testing;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IDisposable"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IDisposable"/> interface.
    /// </typeparam>
    public abstract class IDisposableSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : IDisposable
    {
        /// <summary>
        ///     Checks whether <see cref="IDisposable.Dispose"/> method never throw exception upon multiple calls.
        /// </summary>
        /// <param name="sut">
        ///     Object under test.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Dispose_Void_Never_Throws(TSut sut)
        {
            // Fixture setup

            // Exercise system
            sut.Dispose();
            sut.Dispose();
            sut.Dispose();

            // Verify outcome

            // Teardown
        }
    }
}
