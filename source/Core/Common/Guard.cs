//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace VSDT.Common
{
	/// <summary>
	/// Common guard clauses
	/// </summary>
	public static class Guard
	{
		/// <summary>
		/// Checks a string argument to ensure it isn't null or empty
		/// </summary>
		/// <param name="argumentValue">The argument value to check.</param>
		/// <param name="argumentName">The name of the argument.</param>
		public static void ArgumentNotNullOrEmptyString(string argumentValue, string argumentName)
		{
			ArgumentNotNull(argumentValue, argumentName);

			if (argumentValue.Length == 0)
				throw new ArgumentException(string.Format("{0}不能为空", argumentName));
		}

		/// <summary>
		/// Checks an argument to ensure it isn't null
		/// </summary>
		/// <param name="argumentValue">The argument value to check.</param>
		/// <param name="argumentName">The name of the argument.</param>
		public static void ArgumentNotNull(object argumentValue, string argumentName)
		{
			if (argumentValue == null)
                ThrowArgumentNullException(argumentName);
		}

		/// <summary>
		/// Checks an Enum argument to ensure that its value is defined by the specified Enum type.
		/// </summary>
		/// <param name="enumType">The Enum type the value should correspond to.</param>
		/// <param name="value">The value to check for.</param>
		/// <param name="argumentName">The name of the argument holding the value.</param>
		public static void EnumValueIsDefined(Type enumType, object value, string argumentName)
		{
			if (Enum.IsDefined(enumType, value) == false)
				throw new ArgumentException(String.Format("参数{0}与{1}不匹配", 
					argumentName, enumType.ToString()));
		}
        public static void ArgumentNonNegative(int value, string name)
        {
            if (value < 0)
                ThrowArgumentException(name, value);
        }
        public static void ArgumentPositive(int value, string name)
        {
            if ((int)value <= 0)
                ThrowArgumentException(name, value);
        }
        public static void ArgumentNonNegative(float value, string name)
        {
            if (value < 0)
                ThrowArgumentException(name, value);
        }
        public static void ArgumentPositive(float value, string name)
        {
            if (value <= 0)
                ThrowArgumentException(name, value);
        }
       
        static void ThrowArgumentException(string propName, object val)
        {
            string valueStr = !Object.ReferenceEquals(val, null) ? val.ToString() : "null";
            string s = String.Format("'{0}' is not a valid value for '{1}'", valueStr, propName);
            throw new ArgumentException(s);
        }
        static void ThrowArgumentNullException(string propName)
        {
            throw new ArgumentNullException(propName);
        }
        public static void IsTrue(bool value)
        {
            if (!value)
            {

            }
        }
        //public static void IsTrue(bool value, string msg);
        //public static void NotNull(object member);
        //public static void NotNull(object member, string name);
 
	}

    public sealed class AssertUtility
    {
        //public static void ArgumentAssignableFrom(object argument, Type BaseType, string name);
        //public static void ArgumentAssignableFrom(object argument, Type BaseType, string name, string message);
        //public static void ArgumentHasText(string argument, string name);
        //public static void ArgumentHasText(string argument, string name, string message);
        //public static void ArgumentNotNull(object argument, string name);
        //public static void ArgumentNotNull(object argument, string name, string message);
        //public static void ArgumentSameType(object argument, Type argumentType, string name);
        //public static void ArgumentSameType(object argument, Type argumentType, string name, string message);
        //public static void EnumDefined(Type enumType, object enumValue, string name);
        //public static void IsTrue(bool value);
        //public static void IsTrue(bool value, string msg);
        //public static void NotNull(object member);
        //public static void NotNull(object member, string name);
    }
}
