// -----------------------------------------------------------------------
// <copyright file="ParadigmExecutionErrorTestCase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.ComponentModel;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     A simple implementation of <see cref="IXunitTestCase"/> that can be used to report an error rather than running a test.
    /// </summary>
    public class ParadigmExecutionErrorTestCase : ExecutionErrorTestCase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmExecutionErrorTestCase"/> class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public ParadigmExecutionErrorTestCase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmExecutionErrorTestCase"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">
        ///     The message sink used to send diagnostic messages.
        /// </param>
        /// <param name="defaultMethodDisplay">
        ///     Default method display to use (when not customized).
        /// </param>
        /// <param name="testMethod">
        ///     The test method this test case belongs to.
        /// </param>
        /// <param name="errorMessage">
        ///     The error message to report for the test.
        /// </param>
        public ParadigmExecutionErrorTestCase(
            IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay,
            ParadigmTestMethod testMethod,
            string errorMessage)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, errorMessage)
        {
        }

        /// <summary>
        ///     Gets the paradigm test method this test case belongs to.
        /// </summary>
        public ParadigmTestMethod ParadigmTestMethod
        {
            get
            {
                return this.TestMethod as ParadigmTestMethod;
            }
        }

        /// <summary>
        ///     Gets the display name for the test case.
        /// </summary>
        /// <param name="factAttribute">
        ///     The fact attribute the decorated the test case.
        /// </param>
        /// <param name="displayName">
        ///     The base display name from <see cref="TestMethodTestCase.BaseDisplayName"/>.
        /// </param>
        /// <returns>
        ///     The display name for the test case.
        /// </returns>
        protected override string GetDisplayName(IAttributeInfo factAttribute, string displayName)
        {
            string inheritedDisplayName = base.GetDisplayName(factAttribute, displayName);
            return ParadigmTestCase.ComposeDisplayName(inheritedDisplayName, this.BaseDisplayName, this.ParadigmTestMethod);
        }

        /// <summary>
        ///     Gets the unique ID for the test case.
        /// </summary>
        /// <returns>
        ///     The unique ID for the test case.
        /// </returns>
        protected override string GetUniqueID()
        {
            return ParadigmTestCase.ComposeUniqueId(base.GetUniqueID(), this.ParadigmTestMethod.Method.SutModificationMethod);
        }
    }
}
