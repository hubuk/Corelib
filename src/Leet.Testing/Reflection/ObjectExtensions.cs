// -----------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Leet.Testing.Properties;

    /// <summary>
    ///     A <see langword="static"/> class that provides an extension methods for <see cref="object"/> type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Empty array of parameter objects.
        /// </summary>
        private static readonly object[] emptyArguments = new object[0];

        /// <summary>
        ///     Gets the specified field value.
        /// </summary>
        /// <param name="instance">
        ///     Object which field shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <returns>
        ///     The value of the field.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetFieldValue(this object instance, MemberVisibilityFlags visibility, string fieldName)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            FieldInfo field = instance.GetType().GetField(false, visibility, fieldName);
            return field.GetValue(instance);
        }

        /// <summary>
        ///     Set the specified field value.
        /// </summary>
        /// <param name="instance">
        ///     Object which field shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <param name="value">
        ///     Field value to set.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetFieldValue(this object instance, MemberVisibilityFlags visibility, string fieldName, object value)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            FieldInfo field = instance.GetType().GetField(false, visibility, fieldName);
            field.SetValue(null, value);
        }

        /// <summary>
        ///     Adds the specified event handler to the event.
        /// </summary>
        /// <param name="instance">
        ///     Object which event shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="handler">
        ///     Event handler to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void AddEventHandler(this object instance, MemberVisibilityFlags visibility, string eventName, Delegate handler)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            EventInfo eventInfo = instance.GetType().GetEvent(false, visibility, eventName);
            eventInfo.AddEventHandler(instance, handler);
        }

        /// <summary>
        ///     Removes the specified event handler from the event.
        /// </summary>
        /// <param name="instance">
        ///     Object which event shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="handler">
        ///     Event handler to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void RemoveEventHandler(this object instance, MemberVisibilityFlags visibility, string eventName, Delegate handler)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            EventInfo eventInfo = instance.GetType().GetEvent(false, visibility, eventName);
            eventInfo.RemoveEventHandler(instance, handler);
        }

        /// <summary>
        ///     Tries to adds the specified event handler to the event and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which event shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="handler">
        ///     Event handler to add.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static T AddEventHandlerWithException<T>(this object instance, MemberVisibilityFlags visibility, string eventName, Delegate handler)
            where T : Exception
        {
            EventInfo eventInfo = instance.GetType().GetEvent(false, visibility, eventName);

            try
            {
                eventInfo.AddEventHandler(instance, handler);
                throw new ExpectedExceptionNotThrownException();
            }
            catch (TargetInvocationException e) when (e.InnerException is T)
            {
                return (T)e.InnerException;
            }
            catch (Exception e)
            {
                throw new ExpectedExceptionNotThrownException(Resources.ExpectedExceptionNotThrownException_DefaultMessage, e);
            }
        }

        /// <summary>
        ///     Tries to remove the specified event handler from the event and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which event shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="handler">
        ///     Event handler to add.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static T RemoveEventHandlerWithException<T>(this object instance, MemberVisibilityFlags visibility, string eventName, Delegate handler)
            where T : Exception
        {
            EventInfo eventInfo = instance.GetType().GetEvent(false, visibility, eventName);

            try
            {
                eventInfo.RemoveEventHandler(instance, handler);
                throw new ExpectedExceptionNotThrownException();
            }
            catch (TargetInvocationException e) when (e.InnerException is T)
            {
                return (T)e.InnerException;
            }
            catch (Exception e)
            {
                throw new ExpectedExceptionNotThrownException(Resources.ExpectedExceptionNotThrownException_DefaultMessage, e);
            }
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName)
        {
            return GetPropertyValue(instance, visibility, propertyName, Type.EmptyTypes, emptyArguments);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexArguments">
        ///     The array of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, params object[] indexArguments)
        {
            return GetPropertyValue(instance, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, IEnumerable<object> indexArguments)
        {
            return GetPropertyValue(instance, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexParameters">
        ///     The collection of parameter types of the property to be invoked.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = instance.GetType().GetProperty(false, propertyName, indexParameters);

            if (!property.CanRead)
            {
                throw new MissingMemberDeclarationException();
            }

            if (!visibility.IsMatch(property.GetMethod))
            {
                throw new MissingMemberDeclarationException();
            }

            return property.GetValue(instance, indexArguments.ToArray());
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, object value)
        {
            SetPropertyValue(instance, visibility, propertyName, value, Type.EmptyTypes, null);
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <param name="indexArguments">
        ///     The array of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, object value, params object[] indexArguments)
        {
            SetPropertyValue(instance, visibility, propertyName, value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<object> indexArguments)
        {
            SetPropertyValue(instance, visibility, propertyName, value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <param name="indexParameters">
        ///     The collection of parameter types of the property to be invoked.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetPropertyValue(this object instance, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = instance.GetType().GetProperty(false, propertyName, indexParameters);

            if (!property.CanWrite)
            {
                throw new MissingMemberDeclarationException();
            }

            if (!visibility.IsMatch(property.SetMethod))
            {
                throw new MissingMemberDeclarationException();
            }

            property.SetValue(instance, value, indexArguments.AsArray());
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T GetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName)
            where T : Exception
        {
            return GetPropertyValueWithException<T>(instance, visibility, propertyName, Type.EmptyTypes, null);
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexArguments">
        ///     The array of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T GetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, params object[] indexArguments)
            where T : Exception
        {
            return GetPropertyValueWithException<T>(instance, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T GetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, IEnumerable<object> indexArguments)
            where T : Exception
        {
            return GetPropertyValueWithException<T>(instance, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexParameters">
        ///     The collection of parameter types of the property to be invoked.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T GetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
            where T : Exception
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = instance.GetType().GetProperty(false, propertyName, indexParameters);

            if (!property.CanRead)
            {
                throw new MissingMemberDeclarationException();
            }

            if (!visibility.IsMatch(property.GetMethod))
            {
                throw new MissingMemberDeclarationException();
            }

            try
            {
                property.GetValue(instance, indexArguments.AsArray());
                throw new ExpectedExceptionNotThrownException();
            }
            catch (TargetInvocationException e) when (e.InnerException is T)
            {
                return (T)e.InnerException;
            }
            catch (Exception e)
            {
                throw new ExpectedExceptionNotThrownException(Resources.ExpectedExceptionNotThrownException_DefaultMessage, e);
            }
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T SetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, object value)
            where T : Exception
        {
            return SetPropertyValueWithException<T>(instance, visibility, propertyName, value.GetType(), value, Type.EmptyTypes, null);
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <param name="indexArguments">
        ///     The array of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T SetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, object value, params object[] indexArguments)
            where T : Exception
        {
            return SetPropertyValueWithException<T>(instance, visibility, propertyName, value.GetType(), value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T SetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<object> indexArguments)
            where T : Exception
        {
            return SetPropertyValueWithException<T>(instance, visibility, propertyName, value.GetType(), value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which property shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="value">
        ///     Property value to set.
        /// </param>
        /// <param name="indexParameters">
        ///     The collection of parameter types of the property to be invoked.
        /// </param>
        /// <param name="indexArguments">
        ///     The collection of the arguments that shall be passed to the property on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the property during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexArguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T SetPropertyValueWithException<T>(this object instance, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
            where T : Exception
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = instance.GetType().GetProperty(false, propertyName, indexParameters);

            if (!property.CanWrite)
            {
                throw new MissingMemberDeclarationException();
            }

            if (!visibility.IsMatch(property.SetMethod))
            {
                throw new MissingMemberDeclarationException();
            }

            try
            {
                property.SetValue(instance, value, indexArguments.AsArray());
                throw new ExpectedExceptionNotThrownException();
            }
            catch (TargetInvocationException e) when (e.InnerException is T)
            {
                return (T)e.InnerException;
            }
            catch (Exception e)
            {
                throw new ExpectedExceptionNotThrownException(Resources.ExpectedExceptionNotThrownException_DefaultMessage, e);
            }
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters.
        /// </summary>
        /// <param name="instance">
        ///     Object which method shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the method on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     Expected exception has not been thrown by the member.
        /// </exception>
        public static object InvokeMethod(this object instance, MemberVisibilityFlags visibility, string methodName, params object[] arguments)
        {
            return InvokeMethod(instance, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a static with a specified name, return type and set of parameters.
        /// </summary>
        /// <param name="instance">
        ///     Object which method shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the method on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     Expected exception has not been thrown by the member.
        /// </exception>
        public static object InvokeMethod(this object instance, MemberVisibilityFlags visibility, string methodName, IEnumerable<object> arguments)
        {
            return InvokeMethod(instance, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters.
        /// </summary>
        /// <param name="instance">
        ///     Object which method shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to invoke.
        /// </param>
        /// <param name="parameters">
        ///     The collection of parameter types of the method to be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the method on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     Expected exception has not been thrown by the member.
        /// </exception>
        public static object InvokeMethod(this object instance, MemberVisibilityFlags visibility, string methodName, IEnumerable<Type> parameters, IEnumerable<object> arguments)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            MethodInfo method = instance.GetType().GetMethod(false, visibility, methodName, parameters);

            return method.Invoke(instance, arguments.AsArray());
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which method shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the method on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the method during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     Expected exception has not been thrown by the member.
        /// </exception>
        public static T InvokeMethodWithException<T>(this object instance, MemberVisibilityFlags visibility, string methodName, params object[] arguments)
            where T : Exception
        {
            return InvokeMethodWithException<T>(instance, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which method shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to invoke.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the method on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the method during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     Expected exception has not been thrown by the member.
        /// </exception>
        public static T InvokeMethodWithException<T>(this object instance, MemberVisibilityFlags visibility, string methodName, IEnumerable<object> arguments)
            where T : Exception
        {
            return InvokeMethodWithException<T>(instance, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="instance">
        ///     Object which method shall be accessed.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to invoke.
        /// </param>
        /// <param name="parameters">
        ///     The collection of parameter types of the method to be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the method on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the method during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     Expected exception has not been thrown by the member.
        /// </exception>
        public static T InvokeMethodWithException<T>(this object instance, MemberVisibilityFlags visibility, string methodName, IEnumerable<Type> parameters, IEnumerable<object> arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            MethodInfo method = instance.GetType().GetMethod(false, visibility, methodName, parameters);

            try
            {
                method.Invoke(instance, arguments.AsArray());
                throw new ExpectedExceptionNotThrownException();
            }
            catch (TargetInvocationException e) when (e.InnerException is T)
            {
                return (T)e.InnerException;
            }
            catch (Exception e)
            {
                throw new ExpectedExceptionNotThrownException(Resources.ExpectedExceptionNotThrownException_DefaultMessage, e);
            }
        }
    }
}
