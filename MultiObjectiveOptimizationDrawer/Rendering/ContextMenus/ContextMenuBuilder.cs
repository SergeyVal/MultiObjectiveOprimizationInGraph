using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MultiObjectiveOptimizationDrawer.Rendering.ContextMenus
{
    public class ContextMenuBuilder
    {
        private List<MenuItem> _items = new List<MenuItem>();

        public void AddMenuItem(string name, string header, RoutedEventHandler handler)
        {
            var menuItem = new MenuItem();
            menuItem.Click += handler;
            menuItem.Header = header;
            menuItem.Name = name;
            _items.Add(menuItem);
        }

        public ContextMenu Get(string menuName)
        {
            var menu = new ContextMenu {Name = menuName};
            _items.ForEach(x => menu.Items.Add(x));
            return menu;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}