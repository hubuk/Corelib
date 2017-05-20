// -----------------------------------------------------------------------
// <copyright file="TestInfo.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading;

    /// <summary>
    ///     Contains information about test.
    /// </summary>
    public abstract partial class TestInfo
    {
        /// <summary>
        ///     Lazily initialized object that contains information about types used to compose the test.
        /// </summary>
        private readonly Lazy<SpecificationInfo> typesInfo;

        /// <summary>
        ///     Lazily initialized read-only collection of the additional generic types used in the test.
        /// </summary>
        private readonly Lazy<IReadOnlyList<Type>> additionalGenericTypes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestInfo"/> class.
        /// </summary>
        /// <param name="testMethod">
        ///     Information about the test method.
        /// </param>
        /// <param name="typeUnderTest">
        ///     The type under the test.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="testMethod"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="typeUnderTest"/> is <see langword="null"/>.
        /// </exception>
        internal TestInfo(ParadigmTestMethod testMethod, Type typeUnderTest)
        {
            if (object.ReferenceEquals(testMethod, null))
            {
                throw new ArgumentNullException(nameof(testMethod));
            }

            if (object.ReferenceEquals(typeUnderTest, null))
            {
                throw new ArgumentNullException(nameof(typeUnderTest));
            }

            this.TestMethod = testMethod;
            this.TypeUnderTest = typeUnderTest;
            this.typesInfo = new Lazy<SpecificationInfo>(this.GetSpecificationInfo, LazyThreadSafetyMode.ExecutionAndPublication);
            this.additionalGenericTypes = new Lazy<IReadOnlyList<Type>>(this.CreateAdditionalGenericTypesList, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        ///     Gets the information about the test method.
        /// </summary>
        public ParadigmTestMethod TestMethod
        {
            get;
        }

        /// <summary>
        ///     Gets a type under the test.
        /// </summary>
        public Type TypeUnderTest
        {
            get;
        }

        /// <summary>
        ///     Gets a type that the type under the test is tested as.
        /// </summary>
        public Type TestedAs
        {
            get
            {
                return this.typesInfo.Value.TestedAs;
            }
        }

        /// <summary>
        ///     Gets a type of the specification used in the test.
        /// </summary>
        public Type SpecificationType
        {
            get
            {
                return this.typesInfo.Value.SpecificationType;
            }
        }

        /// <summary>
        ///     Gets the collection of the additional generic types used in the test.
        /// </summary>
        public IReadOnlyList<Type> AdditionalGenericTypes
        {
            get
            {
                return this.additionalGenericTypes.Value;
            }
        }

        /// <summary>
        ///     Captures a information about the test represented by the test method.
        /// </summary>
        /// <param name="testMethod">
        ///     Information about the test method.
        /// </param>
        /// <param name="info">
        ///     Information about the test captured fro mthe test method.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the test and specification information has been successfuly captured from the test method;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public static bool TryCapture(ParadigmTestMethod testMethod, out TestInfo info)
        {
            if (!StaticTypeTestInfo.TryGetTypeUnderTest(testMethod, out Type typeUnderTest))
            {
                if (!InstanceTypeTestInfo.TryGetTypeUnderTest(testMethod, out typeUnderTest))
                {
                    info = null;
                    return false;
                }
            }

            if (!StaticTypeTestInfo.TryCaptureStatic(testMethod, typeUnderTest, out info))
            {
                if (!InstanceTypeTestInfo.TryCaptureInstance(testMethod, typeUnderTest, out info))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            Type testInstanceType = this.TestMethod.TestClass.Class.ToRuntimeType();
            Type nestedTestDeclaringType = GetOutermostDeclaringType(testInstanceType);

            result.Append("(");

            AppendTypeShortName(this.TypeUnderTest, result);

            if (testInstanceType != nestedTestDeclaringType)
            {
                result.Append(".");
                AppendTypeShortName(testInstanceType, result);
            }

            if (this.TypeUnderTest != this.TestedAs)
            {
                result.Append(" as ");
                AppendTypeShortName(this.TestedAs, result);
            }

            if (this.AdditionalGenericTypes.Count > 0)
            {
                result.Append(" with <");

                foreach (var type in this.AdditionalGenericTypes)
                {
                    AppendTypeShortName(type, result);
                    result.Append(",");
                }

                result.Remove(result.Length - 1, 1);
                result.Append(">");
            }

            result.Append(")");

            return result.ToString();
        }

        /// <summary>
        ///     Gets the outermost type that declares the specified type.
        /// </summary>
        /// <param name="type">
        ///     The type which outermost declaring type shall be obtained.
        /// </param>
        /// <returns>
        ///     The outermost type that declares the specified type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        protected static Type GetOutermostDeclaringType(Type type)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            while (type.DeclaringType != null)
            {
                type = type.DeclaringType;
            }

            return type;
        }

        /// <summary>
        ///     Gets the information about test specification from the test method.
        /// </summary>
        /// <returns>
        ///     Information about test specification from the test method.
        /// </returns>
        protected abstract SpecificationInfo GetSpecificationInfo();

        /// <summary>
        ///     Gets the collection of the additional generic types used in the test from the test method.
        /// </summary>
        /// <returns>
        ///     The collection of the additional generic types used in the test from the test method.
        /// </returns>
        protected abstract IEnumerable<Type> GetAdditionalGenericTypes();

        /// <summary>
        ///     Appends specified type short name to the specified string builder.
        /// </summary>
        /// <param name="type">
        ///     The type which short name shall be appended to the specified string builder.
        /// </param>
        /// <param name="builder">
        ///     A string builder to which the specified type short name shall be appended.
        /// </param>
        private static void AppendTypeShortName(Type type, StringBuilder builder)
        {
            if (!type.IsGenericType)
            {
                builder.Append(type.Name);
                return;
            }

            int appendCount = type.Name.IndexOf('`');
            builder.Append(type.Name, 0, appendCount);
            builder.Append("<");

            var genericArguments = type.GetGenericArguments();
            for (int i = 0; i < genericArguments.Length; ++i)
            {
                if (i != 0)
                {
                    builder.Append(",");
                }

                AppendTypeShortName(genericArguments[i], builder);
            }

            builder.Append(">");
        }

        /// <summary>
        ///     Creates a read-only list from the captured additional generic types collection.
        /// </summary>
        /// <returns>
        ///     A read-only list created from the captured additional generic types collection.
        /// </returns>
        private IReadOnlyList<Type> CreateAdditionalGenericTypesList()
        {
            return new ReadOnlyCollection<Type>(this.GetAdditionalGenericTypes().ToList());
        }
    }
}
