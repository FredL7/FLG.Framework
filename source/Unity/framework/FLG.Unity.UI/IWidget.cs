using UnityEngine;

using FLG.Cs.Datamodel.UI.Layouts;


namespace FLG.Unity.UI {
    internal interface IWidget<T> where T : ILayoutElement {
        public T Widget { get; }
        public GameObject Draw(bool fromEditor);
    }
}
