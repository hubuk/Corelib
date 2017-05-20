// -----------------------------------------------------------------------
// <copyright file="ParadigmAttribute.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xunit;
    using Xunit.Sdk;

    /// <summary>
    ///     Attribute that is applied to a method to indicate that it is a test method with or without parameters
    ///     that should be run by the test runner. It can also be provided by names of the test class instance
    ///     methods that shall be called during system-under-test creation to modify it in a seperate test runs.
    /// </summary>
    [XunitTestCaseDiscoverer("Leet.Testing.Paradigmatic.ParadigmDiscoverer", "Leet.Testing")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ParadigmAttribute : FactAttribute
    {
        /// <summary>
        ///     Empty string array to share across <see cref="ParadigmAttribute"/> instances initialized without
        ///     SUT modifiers.
        /// </summary>
        private static readonly string[] emptyStringArray = new string[0];

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmAttribute"/> class.
        /// </summary>
        public ParadigmAttribute()
        {
            this.SutModificationMethods = emptyStringArray;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmAttribute"/> class.
        /// </summary>
        /// <param name="sutModificationMethods">
        ///     Array of names of the test class instance methods that shall be called during system-under-test
        ///     creation to modify it in a seperate test runs.
        /// </param>
        public ParadigmAttribute(params string[] sutModificationMethods)
        {
            this.SutModificationMethods = new ReadOnlyCollection<string>(sutModificationMethods);
        }

        /// <summary>
        ///     Gets a read-only collection of system-under-test modificatiom method names.
        /// </summary>
        public IReadOnlyList<string> SutModificationMethods
        {
            get;
        }
    }
}
