// -----------------------------------------------------------------------
// <copyright file="FieldDefinitionDetails.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;

    /// <summary>
    ///     Represents a detailed information about type's field.
    /// </summary>
    [Flags]
    public enum FieldDefinitionDetails
    {
        /// <summary>
        ///     Public instance field that may be inherited from base types.
        /// </summary>
        Default = 0x00,

        /// <summary>
        ///     Field is declared in the specified type and not inherited.
        /// </summary>
        Declared = 0x01,

        /// <summary>
        ///     Field has protected visibility.
        /// </summary>
        Protected = 0x02,

        /// <summary>
        ///     Field is declared on a type level.
        /// </summary>
        Static = 0x04,

        /// <summary>
        ///     Field is read-only.
        /// </summary>
        ReadOnly = 0x08,

        /// <summary>
        ///     Field is const.
        /// </summary>
        Const = 0x10,
    }
}
