// -----------------------------------------------------------------------
// <copyright file="EventAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using AutoFixture.Idioms;
    using Leet.Testing.Properties;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The base class for idiomatic assertion that checks condition about type's event.
    /// </summary>
    public abstract class EventAssertion : IdiomaticAssertion
    {
        /// <summary>
        ///     Details about the event declaration.
        /// </summary>
        private readonly MemberDefinitionDetails details;

        /// <summary>
        ///     The name of the event to locate.
        /// </summary>
        private readonly string eventName;

        /// <summary>
        ///     The requested event's type.
        /// </summary>
        private readonly Type eventType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventAssertion"/> class.
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
        protected EventAssertion(MemberDefinitionDetails details, string eventName, Type eventType)
        {
            if (object.ReferenceEquals(eventName, null))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(eventName));
            }

            if (object.ReferenceEquals(eventType, null))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            this.details = details;
            this.eventName = eventName;
            this.eventType = eventType;
        }

        /// <summary>
        ///     Gets the details about the event declaration.
        /// </summary>
        public MemberDefinitionDetails Details
        {
            get
            {
                return this.details;
            }
        }

        /// <summary>
        ///     Gets the name of the event to locate.
        /// </summary>
        public string EventName
        {
            get
            {
                return this.eventName;
            }
        }

        /// <summary>
        ///     Gets the requested event's type.
        /// </summary>
        public Type EventType
        {
            get
            {
                return this.eventType;
            }
        }
    }
}
