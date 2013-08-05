using System.Collections.Generic;
using System.Linq;

namespace GoogleMapsCoordonates.Helpers
{
    public static class ListExtensions
    {
        public static IList<T> TruncateCoordinates<T>(this IList<T> list, int truncateRange)
        {
            var start = list.First();
            var end = list.Last();

            if (list.Count <= truncateRange)
            {
                return list;
            }

            list.RemoveAt(0);
            list.RemoveAt(list.Count - 1);
            truncateRange -= 2;

            var truncateLenght = (list.Count / truncateRange);

            var truncatedList = list.Where(x =>
            {
                var indexOf = list.IndexOf(x);
                var divide = list.IndexOf(x) % truncateLenght;
                return (divide == 1 && indexOf != 1);
            }
                ).ToList();

            if (truncatedList.Count <= truncateRange && truncatedList.Count > truncateRange - 5)
            {
                truncatedList.Insert(0, start);
                truncatedList.Add(end);
                return truncatedList;
            }

            truncatedList.Clear();
            var calculatedRangeToSplit = (list.Count - truncateRange) / truncateRange;
            var counter = 0;
            while (truncatedList.Count <= truncateRange)
            {
                truncatedList.Add(list[counter]);
                counter += calculatedRangeToSplit + 1;
            }

            truncatedList.Insert(0, start);
            truncatedList.Add(end);

            return truncatedList.ToList();
        }
    }
}