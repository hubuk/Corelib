// -----------------------------------------------------------------------
// <copyright file="DisposableBaseTestsAsIDisposable.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using Leet.Specifications;

    /// <summary>
    ///     A class that tests <see cref="DisposableBase"/> class in a conformance to
    ///     behavior specified for <see cref="IDisposable"/> interface.
    /// </summary>
    public sealed class DisposableBaseTestsAsIDisposable : IDisposableSpecification<DisposableBase>
    {
    }
}
