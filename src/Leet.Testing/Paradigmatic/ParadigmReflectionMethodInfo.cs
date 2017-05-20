// -----------------------------------------------------------------------
// <copyright file="ParadigmReflectionMethodInfo.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.Reflection;
    using Xunit.Abstractions;

    /// <summary>
    ///     Represents a reflection-backed implementation of <see cref="IMethodInfo"/>.
    /// </summary>
    public class ParadigmReflectionMethodInfo : ParadigmMethodInfo, IReflectionMethodInfo
    {
        /// <summary>
        ///     Inner method info to use an information source for current object.
        /// </summary>
        private readonly IReflectionMethodInfo inner;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmReflectionMethodInfo"/> class.
        /// </summary>
        /// <param name="inner">
        ///     Inner method info to use an information source for current object.
        /// </param>
        /// <param name="sutModificationMethod">
        ///     Name of the method that shall be used to modify the system-under-test after its creation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="inner"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="sutModificationMethod"/> is <see langword="null"/>.
        /// </exception>
        public ParadigmReflectionMethodInfo(IReflectionMethodInfo inner, string sutModificationMethod)
            : base(inner, sutModificationMethod)
        {
            this.inner = inner;
        }

        /// <summary>
        ///     Gets the underlying <see cref="MethodInfo"/> for the method.
        /// </summary>
        public MethodInfo MethodInfo
        {
            get
            {
                return this.inner.MethodInfo;
            }
        }
    }
}
