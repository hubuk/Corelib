// -----------------------------------------------------------------------
// <copyright file="DataAttributeSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Reflection;
    using Leet.Testing;
    using Ploeh.AutoFixture;
    using Xunit;
    using Xunit.Sdk;

    /// <summary>
    ///     A class that specifies behavior for <see cref="DataAttribute"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="DataAttribute"/> class.
    /// </typeparam>
    public abstract class DataAttributeSpecification<TSut> : ObjectSpecification<TSut>
        where TSut : DataAttribute
    {
        /// <summary>
        ///     Checks whether the <see cref="DataAttribute.GetData(MethodInfo)"/> method throw <see cref="ArgumentNullException"/>
        ///     when called with <see langword="null"/> <see cref="MethodInfo"/> argument.
        /// </summary>
        [Paradigm]
        public void GetData_MethodInfo_CalledWithNullMethodInfo_Trows()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            TSut sut = fixture.Create<TSut>();

            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.GetData(null);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <see cref="DataAttribute.GetData(MethodInfo)"/> method returns collection of arrays of correct size.
        /// </summary>
        /// <param name="theoryMethod">
        ///     Test theory method.
        /// </param>
        [Paradigm]
        [ClassData(typeof(TheoryMethods))]
        public void GetData_MethodInfo_Always_ReturnsCorrectNumberOfArrayItems(MethodInfo theoryMethod)
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            TSut sut = fixture.CreateSut<TSut>();

            // Exercise system
            var data = sut.GetData(theoryMethod);

            // Verify outcome
            Assert.All(data, item => Assert.Equal(theoryMethod.GetParameters().Length, item.Length));

            // Teardown
        }
    }
}
