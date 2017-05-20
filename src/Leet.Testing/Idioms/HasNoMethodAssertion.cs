// -----------------------------------------------------------------------
// <copyright file="HasNoMethodAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using System.Collections.Generic;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     The idiomatic assertion that checks whether the specified type does not have specified method defined.
    /// </summary>
    public class HasNoMethodAssertion : MethodAssertion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoMethodAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     Type of the method return value.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public HasNoMethodAssertion(MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters)
            : base(details, methodName, returnType, parameters)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HasNoMethodAssertion"/> class.
        /// </summary>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     Type of the method return value.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public HasNoMethodAssertion(MemberDefinitionDetails details, string methodName, Type returnType, params Type[] parameters)
            : base(details, methodName, returnType, parameters)
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
            AssertType.HasNoMethod(type, this.Details, this.MethodName, this.ReturnType, this.ParametersArray);
        }
    }
}
