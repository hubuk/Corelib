// -----------------------------------------------------------------------
// <copyright file="ParadigmTestMethod.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;
    using System.ComponentModel;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    ///     Represents a paradigm test method.
    /// </summary>
    public class ParadigmTestMethod : LongLivedMarshalByRefObject, ITestMethod, IXunitSerializable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTestMethod"/> class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public ParadigmTestMethod()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParadigmTestMethod" /> class.
        /// </summary>
        /// <param name="class">
        ///     tHE test class that this test method belongs to.
        /// </param>
        /// <param name="method">
        ///     the method associated with this test method.
        /// </param>
        public ParadigmTestMethod(ITestClass @class, ParadigmMethodInfo method)
        {
            this.Method = method;
            this.TestClass = @class;
        }

        /// <summary>
        ///     Gets the method associated with this test method.
        /// </summary>
        public ParadigmMethodInfo Method
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the method associated with this test method.
        /// </summary>
        IMethodInfo ITestMethod.Method
        {
            get
            {
                return this.Method;
            }
        }

        /// <summary>
        ///     Gets the test class that this test method belongs to.
        /// </summary>
        public ITestClass TestClass
        {
            get;
            private set;
        }

        /// <summary>
        ///     Called when the object should store its data into the serialization info.
        /// </summary>
        /// <param name="info">
        ///     The info to store the data in.
        /// </param>
        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("MethodName", this.Method.Name, null);
            info.AddValue("SutModificationMethod", this.Method.SutModificationMethod, null);
            info.AddValue("TestClass", this.TestClass, null);
        }

        /// <summary>
        ///     Called when the object should populate itself with data from the serialization info.
        /// </summary>
        /// <param name="info">
        ///     The info to get the data from.
        /// </param>
        public void Deserialize(IXunitSerializationInfo info)
        {
            this.TestClass = info.GetValue<ITestClass>("TestClass");
            string value = info.GetValue<string>("MethodName");
            string sutModificationMethod = info.GetValue<string>("SutModificationMethod");
            IMethodInfo mi = this.TestClass.Class.GetMethod(value, true);
            switch (mi)
            {
                case IReflectionMethodInfo rmi:
                {
                    this.Method = new ParadigmReflectionMethodInfo(rmi, sutModificationMethod);
                    break;
                }

                default:
                {
                    this.Method = new ParadigmMethodInfo(mi, sutModificationMethod);
                    break;
                }
            }
        }
    }
}
