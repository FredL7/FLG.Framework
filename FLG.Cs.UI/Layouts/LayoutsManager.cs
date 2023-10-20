﻿using System.Diagnostics;
using System.Numerics;

using System.IO;

namespace FLG.Cs.UI.Layouts {
    internal class LayoutsManager {
        private Dictionary<uint, Layout> _layouts;
        private Layout? _current = null;

        internal LayoutsManager()
        {
            _layouts = new();
        }

        public IEnumerable<ILayout> GetLayouts() => _layouts.Values;

        internal void RegisterLayouts(string layoutsDir, Window window)
        {
            if (!Directory.Exists(layoutsDir))
            {
                // TODO: Replace by logger
                Console.WriteLine($"Error, {Path.GetFullPath(layoutsDir)} does not exists");
                return;
            }

            var files = Directory.GetFiles(layoutsDir);
            List<string> layoutFiles = new();
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".layout")
                    layoutFiles.Add(Path.GetFullPath(file));
            }
            if (layoutFiles.Count == 0)
            {
                Console.WriteLine($"Error, {Path.GetFullPath(layoutsDir)} does not contain any layout (.layout)");
                return;
            }

            LayoutXMLParser parser = new();
            uint count = 0;
            foreach (var layoutFile in layoutFiles)
            {
                var layout = parser.Parse(layoutFile);
                if (!parser.IsValid)
                {
                    Console.WriteLine($"Error parsing layout {layoutFile} - {parser.ErrorMsg} ");
                    return;
                }
                _layouts.Add(count++, layout);
            }

            ComputeLayoutsRectXforms(window);
        }

        internal void SetLayoutActive(uint id)
        {
            var target = _layouts[id];
            if (target != _current)
            {
                _current?.SetActive(false);
                _current = target;
                _current.SetActive(true);
            }
        }

        internal void ComputeLayoutsRectXforms(Window window)
        {
            foreach (var layout in _layouts)
                layout.Value.ComputeRectXforms(window);
        }
    }
}
