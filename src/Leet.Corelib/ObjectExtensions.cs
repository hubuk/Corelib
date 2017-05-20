// -----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;

    /// <summary>
    ///     A <see langword="static"/> class that contains extension methods for <see cref="object"/> type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Gets a hash code of the specified object or 0 if the object is a <see langword="null"/> reference.
        /// </summary>
        /// <param name="obj">
        ///     A reference to the object which hash code shall be obtained.
        /// </param>
        /// <returns>
        ///     A hash code of the specified object or 0 if the object is a <see langword="null"/> reference.
        /// </returns>
        public static int GetHashCodeAllowNull(this object obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return 0;
            }

            return obj.GetHashCode();
        }

        /// <summary>
        ///     Returns a string that represents the specified object or specified string if the object is a <see langword="null"/> reference.
        /// </summary>
        /// <param name="obj">
        ///     A reference to the object which string representation shall be obtained.
        /// </param>
        /// <param name="nullRepresentation">
        ///     A value that shall be returned if the specified object is a <see langword="null"/> reference.
        /// </param>
        /// <returns>
        ///     A string that represents the specified object or specified string if the object is a <see langword="null"/> reference.
        /// </returns>
        public static string ToStringAllowNull(this object obj, string nullRepresentation)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return nullRepresentation;
            }

            return obj.ToString();
        }

        /// <summary>
        ///     Gets a type of the specified object and allows the object to be a <see langword="null"/> reference.
        /// </summary>
        /// <param name="value">
        ///     Object which type shall be obtained.
        /// </param>
        /// <returns>
        ///     Type of the specified object or <see langword="null"/> if the object is a <see langword="null"/> reference.
        /// </returns>
        public static Type GetTypeAllowNull(this object value)
        {
            if (object.ReferenceEquals(value, null))
            {
                return null;
            }

            return value.GetType();
        }
    }
}
