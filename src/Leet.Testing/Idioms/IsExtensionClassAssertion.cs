// -----------------------------------------------------------------------
// <copyright file="IsExtensionClassAssertion.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Idioms
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Ploeh.AutoFixture.Idioms;

    /// <summary>
    ///     The base class for idiomatic assertion that checks whether the specified type defines an extension class.
    /// </summary>
    public class IsExtensionClassAssertion : IdiomaticAssertion
    {
        /// <summary>
        ///     Checks whether the specified type is a correct extension class.
        /// </summary>
        /// <param name="type">
        ///     Type to check.
        /// </param>
        public override void Verify(Type type)
        {
            if (!type.IsClass || !type.IsPublic || !(type.IsAbstract && type.IsSealed))
            {
                throw new InvalidExtensionClassImplementationException();
            }

            if (type.GetFields().Any(fi => !fi.IsStatic))
            {
                throw new InvalidExtensionClassImplementationException();
            }

            if (type.GetConstructors().Any(ci => !ci.IsStatic))
            {
                throw new InvalidExtensionClassImplementationException();
            }

            if (type.GetEvents().Any(ei => !ei.AddMethod.IsStatic))
            {
                throw new InvalidExtensionClassImplementationException();
            }

            if (type.GetProperties().Any(pi => (pi.CanRead && !pi.GetMethod.IsStatic) || (pi.CanWrite && !pi.SetMethod.IsStatic)))
            {
                throw new InvalidExtensionClassImplementationException();
            }

            if (type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Any(mi => !mi.IsStatic || (mi.IsPublic && !mi.IsDefined(typeof(ExtensionAttribute)))))
            {
                throw new InvalidExtensionClassImplementationException();
            }
        }
    }
}
