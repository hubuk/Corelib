// -----------------------------------------------------------------------
// <copyright file="TestInfo.SpecificationInfo.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Paradigmatic
{
    using System;

    /// <content>
    ///     Contains information about test.
    /// </content>
    public partial class TestInfo
    {
        /// <summary>
        ///     Contains information about specification used during the test.
        /// </summary>
        protected class SpecificationInfo
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="SpecificationInfo"/> class.
            /// </summary>
            /// <param name="testedAs">
            ///     Type that the type under the test is tested as.
            /// </param>
            /// <param name="specificationType">
            ///     Type of the specification used in the test.
            /// </param>
            /// <exception cref="ArgumentNullException">
            ///     <paramref name="testedAs"/> is <see langword="null"/>.
            ///     <para>-or-</para>
            ///     <paramref name="specificationType"/> is <see langword="null"/>.
            /// </exception>
            public SpecificationInfo(Type testedAs, Type specificationType)
            {
                if (object.ReferenceEquals(testedAs, null))
                {
                    throw new ArgumentNullException(nameof(testedAs));
                }

                if (object.ReferenceEquals(specificationType, null))
                {
                    throw new ArgumentNullException(nameof(specificationType));
                }

                this.TestedAs = testedAs;
                this.SpecificationType = specificationType;
            }

            /// <summary>
            ///     Gets a type that the type under the test is tested as.
            /// </summary>
            public Type TestedAs
            {
                get;
            }

            /// <summary>
            ///     Gets a type of the specification used in the test.
            /// </summary>
            public Type SpecificationType
            {
                get;
            }
        }
    }
}