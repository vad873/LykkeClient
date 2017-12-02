using System;
using System.Collections.Generic;

namespace LykkeClient
{
	class Guard
	{
		public static void ForLessEqualZero(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}

		public static void ForLessEqualZero(decimal value, string parameterName)
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}

		public static void ForPrecedesDate(DateTime value, DateTime dateToPrecede, string parameterName)
		{
			if (value >= dateToPrecede)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}

		public static void IsNotNullOrEmpty(string value, string parameterName)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}

		public static void IsNotNullOrWhiteSpace(string value, string parameterName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}

		public static void IsNotDefault<T>(T value, string name) where T : struct
		{
			if (EqualityComparer<T>.Default.Equals(value, default(T)))
			{
				throw new ArgumentException($"{name} should not be default value");
			}
		}

		public static void IsNotNull<T>(T value, string name)
		{
			if (value == null)
			{
				throw new ArgumentException($"{name} should not be null");
			}
		}

		public static void IsTrue(bool condition, string parameterName)
		{
			if (!condition)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
		}
	}
}
