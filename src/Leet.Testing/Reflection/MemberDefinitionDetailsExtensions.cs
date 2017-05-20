// -----------------------------------------------------------------------
// <copyright file="MemberDefinitionDetailsExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System.Reflection;

    /// <summary>
    ///     Defines extension methods for <see cref="MemberDefinitionDetails"/> enumeration.
    /// </summary>
    public static class MemberDefinitionDetailsExtensions
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
        public static BindingFlags ToBindingFlags(this MemberDefinitionDetails details)
        {
            BindingFlags result = BindingFlags.Default;

            if (details.HasFlag(MemberDefinitionDetails.Declared))
            {
                result |= BindingFlags.DeclaredOnly;
            }

            if (details.HasFlag(MemberDefinitionDetails.Static))
            {
                result |= BindingFlags.Static;
            }
            else
            {
                result |= BindingFlags.Instance;
            }

            if (details.HasFlag(MemberDefinitionDetails.Protected))
            {
                result |= BindingFlags.NonPublic;
            }
            else
            {
                result |= BindingFlags.Public;
            }

            return result;
        }
    }
}
