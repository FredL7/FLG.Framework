﻿using FLG.Cs.IDatamodel;
using FLG.Cs.Math;

namespace FLG.Cs.UI {
    public class UIFactoryProxy : IUIFactory {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() { }

        // TMP
        public ILayoutElement ProxyLayoutElement(string name, float width = 0, float height = 0, Spacing margin = default, Spacing padding = default, int order = 0, float weight = 1)
        {
            throw new NotImplementedException();
        }

        #region Layouts
        public ILayoutElement HStack(string name, float width = 0, float height = 0, Spacing margin = default, Spacing padding = default, int order = 0, float weight = 1, bool isTarget = false, EGridDirection direction = EGridDirection.NORMAL, EGridJustify justify = EGridJustify.START, EGridAlignment alignment = EGridAlignment.START)
        {
            throw new NotImplementedException();
        }

        public ILayoutElement VStack(string name, float width = 0, float height = 0, Spacing margin = default, Spacing padding = default, int order = 0, float weight = 1, bool isTarget = false, EGridDirection direction = EGridDirection.NORMAL, EGridJustify justify = EGridJustify.START, EGridAlignment alignment = EGridAlignment.START)
        {
            throw new NotImplementedException();
        }
        #endregion Layouts

        #region Widgets
        public ILayoutElement Label(string name, string text, float width = 0, float height = 0, Spacing margin = default, Spacing padding = default, int order = 0, float weight = 1)
        {
            throw new NotImplementedException();
        }

        public ILayoutElement Sprite(string name, string source, float width = 0, float height = 0, Spacing margin = default, Spacing padding = default, int order = 0, float weight = 1)
        {
            throw new NotImplementedException();
        }
        #endregion Widgets
    }
}