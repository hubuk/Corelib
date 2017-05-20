// -----------------------------------------------------------------------
// <copyright file="DomainFixtureTestsAsIFixture.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using Leet.Specifications;
    using Leet.Testing;
    using Ploeh.AutoFixture;

    /// <summary>
    ///      A class that tests <see cref="DomainFixture"/> class in a conformance to
    ///     behavior specified for <see cref="IFixture"/> interface.
    /// </summary>
    public class DomainFixtureTestsAsIFixture : IFixtureSpecification<DomainFixture>
    {
    }
}
