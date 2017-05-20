// -----------------------------------------------------------------------
// <copyright file="TestDataProviderBaseTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using Leet.Specifications;
using Leet.Testing;
using Leet.Testing.Reflection;
using NSubstitute;
using Xunit;

/// <summary>
///     Defines tests for <see cref="TestDataProviderBase"/> class.
/// </summary>
public sealed class TestDataProviderBaseTests : TestDataProviderBaseSpecification<TestDataProviderBase>
{
    /// <summary>
    ///     Checks whether the <see cref="IEnumerable.GetEnumerator"/> for <see cref="TestDataProviderBase"/>
    ///     returns an enumerator of the collection provided in the constructor.
    /// </summary>
    [Paradigm]
    public void GetEnumerator_Always_ReturnsEnumeratorOfCollectionPassedInConstructor()
    {
        // Fixture setup
        var enumerable = Substitute.For<IEnumerable<object[]>>();
        var expected = Substitute.For<IEnumerator<object[]>>();
        enumerable.GetEnumerator().Returns(expected);
        IEnumerable<object[]> sut = (TestDataProviderBase)typeof(TestDataProviderBase).InvokeConstructorViaProxy(new Type[] { typeof(IEnumerable<object[]>) }, new object[] { enumerable });

        // Exercise system
        var result = sut.GetEnumerator();

        // Verify outcome
        Assert.Same(expected, result);

        // Teardown
    }
}
