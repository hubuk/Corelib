// -----------------------------------------------------------------------
// <copyright file="StaticTypeTestInfo.cs" company="Leet">
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
    ///     Contains information about static type test.
    /// </summary>
    internal class StaticTypeTestInfo : TestInfo
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StaticTypeTestInfo"/> class.
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
        private StaticTypeTestInfo(ParadigmTestMethod testMethod, Type typeUnderTest)
            : base(testMethod, typeUnderTest)
        {
        }

        /// <summary>
        ///     Captures a information about the static type test represented by the test method.
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
        internal static bool TryCaptureStatic(ParadigmTestMethod testMethod, Type typeUnderTest, out TestInfo info)
        {
            MethodInfo testMethodInfo = testMethod.Method.ToRuntimeMethod();
            Type testDeclarationType = testMethodInfo.DeclaringType;

            if (typeof(StaticSpecification).IsAssignableFrom(testDeclarationType) &&
                testDeclarationType.IsDefined(typeof(StaticSpecificationTypeAttribute), true))
            {
                info = new StaticTypeTestInfo(testMethod, typeUnderTest);
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
        ///     <see langword="true"/> if the specified method is testing a static type returned in <paramref name="typeUnderTest"/>;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryGetTypeUnderTest(ParadigmTestMethod testMethod, out Type typeUnderTest)
        {
            Type testInstanceType = testMethod.TestClass.Class.ToRuntimeType();
            Type nestedTestDeclaringType = GetOutermostDeclaringType(testInstanceType);

            var specificationType = nestedTestDeclaringType;
            while (!object.ReferenceEquals(specificationType, null))
            {
                StaticSpecificationTypeAttribute attribute = specificationType.GetCustomAttribute<StaticSpecificationTypeAttribute>(false);
                if (!object.ReferenceEquals(attribute, null))
                {
                    typeUnderTest = attribute.TargetType;
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
            Type testDeclarationType = testMethodInfo.DeclaringType;

            var specificationType = testDeclarationType;
            while (!object.ReferenceEquals(specificationType, null))
            {
                StaticSpecificationTypeAttribute attribute = specificationType.GetCustomAttribute<StaticSpecificationTypeAttribute>(false);
                if (!object.ReferenceEquals(attribute, null))
                {
                    return new SpecificationInfo(attribute.TargetType, specificationType);
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
            if (!this.SpecificationType.IsGenericType)
            {
                return Enumerable.Empty<Type>();
            }

            var specificationDefinitionArguments = this.SpecificationType.GetGenericTypeDefinition().GetGenericArguments();
            var list = new List<Type>(specificationDefinitionArguments.Length);
            for (int i = 0; i < specificationDefinitionArguments.Length; ++i)
            {
                if (!this.TestedAs.IsGenericType || !this.TestedAs.GetGenericTypeDefinition().GetGenericArguments().Any(arg => arg.Name == specificationDefinitionArguments[i].Name))
                {
                    list.Add(this.SpecificationType.GetGenericArguments()[i]);
                }
            }

            return list.AsEnumerable();
        }
    }
}
