// -----------------------------------------------------------------------
// <copyright file="DomainFixtureBuilder.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Reflection;
    using AutoFixture.Kernel;
    using Leet.Testing;

    /// <summary>
    ///     Provides a mechanism for creating <see cref="DomainFixture"/> objects.
    /// </summary>
    public class DomainFixtureBuilder : ISpecimenBuilder
    {
        /// <summary>
        ///     Assembly of problem domain.
        /// </summary>
        private readonly Assembly domainAssembly;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainFixtureBuilder"/> class.
        /// </summary>
        /// <param name="domainAssembly">
        ///     Assembly of problem domain.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="domainAssembly"/> is <see langword="null"/>.
        /// </exception>
        public DomainFixtureBuilder(Assembly domainAssembly)
        {
            if (object.ReferenceEquals(domainAssembly, null))
            {
                throw new ArgumentNullException(nameof(domainAssembly));
            }

            this.domainAssembly = domainAssembly;
        }

        /// <summary>
        ///     Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">
        ///     The request that describes what to create.
        /// </param>
        /// <param name="context">
        ///     A context that can be used to create other specimens.
        /// </param>
        /// <returns>
        ///     The requested specimen if possible;
        ///     otherwise a <see cref="NoSpecimen"/> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            Type requestedType;

            switch (request)
            {
                case SeededRequest sr:
                    {
                        requestedType = sr.Request as Type;
                        break;
                    }

                case ParameterInfo pi:
                    {
                        requestedType = pi.ParameterType;
                        break;
                    }

                default:
                    {
                        return new NoSpecimen();
                    }
            }

            if (object.ReferenceEquals(requestedType, null))
            {
                return new NoSpecimen();
            }

            if (requestedType.Equals(typeof(DomainFixture)))
            {
                return DomainFixture.LoadFrom(this.domainAssembly);
            }

            return new NoSpecimen();
        }
    }
}
