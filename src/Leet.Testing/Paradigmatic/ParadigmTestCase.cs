// -----------------------------------------------------------------------
// <copyright file="ParadigmTestCase.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    ///     Implementation of the <see cref="IXunitTestCase"/> for xUnit v2 that supports tests decorated with
    ///     <see cref="ParadigmAttribute"/>.
    /// </summary>
    public class ParadigmTestCase : XunitTestCase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTestCase"/> class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public ParadigmTestCase()
            : base()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTestCase"/> class.
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
        /// <param name="testMethodArguments">
        ///     The arguments for the test method.
        /// </param>
        public ParadigmTestCase(
            IMessageSink diagnosticMessageSink,
            TestMethodDisplay defaultMethodDisplay,
            ParadigmTestMethod testMethod,
            object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
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
            return new ParadigmTestCaseRunner(this, this.DisplayName, this.SkipReason, constructorArguments, this.TestMethodArguments, messageBus, aggregator, cancellationTokenSource).RunAsync();
        }

        /// <summary>
        ///     Composes a <see cref="string"/> that represents a display name for the specified test.
        /// </summary>
        /// <param name="inheritedDisplayName">
        ///     Base test display name.
        /// </param>
        /// <param name="baseDisplayName">
        ///     Base display name of the test,
        /// </param>
        /// <param name="testMethod">
        ///     Test method that represetms a specified test.
        /// </param>
        /// <returns>
        ///     The composed display name for the test.
        /// </returns>
        internal static string ComposeDisplayName(string inheritedDisplayName, string baseDisplayName, ParadigmTestMethod testMethod)
        {
            if (!TestInfo.TryCapture(testMethod, out TestInfo info))
            {
                return inheritedDisplayName;
            }

            string result = info.ToString();

            string testName = testMethod.Method.Name;
            string arguments = inheritedDisplayName.Substring(baseDisplayName.Length);
            string modification = testMethod.Method.SutModificationMethod;

            if (modification != null)
            {
                return result + " " + modification + " then " + testName + arguments;
            }
            else
            {
                return result + " " + testName + arguments;
            }
        }

        /// <summary>
        ///     Composes a <see cref="string"/> that represents an unique identifier composed of base test unique ID and
        ///     system-under-test modification method.
        /// </summary>
        /// <param name="inheritedUniqueId">
        ///     Base test unique identifier.
        /// </param>
        /// <param name="sutModificationMethod">
        ///     Name of the method that shall be used to modify an instance of the system-under-test,
        /// </param>
        /// <returns>
        ///     The unique identifier composed of base test unique ID and system-under-test modification method.
        /// </returns>
        internal static string ComposeUniqueId(string inheritedUniqueId, string sutModificationMethod)
        {
            if (object.ReferenceEquals(sutModificationMethod, null))
            {
                return inheritedUniqueId;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(memoryStream))
            {
                writer.Write(inheritedUniqueId);
                writer.Write(sutModificationMethod);
                writer.Flush();

                memoryStream.Position = 0L;
                byte[] resultArray = new byte[20];
                byte[] sourceArray = memoryStream.ToArray();
                Sha1Digest sha1 = new Sha1Digest();
                sha1.BlockUpdate(sourceArray, 0, sourceArray.Length);
                sha1.DoFinal(resultArray, 0);

                return string.Join(string.Empty, resultArray.Select(h => h.ToString("X2")));
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
