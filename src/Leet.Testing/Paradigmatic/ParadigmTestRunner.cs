// -----------------------------------------------------------------------
// <copyright file="ParadigmTestRunner.cs" company="Leet">
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
    using System.Threading.Tasks;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     The test runner for paradigm tests.
    /// </summary>
    public class ParadigmTestRunner : XunitTestRunner
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTestRunner"/> class.
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
        public ParadigmTestRunner(
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
            : base(
                  test,
                  messageBus,
                  testClass,
                  constructorArguments,
                  testMethod,
                  testMethodArguments,
                  skipReason,
                  beforeAfterAttributes,
                  aggregator,
                  cancellationTokenSource)
        {
        }

        /// <summary>
        ///     Invokes the test method.
        /// </summary>
        /// <param name="aggregator">
        ///     The exception aggregator used to run code and collect exceptions.
        /// </param>
        /// <returns>
        ///     Returns the execution time (in seconds) spent running the test method.
        /// </returns>
        protected override Task<decimal> InvokeTestMethodAsync(ExceptionAggregator aggregator)
        {
            return new ParadigmTestInvoker(
                this.Test,
                this.MessageBus,
                this.TestClass,
                this.ConstructorArguments,
                this.TestMethod,
                this.TestMethodArguments,
                this.BeforeAfterAttributes,
                aggregator,
                this.CancellationTokenSource).RunAsync();
        }
    }
}
