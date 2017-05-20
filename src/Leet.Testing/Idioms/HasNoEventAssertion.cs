// -----------------------------------------------------------------------
// <copyright file="HasNoEventAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The idiomatic assertion that checks whether the specified type does not have specified event defined.
    /// </summary>
    public class HasNoEventAssertion : EventAssertion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoEventAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the event declaration.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="eventType">
        ///     The requested event's type.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        public HasNoEventAssertion(MemberDefinitionDetails details, string eventName, Type eventType)
            : base(details, eventName, eventType)
        {
        }

        /// <summary>
        ///     Verifies whether the specified type has requested constructor defined.
        /// </summary>
        /// <param name="type">
        ///     Type to examine.
        /// </param>
        public override void Verify(Type type)
        {
            AssertType.HasNoEvent(type, this.Details, this.EventName, this.EventType);
        }
    }
}
