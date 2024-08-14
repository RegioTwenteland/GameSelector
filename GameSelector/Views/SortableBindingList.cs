using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameSelector.Views
{
    /// <summary>
    /// Adds sorting capabilities to a <see cref="BindingList{T}"/>.
    /// </summary>
    /// <remarks>
    /// Copied from https://stackoverflow.com/a/281324.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    internal class SortableBindingList<T> : BindingList<T>
    {
        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> itemsList = (List<T>)Items;
            if (property.PropertyType.GetInterface("IComparable") != null)
            {
                itemsList.Sort(new Comparison<T>(delegate (T x, T y)
                {
                    // Compare x to y if x is not null. If x is, but y isn't, we compare y
                    // to x and reverse the result. If both are null, they're equal.
                    if (property.GetValue(x) != null)
                        return ((IComparable)property.GetValue(x)).CompareTo(property.GetValue(y)) * (direction == ListSortDirection.Descending ? -1 : 1);
                    else if (property.GetValue(y) != null)
                        return ((IComparable)property.GetValue(y)).CompareTo(property.GetValue(x)) * (direction == ListSortDirection.Descending ? 1 : -1);
                    else
                        return 0;
                }));
            }
        }

        protected override bool SupportsSortingCore => true;
    }
}
