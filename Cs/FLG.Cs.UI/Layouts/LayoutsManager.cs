﻿using FLG.Cs.Logger;
using FLG.Cs.Math;
using FLG.Cs.ServiceLocator;
using FLG.Cs.UI.Pages;

namespace FLG.Cs.UI.Layouts {
    internal class LayoutsManager {
        private Dictionary<string, Layout> _layouts;
        private Layout? _current = null;

        internal LayoutsManager()
        {
            _layouts = new();
        }

        public IEnumerable<ILayout> GetLayouts() => _layouts.Values;

        internal void RegisterLayouts(string layoutsDir, Window window)
        {
            _layouts = new();
            var layouts = LayoutXMLParser.Parse(layoutsDir);
            if (layouts != null)
                foreach (var layout in layouts)
                {
                    if (!_layouts.ContainsKey(layout.GetName()))
                    {
                        _layouts.Add(layout.GetName(), layout);
                        Locator.Instance.Get<ILogManager>().Info($"Registered layout \"{layout.GetName()}\"");
                    }
                    else
                    {
                        Locator.Instance.Get<ILogManager>().Warn($"Already has a layout named \"{layout.GetName()}\"");
                    }
                }
            ComputeLayoutsRectXforms(window);
        }

        internal void SetLayoutActive(string id)
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

        internal void ComputeTargetRectXforms(string layoutid, string targetid, List<AbstractLayoutElement> content)
        {
            if (!_layouts.ContainsKey(layoutid))
            {
                Locator.Instance.Get<ILogManager>().Error($"No layout with it {layoutid}");
                return;
            }

            if (_layouts[layoutid].GetTarget(targetid) == null)
            {
                Locator.Instance.Get<ILogManager>().Error($"Layout \"{layoutid}\" does not contain target with id {targetid}");
                return;
            }

            var target = _layouts[layoutid].GetTarget(targetid) as AbstractLayoutElementComposite;
            if (target == null)
            {
                Locator.Instance.Get<ILogManager>().Error($"Target \"{targetid}\" cannot contain childrens");
                return;
            }

            target.ComputeContentSizesAndPositions(content);
        }
    }
}
