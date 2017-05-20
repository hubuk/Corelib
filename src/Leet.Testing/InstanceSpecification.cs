// -----------------------------------------------------------------------
// <copyright file="InstanceSpecification.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     A base class for instance behavior specification.
    /// </summary>
    public abstract class InstanceSpecification : SpecificationBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceSpecification"/> class.
        /// </summary>
        internal InstanceSpecification()
        {
        }

        /// <summary>
        ///     Gets the name of the method that shall be called to modify an instance system-under-test for the tests
        ///     decorated with non-empty <see cref="ParadigmAttribute(string[])"/> attribute.
        /// </summary>
        public string SutModificationMethod
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Modifies system-under-test object with the modification method which name
        ///     is stored in <see cref="SutModificationMethod"/> property.
        /// </summary>
        /// <param name="sut">
        ///     Instance of the system-under-test.
        /// </param>
        /// <returns>
        ///     A modified instance of the system-under-test.
        /// </returns>
        public object ModifySut(object sut)
        {
            if (object.ReferenceEquals(this.SutModificationMethod, null))
            {
                return sut;
            }

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var thisType = this.GetType();
            var method = thisType.GetMethod(this.SutModificationMethod, bindingFlags);
            return method.Invoke(this, new object[] { sut });
        }
    }
}
