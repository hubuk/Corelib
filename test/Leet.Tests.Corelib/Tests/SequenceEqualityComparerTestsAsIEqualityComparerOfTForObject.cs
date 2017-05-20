// -----------------------------------------------------------------------
// <copyright file="SequenceEqualityComparerTestsAsIEqualityComparerOfTForObject.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System.Collections.Generic;
    using Leet.Specifications;

    /// <summary>
    ///     A class that tests <see cref="SequenceEqualityComparer{T}"/> class in a conformance to
    ///     behavior specified for <see cref="IEqualityComparer{T}"/> interface for <see cref="object"/>
    ///     generic type argument.
    /// </summary>
    public sealed class SequenceEqualityComparerTestsAsIEqualityComparerOfTForObject : IEqualityComparerSpecification<SequenceEqualityComparer<object>, IEnumerable<object>>
    {
    }
}
