// -----------------------------------------------------------------------
// <copyright file="MissingInterfaceDeclarationException.cs" company="Leet">
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
    ///     The exception that is thrown when a requested interface is not declared in a specified type.
    /// </summary>
    [Serializable]
    public class MissingInterfaceDeclarationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingInterfaceDeclarationException"/> class.
        /// </summary>
        public MissingInterfaceDeclarationException()
            : base(Resources.MissingInterfaceDeclarationException_DefaultMessage)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingInterfaceDeclarationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error.
        /// </param>
        public MissingInterfaceDeclarationException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingInterfaceDeclarationException"/> class with
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
        public MissingInterfaceDeclarationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MissingInterfaceDeclarationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        ///     The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        ///     The contextual information about the source or destination.
        /// </param>
        protected MissingInterfaceDeclarationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
