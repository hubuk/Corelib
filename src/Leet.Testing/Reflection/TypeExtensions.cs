// -----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Leet">
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
    using Castle.DynamicProxy;
    using Leet.Testing.Properties;

    /// <summary>
    ///     A <see langword="static"/> class that contains an extension methods for <see cref="Type"/> type.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Empty array of parameter objects.
        /// </summary>
        private static readonly object[] emptyArguments = new object[0];

        /// <summary>
        ///     Gets a collection which contains the specified <paramref name="type"/> as its first elements and sequence of its recurrent base types following.
        /// </summary>
        /// <param name="type">
        ///     The type which hierarchy shall be obtained.
        /// </param>
        /// <returns>
        ///     The collection which contains the specified <paramref name="type"/> as its first elements and sequence of its recurrent base types following.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        public static IEnumerable<Type> GetTypeHierarchy(this Type type)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type currentType = type;

            do
            {
                yield return currentType;
                currentType = currentType.BaseType;
            }
            while (!object.ReferenceEquals(currentType, null));
        }

        /// <summary>
        ///     Determines whether the secified <paramref name="type"/> declares implementation of the the specified <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="interfaceType">
        ///     The type of the interface that the <paramref name="type"/> shall implement.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the <paramref name="type"/> has <paramref name="interfaceType"/> implementation on its interface list;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="interfaceType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="interfaceType"/> is not an interface type.
        /// </exception>
        public static bool DeclaresInterface(this Type type, Type interfaceType)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(interfaceType, null))
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NotInterfaceType, nameof(interfaceType));
            }

            return type.GetInterfaces().Any(declaredInterface => interfaceType.Equals(declaredInterface));
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified field.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the field declaration.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <param name="fieldType">
        ///     The requested field's type.
        /// </param>
        /// <returns>
        ///     When the specified field is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consist of white spaces only.
        /// </exception>
        public static bool IsFieldDeclared(this Type type, FieldDefinitionDetails details, string fieldName, Type fieldType)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(fieldName, null))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(fieldName));
            }

            if (object.ReferenceEquals(fieldType, null))
            {
                throw new ArgumentNullException(nameof(fieldType));
            }

            return TryGetField(type, details, fieldName, fieldType, out _);
        }

        /// <summary>
        ///     Gets the specified field value.
        /// </summary>
        /// <param name="type">
        ///     Type which field shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetFieldValue(this Type type, MemberVisibilityFlags visibility, string fieldName)
        {
            FieldInfo field = GetField(type, true, visibility, fieldName);
            return field.GetValue(null);
        }

        /// <summary>
        ///     Set the specified field value.
        /// </summary>
        /// <param name="type">
        ///     Type which field shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetFieldValue(this Type type, MemberVisibilityFlags visibility, string fieldName, object value)
        {
            FieldInfo field = GetField(type, true, visibility, fieldName);
            field.SetValue(null, value);
        }

        /// <summary>
        ///     Gets a field for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared field shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the field declaration.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <param name="fieldType">
        ///     Type of the field value.
        /// </param>
        /// <returns>
        ///     The field for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static FieldInfo GetField(this Type type, FieldDefinitionDetails details, string fieldName, Type fieldType)
        {
            if (!TryGetField(type, details, fieldName, fieldType, out FieldInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a field for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared field shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the field declaration.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <param name="fieldType">
        ///     Type of the field value.
        /// </param>
        /// <param name="result">
        ///     The field for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested field is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        public static bool TryGetField(this Type type, FieldDefinitionDetails details, string fieldName, Type fieldType, out FieldInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(fieldName, null))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(fieldName));
            }

            if (object.ReferenceEquals(fieldType, null))
            {
                throw new ArgumentNullException(nameof(fieldType));
            }

            var flags = details.ToBindingFlags();

            result = type.GetField(
                fieldName,
                flags);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (details.HasFlag(FieldDefinitionDetails.Protected) != (result.IsFamilyOrAssembly || result.IsFamily))
            {
                result = null;
                return false;
            }

            if (details.HasFlag(FieldDefinitionDetails.ReadOnly) != result.IsInitOnly)
            {
                result = null;
                return false;
            }

            if (details.HasFlag(FieldDefinitionDetails.Const) != result.IsLiteral)
            {
                result = null;
                return false;
            }

            if (!result.FieldType.Equals(fieldType))
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified constructor.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <returns>
        ///     When the specified constructor is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        public static bool IsConsructorDeclared(this Type type, MemberVisibilityFlags visibility)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsConsructorDeclared(type, visibility, Type.EmptyTypes);
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified constructor.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <returns>
        ///     When the specified constructor is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static bool IsConsructorDeclared(this Type type, MemberVisibilityFlags visibility, params Type[] parameters)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            return IsConsructorDeclared(type, visibility, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified constructor.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <returns>
        ///     When the specified constructor is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static bool IsConsructorDeclared(this Type type, MemberVisibilityFlags visibility, IEnumerable<Type> parameters)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            return TryGetConstructor(type, visibility, parameters, out _);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeConstructor(this Type type)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InvokeConstructor(type, Type.EmptyTypes, emptyArguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeConstructor(this Type type, params object[] arguments)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InvokeConstructor(type, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeConstructor(this Type type, IEnumerable<object> arguments)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return InvokeConstructor(type, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="parameters">
        ///     The collection of parameter types of the constructor to be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static object InvokeConstructor(this Type type, IEnumerable<Type> parameters, IEnumerable<object> arguments)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            ConstructorInfo constructor = GetConstructor(type, MemberVisibilityFlags.Public, parameters);

            return constructor.Invoke(arguments.AsArray());
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorWithException<T>(this Type type)
            where T : Exception
        {
            return InvokeConstructorWithException<T>(type, false, Type.EmptyTypes, emptyArguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorWithException<T>(this Type type, params object[] arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InvokeConstructorWithException<T>(type, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorWithException<T>(this Type type, IEnumerable<object> arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return InvokeConstructorWithException<T>(type, false, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="parameters">
        ///     The collection of parameter types of the constructor to be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of constructor arguments that shall be passed during invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorWithException<T>(this Type type, IEnumerable<Type> parameters, IEnumerable<object> arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            ConstructorInfo constructor = GetConstructor(type, MemberVisibilityFlags.Public, parameters);

            try
            {
                constructor.Invoke(arguments.AsArray());
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
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeConstructorViaProxy(this Type type)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InvokeConstructorViaProxy(type, Type.EmptyTypes, emptyArguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeConstructorViaProxy(this Type type, params object[] arguments)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InvokeConstructorViaProxy(type, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        public static object InvokeConstructorViaProxy(this Type type, IEnumerable<object> arguments)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return InvokeConstructorViaProxy(type, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="parameters">
        ///     The collection of parameter types of the constructor to be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     Result returned by the invoked constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static object InvokeConstructorViaProxy(this Type type, IEnumerable<Type> parameters, IEnumerable<object> arguments)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            DefaultProxyBuilder proxyBuilder = new DefaultProxyBuilder();
            Type proxyType = proxyBuilder.CreateClassProxyType(type, null, ProxyGenerationOptions.Default);

            IEnumerable<Type> extendedParameters = parameters.Insert(0, typeof(IInterceptor[]));
            IEnumerable<object> extendedArguments = arguments.Insert(0, null);

            ConstructorInfo constructor = GetConstructor(proxyType, MemberVisibilityFlags.Public, extendedParameters);
            return constructor.Invoke(extendedArguments.AsArray());
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorViaProxyWithException<T>(this Type type)
            where T : Exception
        {
            return InvokeConstructorViaProxyWithException<T>(type, false, Type.EmptyTypes, emptyArguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorViaProxyWithException<T>(this Type type, params object[] arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InvokeConstructorViaProxyWithException<T>(type, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of the arguments that shall be passed to the constructor on invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorViaProxyWithException<T>(this Type type, IEnumerable<object> arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return InvokeConstructorViaProxyWithException<T>(type, false, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a constructor of the specified type via proxy object with a specified set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which constructor shall be invoked.
        /// </param>
        /// <param name="parameters">
        ///     The collection of parameter types of the constructor to be invoked.
        /// </param>
        /// <param name="arguments">
        ///     The collection of constructor arguments that shall be passed during invocation.
        /// </param>
        /// <returns>
        ///     An exception throw by the constructor during execution.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        ///     <para>-or-</para>
        ///     <paramref name="arguments"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="ExpectedExceptionNotThrownException">
        ///     The expected exception has not been thrown by he invoked member.
        /// </exception>
        public static T InvokeConstructorViaProxyWithException<T>(this Type type, IEnumerable<Type> parameters, IEnumerable<object> arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            DefaultProxyBuilder proxyBuilder = new DefaultProxyBuilder();
            Type proxyType = proxyBuilder.CreateClassProxyType(type, null, ProxyGenerationOptions.Default);

            IEnumerable<Type> extendedParameters = parameters.Insert(0, typeof(IInterceptor[]));
            IEnumerable<object> extendedArguments = arguments.Insert(0, null);

            ConstructorInfo constructor = GetConstructor(proxyType, MemberVisibilityFlags.Public, extendedParameters);

            try
            {
                constructor.Invoke(extendedArguments.AsArray());
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
        ///     Gets a constructor for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared constructor shall be obtained.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <returns>
        ///     The constructor for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static ConstructorInfo GetConstructor(this Type type, MemberVisibilityFlags visibility)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetConstructor(type, visibility, Type.EmptyTypes);
        }

        /// <summary>
        ///     Gets a constructor for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared constructor shall be obtained.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <returns>
        ///     The constructor for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static ConstructorInfo GetConstructor(this Type type, MemberVisibilityFlags visibility, params Type[] parameters)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            return GetConstructor(type, visibility, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Gets a constructor for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared constructor shall be obtained.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <returns>
        ///     The constructor for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static ConstructorInfo GetConstructor(this Type type, MemberVisibilityFlags visibility, IEnumerable<Type> parameters)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            if (!TryGetConstructor(type, visibility, parameters, out ConstructorInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a constructor for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared constructor shall be obtained.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="result">
        ///     The constructor for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested constructor is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        public static bool TryGetConstructor(this Type type, MemberVisibilityFlags visibility, out ConstructorInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            return TryGetConstructor(type, visibility, Type.EmptyTypes, out result);
        }

        /// <summary>
        ///     Gets a constructor for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared constructor shall be obtained.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="parameters">
        ///     The requested constructor's parameter types.
        /// </param>
        /// <param name="result">
        ///     The constructor for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested constructor is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static bool TryGetConstructor(this Type type, MemberVisibilityFlags visibility, IEnumerable<Type> parameters, out ConstructorInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            BindingFlags flags = visibility.ToBindingFlags(false);

            result = type.GetConstructor(
                flags,
                Type.DefaultBinder,
                parameters.AsArray(),
                null);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (!visibility.IsMatch(result))
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified event.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the event declaration.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="eventType">
        ///     The requested event's type.
        /// </param>
        /// <returns>
        ///     When the specified event is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        public static bool IsEventDeclared(this Type type, MemberDefinitionDetails details, string eventName, Type eventType)
        {
            return TryGetEvent(type, details, eventName, eventType, out _);
        }

        /// <summary>
        ///     Adds the specified event handler to the event.
        /// </summary>
        /// <param name="type">
        ///     Type which event shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void AddEventHandler(this Type type, MemberVisibilityFlags visibility, string eventName, Delegate handler)
        {
            EventInfo eventInfo = GetEvent(type, true, visibility, eventName);
            eventInfo.AddEventHandler(null, handler);
        }

        /// <summary>
        ///     Removes the specified event handler from the event.
        /// </summary>
        /// <param name="type">
        ///     Type which event shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void RemoveEventHandler(this Type type, MemberVisibilityFlags visibility, string eventName, Delegate handler)
        {
            EventInfo eventInfo = GetEvent(type, true, visibility, eventName);
            eventInfo.RemoveEventHandler(null, handler);
        }

        /// <summary>
        ///     Tries to adds the specified event handler to the event and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which event shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static T AddEventHandlerWithException<T>(this Type type, MemberVisibilityFlags visibility, string eventName, Delegate handler)
            where T : Exception
        {
            EventInfo eventInfo = GetEvent(type, true, visibility, eventName);

            try
            {
                eventInfo.AddEventHandler(null, handler);
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
        /// <param name="type">
        ///     Type which event shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static T RemoveEventHandlerWithException<T>(this Type type, MemberVisibilityFlags visibility, string eventName, Delegate handler)
            where T : Exception
        {
            EventInfo eventInfo = GetEvent(type, true, visibility, eventName);

            try
            {
                eventInfo.RemoveEventHandler(null, handler);
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
        ///     Gets a event for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared event shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the event declaration.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="eventType">
        ///     Type of the event value.
        /// </param>
        /// <returns>
        ///     The event for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static EventInfo GetEvent(this Type type, MemberDefinitionDetails details, string eventName, Type eventType)
        {
            if (!TryGetEvent(type, details, eventName, eventType, out EventInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets an event for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared event shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the event declaration.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="eventType">
        ///     Type of the event value.
        /// </param>
        /// <param name="result">
        ///     The event for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested event is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        public static bool TryGetEvent(this Type type, MemberDefinitionDetails details, string eventName, Type eventType, out EventInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(eventName, null))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(eventName));
            }

            if (object.ReferenceEquals(eventType, null))
            {
                throw new ArgumentNullException(nameof(eventType));
            }

            BindingFlags flags = details.ToBindingFlags();

            result = type.GetEvent(
                eventName,
                flags);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (details.HasFlag(MemberDefinitionDetails.Protected) && (!result.AddMethod.IsFamilyOrAssembly && !result.AddMethod.IsFamily))
            {
                result = null;
                return false;
            }

            if (result.AddMethod.IsAbstract != details.HasFlag(MemberDefinitionDetails.Abstract))
            {
                result = null;
                return false;
            }

            if (details.HasFlag(MemberDefinitionDetails.Virtual) != (result.AddMethod.IsVirtual && !result.AddMethod.IsFinal))
            {
                result = null;
                return false;
            }

            if (!result.EventHandlerType.Equals(eventType))
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified property.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     The requested property's type.
        /// </param>
        /// <returns>
        ///     When the specified property is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        public static bool IsPropertyDeclared(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType)
        {
            return IsPropertyDeclared(type, details, propertyName, propertyType, (IEnumerable<Type>)Type.EmptyTypes);
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified property.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     The requested property's type.
        /// </param>
        /// <param name="indexParameters">
        ///     The requested property's index parameter types.
        /// </param>
        /// <returns>
        ///     When the specified property is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        public static bool IsPropertyDeclared(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, params Type[] indexParameters)
        {
            return IsPropertyDeclared(type, details, propertyName, propertyType, (IEnumerable<Type>)indexParameters);
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified property.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     The requested property's type.
        /// </param>
        /// <param name="indexParameters">
        ///     The requested property's index parameter types.
        /// </param>
        /// <returns>
        ///     When the specified property is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        public static bool IsPropertyDeclared(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters)
        {
            return TryGetProperty(type, details, propertyName, propertyType, indexParameters, out _);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static object GetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName)
        {
            return GetPropertyValue(type, visibility, propertyName, Type.EmptyTypes, emptyArguments);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static object GetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, params object[] indexArguments)
        {
            return GetPropertyValue(type, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static object GetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, IEnumerable<object> indexArguments)
        {
            return GetPropertyValue(type, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Gets the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static object GetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
        {
            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = GetProperty(type, true, propertyName, indexParameters);

            if (!property.CanRead)
            {
                throw new MissingMemberDeclarationException();
            }

            if (!visibility.IsMatch(property.GetMethod))
            {
                throw new MissingMemberDeclarationException();
            }

            return property.GetValue(null, indexArguments.ToArray());
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void SetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, object value)
        {
            SetPropertyValue(type, visibility, propertyName, value, Type.EmptyTypes, null);
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static void SetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, object value, params object[] indexArguments)
        {
            SetPropertyValue(type, visibility, propertyName, value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static void SetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<object> indexArguments)
        {
            SetPropertyValue(type, visibility, propertyName, value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Set the specified property value.
        /// </summary>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static void SetPropertyValue(this Type type, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
        {
            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = GetProperty(type, true, propertyName, indexParameters);

            if (!property.CanWrite)
            {
                throw new MissingMemberDeclarationException();
            }

            if (!visibility.IsMatch(property.SetMethod))
            {
                throw new MissingMemberDeclarationException();
            }

            property.SetValue(null, value, indexArguments.AsArray());
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T GetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName)
            where T : Exception
        {
            return GetPropertyValueWithException<T>(type, visibility, propertyName, Type.EmptyTypes, null);
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T GetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, params object[] indexArguments)
            where T : Exception
        {
            return GetPropertyValueWithException<T>(type, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T GetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, IEnumerable<object> indexArguments)
            where T : Exception
        {
            return GetPropertyValueWithException<T>(type, visibility, propertyName, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to get the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T GetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
            where T : Exception
        {
            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = GetProperty(type, true, propertyName, indexParameters);

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
                property.GetValue(null, indexArguments.AsArray());
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
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T SetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, object value)
            where T : Exception
        {
            return SetPropertyValueWithException<T>(type, visibility, propertyName, value.GetType(), value, Type.EmptyTypes, null);
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T SetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, object value, params object[] indexArguments)
            where T : Exception
        {
            return SetPropertyValueWithException<T>(type, visibility, propertyName, value.GetType(), value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T SetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<object> indexArguments)
            where T : Exception
        {
            return SetPropertyValueWithException<T>(type, visibility, propertyName, value.GetType(), value, indexArguments.Select(argument => argument.GetType()), indexArguments);
        }

        /// <summary>
        ///     Makes an attempt to set the specified property value and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which property shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T SetPropertyValueWithException<T>(this Type type, MemberVisibilityFlags visibility, string propertyName, object value, IEnumerable<Type> indexParameters, IEnumerable<object> indexArguments)
            where T : Exception
        {
            if (object.ReferenceEquals(indexArguments, null))
            {
                throw new ArgumentNullException(nameof(indexArguments));
            }

            PropertyInfo property = GetProperty(type, true, propertyName, indexParameters);

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
                property.SetValue(null, value, indexArguments.AsArray());
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
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <returns>
        ///     The property for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static PropertyInfo GetProperty(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType)
        {
            return GetProperty(type, details, propertyName, propertyType, (IEnumerable<Type>)Type.EmptyTypes);
        }

        /// <summary>
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <param name="indexParameters">
        ///     A array of index paramter types of the property.
        /// </param>
        /// <returns>
        ///     The property for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static PropertyInfo GetProperty(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, params Type[] indexParameters)
        {
            return GetProperty(type, details, propertyName, propertyType, (IEnumerable<Type>)indexParameters);
        }

        /// <summary>
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <param name="indexParameters">
        ///     A collection of index paramter types of the property.
        /// </param>
        /// <returns>
        ///     The property for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static PropertyInfo GetProperty(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters)
        {
            if (!TryGetProperty(type, details, propertyName, propertyType, indexParameters, out PropertyInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <param name="result">
        ///     The property for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested property is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        /// </exception>
        public static bool TryGetProperty(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, out PropertyInfo result)
        {
            return TryGetProperty(type, details, propertyName, propertyType, Type.EmptyTypes, out result);
        }

        /// <summary>
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the property declaration.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="propertyType">
        ///     Type of the property value.
        /// </param>
        /// <param name="indexParameters">
        ///     A collection of index paramter types of the property.
        /// </param>
        /// <param name="result">
        ///     The property for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested property is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyType"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        public static bool TryGetProperty(this Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters, out PropertyInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(propertyName, null))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(propertyName));
            }

            if (object.ReferenceEquals(propertyType, null))
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (object.ReferenceEquals(indexParameters, null))
            {
                throw new ArgumentNullException(nameof(indexParameters));
            }

            if (indexParameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(indexParameters));
            }

            BindingFlags flags = details.ToBindingFlags();

            result = type.GetProperty(
                propertyName,
                flags,
                Type.DefaultBinder,
                propertyType,
                indexParameters.AsArray(),
                null);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (!result.PropertyType.Equals(propertyType))
            {
                result = null;
                return false;
            }

            if (!details.HasFlag(PropertyDefinitionDetails.NoGetter))
            {
                MethodInfo getter = result.GetMethod;
                if (object.ReferenceEquals(getter, null))
                {
                    result = null;
                    return false;
                }

                if (details.HasFlag(PropertyDefinitionDetails.ProtectedGetter))
                {
                    if (!getter.IsFamilyOrAssembly && !getter.IsFamily)
                    {
                        result = null;
                        return false;
                    }
                }
                else if (details.HasFlag(PropertyDefinitionDetails.PublicGetter))
                {
                    if (!getter.IsPublic)
                    {
                        result = null;
                        return false;
                    }
                }
            }
            else if (result.CanRead)
            {
                result = null;
                return false;
            }

            if (!details.HasFlag(PropertyDefinitionDetails.NoSetter))
            {
                MethodInfo setter = result.SetMethod;
                if (object.ReferenceEquals(setter, null))
                {
                    result = null;
                    return false;
                }

                if (details.HasFlag(PropertyDefinitionDetails.ProtectedSetter))
                {
                    if (!setter.IsFamilyOrAssembly && !setter.IsFamily)
                    {
                        result = null;
                        return false;
                    }
                }
                else if (details.HasFlag(PropertyDefinitionDetails.PublicSetter))
                {
                    if (!setter.IsPublic)
                    {
                        result = null;
                        return false;
                    }
                }
            }
            else if (result.CanWrite)
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified method.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     The requested method's return type.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <returns>
        ///     When the specified method is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static bool IsMethodDeclared(this Type type, MemberDefinitionDetails details, string methodName, Type returnType, params Type[] parameters)
        {
            return IsMethodDeclared(type, details, methodName, returnType, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Gets a value that indicates whether the specified type declares specified method.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     The requested method's return type.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <returns>
        ///     When the specified method is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static bool IsMethodDeclared(this Type type, MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters)
        {
            return TryGetMethod(type, details, methodName, returnType, parameters, out _);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which method shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static object InvokeMethod(this Type type, MemberVisibilityFlags visibility, string methodName, params object[] arguments)
        {
            return InvokeMethod(type, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a static with a specified name, return type and set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which method shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static object InvokeMethod(this Type type, MemberVisibilityFlags visibility, string methodName, IEnumerable<object> arguments)
        {
            return InvokeMethod(type, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters.
        /// </summary>
        /// <param name="type">
        ///     Type which method shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static object InvokeMethod(this Type type, MemberVisibilityFlags visibility, string methodName, IEnumerable<Type> parameters, IEnumerable<object> arguments)
        {
            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            MethodInfo method = GetMethod(type, true, visibility, methodName, parameters);

            return method.Invoke(null, arguments.AsArray());
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which method shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T InvokeMethodWithException<T>(this Type type, MemberVisibilityFlags visibility, string methodName, params object[] arguments)
            where T : Exception
        {
            return InvokeMethodWithException<T>(type, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which method shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T InvokeMethodWithException<T>(this Type type, MemberVisibilityFlags visibility, string methodName, IEnumerable<object> arguments)
            where T : Exception
        {
            return InvokeMethodWithException<T>(type, visibility, methodName, arguments.Select(argument => argument.GetType()), arguments);
        }

        /// <summary>
        ///     Invokes a method with a specified name, return type and set of parameters and returns an expected exception.
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the expected exception.
        /// </typeparam>
        /// <param name="type">
        ///     Type which method shall be invoked.
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
        ///     <paramref name="type"/> is <see langword="null"/>.
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
        public static T InvokeMethodWithException<T>(this Type type, MemberVisibilityFlags visibility, string methodName, IEnumerable<Type> parameters, IEnumerable<object> arguments)
            where T : Exception
        {
            if (object.ReferenceEquals(arguments, null))
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            MethodInfo method = GetMethod(type, true, visibility, methodName, parameters);

            try
            {
                method.Invoke(null, arguments.AsArray());
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
        ///     Gets a method for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared method shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     Type of the method return value.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <returns>
        ///     The method for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static MethodInfo GetMethod(this Type type, MemberDefinitionDetails details, string methodName, Type returnType, params Type[] parameters)
        {
            return GetMethod(type, details, methodName, returnType, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Gets a method for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared method shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     Type of the method return value.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <returns>
        ///     The method for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static MethodInfo GetMethod(this Type type, MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters)
        {
            if (!TryGetMethod(type, details, methodName, returnType, parameters, out MethodInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a method for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared method shall be obtained.
        /// </param>
        /// <param name="details">
        ///     Details about the method declaration.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="returnType">
        ///     Type of the method return value.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <param name="result">
        ///     The method for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested method is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="returnType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        public static bool TryGetMethod(this Type type, MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters, out MethodInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(methodName, null))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(methodName));
            }

            if (object.ReferenceEquals(returnType, null))
            {
                throw new ArgumentNullException(nameof(returnType));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            var flags = details.ToBindingFlags();

            result = type.GetMethod(
                methodName,
                flags,
                Type.DefaultBinder,
                parameters.AsArray(),
                null);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (details.HasFlag(MemberDefinitionDetails.Protected) && (!result.IsFamilyOrAssembly && !result.IsFamily))
            {
                result = null;
                return false;
            }

            if (result.IsAbstract != details.HasFlag(MemberDefinitionDetails.Abstract))
            {
                result = null;
                return false;
            }

            if (details.HasFlag(MemberDefinitionDetails.Virtual) != (result.IsVirtual && !result.IsFinal))
            {
                result = null;
                return false;
            }

            if (!result.ReturnType.Equals(returnType))
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a field for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared field shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the field shall be <see langword="static"/>;
        ///     <see langword="false"/> if the field shall be an instance field.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <returns>
        ///     The field for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        internal static FieldInfo GetField(this Type type, bool isStatic, MemberVisibilityFlags visibility, string fieldName)
        {
            if (!TryGetField(type, isStatic, visibility, fieldName, out FieldInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a field for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared field shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the field shall be <see langword="static"/>;
        ///     <see langword="false"/> if the field shall be an instance field.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field to locate.
        /// </param>
        /// <param name="result">
        ///     The field for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested field is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="fieldName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="fieldName"/> consists of white spaces only or is an empty string.
        /// </exception>
        internal static bool TryGetField(this Type type, bool isStatic, MemberVisibilityFlags visibility, string fieldName, out FieldInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(fieldName, null))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(fieldName));
            }

            BindingFlags flags = visibility.ToBindingFlags(isStatic);

            result = type.GetField(
                fieldName,
                flags);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (!visibility.IsMatch(result))
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a event for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared event shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the event shall be <see langword="static"/>;
        ///     <see langword="false"/> if the event shall be an instance event.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <returns>
        ///     The event for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        internal static EventInfo GetEvent(this Type type, bool isStatic, MemberVisibilityFlags visibility, string eventName)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(eventName, null))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(eventName));
            }

            if (!TryGetEvent(type, isStatic, visibility, eventName, out EventInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets an event for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared event shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the event shall be <see langword="static"/>;
        ///     <see langword="false"/> if the event shall be an instance event.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="eventName">
        ///     The name of the event to locate.
        /// </param>
        /// <param name="result">
        ///     The event for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested event is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="eventName"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="eventName"/> consists of white spaces only or is an empty string.
        /// </exception>
        internal static bool TryGetEvent(this Type type, bool isStatic, MemberVisibilityFlags visibility, string eventName, out EventInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(eventName, null))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(eventName));
            }

            BindingFlags flags = visibility.ToBindingFlags(isStatic);

            result = type.GetEvent(
                eventName,
                flags);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (!visibility.IsMatch(result.AddMethod))
            {
                result = null;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the property shall be <see langword="static"/>;
        ///     <see langword="false"/> if the property shall be an instance property.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexParameters">
        ///     A collection of index paramter types of the property.
        /// </param>
        /// <returns>
        ///     The property for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        internal static PropertyInfo GetProperty(this Type type, bool isStatic, string propertyName, IEnumerable<Type> indexParameters)
        {
            if (!TryGetProperty(type, isStatic, propertyName, indexParameters, out PropertyInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a property for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared property shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the property shall be <see langword="static"/>;
        ///     <see langword="false"/> if the property shall be an instance property.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property to locate.
        /// </param>
        /// <param name="indexParameters">
        ///     A collection of index paramter types of the property.
        /// </param>
        /// <param name="result">
        ///     The property for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested property is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="propertyName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="propertyName"/> consist of white spaces only.
        ///     <para>-or-</para>
        ///     <paramref name="indexParameters"/> consist <see langword="null"/> item.
        /// </exception>
        internal static bool TryGetProperty(this Type type, bool isStatic, string propertyName, IEnumerable<Type> indexParameters, out PropertyInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(propertyName, null))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(propertyName));
            }

            if (object.ReferenceEquals(indexParameters, null))
            {
                throw new ArgumentNullException(nameof(indexParameters));
            }

            if (indexParameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(indexParameters));
            }

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | (isStatic ? BindingFlags.Static : BindingFlags.Instance);

            result = type.GetProperty(
                propertyName,
                flags,
                Type.DefaultBinder,
                null,
                indexParameters.AsArray(),
                null);

            return !object.ReferenceEquals(result, null);
        }

        /// <summary>
        ///     Gets a method for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared method shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the method shall be <see langword="static"/>;
        ///     <see langword="false"/> if the method shall be an instance method.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <returns>
        ///     The method for the specified type that matches the specified criteria.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        internal static MethodInfo GetMethod(this Type type, bool isStatic, MemberVisibilityFlags visibility, string methodName, IEnumerable<Type> parameters)
        {
            if (!TryGetMethod(type, isStatic, visibility, methodName, parameters, out MethodInfo result))
            {
                throw new MissingMemberDeclarationException();
            }

            return result;
        }

        /// <summary>
        ///     Gets a method for the specified type that matches the specified criteria.
        /// </summary>
        /// <param name="type">
        ///     The type which declared method shall be obtained.
        /// </param>
        /// <param name="isStatic">
        ///     The value that is set to <see langword="true"/> if the method shall be <see langword="static"/>;
        ///     <see langword="false"/> if the method shall be an instance method.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <param name="methodName">
        ///     The name of the method to locate.
        /// </param>
        /// <param name="parameters">
        ///     The requested method's parameter types.
        /// </param>
        /// <param name="result">
        ///     The method for the specified type that matches the specified criteria.
        /// </param>
        /// <returns>
        ///     When the requested method is found the return value is <see langword="true"/>;
        ///     <see langword="false"/>, otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="methodName"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="methodName"/> consists of white spaces only or is an empty string.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        internal static bool TryGetMethod(this Type type, bool isStatic, MemberVisibilityFlags visibility, string methodName, IEnumerable<Type> parameters, out MethodInfo result)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (object.ReferenceEquals(methodName, null))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_StringWhiteSpaceOrEmpty, nameof(methodName));
            }

            if (object.ReferenceEquals(parameters, null))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Contains(null))
            {
                throw new ArgumentException(Resources.Exceptions_Argument_NullCollectionItem, nameof(parameters));
            }

            BindingFlags flags = visibility.ToBindingFlags(isStatic);

            result = type.GetMethod(
                methodName,
                flags,
                Type.DefaultBinder,
                parameters.AsArray(),
                null);

            if (object.ReferenceEquals(result, null))
            {
                return false;
            }

            if (!visibility.IsMatch(result))
            {
                result = null;
                return false;
            }

            return true;
        }
    }
}
