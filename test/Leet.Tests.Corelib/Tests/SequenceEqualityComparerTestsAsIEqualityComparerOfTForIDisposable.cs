﻿// -----------------------------------------------------------------------
// <copyright file="SequenceEqualityComparerTestsAsIEqualityComparerOfTForIDisposable.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Leet;
using Leet.Specifications;

/// <summary>
///     A class that tests <see cref="SequenceEqualityComparer{T}"/> class in a conformance to
///     behavior specified for <see cref="IEqualityComparer{T}"/> interface for <see cref="IDisposable"/>
///     generic type argument.
/// </summary>
public sealed class SequenceEqualityComparerTestsAsIEqualityComparerOfTForIDisposable : IEqualityComparerSpecification<SequenceEqualityComparer<IDisposable>, IEnumerable<IDisposable>>
{
}
