// -----------------------------------------------------------------------
// <copyright file="TestDataProviderBaseTestsAsIEnumerable.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Leet.Specifications;
using Leet.Testing;
using Leet.Testing.Reflection;

/// <summary>
///      A class that tests <see cref="TestDataProviderBase"/> class in a conformance to
///     behavior specified for <see cref="IEnumerable{T}"/> interface.
/// </summary>
public class TestDataProviderBaseTestsAsIEnumerable : IEnumerableSpecification<TestDataProviderBase>
{
    /// <summary>
    ///     A class that specifies behavior for <see cref="IEnumerator"/> returned by <see cref="IEnumerable.GetEnumerator"/>
    ///     method for <see cref="TestDataProviderBase"/> class.
    /// </summary>
    public class GetEnumeratorReturnValue : GetEnumeratorReturnValue<IEnumerator>
    {
        /// <summary>
        ///     Gets a value indicating whether the enumerator supports <see cref="IEnumerator.Reset"/> method.
        /// </summary>
        protected override bool IsResetSupported
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///     Generates an enumerator of the collection filled with the specified items.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of items in the requested generator array.
        /// </param>
        /// <param name="items">
        ///     Array of items that shall represent the collection to enumerate.
        /// </param>
        /// <param name="enumerator">
        ///     The enumerator of the generated collection.
        /// </param>
        /// <param name="modify">
        ///     An action that modifies the collection items.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the system-under-test supports generating items from the specified array;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public override bool GenerateEnumerator(int itemCount, out object[] items, out IEnumerator enumerator, out Action modify)
        {
            IFixture fixture = DomainFixture.CreateFor(this);
            var i = fixture.CreateMany<object[]>(itemCount);
            items = i.ToArray();
            var list = i.ToList();
            IEnumerable sut = (TestDataProviderBase)typeof(TestDataProviderBase).InvokeConstructorViaProxy(new Type[] { typeof(IEnumerable<object[]>) }, new object[] { list });
            enumerator = sut.GetEnumerator();
            modify = () => list.Add(null);
            enumerator = this.ModifySut(enumerator);
            return true;
        }

        /// <summary>
        ///     Creates an instance of the <see cref="IEnumerable"/> system-under-test object.
        /// </summary>
        /// <returns>
        ///     An instance of the <see cref="IEnumerable"/> system-under-test object.
        /// </returns>
        public override IEnumerator Create()
        {
            IFixture fixture = DomainFixture.CreateFor(this);
            int i = fixture.Create<int>();
            this.GenerateEnumerator(i, out _, out IEnumerator result, out _);
            return result;
        }
    }
}
