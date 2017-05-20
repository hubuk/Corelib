// -----------------------------------------------------------------------
// <copyright file="OperatorNames.cs" company="Leet">
//     Copyright (c) Leet. All rights reserved.
//     Licensed under the MIT License.
//     See License.txt in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1303 // ConstFieldNamesMustBeginWithUpperCaseLetter
#pragma warning disable SA1310 // FieldNamesMustNotContainUnderscore

namespace Leet.Testing.Reflection
{
    /// <summary>
    ///     Provides names of the .Net operators.
    /// </summary>
    public static class OperatorNames
    {
        /// <summary>
        ///     Implicit conversion operator.
        /// </summary>
        public const string op_Implicit = nameof(op_Implicit);

        /// <summary>
        ///     Conversion to <see langword="true"/> operator.
        /// </summary>
        public const string op_True = nameof(op_True);

        /// <summary>
        ///     Conversion to <see langword="false"/> operator.
        /// </summary>
        public const string op_False = nameof(op_False);

        /// <summary>
        ///     Incrementation operator.
        /// </summary>
        public const string op_Increment = nameof(op_Increment);

        /// <summary>
        ///     Decrementation operator.
        /// </summary>
        public const string op_Decrement = nameof(op_Decrement);

        /// <summary>
        ///     Unary negation operator.
        /// </summary>
        public const string op_UnaryNegation = nameof(op_UnaryNegation);

        /// <summary>
        ///     Unary plus operator.
        /// </summary>
        public const string op_UnaryPlus = nameof(op_UnaryPlus);

        /// <summary>
        ///     Logical not operator.
        /// </summary>
        public const string op_LogicalNot = nameof(op_LogicalNot);

        /// <summary>
        ///     Ones complement operator.
        /// </summary>
        public const string op_OnesComplement = nameof(op_OnesComplement);

        /// <summary>
        ///     Explicit conversion operator.
        /// </summary>
        public const string op_Explicit = nameof(op_Explicit);

        /// <summary>
        ///     Division operator.
        /// </summary>
        public const string op_Division = nameof(op_Division);

        /// <summary>
        ///     Modulus operator.
        /// </summary>
        public const string op_Modulus = nameof(op_Modulus);

        /// <summary>
        ///     Multiply operator.
        /// </summary>
        public const string op_Multiply = nameof(op_Multiply);

        /// <summary>
        ///     Addition operator.
        /// </summary>
        public const string op_Addition = nameof(op_Addition);

        /// <summary>
        ///     Subtraction operator.
        /// </summary>
        public const string op_Subtraction = nameof(op_Subtraction);

        /// <summary>
        ///     Left shift operator.
        /// </summary>
        public const string op_LeftShift = nameof(op_LeftShift);

        /// <summary>
        ///     Right shift operator.
        /// </summary>
        public const string op_RightShift = nameof(op_RightShift);

        /// <summary>
        ///     Greater than comparison operator.
        /// </summary>
        public const string op_GreaterThan = nameof(op_GreaterThan);

        /// <summary>
        ///     Less than comparison operator.
        /// </summary>
        public const string op_LessThan = nameof(op_LessThan);

        /// <summary>
        ///     Greater than or equal comparison operator.
        /// </summary>
        public const string op_GreaterThanOrEqual = nameof(op_GreaterThanOrEqual);

        /// <summary>
        ///     Less than or equal comparison operator.
        /// </summary>
        public const string op_LessThanOrEqual = nameof(op_LessThanOrEqual);

        /// <summary>
        ///     Equality comparison operator.
        /// </summary>
        public const string op_Equality = nameof(op_Equality);

        /// <summary>
        ///     Inequality comparison operator.
        /// </summary>
        public const string op_Inequality = nameof(op_Inequality);

        /// <summary>
        ///     Bitwise AND operator.
        /// </summary>
        public const string op_BitwiseAnd = nameof(op_BitwiseAnd);

        /// <summary>
        ///     Exclusive OR operator.
        /// </summary>
        public const string op_ExclusiveOr = nameof(op_ExclusiveOr);

        /// <summary>
        ///     Bitwise OR operator.
        /// </summary>
        public const string op_BitwiseOr = nameof(op_BitwiseOr);
    }
}

#pragma warning restore SA1310 // FieldNamesMustNotContainUnderscore
#pragma warning restore SA1303 // ConstFieldNamesMustBeginWithUpperCaseLetter
