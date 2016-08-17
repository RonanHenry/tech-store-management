using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TechStoreWpf.Views;

namespace TechStoreWpf.Helpers
{
    /// <summary>
    /// Defines utility methods.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Finds the parent object with a specific type and an optional name from a child element.
        /// </summary>
        /// <typeparam name="T">Type of the parent to find.</typeparam>
        /// <param name="child">Child element from which to retrieve the parent.</param>
        /// <param name="parentName">Name of the parent element.</param>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject child, string parentName = null) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // End of tree reached
            if (parentObject == null)
                return null;

            T parent = parentObject as T;

            if (!string.IsNullOrEmpty(parentName)) // Search by type and name
            {
                if (parent != null)
                {
                    var frameworkElement = parent as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == parentName)
                    {
                        return parent;
                    }
                    else
                    {
                        return FindParent<T>(parentObject, parentName);
                    }
                }
                else
                {
                    return FindParent<T>(parentObject, parentName);
                }
            }
            else // Search by type only
            {
                if (parent != null)
                {
                    return parent;
                }
                else
                {
                    return FindParent<T>(parentObject);
                }
            }
        }
    }
}
