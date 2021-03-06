#region copyright
// <copyright file="EnsureTypeExtensions.cs" company="ehong"> 
// Copyright (c) ehong. All Right Reserved
// </copyright>
// <author>����</author>
// <datecreated>2012-11-07</datecreated>
#endregion
namespace Cis.EnsureThat
{
    using System;
    using System.Diagnostics;

    using Cis.EnsureThat.Core;
    using Cis.EnsureThat.Resources;

    public static class EnsureTypeExtensions
    {
        #region Public Methods and Operators

        [DebuggerStepThrough]
        public static TypeParam IsBool(this TypeParam param)
        {
            return IsOfType(param, Types.BoolType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsDateTime(this TypeParam param)
        {
            return IsOfType(param, Types.DateTimeType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsDecimal(this TypeParam param)
        {
            return IsOfType(param, Types.DecimalType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsDouble(this TypeParam param)
        {
            return IsOfType(param, Types.DoubleType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsFloat(this TypeParam param)
        {
            return IsOfType(param, Types.FloatType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsInt(this TypeParam param)
        {
            return IsOfType(param, Types.IntType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsOfType(this TypeParam param, Type type)
        {
            if (!param.Type.Equals(type))
            {
                throw ExceptionFactory.CreateForParamValidation(
                    param.Name, ExceptionMessages.EnsureExtensions_IsNotOfType.Inject(param.Type.FullName));
            }

            return param;
        }

        [DebuggerStepThrough]
        public static TypeParam IsShort(this TypeParam param)
        {
            return IsOfType(param, Types.ShortType);
        }

        [DebuggerStepThrough]
        public static TypeParam IsString(this TypeParam param)
        {
            return IsOfType(param, Types.StringType);
        }

        #endregion

        private static class Types
        {
            #region Static Fields

            internal static readonly Type BoolType = typeof(bool);

            internal static readonly Type DateTimeType = typeof(DateTime);

            internal static readonly Type DecimalType = typeof(decimal);

            internal static readonly Type DoubleType = typeof(double);

            internal static readonly Type FloatType = typeof(float);

            internal static readonly Type IntType = typeof(int);

            internal static readonly Type ShortType = typeof(short);

            internal static readonly Type StringType = typeof(string);

            #endregion
        }
    }
}