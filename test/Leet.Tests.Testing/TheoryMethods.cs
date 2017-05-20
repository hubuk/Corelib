// -----------------------------------------------------------------------
// <copyright file="TheoryMethods.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Leet.Testing;

    /// <summary>
    ///     Defines various theory methods used as a test data for a test that expects paramters of <see cref="MethodInfo"/> type.
    /// </summary>
    public class TheoryMethods : TestDataProviderBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TheoryMethods"/> class.
        /// </summary>
        public TheoryMethods()
            : base(GetParameters())
        {
        }

        /// <summary>
        ///     A test method with no parameters.
        /// </summary>
        public void NoParameters()
        {
        }

        /// <summary>
        ///     A test method with one <see cref="object"/> parameters.
        /// </summary>
        /// <param name="obj">
        ///     An object parameter.
        /// </param>
        public void OneParameter(object obj)
        {
        }

        /// <summary>
        ///     A test method with one <see cref="int"/> parameters.
        /// </summary>
        /// <param name="number">
        ///     A number parameter.
        /// </param>
        public void OneParameter(int number)
        {
        }

        /// <summary>
        ///     A test method with one <see cref="IDisposable"/> parameters.
        /// </summary>
        /// <param name="disposable">
        ///     A disposable object.
        /// </param>
        public void OneParameter(IDisposable disposable)
        {
        }

        /// <summary>
        ///     A test method with two object parameters.
        /// </summary>
        /// <param name="obj1">
        ///     The first parameter.
        /// </param>
        /// <param name="obj2">
        ///     The second parameter.
        /// </param>
        public void TwoParameters(object obj1, object obj2)
        {
        }

        /// <summary>
        ///     A test method with one <see cref="object"/> and one <see cref="int"/> parameter.
        /// </summary>
        /// <param name="obj">
        ///     The first parameter.
        /// </param>
        /// <param name="number">
        ///     The second parameter.
        /// </param>
        public void TwoParameters(object obj, int number)
        {
        }

        /// <summary>
        ///     A test method with one <see cref="object"/> and one <see cref="IDisposable"/> parameter.
        /// </summary>
        /// <param name="obj">
        ///     The first parameter.
        /// </param>
        /// <param name="disposable">
        ///     The second parameter.
        /// </param>
        public void TwoParameters(object obj, IDisposable disposable)
        {
        }

        /// <summary>
        ///     A test method with one <see cref="int"/> and one <see cref="IDisposable"/> parameter.
        /// </summary>
        /// <param name="number">
        ///     The first parameter.
        /// </param>
        /// <param name="disposable">
        ///     The second parameter.
        /// </param>
        public void TwoParameters(int number, IDisposable disposable)
        {
        }

        /// <summary>
        ///     A test method with one <see cref="object"/> parameter and an array of parameters as <see langword="params"/> parameter collection.
        /// </summary>
        /// <param name="obj">
        ///     The first parameter.
        /// </param>
        /// <param name="others">
        ///     The parameter collection.
        /// </param>
        public void ParamsParameters(object obj, params object[] others)
        {
        }

        /// <summary>
        ///     Gets a collection of <see cref="MethodInfo"/> representing an instance methods declared in the current type as a test method parameters.
        /// </summary>
        /// <returns>
        ///     A collection of <see cref="MethodInfo"/> representing an instance methods declared in the current type as a test method parameters.
        /// </returns>
        private static IEnumerable<object[]> GetParameters()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            return typeof(TheoryMethods).GetMethods(flags)
                .Select(method => new object[] { method });
        }
    }
}
