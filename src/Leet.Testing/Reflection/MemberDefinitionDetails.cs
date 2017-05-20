// -----------------------------------------------------------------------
// <copyright file="MemberDefinitionDetails.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;

    /// <summary>
    ///     Represents a detailed information about type's member.
    /// </summary>
    [Flags]
    public enum MemberDefinitionDetails
    {
        /// <summary>
        ///     Public instance non-virtual member that might be inherited from base types.
        /// </summary>
        Default = 0x00,

        /// <summary>
        ///     Member is declared in the specified type and not inherited.
        /// </summary>
        Declared = 0x01,

        /// <summary>
        ///     Member has protected visibility.
        /// </summary>
        Protected = 0x02,

        /// <summary>
        ///     Member is declared on a type level.
        /// </summary>
        Static = 0x04,

        /// <summary>
        ///     Member is declared abstract.
        /// </summary>
        Abstract = 0x08,

        /// <summary>
        ///     Member is declared virtual.
        /// </summary>
        Virtual = 0x10,
    }
}
