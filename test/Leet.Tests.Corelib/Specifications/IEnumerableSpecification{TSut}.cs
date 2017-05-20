// -----------------------------------------------------------------------
// <copyright file="IEnumerableSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System.Collections;
    using Leet.Testing;
    using Ploeh.AutoFixture;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IEnumerable"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IEnumerable"/> interface.
    /// </typeparam>
    public abstract class IEnumerableSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : IEnumerable
    {
        /// <summary>
        ///     A class that specifies behavior for <see cref="IEnumerator"/> returned by <see cref="IEnumerable.GetEnumerator"/>
        ///     method.
        /// </summary>
        /// <typeparam name="TEnumerator">
        ///     Type of the enumerator returned from the <see cref="IEnumerable.GetEnumerator"/> method.
        /// </typeparam>
        public abstract class GetEnumeratorReturnValue<TEnumerator>
            : IEnumeratorSpecification<TEnumerator>, IFixtureOverride<IEnumerator>
            where TEnumerator : IEnumerator
        {
            /// <summary>
            ///     Creates an instance of the <see cref="IEnumerable"/> system-under-test object.
            /// </summary>
            /// <returns>
            ///     An instance of the <see cref="IEnumerable"/> system-under-test object.
            /// </returns>
            public virtual IEnumerator Create()
            {
                IFixture fixture = DomainFixture.CreateFor(this);
                IEnumerable sut = fixture.CreateSut<TSut>();
                return sut.GetEnumerator();
            }
        }
    }
}
