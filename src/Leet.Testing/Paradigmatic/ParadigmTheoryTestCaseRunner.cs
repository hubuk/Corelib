// -----------------------------------------------------------------------
// <copyright file="ParadigmTheoryTestCaseRunner.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     The test case runner for paradigm theory tests.
    /// </summary>
    public class ParadigmTheoryTestCaseRunner : XunitTheoryTestCaseRunner
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTheoryTestCaseRunner"/> class.
        /// </summary>
        /// <param name="testCase">
        ///     The test case to be run.
        /// </param>
        /// <param name="displayName">
        ///     The display name of the test case.
        /// </param>
        /// <param name="skipReason">
        ///     The skip reason, if the test is to be skipped.
        /// </param>
        /// <param name="constructorArguments">
        ///     The arguments to be passed to the test class constructor.
        /// </param>
        /// <param name="diagnosticMessageSink">
        ///     The message sink used to send diagnostic messages to.
        /// </param>
        /// <param name="messageBus">
        ///     The message bus to report run status to.
        /// </param>
        /// <param name="aggregator">
        ///     The exception aggregator used to run code and collect exceptions.
        /// </param>
        /// <param name="cancellationTokenSource">
        ///     The task cancellation token source, used to cancel the test run.
        /// </param>
        public ParadigmTheoryTestCaseRunner(
            IXunitTestCase testCase,
            string displayName,
            string skipReason,
            object[] constructorArguments,
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
            : base(
                  testCase,
                  displayName,
                  skipReason,
                  constructorArguments,
                  diagnosticMessageSink,
                  messageBus,
                  aggregator,
                  cancellationTokenSource)
        {
        }

        /// <summary>
        ///     Creates the test runner used to run the given test.
        /// </summary>
        /// <param name="test">
        ///     The test that this invocation belongs to.
        /// </param>
        /// <param name="messageBus">
        ///     The message bus to report run status to.
        /// </param>
        /// <param name="testClass">
        ///     The test class that the test method belongs to.
        /// </param>
        /// <param name="constructorArguments">
        ///     The arguments to be passed to the test class constructor.
        /// </param>
        /// <param name="testMethod">
        ///     The test method that will be invoked.
        /// </param>
        /// <param name="testMethodArguments">
        ///     The arguments to be passed to the test method.
        /// </param>
        /// <param name="skipReason">
        ///     The skip reason, if the test is to be skipped.
        /// </param>
        /// <param name="beforeAfterAttributes">
        ///     The list of <see cref="BeforeAfterTestAttribute"/>s for this test invocation.
        /// </param>
        /// <param name="aggregator">
        ///     The exception aggregator used to run code and collect exceptions.
        /// </param>
        /// <param name="cancellationTokenSource">
        ///     The task cancellation token source, used to cancel the test run.
        /// </param>
        /// <returns>
        ///     The test runner used to run the given test.
        /// </returns>
        protected override XunitTestRunner CreateTestRunner(
            ITest test,
            IMessageBus messageBus,
            Type testClass,
            object[] constructorArguments,
            MethodInfo testMethod,
            object[] testMethodArguments,
            string skipReason,
            IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            return new ParadigmTestRunner(
                test,
                messageBus,
                testClass,
                constructorArguments,
                testMethod,
                testMethodArguments,
                skipReason,
                beforeAfterAttributes,
                aggregator,
                cancellationTokenSource);
        }
    }
}
