using System.Windows.Controls;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public class Render
    {
        private readonly NodeCollectionStorage<FullConnectedGraph> _nodeCollectionStorage;
        private readonly NodeRender _nodeRender;
        private readonly LinkRender _linkRender;
        private readonly RouteRender _routeRender;
        private readonly Canvas _canvas;

        public Render(Canvas canvas, NodeCollectionStorage<FullConnectedGraph> nodeCollectionStorage, SolversHolder solversHolder)
        {
            _canvas = canvas;
            _nodeCollectionStorage = nodeCollectionStorage;
            _nodeRender = RenderFactory.GetNodeRender(nodeCollectionStorage, canvas);
            _linkRender = RenderFactory.GetLinkRender(nodeCollectionStorage, canvas);
            _routeRender = RenderFactory.GetRouteRender(canvas);
        }

        public void DrawResults(Result result)
        {
            _routeRender.DrawResults(result);
        }
    }
}