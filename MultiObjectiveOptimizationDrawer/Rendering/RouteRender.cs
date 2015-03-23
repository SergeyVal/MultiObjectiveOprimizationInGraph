using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using MultiObjectiveOptimizationDrawer.Extensions;
using MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public class RouteRender
    {
        private readonly Canvas _canvas;
        private readonly PolylineCreator _polyLineCreator;
        
        public RouteRender(Canvas canvas, PolylineCreator polyLineCreator)
        {
            _canvas = canvas;
            _polyLineCreator = polyLineCreator;
        }

        public void DrawResults(Result result)
        {
            var routes = result.Select(x => x.Key);
            Draw(routes);
        }
        
        public void ClearResult()
        {
            var results = _canvas.Children.OfType<Polyline>();
            _canvas.Children.Remove(results);
        }

        private void Draw(Route route)
        {
            var line = _polyLineCreator.Create(route);
            _canvas.Children.Add(line);
            Canvas.SetZIndex(line, 0);
            _polyLineCreator.SetNextRandomBrush();
        }

        private void Draw(IEnumerable<Route> routes)
        {
            ClearResult();
            routes.ForEach(Draw);
        }

        

       
    }
}