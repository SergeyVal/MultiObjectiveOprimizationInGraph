using System.Windows;
using System.Windows.Controls;

namespace MultiObjectiveOptimizationDrawer.Rendering.ContextMenus
{
    public static class NodeContextMenuCreator
    {
        private static readonly ContextMenuBuilder _builder = new ContextMenuBuilder();
        public static ContextMenu NodeContextMenu(RoutedEventHandler startNodeEventHandler,
            RoutedEventHandler endNodeEventHandler, RoutedEventHandler removeEventHandler)
        {
            _builder.AddMenuItem("StartPointMenuItem", "Start Point",startNodeEventHandler);
            _builder.AddMenuItem("EndPointMenuItem", "End Point", endNodeEventHandler);
            _builder.AddMenuItem("RemovePointMenuItem", "Remove Point", removeEventHandler);
            var result = _builder.Get("NodeContextMent");
            _builder.Clear();
            return result;

        }

        public static ContextMenu StartAndEndNodeContextMenu(RoutedEventHandler removeEventHandler)
        {
            _builder.AddMenuItem("RemovePointMenuItem", "Remove Point", removeEventHandler);
            var result = _builder.Get("StartAndEndNodeContextMent");
            _builder.Clear();
            return result;

        }
    }
}