using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using System.Windows.Media;

namespace MultiObjectiveOptimizationDrawer.Rendering
{
    public static class BrushesList
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private static int _brushIndex = 0;
        private static readonly List<Brush> _brushes;

        static BrushesList()
        {
            _brushes = new List<Brush>();
            var members = typeof (Brushes).GetProperties();
            foreach (var info in members)
            {
                var brush = (SolidColorBrush)info.GetValue(typeof(Brush));
                if(brush.Color.R + brush.Color.G + brush.Color.B <512)
                    _brushes.Add(brush);
            }
        }

        public static Brush NextRandom()
        {
            return _brushes[_random.Next(_brushes.Count)];
        }

        public static Brush Next()
        {
            var ret = _brushes[_brushIndex];
            _brushIndex = (_brushIndex + 1)%_brushes.Count;
            return ret;
            
        }

    }
}