using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationDrawer.Events;
using MultiObjectiveOptimizationDrawer.Extensions;
using MultiObjectiveOptimizationDrawer.Rendering.ContextMenus;
using MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public class NodeRender
    {
        private readonly NodeCollectionStorage<FullConnectedGraph> _nodeCollectionStorage;
        private readonly EllipseCreator _nodeEllipseCreator;
        private readonly EllipseCreator _startNodeEllipseCreator;
        private readonly EllipseCreator _endNodeEllipseCreator;
        private readonly Canvas _canvas;
        private Ellipse _startEllipse;
        private Ellipse _endEllipse;

        public NodeRender(NodeCollectionStorage<FullConnectedGraph> nodeCollectionStorage, Canvas canvas, 
            EllipseCreator nodeEllipseCreator, EllipseCreator startNodeEllipseCreator, EllipseCreator endNodeEllipseCreator)
        {
            _canvas = canvas;
            _nodeEllipseCreator = nodeEllipseCreator;
            _startNodeEllipseCreator = startNodeEllipseCreator;
            _endNodeEllipseCreator = endNodeEllipseCreator;
            _canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            _nodeCollectionStorage = nodeCollectionStorage;
            _nodeCollectionStorage.CollectionChanged += NodeCollectionChangeHandler;
            _nodeCollectionStorage.PointPropertyChanged += PointPropertyChangedHandler;
            _nodeEllipseCreator.ContextMenu = NodeContextMenuCreator.NodeContextMenu(StartPointMenuItem_OnClick,
                EndPointMenuItem_OnClick, RemovePointMenuItem_OnClick);
            var startEndContextMenu =
                NodeContextMenuCreator.StartAndEndNodeContextMenu(RemoveStartOrEndPointMenuItem_OnClick);
            _endNodeEllipseCreator.ContextMenu = startEndContextMenu;
            _startNodeEllipseCreator.ContextMenu = startEndContextMenu;
            
        }
        
        private Ellipse CreateEllipse(Point point, EllipseCreator creator)
        {
            return creator.Create(point);
        }

        private void DrawEllipse(Ellipse ellipse)
        {
            _canvas.Children.Add(ellipse);
            Canvas.SetZIndex(ellipse, int.MaxValue);
        }
        
        private void RemoveEllipse(Ellipse ellipse)
        {
            _canvas.Children.Remove(ellipse);
        }

        private void RemoveEllipse(Point centerPoint)
        {
            var canvasEllipses = _canvas.Children.OfType<Ellipse>();
            var match = canvasEllipses.Last(x => x.Center().AlmostEqual(centerPoint));
            _canvas.Children.Remove(match);
        }

        private void PointPropertyChangedHandler(object sender, PointChangedArgs args)
        {
            if (_startEllipse.Center().AlmostEqual(args.Point)) RemoveEllipse(_startEllipse);
            else if (_endEllipse.Center().AlmostEqual(args.Point)) RemoveEllipse(_endEllipse);
        }

        private void NodeCollectionChangeHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                e.NewItems.OfType<Point>().ForEach(x => DrawEllipse(CreateEllipse(x, _nodeEllipseCreator)));
            }
            if (e.OldItems != null)
            {
                var changed = e.OldItems.OfType<Point>();
                changed.ForEach(RemoveEllipse);
            }
        }
        
        private void StartPointMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var point = GetEllipseFromObjectSender(sender).Center();
            _nodeCollectionStorage.Start = point;
            _startEllipse = CreateEllipse(point,_startNodeEllipseCreator);
            DrawEllipse(_startEllipse);
        }

        private void EndPointMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var point = GetEllipseFromObjectSender(sender).Center();
            _nodeCollectionStorage.End = point;
            _endEllipse = CreateEllipse(point, _endNodeEllipseCreator);
            DrawEllipse(_endEllipse);
        }

        private void RemovePointMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var el = GetEllipseFromObjectSender(sender);
            _nodeCollectionStorage.Remove(el.Center());
            RemoveEllipse(el);
        }

        private void RemoveStartOrEndPointMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var ellipse = GetEllipseFromObjectSender(sender);
            var center = ellipse.Center();
            if (_nodeCollectionStorage.Start.AlmostEqual(center))
            {
                _nodeCollectionStorage.Start = default (Point);
                _startEllipse = null;
            }
            else if (_nodeCollectionStorage.End.AlmostEqual(center))
            {
                _nodeCollectionStorage.End = default (Point);
                _endEllipse = null;
            }
            RemoveEllipse(ellipse);
        }
        
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(_canvas);
            _nodeCollectionStorage.Add(point);
            CreateEllipse(point,_nodeEllipseCreator);
        }

        private Ellipse GetEllipseFromObjectSender(object sender)
        {
            var menuitem = (MenuItem)sender;
            var menu = (ContextMenu)menuitem.Parent;
            return (Ellipse)menu.PlacementTarget;
        }
        
    }
}