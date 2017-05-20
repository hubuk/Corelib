// -----------------------------------------------------------------------
// <copyright file="IEnumeratorSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Leet.Testing;
    using Ploeh.AutoFixture;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="IEnumerator"/> interface.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="IEnumerator"/> interface.
    /// </typeparam>
    public abstract class IEnumeratorSpecification<TSut> : InstanceSpecification<TSut>
        where TSut : IEnumerator
    {
        /// <summary>
        ///     Gets a value indicating whether the enumerator supports <see cref="IEnumerator.Reset"/> method.
        /// </summary>
        protected abstract bool IsResetSupported
        {
            get;
        }

        /// <summary>
        ///     Generates an enumerator of the collection filled with the specified items.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of items in the requested generator array.
        /// </param>
        /// <param name="items">
        ///     Array of items that shall represent the collection to enumerate.
        /// </param>
        /// <param name="enumerator">
        ///     The enumerator of the generated collection.
        /// </param>
        /// <param name="modify">
        ///     An action that modifies the collection items.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the system-under-test supports generating items from the specified array;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public abstract bool GenerateEnumerator(int itemCount, out object[] items, out TSut enumerator, out Action modify);

        /// <summary>
        ///     A test that checks whether <see cref="IEnumerator.Current"/> property throws an exception when called
        ///     before enumeration starts.
        /// </summary>
        /// <param name="sut">
        ///     The system under test.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void Current_BeforeFirstMoveNext_Throws(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Current;
            });

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether <see cref="IEnumerator.Current"/> property does not throws an exception
        ///     for all items in the collection.
        /// </summary>
        /// <param name="sut">
        ///     The system under test.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void Current_ForNonEmptyCollection_ReturnsItems(TSut sut)
        {
            // Fixture setup

            // Exercise system
            // Verify outcome
            while (sut.MoveNext())
            {
                _ = sut.Current;
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether <see cref="IEnumerator.Current"/> property throws an exception when called
        ///     for first item in non empty collection.
        /// </summary>
        /// <param name="sut">
        ///     The system under test.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void Current_CalledAfterLastItem_Throws(TSut sut)
        {
            // Fixture setup
            while (sut.MoveNext())
            {
            }

            // Exercise system
            // Verify outcome
            Assert.Throws<InvalidOperationException>(() =>
            {
                _ = sut.Current;
            });

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.Current"/> property
        ///     throws an exception when called after modification executed without iteration.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void Current_AfterModificationWithoutIteration_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    _ = sut.Current;
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.Current"/> property
        ///     throws an exception when called after modification executed after iterating one item.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void Current_AfterModificationPostIterationOneItem_ReturnsLastItem()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                sut.MoveNext();
                var expected = sut.Current;
                modify();

                // Exercise system
                var actual = sut.Current;

                // Verify outcome
                Assert.Same(expected, actual);
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.Current"/> returns default value
        ///     when called after modification executed after iterating all items.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void Current_AfterModificationPostAllItems_ReturnsDefaultValue()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                while (sut.MoveNext())
                {
                }

                modify();

                // Exercise system
                // Verify outcome
                var result = sut.Current;
                Assert.True(object.ReferenceEquals(result, null) || result.Equals(Activator.CreateInstance(result.GetType())));
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether an <see cref="IEnumerator.MoveNext"/> method returns <see langword="true"/>
        ///     for all collection item it iterates.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of items in the collection.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void MoveNext_ForAllItems_ReturnsTrue(int itemCount)
        {
            // Fixture setup
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out _))
            {
                // Exercise system
                // Verify outcome
                while (itemCount-- > 0)
                {
                    Assert.True(sut.MoveNext());
                }
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether an <see cref="IEnumerator.MoveNext"/> method returns <see langword="false"/>
        ///     when all collection item have been iterated.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of items in the collection.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void MoveNext_AfterEnumeratingAllItems_ReturnsFalse(int itemCount)
        {
            // Fixture setup
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out _))
            {
                while (itemCount-- > 0)
                {
                    sut.MoveNext();
                }

                // Exercise system
                // Verify outcome
                Assert.False(sut.MoveNext());
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether the enumerator returns no items for empty collection.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void MoveNext_ForEmptyCollection_ReturnsNoItems()
        {
            // Fixture setup
            if (this.GenerateEnumerator(0, out _, out TSut sut, out _))
            {
                // Exercise system
                // Verify outcome
                Assert.False(sut.MoveNext());
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether the enumerator returns collection's items in a correct order.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of items in the collection.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [InlineData(1)]
        [InlineData(3)]
        public void MoveNext_ForNonEmptyCollection_IteratesThroughCorrectItems(int itemCount)
        {
            // Fixture setup
            if (this.GenerateEnumerator(itemCount, out object[] items, out TSut sut, out _))
            {
                int index = 0;

                // Exercise system
                // Verify outcome
                while (sut.MoveNext())
                {
                    Assert.StrictEqual(items[index++], sut.Current);
                }
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether the enumerator iterates through all collection items.
        /// </summary>
        /// <param name="itemCount">
        ///     Number of items in the collection.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [InlineData(1)]
        [InlineData(3)]
        public void MoveNext_ForNonEmptyCollection_IteratesAllItems(int itemCount)
        {
            // Fixture setup
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out _))
            {
                int index = 0;

                // Exercise system
                while (sut.MoveNext())
                {
                    index++;
                }

                // Verify outcome
                Assert.Equal(itemCount, index);
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.MoveNext"/> method
        ///     throws an exception when called after modification executed without iteration.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void MoveNext_AfterModificationWithoutIteration_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    sut.MoveNext();
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.MoveNext"/> method
        ///     throws an exception when called after modification executed after iterating one item.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void MoveNext_AfterModificationPostIterationOneItem_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                sut.MoveNext();
                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    sut.MoveNext();
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.MoveNext"/> returns <see langword="false"/> value
        ///     when called after modification executed after iterating all items.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void MoveNext_AfterModificationPostAllItems_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                while (sut.MoveNext())
                {
                }

                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    sut.MoveNext();
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether an <see cref="IEnumerator.Reset"/> method throws an exception
        ///     when called before iteration.
        /// </summary>
        /// <param name="sut">
        ///     The enumerator under test.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void Reset_BeforeIteration_Throws(TSut sut)
        {
            // Fixture setup
            if (!this.IsResetSupported)
            {
                // Exercise system
                // Verify outcome
                Assert.Throws<NotSupportedException>(() =>
                {
                    sut.Reset();
                });
            }
            else
            {
                // Exercise system
                // Verify outcome
                sut.Reset();
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether an <see cref="IEnumerator.Reset"/> method throws an exception
        ///     when called after first <see cref="IEnumerator.MoveNext"/> method call.
        /// </summary>
        /// <param name="sut">
        ///     The enumerator under test.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void Reset_AfterFirstItem_Throws(TSut sut)
        {
            // Fixture setup
            sut.MoveNext();

            if (!this.IsResetSupported)
            {
                // Exercise system
                // Verify outcome
                Assert.Throws<NotSupportedException>(() =>
                {
                    sut.Reset();
                });
            }
            else
            {
                // Exercise system
                // Verify outcome
                sut.Reset();
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether an <see cref="IEnumerator.Reset"/> method throws an exception
        ///     when called after all items iterated.
        /// </summary>
        /// <param name="sut">
        ///     The enumerator under test.
        /// </param>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        [AutoDomainData]
        public void Reset_AfterAllItems_Throws(TSut sut)
        {
            // Fixture setup
            while (sut.MoveNext())
            {
            }

            if (!this.IsResetSupported)
            {
                // Exercise system
                // Verify outcome
                Assert.Throws<NotSupportedException>(() =>
                {
                    sut.Reset();
                });
            }
            else
            {
                // Exercise system
                // Verify outcome
                sut.Reset();
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.Reset"/> throws an exception
        ///     when called after modification executed without iteration.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void Reset_AfterModificationWithoutIteration_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    sut.Reset();
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.Reset"/> throws an exception
        ///     when called after modification executed after iterating one item.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void Reset_AfterModificationPostIterationOneItem_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                sut.MoveNext();
                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    sut.Reset();
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A test that checks whether a <see cref="IEnumerator.Reset"/> throws an exception
        ///     when called after modification executed after iterating all items.
        /// </summary>
        [Paradigm(nameof(ResetNewInstance), nameof(ResetAfterFirstIteration), nameof(ResetAfterAllItems))]
        public void Reset_AfterModificationPostAllItems_Throws()
        {
            // Fixture setup
            IFixture fixture = DomainFixture.CreateFor(this);
            int itemCount = fixture.Create<int>();
            if (this.GenerateEnumerator(itemCount, out _, out TSut sut, out Action modify))
            {
                while (sut.MoveNext())
                {
                }

                modify();

                // Exercise system
                // Verify outcome
                Assert.Throws<InvalidOperationException>(() =>
                {
                    sut.Reset();
                });
            }

            // Teardown
        }

        /// <summary>
        ///     A system-under-test modification method that resets the enumerator without performing prior iterations.
        /// </summary>
        /// <param name="sut">
        ///     An instance of the system-under-test.
        /// </param>
        /// <returns>
        ///     A modified instance of the system-under-test.
        /// </returns>
        protected virtual TSut ResetNewInstance(TSut sut)
        {
            if (!this.IsResetSupported)
            {
                Assert.Throws<NotSupportedException>(() =>
                {
                    sut.Reset();
                });
            }
            else
            {
                sut.Reset();
            }

            return sut;
        }

        /// <summary>
        ///     A system-under-test modification method that resets the enumerator after advancing one item.
        /// </summary>
        /// <param name="sut">
        ///     An instance of the system-under-test.
        /// </param>
        /// <returns>
        ///     A modified instance of the system-under-test.
        /// </returns>
        protected virtual TSut ResetAfterFirstIteration(TSut sut)
        {
            if (!this.IsResetSupported)
            {
                Assert.Throws<NotSupportedException>(() =>
                {
                    sut.Reset();
                });
            }
            else
            {
                sut.MoveNext();
                sut.Reset();
            }

            return sut;
        }

        /// <summary>
        ///     A system-under-test modification method that resets the enumerator after advancing all items.
        /// </summary>
        /// <param name="sut">
        ///     An instance of the system-under-test.
        /// </param>
        /// <returns>
        ///     A modified instance of the system-under-test.
        /// </returns>
        protected virtual TSut ResetAfterAllItems(TSut sut)
        {
            if (!this.IsResetSupported)
            {
                Assert.Throws<NotSupportedException>(() =>
                {
                    sut.Reset();
                });
            }
            else
            {
                while (sut.MoveNext())
                {
                }

                sut.Reset();
            }

            return sut;
        }
    }
}
