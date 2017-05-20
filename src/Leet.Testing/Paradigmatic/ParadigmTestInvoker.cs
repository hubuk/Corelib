// -----------------------------------------------------------------------
// <copyright file="ParadigmTestInvoker.cs" company="Leet">
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
    ///     The test invoker for paradigm tests.
    /// </summary>
    public class ParadigmTestInvoker : XunitTestInvoker
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTestInvoker"/> class.
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
        /// <param name="beforeAfterAttributes">
        ///     The list of <see cref="T:BeforeAfterTestAttributes"/> for this test invocation.
        /// </param>
        /// <param name="aggregator">
        ///     The exception aggregator used to run code and collect exceptions.
        /// </param>
        /// <param name="cancellationTokenSource">
        ///     The task cancellation token source, used to cancel the test run.
        /// </param>
        public ParadigmTestInvoker(
            ITest test,
            IMessageBus messageBus,
            Type testClass,
            object[] constructorArguments,
            MethodInfo testMethod,
            object[] testMethodArguments,
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
                  beforeAfterAttributes,
                  aggregator,
                  cancellationTokenSource)
        {
        }

        /// <summary>
        ///     Creates the test class, unless the test method is static or there have already been errors.
        /// </summary>
        /// <returns>
        ///     The class instance, if appropriate;
        ///     <see langword="null"/>, otherwise.
        /// </returns>
        protected override object CreateTestClass()
        {
            object result = base.CreateTestClass();
            if (result is InstanceSpecification spec)
            {
                spec.SutModificationMethod = (this.Test.TestCase.TestMethod as ParadigmTestMethod).Method.SutModificationMethod;
            }

            return result;
        }
    }
}
