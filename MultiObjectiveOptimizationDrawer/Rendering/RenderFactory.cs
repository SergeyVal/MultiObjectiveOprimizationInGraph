using System.Windows.Controls;
using System.Windows.Media;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators;
using MultiObjectiveOptimizationLib.NodeCollections;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public static class RenderFactory
    {
        public static NodeRender GetNodeRender(NodeCollectionStorage<FullConnectedGraph> storage, Canvas canvas)
        {
            var nodeEllipseCreator = new EllipseCreator(Brushes.Black, Brushes.DarkGray, 5, 0.7);
            var startNodeEllipseCreator = new EllipseCreator(Brushes.Blue, Brushes.Transparent, 8, 0.9);
            var endNodeEllipseCreator = new EllipseCreator(Brushes.Red, Brushes.Transparent, 8, 0.9);
            return new NodeRender(storage,canvas,nodeEllipseCreator,startNodeEllipseCreator,endNodeEllipseCreator);
        }

        public static LinkRender GetLinkRender(NodeCollectionStorage<FullConnectedGraph> storage, Canvas canvas)
        {
            var lineCreator = new LineCreator(Brushes.DimGray,1,0.5);
            return new LinkRender(storage,canvas,lineCreator);
        }

        public static RouteRender GetRouteRender(Canvas canvas)
        {
            var polylineCreator = new PolylineCreator(Brushes.Coral, 3, 0.3);
            return new RouteRender(canvas, polylineCreator);
        }

        public static ResultRender GetResultRender(Canvas canvas, SolversHolder solversHolder, 
            NodeCollectionStorage<FullConnectedGraph> storage)
        {
            var ellipseCreator = new EllipseCreator(Brushes.Black, Brushes.SlateGray, 4, 0.5);
            var polyLinesCreator = new PolylineCreator(Brushes.Blue, 1, 1);
            const double border = 5;
            return new ResultRender(canvas,solversHolder,ellipseCreator,polyLinesCreator,border, storage);
        }
    }
}