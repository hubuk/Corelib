// -----------------------------------------------------------------------
// <copyright file="MemberVisibilityFlags.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;

    /// <summary>
    ///     Represents a bit field of all available member visibility options.
    /// </summary>
    [Flags]
    public enum MemberVisibilityFlags
    {
        /// <summary>
        ///     No visibility defined for the member.
        /// </summary>
        None = 0x00,

        /// <summary>
        ///     Member has public visibility.
        /// </summary>
        Public = 0x01,

        /// <summary>
        ///     Member is visible by its type family.
        /// </summary>
        Family = 0x02,

        /// <summary>
        ///     Member is visible only by the types defined in the declaring assembly.
        /// </summary>
        Assembly = 0x04,

        /// <summary>
        ///     Member is visible only by its the type family contained in the declaring assembly.
        /// </summary>
        FamilyAndAssembly = 0x08,

        /// <summary>
        ///     Member is visible only by its the type family or all types contained in the declaring assembly.
        /// </summary>
        FamilyOrAssembly = 0x10,

        /// <summary>
        ///     Member is private to its declaring type.
        /// </summary>
        Private = 0x20,

        /// <summary>
        ///     A compound visibility flag that gathers all flags that defines member visible by its type family no matter if visible
        ///     from outside or only inside of the declaring assembly.
        /// </summary>
        AnyFamily = Family | FamilyAndAssembly | FamilyOrAssembly,

        /// <summary>
        ///     A compound visibility flag that gathers all flags that defines member visible by all types defined in the declaring assembly.
        /// </summary>
        AnyAssembly = Assembly | FamilyAndAssembly | FamilyOrAssembly,

        /// <summary>
        ///     A compound visibility flag that gathers all flags that defines member visible by its type family outside the declaring assembly.
        /// </summary>
        PublicFamily = Family | FamilyOrAssembly,

        /// <summary>
        ///     A compound visibility flag that gathers all flags that defines member visible only to the tpyes defined in the declaring assembly.
        /// </summary>
        OnlyAssembly = Assembly | FamilyAndAssembly,

        /// <summary>
        ///     A compound visibility flag that gathers all other visibility flags.
        /// </summary>
        All = Public | Family | Assembly | FamilyAndAssembly | FamilyOrAssembly | Private,
    }
}
