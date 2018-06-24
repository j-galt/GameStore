using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Utilities
{
    public static class ICollectionExtensions
    {
        public static void RefreshItems<T>(this ICollection<T> x, ICollection<T> y)
        {
            x.Clear();

            foreach (var item in y)
                x.Add(item);
        }
    }
}
