using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MultiObjectiveOptimizationDrawer.Data;
using MultiObjectiveOptimizationDrawer.Rendering.UIElementsCreators;
using MultiObjectiveOptimizationLib.Extensions;
using MultiObjectiveOptimizationLib.NodeCollections;
using MultiObjectiveOptimizationLib.Solver;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public class ResultRender
    {
        private readonly Canvas _canvas;
        private readonly EllipseCreator _ellipseCreator;
        private readonly PolylineCreator _polylineCreator;
        private readonly double _border;
        private readonly Dictionary<Result, Brush> _results = new Dictionary<Result, Brush>();
        private Point _maxPoint;
        private Point _minPoint;

        public ResultRender(Canvas canvas, SolversHolder solversHolder, EllipseCreator ellipseCreator, 
            PolylineCreator polylineCreator, double border, NodeCollectionStorage<FullConnectedGraph> storage)
        {
            _canvas = canvas;
            _canvas.SizeChanged += CanvasChangedHanler;
            _ellipseCreator = ellipseCreator;
            _polylineCreator = polylineCreator;
            _border = border;
            storage.CollectionChanged += StorageOnChangeHandler;
            storage.PointPropertyChanged += StorageOnChangeHandler;
        }

        private void StorageOnChangeHandler(object sender, EventArgs args)
        {
            Clear();
        }

        public void DrawResults(Result result)
        {
            if (_results.Count == 0 || _maxPoint == default(Point) || _minPoint == default(Point))
            {
                if (result.Count <= 1) throw new Exception("Imposible to draw result");
            }
            ClearCanvas();
            _results.Add(result, BrushesList.Next());
            FindMaxPointInResult();
            FindMinPointInResult();
            _results.ForEach(x => DrawResult(x.Key));
        }
        
        private void CanvasChangedHanler(object sender, SizeChangedEventArgs e)
        {
            if(_results.IsEmpty()) return;
            ClearCanvas();
            FindMaxPointInResult();
            FindMinPointInResult();
            _results.ForEach(x => DrawResult(x.Key));
        }

        private double XTranslate(double xNorm)
        {
            var x = (xNorm - _minPoint.X)/(_maxPoint.X - _minPoint.X)*(_canvas.ActualWidth-2*_border)+_border;
            return x;
        }

        private double YTranslate(double yNorm)
        {
            var y = (-((yNorm - _minPoint.Y) / (_maxPoint.Y - _minPoint.Y)-1) * (_canvas.ActualHeight-2*_border)+_border);
            return y;
        }

        private Point TransformCoordinates(Vector<double> twoDimVector)
        {
            if(twoDimVector.Count!=2) throw new ArgumentException();
            return new Point(XTranslate(twoDimVector[0]),YTranslate(twoDimVector[1]));
        }

        private void DrawResult(Result result)
        {
            _polylineCreator.Brush = _results[result];
            result.ForEach(x => DrawVector(x.Value));
            ConnectPoints(result);
        }

        private void DrawVector(Vector<double> vector)
        {
            var coords = TransformCoordinates(vector);
            var toolTip = vector.ToString();
            DrawEllipse(coords,toolTip);
        }

        private void DrawEllipse(Point point, string toolTip)
        {
            var el = _ellipseCreator.Create(point);
            el.ToolTip = toolTip;
            _canvas.Children.Add(el);
        }

        private void ConnectPoints(Result result)
        {
            var polyline = _polylineCreator.Create(
                result.OrderBy(x => x.Value[0]).ThenBy(x => x.Value[1]).Select(x => TransformCoordinates(x.Value)));
            _canvas.Children.Add(polyline);
            Canvas.SetZIndex(polyline, int.MinValue);
        }

        private void FindMinPointInResult()
        {
            var allres = _results.SelectMany(x => x.Key);
            _minPoint = new Point(allres.MinElement(x => x.Value[0]).Value[0], allres.MinElement(x => x.Value[1]).Value[1]);
        }

        private void FindMaxPointInResult()
        {
            var allres = _results.SelectMany(x => x.Key);
            _maxPoint = new Point(allres.MaxElement(x => x.Value[0]).Value[0], allres.MaxElement(x => x.Value[1]).Value[1]);
        }

        private void ClearCanvas()
        {
            _canvas.Children.Clear();
        }

        public void Clear()
        {
            _canvas.Children.Clear();
            _results.Clear();
        }

    }
}