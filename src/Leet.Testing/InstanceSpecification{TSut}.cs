// -----------------------------------------------------------------------
// <copyright file="InstanceSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System.Reflection;

    /// <summary>
    ///     A base class for a particular type's instance behavior specification.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type of the system-under-test.
    /// </typeparam>
    public abstract class InstanceSpecification<TSut> : InstanceSpecification
    {
        /// <summary>
        ///     Modifies system-under-test object with the modification method which name
        ///     is stored in <see cref="P:SutModificationMethod"/> property.
        /// </summary>
        /// <param name="sut">
        ///     Instance of the system-under-test.
        /// </param>
        /// <returns>
        ///     A modified instance of the system-under-test.
        /// </returns>
        public TSut ModifySut(TSut sut)
        {
            return (TSut)this.ModifySut((object)sut);
        }
    }
}
