﻿using FLG.Cs.UI.Layouts;


namespace FLG.Cs.UI {
    public class UIManagerProxy : IUIManager {
        public bool IsProxy() => true;
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered() { }

        public IEnumerable<ILayout> GetLayouts() { return new List<ILayout>(); }
    }
}
