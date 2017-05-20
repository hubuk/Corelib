// -----------------------------------------------------------------------
// <copyright file="RandomMultipleRelayTests.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Leet.Specifications;
    using Leet.Testing;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Xunit;

    /// <summary>
    ///     Defines tests for <see cref="RandomMultipleRelay"/> class.
    /// </summary>
    public class RandomMultipleRelayTests : ObjectSpecification<RandomMultipleRelay>
    {
        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay()"/> constructor correctly initialize <see cref="RandomMultipleRelay.MinInclusiveCount"/>
        ///     property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_CorrectlyInitializesMinInclusiveCount()
        {
            // Fixture setup
            int defaultMinInclusiveCount = 1;

            // Exercise system
            RandomMultipleRelay sut = new RandomMultipleRelay();

            // Verify outcome
            Assert.Equal(defaultMinInclusiveCount, sut.MinInclusiveCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay()"/> constructor correctly initialize <see cref="RandomMultipleRelay.MaxExclusiveCount"/>
        ///     property.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_CorrectlyInitializesMaxInclusiveCount()
        {
            // Fixture setup
            int defaultMaxExclusiveCount = 4;

            // Exercise system
            RandomMultipleRelay sut = new RandomMultipleRelay();

            // Verify outcome
            Assert.Equal(defaultMaxExclusiveCount, sut.MaxExclusiveCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay(int, int)"/> constructor correctly initialize <see cref="RandomMultipleRelay.MinInclusiveCount"/>
        ///     property.
        /// </summary>
        /// <param name="minInclusiveCount">
        ///     A minimum inclusive numbers of element to draw from.
        /// </param>
        /// <param name="maxExclusiveCount">
        ///     A maximum exclusive numbers of element to draw from.
        /// </param>
        [Paradigm]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 2)]
        [InlineData(1, 4)]
        public void Constructor_Inte32_Inte32_Always_CorrectlyInitializesMinInclusiveCount(int minInclusiveCount, int maxExclusiveCount)
        {
            // Fixture setup

            // Exercise system
            RandomMultipleRelay sut = new RandomMultipleRelay(minInclusiveCount, maxExclusiveCount);

            // Verify outcome
            Assert.Equal(minInclusiveCount, sut.MinInclusiveCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay(int, int)"/> constructor correctly initialize <see cref="RandomMultipleRelay.MaxExclusiveCount"/>
        ///     property.
        /// </summary>
        /// <param name="minInclusiveCount">
        ///     A minimum inclusive numbers of element to draw from.
        /// </param>
        /// <param name="maxExclusiveCount">
        ///     A maximum exclusive numbers of element to draw from.
        /// </param>
        [Paradigm]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 2)]
        [InlineData(1, 4)]
        public void Constructor_Inte32_Inte32_Always_CorrectlyInitializesMaxExclusiveCount(int minInclusiveCount, int maxExclusiveCount)
        {
            // Fixture setup

            // Exercise system
            RandomMultipleRelay sut = new RandomMultipleRelay(minInclusiveCount, maxExclusiveCount);

            // Verify outcome
            Assert.Equal(maxExclusiveCount, sut.MaxExclusiveCount);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay(int, int)"/> constructor throws exception when called with negative minInclusiveCount parameter.
        /// </summary>
        /// <param name="minInclusiveCount">
        ///     A minimum inclusive numbers of element to draw from.
        /// </param>
        /// <param name="maxExclusiveCount">
        ///     A maximum exclusive numbers of element to draw from.
        /// </param>
        [Paradigm]
        [InlineData(-1, int.MinValue)]
        [InlineData(-1, -2)]
        [InlineData(-1, -1)]
        [InlineData(-1, 0)]
        [InlineData(-1, 1)]
        [InlineData(-1, 2)]
        [InlineData(int.MinValue, int.MinValue)]
        [InlineData(int.MinValue, -2)]
        [InlineData(int.MinValue, -1)]
        [InlineData(int.MinValue, 0)]
        [InlineData(int.MinValue, 1)]
        [InlineData(int.MinValue, 2)]
        public void Constructor_Inte32_Inte32_ForNegativeMinInclusiveCount_Throws(int minInclusiveCount, int maxExclusiveCount)
        {
            // Fixture setup
            // Exercise system

            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(nameof(minInclusiveCount), () =>
            {
                new RandomMultipleRelay(minInclusiveCount, maxExclusiveCount);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay(int, int)"/> constructor throws exception when called with maxExclusiveCount less than or equal to
        ///     minInclusiveCount parameter.
        /// </summary>
        /// <param name="minInclusiveCount">
        ///     A minimum inclusive numbers of element to draw from.
        /// </param>
        /// <param name="maxExclusiveCount">
        ///     A maximum exclusive numbers of element to draw from.
        /// </param>
        [Paradigm]
        [InlineData(0, int.MinValue)]
        [InlineData(0, -1)]
        [InlineData(0, 0)]
        [InlineData(1, int.MinValue)]
        [InlineData(1, -1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        public void Constructor_Inte32_Inte32_ForMaxLessThanOrEualToMin_Throws(int minInclusiveCount, int maxExclusiveCount)
        {
            // Fixture setup
            // Exercise system

            // Verify outcome
            Assert.Throws<ArgumentOutOfRangeException>(nameof(maxExclusiveCount), () =>
            {
                new RandomMultipleRelay(minInclusiveCount, maxExclusiveCount);
            });

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay.Create(object, ISpecimenContext)"/> method returns
        ///     <see cref="NoSpecimen"/> for requests other than <see cref="MultipleRequest"/>.
        /// </summary>
        /// <param name="requestType">
        ///     Type of the request object.
        /// </param>
        [Paradigm]
        [InlineData(typeof(object))]
        [InlineData(typeof(int))]
        [InlineData(typeof(IDisposable))]
        [InlineData(typeof(SeededRequest))]
        public void Create_Object_ISpecimenContext_CalledWithNonMultipleRequest_ReturnsNoSpecimen(Type requestType)
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            RandomMultipleRelay sut = testFixture.Create<RandomMultipleRelay>();
            ISpecimenContext context = new SpecimenContext(testFixture);
            object request = context.Resolve(requestType);

            // Exercise system
            object result = sut.Create(request, context);

            // Verify outcome
            Assert.IsType<NoSpecimen>(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay.Create(object, ISpecimenContext)"/> method returns
        ///     collection for requests of <see cref="MultipleRequest"/> type.
        /// </summary>
        /// <param name="itemType">
        ///     Type of the multiple request item.
        /// </param>
        [Paradigm]
        [InlineData(typeof(object))]
        [InlineData(typeof(int))]
        [InlineData(typeof(IDisposable))]
        [InlineData(typeof(SeededRequest))]
        public void Create_Object_ISpecimenContext_CalledWithMultipleRequest_ReturnsCollection(Type itemType)
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            RandomMultipleRelay sut = testFixture.Create<RandomMultipleRelay>();
            MultipleRequest request = new MultipleRequest(itemType);
            ISpecimenContext context = new SpecimenContext(testFixture);

            // Exercise system
            object result = sut.Create(request, context);

            // Verify outcome
            Assert.IsAssignableFrom<IEnumerable>(result);

            // Teardown
        }

        /// <summary>
        ///     Checks whether <see cref="RandomMultipleRelay.Create(object, ISpecimenContext)"/> method returns
        ///     collection with appropriate numer of items for requests of <see cref="MultipleRequest"/> type.
        /// </summary>
        /// <param name="itemType">
        ///     Type of the multiple request item.
        /// </param>
        [Paradigm]
        [InlineData(typeof(object))]
        [InlineData(typeof(int))]
        [InlineData(typeof(IDisposable))]
        [InlineData(typeof(SeededRequest))]
        public void Create_Object_ISpecimenContext_CalledWithMultipleRequest_ReturnsCollectionWithCorrectNumberOfElements(Type itemType)
        {
            // Fixture setup
            IFixture testFixture = DomainFixture.CreateFor(this);
            RandomMultipleRelay sut = testFixture.Create<RandomMultipleRelay>();
            MultipleRequest request = new MultipleRequest(itemType);
            ISpecimenContext context = new SpecimenContext(testFixture);

            // Exercise system
            object result = sut.Create(request, context);
            IEnumerable<object> collection = result as IEnumerable<object>;
            int count = collection.Count();

            // Verify outcome
            Assert.InRange(count, sut.MinInclusiveCount, sut.MaxExclusiveCount - 1);

            // Teardown
        }
    }
}
