using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    public static class ObjectEx
    {
        /// <summary>
        /// 获取变量名称
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetVarName(this Object obj)
        {
            return nameof(obj);
        }

        /// <summary>
        /// 获取变量名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetVarName<T>(this T t) where T : Object
        {
            return nameof(t);
        }

        /// <summary>
        /// 扩展 获取变量名称(字符串)
        /// </summary>
        /// <param name="var_name"></param>
        /// <param name="exp">可以使lamda表达式</param>
        /// <returns>return string</returns>
        public static string GetVarName<T>(this T var_name, Expression<Func<T, T>> exp)
        {
            return ((MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 获取变量名称
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>return string</returns>
        public static string GetVarName<T>(Expression<Func<T, T>> exp)
        {
            return ((MemberExpression)exp.Body).Member.Name;
        }

        public static string GetName<T>(this Object obj, T it) where T : class
        {
            return typeof(T).GetProperties()[0].Name;
        }

        public static string GetName2<T>(this Object obj, Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }

        public static string GetName3<T>(this Object obj, Func<T> expr)
        {
            return expr.Target.GetType().Module.ResolveField(BitConverter.ToInt32(expr.Method.GetMethodBody().GetILAsByteArray(), 2)).Name;
        }

        /// <summary>
        /// 获取变量名称(字符串)
        /// 警告不能在匿名方法里写其它否则报错
        /// </summary>
        /// <param name="var_name">要获取变量名的变量</param>
        /// <returns>变量名</returns>
        public static string GetVarNameReal<T>(this T var_name)
        {
            Expression<Func<T, T>> exp = GetExpression<T>(q => var_name);
            return ((MemberExpression)exp.Body).Member.Name;
        }

        /// <summary>
        /// 获取对应的数据结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static Expression<Func<T, T>> GetExpression<T>(Expression<Func<T, T>> exp)
        {
            return exp;
        }

    }
}