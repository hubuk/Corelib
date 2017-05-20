// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet.Properties;

    /// <summary>
    ///     A <see langword="static"/> class that contains extension methods for <see cref="IEnumerable{T}"/> class.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Enumetrates all the elements in the collection.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection to iterate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
        public static void Iterate<T>(this IEnumerable<T> source)
        {
            if (object.ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }

            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                }
            }
        }

        /// <summary>
        ///     Creates a new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection in which the new item shall be inserted.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item shall be inserted.
        /// </param>
        /// <param name="item">
        ///     A new item to insert at the specified location.
        /// </param>
        /// <returns>
        ///     A new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="insertAt"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="insertAt"/> is greater than <paramref name="source"/> collection size.
        /// </exception>
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> source, int insertAt, T item)
        {
            if (object.ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (insertAt < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(insertAt), insertAt, Resources.Exception_ArgumentOutOfRange_NegativeValue);
            }

            return InsertCore(source, insertAt, item);
        }

        /// <summary>
        ///     Creates a cartesian product of the specified power using a specified collection as a source.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection for the cartesian product.
        /// </param>
        /// <param name="power">
        ///     A power of the cartesian product to create.
        /// </param>
        /// <returns>
        ///     A cartesian product of the specified collection and specified power.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="power"/> is less than zero.
        /// </exception>
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<T> source, int power)
        {
            if (object.ReferenceEquals(source, null))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (power < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(power), power, Resources.Exception_ArgumentOutOfRange_NegativeValue);
            }

            return Enumerable.Repeat(source, power).CartesianProduct();
        }

        /// <summary>
        ///     Creates a cartesian product of the two collections.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="first">
        ///     A first enumerable collection for cartesian product.
        /// </param>
        /// <param name="second">
        ///     A second enumerable collection for cartesian product.
        /// </param>
        /// <returns>
        ///     A cartesian product of the two collections.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="first"/> is <see langword="null"/>.
        ///     <para>-OR-</para>
        ///     <paramref name="second"/> is <see langword="null"/>.
        /// </exception>
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (object.ReferenceEquals(first, null))
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (object.ReferenceEquals(second, null))
            {
                throw new ArgumentNullException(nameof(second));
            }

            return from f in first
                   from s in second
                   select new[] { f, s };
        }

        /// <summary>
        ///     Creates a cartesian product of the collection of source sequences.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="collections">
        ///     An enumerable collection of the source sequences for the cartesian product.
        /// </param>
        /// <returns>
        ///     A cartesian product of the collection of source sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="collections"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="collections"/> contains <see langword="null"/> element.
        /// </exception>
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> collections)
        {
            if (object.ReferenceEquals(collections, null))
            {
                throw new ArgumentNullException(nameof(collections));
            }

            if (collections.Any(collection => object.ReferenceEquals(collection, null)))
            {
                throw new ArgumentException(Resources.Exception_Argument_CollectionElementNull, nameof(collections));
            }

            if (!collections.Any() || collections.Any(collection => !collection.Any()))
            {
                return Enumerable.Empty<IEnumerable<T>>();
            }

            IEnumerable<IEnumerable<T>> emptyProduct =
              new[] { Enumerable.Empty<T>() };

            return collections.Aggregate(
              emptyProduct,
              (accumulator, sequence) =>
              {
                  return from accseq in accumulator
                         from item in sequence
                         select accseq.Concat(new[] { item });
              });
        }

        /// <summary>
        ///     Obtains an array that corresponds to the specified collection by direct cast if the collection is in fact an array
        ///     or creates a new array with the content taken from the specified collection.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the collection items.
        /// </typeparam>
        /// <param name="collection">
        ///     Collection to convert.
        /// </param>
        /// <returns>
        ///     An array that corresponds to the specified collection.
        /// </returns>
        public static T[] AsArray<T>(this IEnumerable<T> collection)
        {
            if (object.ReferenceEquals(collection, null))
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var result = collection as T[];
            if (object.ReferenceEquals(result, null))
            {
                result = collection.ToArray();
            }

            return result;
        }

        /// <summary>
        ///     Creates a new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the enumerable collection items.
        /// </typeparam>
        /// <param name="source">
        ///     A source collection in which the new item shall be inserted.
        /// </param>
        /// <param name="insertAt">
        ///     An index at which a new item shall be inserted.
        /// </param>
        /// <param name="item">
        ///     A new item to insert at the specified location.
        /// </param>
        /// <returns>
        ///     A new enumerable collection that expands the source collection with a new
        ///     item inserted at the specified location.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="insertAt"/> is greater than <paramref name="source"/> collection size.
        /// </exception>
        private static IEnumerable<T> InsertCore<T>(this IEnumerable<T> source, int insertAt, T item)
        {
            var index = 0;

            foreach (T oldItem in source)
            {
                if (index++ == insertAt)
                {
                    yield return item;
                }

                yield return oldItem;
            }

            if (index == insertAt)
            {
                yield return item;
            }
            else if (index < insertAt)
            {
                throw new ArgumentOutOfRangeException(nameof(insertAt), insertAt, Resources.Exception_ArgumentOutOfRange_InsertionIndexGreaterThanCollectionSize);
            }
        }
    }
}
