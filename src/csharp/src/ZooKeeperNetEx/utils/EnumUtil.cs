using System;
using System.Collections.Generic;
using System.Linq;

namespace org.apache.utils
{
    /// <summary>
    /// Utility methods for enum values. This static type will fail to initialize 
    /// (throwing a <see cref="TypeInitializationException"/>) if
    /// you try to provide a value that is not an enum.
    /// </summary>
    /// <typeparam name="T">An enum type. </typeparam>
    internal static class EnumUtil<T>
        where T : struct
    {
        /// <summary>
        /// In the .NET Framework, objects can be cast to enum values which are not
        /// defined for their type. This method provides a simple fail-fast check
        /// that the enum value is defined, and creates a cast at the same time.
        /// Cast the given value as the given enum type.
        /// Throw an exception if the value is not defined for the given enum type.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <exception cref="InvalidCastException">
        /// If the given value is not a defined value of the enum type.
        /// </exception>
        /// <returns></returns>
        public static T DefinedCast(object enumValue)
        {
            if (!Enum.IsDefined(typeof(T), enumValue))
                throw new InvalidCastException(enumValue + " is not a defined value for enum type " +
                                               typeof(T).FullName);
            return (T)enumValue;
        }

        public static IEnumerable<T> GetValues()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

    }
}
