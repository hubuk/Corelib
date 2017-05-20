// -----------------------------------------------------------------------
// <copyright file="ParadigmMethodInfo.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    ///     Represents information about a method. The primary implementation is based on runtime reflection,
    ///     but may also be implemented by runner authors to provide non-reflection-based test discovery
    ///     (for example, AST-based runners like CodeRush or Resharper).
    /// </summary>
    public class ParadigmMethodInfo : LongLivedMarshalByRefObject, IMethodInfo
    {
        /// <summary>
        ///     Inner method info to use an information source for current object.
        /// </summary>
        private readonly IMethodInfo inner;

        /// <summary>
        ///     Name of the method that shall be used to modify the system-under-test after its creation.
        /// </summary>
        private readonly string sutModificationMethod;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmMethodInfo"/> class.
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
        public ParadigmMethodInfo(IMethodInfo inner, string sutModificationMethod)
        {
            if (object.ReferenceEquals(inner, null))
            {
                throw new ArgumentNullException(nameof(inner));
            }

            this.inner = inner;
            this.sutModificationMethod = sutModificationMethod;
        }

        /// <summary>
        ///     Gets the name of the method that shall be used to modify the system-under-test after its creation.
        /// </summary>
        public string SutModificationMethod
        {
            get
            {
                return this.sutModificationMethod;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the method is abstract.
        /// </summary>
        public bool IsAbstract
        {
            get
            {
                return this.inner.IsAbstract;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the method is a generic definition (i.e., an open generic).
        /// </summary>
        public bool IsGenericMethodDefinition
        {
            get
            {
                return this.inner.IsGenericMethodDefinition;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the method is public.
        /// </summary>
        public bool IsPublic
        {
            get
            {
                return this.inner.IsPublic;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the method is static.
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return this.inner.IsStatic;
            }
        }

        /// <summary>
        ///     Gets the name of the method.
        /// </summary>
        public string Name
        {
            get
            {
                return this.inner.Name;
            }
        }

        /// <summary>
        ///     Gets the fully qualified type name of the return type.
        /// </summary>
        public ITypeInfo ReturnType
        {
            get
            {
                return this.inner.ReturnType;
            }
        }

        /// <summary>
        ///     Gets a value which represents the class that this method was reflected from
        ///     (i.e., equivalent to MethodInfo.ReflectedType).
        /// </summary>
        public ITypeInfo Type
        {
            get
            {
                return this.inner.Type;
            }
        }

        /// <summary>
        ///     Gets all the custom attributes for the method that are of the given type.
        /// </summary>
        /// <param name="assemblyQualifiedAttributeTypeName">
        ///     The type of the attribute, in assembly qualified form.
        /// </param>
        /// <returns>
        ///     The matching attributes that decorate the method.
        /// </returns>
        public IEnumerable<IAttributeInfo> GetCustomAttributes(string assemblyQualifiedAttributeTypeName)
        {
            return this.inner.GetCustomAttributes(assemblyQualifiedAttributeTypeName);
        }

        /// <summary>
        ///     Gets the types of the generic arguments for generic methods.
        /// </summary>
        /// <returns>
        ///     The argument types.
        /// </returns>
        public IEnumerable<ITypeInfo> GetGenericArguments()
        {
            return this.inner.GetGenericArguments();
        }

        /// <summary>
        ///     Gets information about the parameters to the method.
        /// </summary>
        /// <returns>
        ///     The method's parameters.
        /// </returns>
        public IEnumerable<IParameterInfo> GetParameters()
        {
            return this.inner.GetParameters();
        }

        /// <summary>
        ///     Converts an open generic method into a closed generic method, using the provided type arguments.
        /// </summary>
        /// <param name="typeArguments">
        ///     The type arguments to be used in the generic definition.
        /// </param>
        /// <returns>
        ///     A new <see cref="IMethodInfo"/> that represents the closed generic method.
        /// </returns>
        public IMethodInfo MakeGenericMethod(params ITypeInfo[] typeArguments)
        {
            return this.inner.MakeGenericMethod(typeArguments);
        }
    }
}
