// -----------------------------------------------------------------------
// <copyright file="ParadigmTheoryTestCase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     Implementation of the <see cref="IXunitTestCase"/> for xUnit v2 that supports tests decorated with
    ///     <see cref="ParadigmAttribute"/> and taking test theory parameters.
    /// </summary>
    public class ParadigmTheoryTestCase : XunitTheoryTestCase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTheoryTestCase"/> class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public ParadigmTheoryTestCase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTheoryTestCase"/> class.
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
        public ParadigmTheoryTestCase(
            IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay,
            ParadigmTestMethod testMethod)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod)
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
        ///     Executes the test case, returning 0 or more result messages through the message sink.
        /// </summary>
        /// <param name="diagnosticMessageSink">
        ///     The message sink used to send diagnostic messages to.
        /// </param>
        /// <param name="messageBus">
        ///     The message bus to report results to.
        /// </param>
        /// <param name="constructorArguments">
        ///     The arguments to pass to the constructor.
        /// </param>
        /// <param name="aggregator">
        ///     The error aggregator to use for catching exception.
        /// </param>
        /// <param name="cancellationTokenSource">
        ///     The cancellation token source that indicates whether cancellation has been requested.
        /// </param>
        /// <returns>
        ///     Returns the summary of the test case run.
        /// </returns>
        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink, IMessageBus messageBus, object[] constructorArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new ParadigmTheoryTestCaseRunner(this, this.DisplayName, this.SkipReason, constructorArguments, diagnosticMessageSink, messageBus, aggregator, cancellationTokenSource).RunAsync();
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
            return ParadigmTestCase.ComposeDisplayName(inheritedDisplayName, this.BaseDisplayName, this.TestMethod as ParadigmTestMethod);
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
