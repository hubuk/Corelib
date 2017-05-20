﻿// -----------------------------------------------------------------------
// <copyright file="HasConstructorAssertion.cs" company="Leet">
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
    ///     The idiomatic assertion that checks whether the specified type has specified constructor defined.
    /// </summary>
    public class HasConstructorAssertion : ConstructorAssertion
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HasConstructorAssertion"/> class.
        /// </summary>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public HasConstructorAssertion(MemberVisibilityFlags visibility, IEnumerable<Type> parameters)
            : base(visibility, parameters)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HasConstructorAssertion"/> class.
        /// </summary>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public HasConstructorAssertion(MemberVisibilityFlags visibility, params Type[] parameters)
            : base(visibility, parameters)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HasConstructorAssertion"/> class.
        /// </summary>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        public HasConstructorAssertion(MemberVisibilityFlags visibility)
            : base(visibility, Type.EmptyTypes)
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
            AssertType.HasConstructor(type, this.Visibility, this.ParametersArray);
        }
    }
}
