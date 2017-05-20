// -----------------------------------------------------------------------
// <copyright file="AssertType.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Assertions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Leet.Testing.Reflection;

    /// <summary>
    ///     Defines an assertion method for <see cref="Type"/> class.
    /// </summary>
    public static class AssertType
    {
        /// <summary>
        ///     Checks whether the specified type declares specified interface.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="interfaceType">
        ///     The type of the interface that the <paramref name="type"/> shall implement.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="interfaceType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="interfaceType"/> is not an interface type.
        /// </exception>
        public static void DeclaresInterface(Type type, Type interfaceType)
        {
            if (!type.DeclaresInterface(interfaceType))
            {
                throw new MissingInterfaceDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type or its base types declare specified interface.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="interfaceType">
        ///     The type of the interface that the <paramref name="type"/> shall implement.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="interfaceType"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="interfaceType"/> is not an interface type.
        /// </exception>
        public static void HierarchyDeclaresInterface(Type type, Type interfaceType)
        {
            if (!type.GetTypeHierarchy().Any(baseType => baseType.DeclaresInterface(interfaceType)))
            {
                throw new MissingInterfaceDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type declares specified field.
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
        public static void HasField(Type type, FieldDefinitionDetails details, string fieldName, Type fieldType)
        {
            if (@type.TryGetField(details, fieldName, fieldType, out _))
            {
                throw new MissingMemberDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified field.
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
        public static void HasNoField(Type type, FieldDefinitionDetails details, string fieldName, Type fieldType)
        {
            if (type.TryGetField(details, fieldName, fieldType, out _))
            {
                throw new MemberDeclarationNotExpectedException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type has default parameterless constructor available.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void HasParameterlessConstructor(Type type, MemberVisibilityFlags visibility)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsConsructorDeclared(visibility, Type.EmptyTypes))
            {
                if (type.GetConstructors().Length != 0)
                {
                    throw new MissingMemberDeclarationException();
                }
            }
        }

        /// <summary>
        ///     Checks whether the specified type has no default parameterless constructor available.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoParameterlessConstructor(Type type, MemberVisibilityFlags visibility)
        {
            if (object.ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsConsructorDeclared(visibility, Type.EmptyTypes))
            {
                throw new MemberDeclarationNotExpectedException();
            }

            if (type.GetConstructors().Length == 0)
            {
                throw new MemberDeclarationNotExpectedException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type declares specified constructor.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="MissingMemberDeclarationException">
        ///     Requested member has not been found on the specified type.
        /// </exception>
        public static void HasConstructor(Type type, MemberVisibilityFlags visibility)
        {
            HasConstructor(type, visibility, Type.EmptyTypes);
        }

        /// <summary>
        ///     Checks whether the specified type declares specified constructor.
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
        public static void HasConstructor(Type type, MemberVisibilityFlags visibility, params Type[] parameters)
        {
            HasConstructor(type, visibility, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Checks whether the specified type declares specified constructor.
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
        public static void HasConstructor(this Type type, MemberVisibilityFlags visibility, IEnumerable<Type> parameters)
        {
            if (!type.TryGetConstructor(visibility, parameters, out _))
            {
                throw new MissingMemberDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified constructor.
        /// </summary>
        /// <param name="type">
        ///     The type to examine.
        /// </param>
        /// <param name="visibility">
        ///     Visibility of the requested member.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoConstructor(Type type, MemberVisibilityFlags visibility)
        {
            HasNoConstructor(type, visibility, Type.EmptyTypes);
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified constructor.
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoConstructor(Type type, MemberVisibilityFlags visibility, params Type[] parameters)
        {
            HasNoConstructor(type, visibility, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified constructor.
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
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is <see langword="null"/>.
        ///     <para>-or-</para>
        ///     <paramref name="parameters"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameters"/> contain <see langword="null"/> item.
        /// </exception>
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoConstructor(Type type, MemberVisibilityFlags visibility, IEnumerable<Type> parameters)
        {
            if (type.TryGetConstructor(visibility, parameters, out _))
            {
                throw new MemberDeclarationNotExpectedException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type declares specified event.
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
        public static void HasEvent(Type type, MemberDefinitionDetails details, string eventName, Type eventType)
        {
            if (!type.TryGetEvent(details, eventName, eventType, out _))
            {
                throw new MissingMemberDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified event.
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
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoEvent(Type type, MemberDefinitionDetails details, string eventName, Type eventType)
        {
            if (type.TryGetEvent(details, eventName, eventType, out _))
            {
                throw new MemberDeclarationNotExpectedException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type declares specified property.
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
        public static void HasProperty(Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType)
        {
            HasProperty(type, details, propertyName, propertyType, (IEnumerable<Type>)Type.EmptyTypes);
        }

        /// <summary>
        ///     Checks whether the specified type declares specified property.
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
        public static void HasProperty(Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, params Type[] indexParameters)
        {
            HasProperty(type, details, propertyName, propertyType, (IEnumerable<Type>)indexParameters);
        }

        /// <summary>
        ///     Checks whether the specified type declares specified property.
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
        public static void HasProperty(Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters)
        {
            if (!type.TryGetProperty(details, propertyName, propertyType, indexParameters, out _))
            {
                throw new MissingMemberDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified property.
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
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoProperty(Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType)
        {
            HasNoProperty(type, details, propertyName, propertyType, (IEnumerable<Type>)Type.EmptyTypes);
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified property.
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
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested memeber has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoProperty(Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, params Type[] indexParameters)
        {
            HasNoProperty(type, details, propertyName, propertyType, (IEnumerable<Type>)indexParameters);
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified property.
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
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Requested member has been found on the specified type but was not expected.
        /// </exception>
        public static void HasNoProperty(Type type, PropertyDefinitionDetails details, string propertyName, Type propertyType, IEnumerable<Type> indexParameters)
        {
            if (type.TryGetProperty(details, propertyName, propertyType, indexParameters, out _))
            {
                throw new MemberDeclarationNotExpectedException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type declares specified method.
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
        public static void HasMethod(Type type, MemberDefinitionDetails details, string methodName, Type returnType, params Type[] parameters)
        {
            HasMethod(type, details, methodName, returnType, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Checks whether the specified type declares specified method.
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
        public static void HasMethod(Type type, MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters)
        {
            if (!type.TryGetMethod(details, methodName, returnType, parameters, out _))
            {
                throw new MissingMemberDeclarationException();
            }
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified method.
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
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Declaration of the specified member has been found in the type but was not expected.
        /// </exception>
        public static void HasNoMethod(Type type, MemberDefinitionDetails details, string methodName, Type returnType, params Type[] parameters)
        {
            HasNoMethod(type, details, methodName, returnType, (IEnumerable<Type>)parameters);
        }

        /// <summary>
        ///     Checks whether the specified type does not declare specified method.
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
        /// <exception cref="MemberDeclarationNotExpectedException">
        ///     Declaration of the specified member has been found in the type but was not expected.
        /// </exception>
        public static void HasNoMethod(Type type, MemberDefinitionDetails details, string methodName, Type returnType, IEnumerable<Type> parameters)
        {
            if (type.TryGetMethod(details, methodName, returnType, parameters, out _))
            {
                throw new MemberDeclarationNotExpectedException();
            }
        }
    }
}
