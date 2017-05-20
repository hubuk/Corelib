// -----------------------------------------------------------------------
// <copyright file="IFixtureOverride{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    /// <summary>
    ///     Defines an interface for overriding fixture mechanism for specified system-under-test types.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type of the system-under-test object which creation mechanism shall be overridden.
    /// </typeparam>
    public interface IFixtureOverride<TSut>
    {
        /// <summary>
        ///     Creates an instance of the system-under-test object.
        /// </summary>
        /// <returns>
        ///     An instance of the system-under-test object.
        /// </returns>
        TSut Create();
    }
}
