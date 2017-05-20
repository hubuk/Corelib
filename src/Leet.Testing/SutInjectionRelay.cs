// -----------------------------------------------------------------------
// <copyright file="SutInjectionRelay.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Reflection;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    ///     Class that is responsible for inhecting overridden fixture behavior defined via <see cref="IFixtureOverride{TSut}"/> interface.
    /// </summary>
    public class SutInjectionRelay : ISpecimenBuilder
    {
        /// <summary>
        ///     Method info object for <see cref="TryOverrideFixture{TSut}(out TSut)"/> method.
        /// </summary>
        private static MethodInfo overrdeFixtureMethod = typeof(SutInjectionRelay).GetMethod(nameof(TryOverrideFixture), BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        ///     Object that defines a specification for a type-under-test.
        /// </summary>
        private readonly object specification;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SutInjectionRelay"/> class.
        /// </summary>
        /// <param name="specification">
        ///     Object that defines a specification for a type-under-test.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="specification"/> is <see langword="null"/>.
        /// </exception>
        public SutInjectionRelay(object specification)
        {
            if (object.ReferenceEquals(specification, null))
            {
                throw new ArgumentNullException(nameof(specification));
            }

            this.specification = specification;
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
        ///     The requested specimen if possible; otherwise a <see cref="NoSpecimen"/> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            if (object.ReferenceEquals(context, null))
            {
                throw new ArgumentNullException(nameof(context));
            }

            Type sutType = null;

            switch (request)
            {
                case SutRequest sutRequest:
                    {
                        sutType = sutRequest.SutType;
                        break;
                    }

                case ParameterInfo info:
                    {
                        if (string.Equals(info.Name, "sut", StringComparison.Ordinal))
                        {
                            sutType = info.ParameterType;
                        }

                        break;
                    }
            }

            if (object.ReferenceEquals(sutType, null))
            {
                return new NoSpecimen();
            }

            if (!this.TryOverrideFixtureByType(sutType, out object sut))
            {
                sut = context.Resolve(sutType);
            }

            if (this.specification is InstanceSpecification specificationBase)
            {
                sut = specificationBase.ModifySut(sut);
            }

            return sut;
        }

        /// <summary>
        ///     Creates an object that overrides the fixture algoritms if the specification implements override interface
        ///     for the requested type.
        /// </summary>
        /// <typeparam name="TSut">
        ///     Type of the system-under-test object to create.
        /// </typeparam>
        /// <param name="sut">
        ///     The object that overrides the fixture algoritms if the specification implements override interface
        ///     for the requested type.
        /// </param>
        /// <returns>
        ///     <see langword="true"/>  if the override has been sucessfully performed;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        private bool TryOverrideFixture<TSut>(out TSut sut)
        {
            if (this.specification is IFixtureOverride<TSut> overrideFixture)
            {
                sut = overrideFixture.Create();
                return true;
            }

            sut = default(TSut);
            return false;
        }

        /// <summary>
        ///     Creates an object that overrides the fixture algoritms if the specification implements override interface
        ///     for the requested type.
        /// </summary>
        /// <param name="sutType">
        ///     Type of the requested system-under-test object.
        /// </param>
        /// <param name="sut">
        ///     The object that overrides the fixture algoritms if the specification implements override interface
        ///     for the requested type.
        /// </param>
        /// <returns>
        ///     <see langword="true"/>  if the override has been sucessfully performed;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        private bool TryOverrideFixtureByType(Type sutType, out object sut)
        {
            sut = null;
            var genericMethod = overrdeFixtureMethod.MakeGenericMethod(sutType);
            object[] parameters = new object[1];
            bool result = (bool)genericMethod.Invoke(this, parameters);
            sut = parameters[0];
            return result;
        }
    }
}
