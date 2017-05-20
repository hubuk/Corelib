// -----------------------------------------------------------------------
// <copyright file="InstanceTypeTestInfo.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Leet.Testing.Properties;

    /// <summary>
    ///     Contains information about type instance test.
    /// </summary>
    internal class InstanceTypeTestInfo : TestInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceTypeTestInfo"/> class.
        /// </summary>
        /// <param name="testMethod">
        ///     Information about the test method.
        /// </param>
        /// <param name="typeUnderTest">
        ///     Type under the test.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="testMethod"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="typeUnderTest"/> is <see langword="null"/>.
        /// </exception>
        private InstanceTypeTestInfo(ParadigmTestMethod testMethod, Type typeUnderTest)
            : base(testMethod, typeUnderTest)
        {
        }

        /// <summary>
        ///     Captures a information about the type instance test represented by the test method.
        /// </summary>
        /// <param name="testMethod">
        ///     Information about the test method.
        /// </param>
        /// <param name="typeUnderTest">
        ///     Type under the test.
        /// </param>
        /// <param name="info">
        ///     Information about the test captured fro mthe test method.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the test and specification information has been successfuly captured from the test method;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryCaptureInstance(ParadigmTestMethod testMethod, Type typeUnderTest, out TestInfo info)
        {
            MethodInfo testMethodInfo = testMethod.Method.ToRuntimeMethod();
            Type testDeclaringType = testMethodInfo.DeclaringType;

            if (typeof(InstanceSpecification).IsAssignableFrom(testDeclaringType))
            {
                info = new InstanceTypeTestInfo(testMethod, typeUnderTest);
                return true;
            }

            info = null;
            return false;
        }

        /// <summary>
        ///     Gets a type under test for the specified test method.
        /// </summary>
        /// <param name="testMethod">
        ///     Information about the test method.
        /// </param>
        /// <param name="typeUnderTest">
        ///     Type under test for the specified test method.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified method is testing an instance of a type returned in <paramref name="typeUnderTest"/>;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryGetTypeUnderTest(ParadigmTestMethod testMethod, out Type typeUnderTest)
        {
            Type testInstanceType = testMethod.TestClass.Class.ToRuntimeType();
            Type nestedTestDeclaringType = GetOutermostDeclaringType(testInstanceType);

            Type specificationType = nestedTestDeclaringType;
            while (!object.ReferenceEquals(specificationType, null))
            {
                if (!object.ReferenceEquals(specificationType.BaseType, null) &&
                    specificationType.BaseType.Equals(typeof(InstanceSpecification)))
                {
                    typeUnderTest = specificationType.GetGenericArguments()[0];
                    return true;
                }

                specificationType = specificationType.BaseType;
            }

            typeUnderTest = null;
            return false;
        }

        /// <summary>
        ///     Gets the information about test specification from the test method.
        /// </summary>
        /// <returns>
        ///     Information about test specification from the test method.
        /// </returns>
        protected override SpecificationInfo GetSpecificationInfo()
        {
            MethodInfo testMethodInfo = this.TestMethod.Method.ToRuntimeMethod();
            Type testDeclaringType = testMethodInfo.DeclaringType;

            Type instanceSpecificationType = testDeclaringType;
            while (!instanceSpecificationType.BaseType.Equals(typeof(InstanceSpecification)))
            {
                instanceSpecificationType = instanceSpecificationType.BaseType;
            }

            Type concreteTypeUnderTest = instanceSpecificationType.GetGenericArguments()[0];

            var specificationType = testDeclaringType;
            while (!object.ReferenceEquals(specificationType, null))
            {
                if (specificationType.IsAbstract && specificationType.IsGenericType)
                {
                    IList<Type> genericArguments = specificationType.GetGenericArguments();
                    int index = genericArguments.IndexOf(concreteTypeUnderTest);
                    if (index >= 0)
                    {
                        Type typeUnderTestGenericParameter = specificationType.GetGenericTypeDefinition().GetGenericArguments()[index];
                        Type[] constraints = typeUnderTestGenericParameter.GetGenericParameterConstraints();
                        if (constraints.Length == 1)
                        {
                            return new SpecificationInfo(constraints[0].UnderlyingSystemType, specificationType);
                        }
                        else if (constraints.Length == 0)
                        {
                            return new SpecificationInfo(typeof(object), specificationType);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                specificationType = specificationType.BaseType;
            }

            throw new InvalidOperationException(Resources.Exception_InvalidOperation_TestDoesNotContainSpecificationInfo);
        }

        /// <summary>
        ///     Gets the collection of the additional generic types used in the test from the test method.
        /// </summary>
        /// <returns>
        ///     The collection of the additional generic types used in the test from the test method.
        /// </returns>
        protected override IEnumerable<Type> GetAdditionalGenericTypes()
        {
            IList<Type> genericArguments = this.SpecificationType.GetGenericArguments();
            int index = genericArguments.IndexOf(this.TypeUnderTest);

            if (this.SpecificationType.IsGenericType)
            {
                return Enumerable.Empty<Type>();
            }

            var specificationDefinitionArguments = this.SpecificationType.GetGenericTypeDefinition().GetGenericArguments();
            var list = new List<Type>(specificationDefinitionArguments.Length);
            for (int i = 0; i < specificationDefinitionArguments.Length; ++i)
            {
                if (i != index && (!this.TestedAs.IsGenericType || !this.TestedAs.GetGenericTypeDefinition().GetGenericArguments().Any(arg => arg.Name == specificationDefinitionArguments[i].Name)))
                {
                    list.Add(this.SpecificationType.GetGenericArguments()[i]);
                }
            }

            return list.AsEnumerable();
        }
    }
}
