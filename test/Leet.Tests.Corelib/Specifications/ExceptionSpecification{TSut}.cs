// -----------------------------------------------------------------------
// <copyright file="ExceptionSpecification{TSut}.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Leet.Specifications
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Leet.Testing;
    using Leet.Testing.Assertions;
    using Leet.Testing.Reflection;
    using Xunit;

    /// <summary>
    ///     A class that specifies behavior for <see cref="Exception"/> class.
    /// </summary>
    /// <typeparam name="TSut">
    ///     Type which shall be tested for conformance with behavior defined for <see cref="Exception"/> class.
    /// </typeparam>
    public abstract class ExceptionSpecification<TSut>
        : ObjectSpecification<TSut>
        where TSut : Exception
    {
        /// <summary>
        ///     Checks whether the <typeparamref name="TSut"/> has <see cref="SerializableAttribute"/> applied.
        /// </summary>
        [Paradigm]
        public void Type_HasSerializableAttribute()
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            // Verify outcome
            Assert.True(Attribute.IsDefined(sutType, typeof(SerializableAttribute), false));

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <typeparamref name="TSut"/> has public parameterless constructor.
        /// </summary>
        [Paradigm]
        public void Type_HasParamterlessConstructorDeclared()
        {
            // Fixture setup
            Type sutType = typeof(TSut);

            // Exercise system
            // Verify outcome
            AssertType.HasConstructor(sutType, MemberVisibilityFlags.Public);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <typeparamref name="TSut"/> has public constructor with string <c>message</c> parameter.
        /// </summary>
        [Paradigm]
        public void Type_HasConstructorWithMessageParamterDeclared()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            string expectedParamterName = "message";

            // Exercise system
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, new Type[] { typeof(string) });
            ParameterInfo parameter = ci.GetParameters().Single();

            // Verify outcome
            Assert.Equal(expectedParamterName, parameter.Name);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <typeparamref name="TSut"/> has public constructor with string <c>message</c> and <see cref="Exception"/>
        ///     <c>innerException</c> parameters.
        /// </summary>
        [Paradigm]
        public void Type_HasConstructorWithMessageAndInnerExceptionParamterDeclared()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            string expectedMessageParamterName = "message";
            string expectedInnerExceptionParamterName = "inner";

            // Exercise system
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, new Type[] { typeof(string), typeof(Exception) });
            ParameterInfo firstParameter = ci.GetParameters().First();
            ParameterInfo secondParameter = ci.GetParameters().Skip(1).Single();

            // Verify outcome
            Assert.Equal(expectedMessageParamterName, firstParameter.Name);
            Assert.Equal(expectedInnerExceptionParamterName, secondParameter.Name);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the <typeparamref name="TSut"/> has protected deserialization constructor.
        /// </summary>
        [Paradigm]
        public void Type_HasDeserializationConstructor()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            string expectedInfoParamterName = "info";
            string expectedContextParamterName = "context";

            // Exercise system
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Family, new Type[] { typeof(SerializationInfo), typeof(StreamingContext) });
            ParameterInfo firstParameter = ci.GetParameters().First();
            ParameterInfo secondParameter = ci.GetParameters().Skip(1).Single();

            // Verify outcome
            Assert.Equal(expectedInfoParamterName, firstParameter.Name);
            Assert.Equal(expectedContextParamterName, secondParameter.Name);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the default constructor assigns some default message.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_AssignsMessage()
        {
            // Fixture setup
            // Exercise system
            TSut sut = Activator.CreateInstance<TSut>();

            // Verify outcome
            Assert.NotNull(sut.Message);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the default constructor does not assign inner exception.
        /// </summary>
        [Paradigm]
        public void Constructor_Void_Always_DoesNotAssignInnerException()
        {
            // Fixture setup
            // Exercise system
            TSut sut = Activator.CreateInstance<TSut>();

            // Verify outcome
            Assert.Null(sut.InnerException);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the constructor with <see cref="string"/> parameter assigns some default message when called with null.
        /// </summary>
        [Paradigm]
        public void Constructor_String_CalledWithNullMessage_AssignsDefaultMessage()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string));

            // Exercise system
            TSut sut = (TSut)ci.Invoke(new object[] { null });

            // Verify outcome
            Assert.NotNull(sut.Message);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the constructor with <see cref="string"/> parameter assigns it to message.
        /// </summary>
        /// <param name="message">
        ///     Message to be passed to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_String_CalledWithNotNullMessage_AssignsIt(string message)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string));

            // Exercise system
            TSut sut = (TSut)ci.Invoke(new object[] { message });

            // Verify outcome
            Assert.Contains(message, sut.Message);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the constructor with <see cref="string"/> parameter does not assign inner exception.
        /// </summary>
        /// <param name="message">
        ///     Message to be passed to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_String_CalledWithNotNullMessage_DoesNotAssignInnerException(string message)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string));

            // Exercise system
            TSut sut = (TSut)ci.Invoke(new object[] { message });

            // Verify outcome
            Assert.Null(sut.InnerException);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the constructor with <see cref="string"/> and <see cref="Exception"/> parameters
        ///     called with two <see langword="null"/> arguments assigns same value as constructor with just one <see cref="string"/> parameter.
        /// </summary>
        [Paradigm]
        public void Constructor_String_Exception_CalledWithNullMessageAndInnerException_AssignsSameValuesAsStringConstructor()
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            ConstructorInfo stringCi = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string));
            TSut stringConstrcutorSut = (TSut)stringCi.Invoke(new object[] { null });
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string), typeof(Exception));

            // Exercise system
            TSut sut = (TSut)ci.Invoke(new object[] { null, null });

            // Verify outcome
            Assert.Equal(stringConstrcutorSut.Message, sut.Message);
            Assert.Null(sut.InnerException);

            // Teardown
        }

        /// <summary>
        ///     Checks whether the constructor with <see cref="string"/> and <see cref="Exception"/> parameters
        ///     called with two non-<see langword="null"/> arguments assigns passed values.
        /// </summary>
        /// <param name="message">
        ///     Message to be passed to the constructor.
        /// </param>
        /// <param name="inner">
        ///     Inner exception to be passed to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Constructor_String_Exception_CalledWithNonNullMessageAndInnerException_AssignsPassedValues(string message, Exception inner)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string), typeof(Exception));

            // Exercise system
            TSut sut = (TSut)ci.Invoke(new object[] { message, inner });

            // Verify outcome
            Assert.Equal(message, sut.Message);
            Assert.Equal(inner, sut.InnerException);

            // Teardown
        }

#if NET46
        /// <summary>
        ///     Checks whether the type can be correctly serialized and deserialized.
        /// </summary>
        /// <param name="message">
        ///     Message to be passed to the constructor.
        /// </param>
        /// <param name="inner">
        ///     Inner exception to be passed to the constructor.
        /// </param>
        [Paradigm]
        [AutoDomainData]
        public void Type_CanBeDeserialized(string message, Exception inner)
        {
            // Fixture setup
            Type sutType = typeof(TSut);
            ConstructorInfo ci = sutType.GetConstructor(MemberVisibilityFlags.Public, typeof(string), typeof(Exception));
            TSut sut = (TSut)ci.Invoke(new object[] { message, inner });
            TSut deserialized = null;

            // Exercise system
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, sut);
                stream.Position = 0;
                deserialized = (TSut)formatter.Deserialize(stream);
            }

            // Verify outcome
            Assert.Equal(sut.Message, deserialized.Message);
            Assert.NotNull(deserialized.InnerException);

            // Teardown
        }
#endif
    }
}
