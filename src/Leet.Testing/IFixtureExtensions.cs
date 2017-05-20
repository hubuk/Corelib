// -----------------------------------------------------------------------
// <copyright file="IFixtureExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    ///     Defines an extension methods for <see cref="IFixture"/> interface.
    /// </summary>
    public static class IFixtureExtensions
    {
        /// <summary>
        ///     Creates an instance of the system-under-test object.
        /// </summary>
        /// <typeparam name="TSut">
        ///     Type of the system-under-test object.
        /// </typeparam>
        /// <param name="fixture">
        ///     The fixture responsible for creation of objects.
        /// </param>
        /// <returns>
        ///     An instance of the system-under-test object.
        /// </returns>
        /// <remarks>
        ///     This method of creating <typeparamref name="TSut"/> object allows override
        ///     via <see cref="IFixtureOverride{TSut}"/> interface.
        /// </remarks>
        public static TSut CreateSut<TSut>(this IFixture fixture)
        {
            SpecimenContext context = new SpecimenContext(fixture);
            return (TSut)fixture.Create(new SutRequest(typeof(TSut)), context);
        }
    }
}
