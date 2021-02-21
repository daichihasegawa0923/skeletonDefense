using System;
using System.Collections.Generic;

namespace Diamond.SkeletonDefense.Data 
{
    public static class ListExtension
    {
        public static void Add<T>(this List<T> list,T item, bool isAllowDuplicate)
        {
            if (!isAllowDuplicate)
            {
                list.Add(item);
                return;
            }

            if(!list.Contains(item))
            {
                list.Add(item);
            }
        }
    }
}