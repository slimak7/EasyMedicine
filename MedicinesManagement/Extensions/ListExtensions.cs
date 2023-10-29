namespace MedicinesManagement.Extensions
{
    public static class ListExtensions
    {
        public static List<T> GetSafeRange<T>(this List<T> list,int page, int count)
        {

            int index = page * count;

            if (index >= list.Count)
            {
                return new List<T>();
            }
            if (count >= list.Count - index)
            {
                count = list.Count - index;
            }

            return list.GetRange(index, count);
        }
    }
}
