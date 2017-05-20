// -----------------------------------------------------------------------
// <copyright file="ConstructorAssertion.cs" company="Leet">
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
    ///     The base class for idiomatic assertion that checks condition about type's constructor.
    /// </summary>
    public abstract class ConstructorAssertion : IdiomaticAssertion
    {
        /// <summary>
        ///     Determines visibility of the constructor.
        /// </summary>
        private readonly MemberVisibilityFlags visibility;

        /// <summary>
        ///     Constructor parameters.
        /// </summary>
        private readonly Type[] parameters;

        /// <summary>
        ///     Read-only view of the constructor parameters.
        /// </summary>
        private readonly Lazy<IReadOnlyList<Type>> readOnlyParameters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConstructorAssertion"/> class.
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
        protected ConstructorAssertion(MemberVisibilityFlags visibility, IEnumerable<Type> parameters)
        {
            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            this.visibility = visibility;
            this.parameters = parameters.ToArray();
            this.readOnlyParameters = new Lazy<IReadOnlyList<Type>>(this.InitializeReadOnlyParameters, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        ///     Gets a visibility of the requested member.
        /// </summary>
        public MemberVisibilityFlags Visibility
        {
            get
            {
                return this.visibility;
            }
        }

        /// <summary>
        ///     Gets a constructor parameters.
        /// </summary>
        public IReadOnlyList<Type> Parameters
        {
            get
            {
                return this.readOnlyParameters.Value;
            }
        }

        /// <summary>
        ///     Gets a constructor parameters array.
        /// </summary>
        protected Type[] ParametersArray
        {
            get
            {
                return this.parameters;
            }
        }

        /// <summary>
        ///     Initializes read-only view of the constructor's parameters.
        /// </summary>
        /// <returns>
        ///     Read-only view of the constructor's parameters.
        /// </returns>
        private IReadOnlyList<Type> InitializeReadOnlyParameters()
        {
            return new ReadOnlyCollection<Type>(this.parameters);
        }
    }
}
