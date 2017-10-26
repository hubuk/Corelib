// -----------------------------------------------------------------------
// <copyright file="DomainCustomization.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using AutoFixture;
    using AutoFixture.AutoNSubstitute;
    using Leet.Testing;

    /// <summary>
    ///     Customizes an <see cref="IFixture"/> by using customizations related to current domain.
    /// </summary>
    internal class DomainCustomization : DomainCustomizationBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainCustomization"/> class.
        /// </summary>
        public DomainCustomization()
            : base(new AutoNSubstituteCustomization())
        {
        }
    }
}
