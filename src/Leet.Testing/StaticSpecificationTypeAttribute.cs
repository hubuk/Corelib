// -----------------------------------------------------------------------
// <copyright file="StaticSpecificationTypeAttribute.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using System;

    /// <summary>
    ///     Attribute that shall be used with <see cref="StaticSpecification"/> type as an indication of a type
    ///     which static behavior is being defined.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class StaticSpecificationTypeAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StaticSpecificationTypeAttribute"/> class.
        /// </summary>
        /// <param name="targetType">
        ///     The type for which the specification is being defined.
        /// </param>
        public StaticSpecificationTypeAttribute(Type targetType)
        {
            if (object.ReferenceEquals(targetType, null))
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            this.TargetType = targetType;
        }

        /// <summary>
        ///     Gets the type for which the specification is being defined.
        /// </summary>
        public Type TargetType
        {
            get;
        }
    }
}
