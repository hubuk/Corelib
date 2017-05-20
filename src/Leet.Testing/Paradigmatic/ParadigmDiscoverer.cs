// -----------------------------------------------------------------------
// <copyright file="ParadigmDiscoverer.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     Discovers test cases for a method decorated with a <see cref="ParadigmAttribute"/>.
    /// </summary>
    public class ParadigmDiscoverer : IXunitTestCaseDiscoverer
    {
        /// <summary>
        ///     A discovere that is used to discover tets for methods without parameters.
        /// </summary>
        private readonly FactDiscoverer factDiscovere;

        /// <summary>
        ///     A discovere that is used to discover tets for methods with parameters.
        /// </summary>
        private readonly TheoryDiscoverer theoryDiscovere;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmDiscoverer"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">
        ///     The message sink used to send diagnostic messages.
        /// </param>
        public ParadigmDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.DiagnosticMessageSink = diagnosticMessageSink;
            this.factDiscovere = new FactDiscoverer(diagnosticMessageSink);
            this.theoryDiscovere = new TheoryDiscoverer(diagnosticMessageSink);
        }

        /// <summary>
        ///     Gets the message sink to be used to send diagnostic messages.
        /// </summary>
        protected IMessageSink DiagnosticMessageSink
        {
            get;
        }

        /// <summary>
        ///     Discover test cases from a test method.
        /// </summary>
        /// <param name="discoveryOptions">
        ///     The discovery options to be used.
        /// </param>
        /// <param name="testMethod">
        ///     The test method the test cases belong to.
        /// </param>
        /// <param name="factAttribute">
        ///     The fact attribute attached to the test method.
        /// </param>
        /// <returns>
        ///     Returns zero or more test cases represented by the test method.
        /// </returns>
        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var sutModificationMethods = (factAttribute.GetConstructorArguments().SingleOrDefault() as string[]) ?? Enumerable.Empty<string>();
            sutModificationMethods = sutModificationMethods.Insert(0, null);

            foreach (var testCase in this.DiscoverInner(discoveryOptions, testMethod, factAttribute))
            {
                foreach (var sutModificationMethod in sutModificationMethods)
                {
                    var methodInfo = this.WrapTestMethod(testMethod, sutModificationMethod);

                    switch (testCase)
                    {
                        case XunitTheoryTestCase theoryTestCase:
                        {
                            yield return new ParadigmTheoryTestCase(this.DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), methodInfo);
                            break;
                        }

                        case XunitSkippedDataRowTestCase skippedTestCase:
                        {
                            yield return new ParadigmSkippedDataRowTestCase(this.DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), methodInfo, skippedTestCase.SkipReason, skippedTestCase.TestMethodArguments);
                            break;
                        }

                        case ExecutionErrorTestCase errorTestCase:
                        {
                            yield return new ParadigmExecutionErrorTestCase(this.DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), methodInfo, errorTestCase.ErrorMessage);
                            break;
                        }

                        default:
                        {
                            yield return new ParadigmTestCase(this.DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), methodInfo, testCase.TestMethodArguments);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Discover test cases depending on test method parameter count.
        /// </summary>
        /// <param name="discoveryOptions">
        ///     The discovery options to be used.
        /// </param>
        /// <param name="testMethod">
        ///     The test method the test cases belong to.
        /// </param>
        /// <param name="factAttribute">
        ///     The fact attribute attached to the test method.
        /// </param>
        /// <returns>
        ///     Returns zero or more test cases represented by the test method.
        /// </returns>
        private IEnumerable<IXunitTestCase> DiscoverInner(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            if (testMethod.Method.GetParameters().Any())
            {
                return this.theoryDiscovere.Discover(discoveryOptions, testMethod, factAttribute);
            }
            else
            {
                return this.factDiscovere.Discover(discoveryOptions, testMethod, factAttribute);
            }
        }

        /// <summary>
        ///     Wrapps specified test method with a layer that provides name of the system-under-test modification method.
        /// </summary>
        /// <param name="testMethod">
        ///     Test method.
        /// </param>
        /// <param name="sutModificationMethod">
        ///     Name of the system-under-test modification method that shall be used for the specified test.
        /// </param>
        /// <returns>
        ///     Test method wrapped with a system-under-test modification method name layer.
        /// </returns>
        private ParadigmTestMethod WrapTestMethod(ITestMethod testMethod, string sutModificationMethod)
        {
            ParadigmMethodInfo methodInfo;

            switch (testMethod.Method)
            {
                case IReflectionMethodInfo rmi:
                {
                    methodInfo = new ParadigmReflectionMethodInfo(rmi, sutModificationMethod);
                    break;
                }

                default:
                {
                    methodInfo = new ParadigmMethodInfo(testMethod.Method, sutModificationMethod);
                    break;
                }
            }

            return new ParadigmTestMethod(testMethod.TestClass, methodInfo);
        }
    }
}
