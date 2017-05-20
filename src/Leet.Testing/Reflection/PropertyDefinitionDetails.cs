// -----------------------------------------------------------------------
// <copyright file="PropertyDefinitionDetails.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;

    /// <summary>
    ///     Represents a detailed information about type's property.
    /// </summary>
    [Flags]
    public enum PropertyDefinitionDetails
    {
        /// <summary>
        ///     Public instance property that may be inherited from base types.
        /// </summary>
        Default = 0x0000,

        /// <summary>
        ///     Property is declared in the specified type and not inherited.
        /// </summary>
        Declared = 0x0001,

        /// <summary>
        ///     Property is declared on a type level.
        /// </summary>
        Static = 0x0002,

        /// <summary>
        ///     Member is declared abstract.
        /// </summary>
        Abstract = 0x0004,

        /// <summary>
        ///     Member is declared virtual.
        /// </summary>
        Virtual = 0x0008,

        /// <summary>
        ///     Property has public getter defined.
        /// </summary>
        PublicGetter = 0x0010,

        /// <summary>
        ///     Property has protected getter defined.
        /// </summary>
        ProtectedGetter = 0x0020,

        /// <summary>
        ///     Property has no getter defined.
        /// </summary>
        NoGetter = 0x0040,

        /// <summary>
        ///     Property has public setter defined.
        /// </summary>
        PublicSetter = 0x0080,

        /// <summary>
        ///     Property has protected setter defined.
        /// </summary>
        ProtectedSetter = 0x0100,

        /// <summary>
        ///     Property has no setter defined.
        /// </summary>
        NoSetter = 0x0200,

        /// <summary>
        ///     Defines all detail flags.
        /// </summary>
        All = Declared | Static | Abstract | Virtual | PublicGetter | ProtectedGetter | NoGetter | PublicSetter | ProtectedSetter | NoSetter,
    }
}
