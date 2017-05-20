// -----------------------------------------------------------------------
// <copyright file="MethodAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using Leet.Testing.Properties;
    using Leet.Testing.Reflection;
    using Ploeh.AutoFixture.Idioms;

    /// <summary>
    ///     The base class for idiomatic assertion that checks condition about type's method.
    /// </summary>
    public abstract class MethodAssertion : IdiomaticAssertion
    {
        /// <summary>
        ///     Details about the method declaration.
        /// </summary>
        private readonly MemberDefinitionDetails details;

        /// <summary>
        ///     The name of the method to locate.
        /// </summary>
        private readonly string methodName;

        /// <summary>
        ///     Type of the method return value.
        /// </summary>
        private readonly Type returnType;

        /// <summary>
        ///     The requested method's parameter types.
        /// </summary>
        private readonly Type[] parameters;

        /// <summary>
        ///     Read-only view of the method parameters.
        /// </summary>
        private readonly Lazy<IReadOnlyList<Type>> readOnlyParameters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MethodAssertion"/> class.
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
        protected MethodAssertion(MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters)
        {
            if (object.ReferenceEquals(methodName, null))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(methodName));
            }

            if (object.ReferenceEquals(returnType, null))
            {
                throw new ArgumentNullException(nameof(returnType));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            this.details = details;
            this.methodName = methodName;
            this.returnType = returnType;
            this.parameters = parameters.ToArray();
            this.readOnlyParameters = new Lazy<IReadOnlyList<Type>>(this.InitializeReadOnlyParameters, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        ///     Gets the details about the method declaration.
        /// </summary>
        public MemberDefinitionDetails Details
        {
            get
            {
                return this.details;
            }
        }

        /// <summary>
        ///     Gets the name of the method to locate.
        /// </summary>
        public string MethodName
        {
            get
            {
                return this.methodName;
            }
        }

        /// <summary>
        ///     Gets the type of the method return value.
        /// </summary>
        public Type ReturnType
        {
            get
            {
                return this.returnType;
            }
        }

        /// <summary>
        ///     Gets a read-only list of the requested method's parameter types.
        /// </summary>
        public IReadOnlyList<Type> Parameters
        {
            get
            {
                return this.readOnlyParameters.Value;
            }
        }

        /// <summary>
        ///     Gets an array that contains requested method's parameter types.
        /// </summary>
        protected Type[] ParametersArray
        {
            get
            {
                return this.parameters;
            }
        }

        /// <summary>
        ///     Initializes read-only view of the method's parameters.
        /// </summary>
        /// <returns>
        ///     Read-only view of the method's parameters.
        /// </returns>
        private IReadOnlyList<Type> InitializeReadOnlyParameters()
        {
            return new ReadOnlyCollection<Type>(this.parameters);
        }
    }
}
