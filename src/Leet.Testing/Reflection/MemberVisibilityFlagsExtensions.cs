// -----------------------------------------------------------------------
// <copyright file="MemberVisibilityFlagsExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;
    using System.Reflection;
    using Leet.Testing.Properties;

    /// <summary>
    ///     Defines extension methods for <see cref="MemberVisibilityFlags"/> enumeration.
    /// </summary>
    public static class MemberVisibilityFlagsExtensions
    {
        /// <summary>
        ///     Determines whether the specified field info matches a vsibility constrain defined by specified flags.
        /// </summary>
        /// <param name="flags">
        ///     The member visibility constraint.
        /// </param>
        /// <param name="info">
        ///     bject that carries detailed info about the member to examine.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified field info matches a vsibility constrain defined by specified flags.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="info"/> is <see langword="null"/>.
        /// </exception>
        public static bool IsMatch(this MemberVisibilityFlags flags, FieldInfo info)
        {
            if (object.ReferenceEquals(info, null))
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (info.IsPublic)
            {
                return flags.HasFlag(MemberVisibilityFlags.Public);
            }

            if (info.IsPrivate)
            {
                return flags.HasFlag(MemberVisibilityFlags.Private);
            }

            if (info.IsFamily)
            {
                return flags.HasFlag(MemberVisibilityFlags.Family);
            }

            if (info.IsFamilyOrAssembly)
            {
                return flags.HasFlag(MemberVisibilityFlags.FamilyOrAssembly);
            }

            if (info.IsFamilyAndAssembly)
            {
                return flags.HasFlag(MemberVisibilityFlags.FamilyAndAssembly);
            }

            if (info.IsAssembly)
            {
                return flags.HasFlag(MemberVisibilityFlags.Assembly);
            }

            throw new ArgumentException(Resources.Exceptions_Argument_MemberInfoUnexpectedVisibility, nameof(info));
        }

        /// <summary>
        ///     Determines whether the specified method info matches a vsibility constrain defined by specified flags.
        /// </summary>
        /// <param name="flags">
        ///     The member visibility constraint.
        /// </param>
        /// <param name="info">
        ///     bject that carries detailed info about the member to examine.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified method info matches a vsibility constrain defined by specified flags.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="info"/> is <see langword="null"/>.
        /// </exception>
        public static bool IsMatch(this MemberVisibilityFlags flags, MethodBase info)
        {
            if (object.ReferenceEquals(info, null))
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (info.IsPublic)
            {
                return flags.HasFlag(MemberVisibilityFlags.Public);
            }

            if (info.IsPrivate)
            {
                return flags.HasFlag(MemberVisibilityFlags.Private);
            }

            if (info.IsFamily)
            {
                return flags.HasFlag(MemberVisibilityFlags.Family);
            }

            if (info.IsFamilyOrAssembly)
            {
                return flags.HasFlag(MemberVisibilityFlags.FamilyOrAssembly);
            }

            if (info.IsFamilyAndAssembly)
            {
                return flags.HasFlag(MemberVisibilityFlags.FamilyAndAssembly);
            }

            if (info.IsAssembly)
            {
                return flags.HasFlag(MemberVisibilityFlags.Assembly);
            }

            throw new ArgumentException(Resources.Exceptions_Argument_MemberInfoUnexpectedVisibility, nameof(info));
        }

        /// <summary>
        ///     Gets a <see cref="BindingFlags"/> value that corresponds to the specified visibility.
        /// </summary>
        /// <param name="visibility">
        ///     Visibility for which the <see cref="BindingFlags"/> shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     <see langword="true"/> if the member is <see langword="static"/>;
        ///     otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        ///     The <see cref="BindingFlags"/> value that corresponds to the specified visibility.
        /// </returns>
        public static BindingFlags ToBindingFlags(this MemberVisibilityFlags visibility, bool isStatic)
        {
            BindingFlags result = BindingFlags.Default;

            if (visibility.HasFlag(MemberVisibilityFlags.Public))
            {
                result |= BindingFlags.Public;
            }

            if ((visibility & ~MemberVisibilityFlags.Public) != MemberVisibilityFlags.None)
            {
                result |= BindingFlags.NonPublic;
            }

            if (isStatic)
            {
                result |= BindingFlags.Static;
            }
            else
            {
                result |= BindingFlags.Instance;
            }

            return result;
        }
    }
}
