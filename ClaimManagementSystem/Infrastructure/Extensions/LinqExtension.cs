
namespace ClaimManagementSystem.Infrastructure.Extensions
{
    #region Using Namespace
    using System;
    using System.Collections.Generic;
    #endregion

    public static class LinqExtension
    {
        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, bool, TResult> projection)
        {
            using (var iterator = source.GetEnumerator())
            {
                //if (!iterator.MoveNext())
                //{
                //    yield break;
                //}
                bool isFirstElement = true;
                TSource previous = iterator.Current;

                while (iterator.MoveNext())
                {
                    if (isFirstElement)
                    {
                        previous = iterator.Current;
                    }

                    yield return projection(previous, iterator.Current, isFirstElement);

                    previous = iterator.Current;
                    isFirstElement = false;
                }
            }
        }
    }
}