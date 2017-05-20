// -----------------------------------------------------------------------
// <copyright file="PropertyDefinitionDetailsExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     Defines extension methods for <see cref="PropertyDefinitionDetails"/> enumeration.
    /// </summary>
    public static class PropertyDefinitionDetailsExtensions
    {
        /// <summary>
        ///     Translates specified <paramref name="details"/> to a <see cref="BindingFlags"/> equivalent.
        /// </summary>
        /// <param name="details">
        ///     The datails of the member to convert.
        /// </param>
        /// <returns>
        ///     The <see cref="BindingFlags"/> value that corresponds to the specified member <paramref name="details"/>.
        /// </returns>
        public static BindingFlags ToBindingFlags(this PropertyDefinitionDetails details)
        {
            BindingFlags result = BindingFlags.Default;

            if (details.HasFlag(PropertyDefinitionDetails.Declared))
            {
                result |= BindingFlags.DeclaredOnly;
            }

            if (details.HasFlag(PropertyDefinitionDetails.Static))
            {
                result |= BindingFlags.Static;
            }
            else
            {
                result |= BindingFlags.Instance;
            }

            if (details.HasFlag(PropertyDefinitionDetails.PublicGetter) || details.HasFlag(PropertyDefinitionDetails.PublicSetter) ||
                (!details.HasFlag(PropertyDefinitionDetails.ProtectedGetter) && !details.HasFlag(PropertyDefinitionDetails.NoGetter)) ||
                (!details.HasFlag(PropertyDefinitionDetails.ProtectedSetter) && !details.HasFlag(PropertyDefinitionDetails.NoSetter)))
            {
                result |= BindingFlags.Public;
            }

            if (details.HasFlag(PropertyDefinitionDetails.ProtectedGetter) || details.HasFlag(PropertyDefinitionDetails.ProtectedSetter) ||
                (!details.HasFlag(PropertyDefinitionDetails.PublicGetter) && !details.HasFlag(PropertyDefinitionDetails.NoGetter)) ||
                (!details.HasFlag(PropertyDefinitionDetails.PublicSetter) && !details.HasFlag(PropertyDefinitionDetails.NoSetter)))
            {
                result |= BindingFlags.NonPublic;
            }

            return result;
        }

        /// <summary>
        ///     Sets a getter visibility for the specified property definition details.
        /// </summary>
        /// <param name="details">
        ///     Property definition details for which the getter visibility shall be applied.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <returns>
        ///     The specified property definition details with the getter visibility applied.
        /// </returns>
        public static PropertyDefinitionDetails SetGetterVisibility(this PropertyDefinitionDetails details, MemberVisibilityFlags visibility)
        {
            PropertyDefinitionDetails result = details &
                ~(PropertyDefinitionDetails.PublicGetter |
                PropertyDefinitionDetails.ProtectedGetter |
                PropertyDefinitionDetails.NoGetter);

            if (visibility.HasFlag(MemberVisibilityFlags.Family) | visibility.HasFlag(MemberVisibilityFlags.FamilyOrAssembly))
            {
                result |= PropertyDefinitionDetails.ProtectedGetter;
            }

            if (visibility.HasFlag(MemberVisibilityFlags.Public))
            {
                result |= PropertyDefinitionDetails.PublicGetter;
            }

            if (visibility == MemberVisibilityFlags.None)
            {
                result |= PropertyDefinitionDetails.NoGetter;
            }

            return result;
        }

        /// <summary>
        ///     Sets a setter visibility for the specified property definition details.
        /// </summary>
        /// <param name="details">
        ///     Property definition details for which the setter visibility shall be applied.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <returns>
        ///     The specified property definition details with the setter visibility applied.
        /// </returns>
        public static PropertyDefinitionDetails SetSetterVisibility(this PropertyDefinitionDetails details, MemberVisibilityFlags visibility)
        {
            PropertyDefinitionDetails result = details &
                ~(PropertyDefinitionDetails.PublicSetter |
                PropertyDefinitionDetails.ProtectedSetter |
                PropertyDefinitionDetails.NoSetter);

            if (visibility.HasFlag(MemberVisibilityFlags.Family) | visibility.HasFlag(MemberVisibilityFlags.FamilyOrAssembly))
            {
                result |= PropertyDefinitionDetails.ProtectedSetter;
            }

            if (visibility.HasFlag(MemberVisibilityFlags.Public))
            {
                result |= PropertyDefinitionDetails.PublicSetter;
            }

            if (visibility == MemberVisibilityFlags.None)
            {
                result |= PropertyDefinitionDetails.NoSetter;
            }

            return result;
        }
    }
}
