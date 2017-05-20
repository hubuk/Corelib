// -----------------------------------------------------------------------
// <copyright file="MemberDeclarationNotExpectedException.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Testing.Reflection
{
    using System;
    using System.Runtime.Serialization;
    using Properties;

    /// <summary>
    ///     The exception that is thrown when a requested member is declared in a specified type but it is not expected to be.
    /// </summary>
    [Serializable]
    public class MemberDeclarationNotExpectedException : MemberAccessException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberDeclarationNotExpectedException"/> class.
        /// </summary>
        public MemberDeclarationNotExpectedException()
            : base(Resources.MemberDeclarationNotExpectedException_DefaultMessage)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberDeclarationNotExpectedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        public MemberDeclarationNotExpectedException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberDeclarationNotExpectedException"/> class with
        ///     a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        ///     The error message that explains the reason for the exception.
        /// </param>
        /// <param name="inner">
        ///     The exception that is the cause of the current exception.
        ///     If the inner parameter is not a <see langword="null"/> reference, the current exception is raised
        ///     in a catch block that handles the inner exception.
        /// </param>
        public MemberDeclarationNotExpectedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberDeclarationNotExpectedException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        ///     The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        ///     The contextual information about the source or destination.
        /// </param>
        protected MemberDeclarationNotExpectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
