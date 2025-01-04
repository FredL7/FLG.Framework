using UnityEngine;


namespace FLG.Unity.Helpers {
    public static class SceneHelper {
        public static void RemoveAllChildrens(Transform parent)
        {
            // Note: Destroy waits until the end of the update loop to actually destroy the element(s)
            // in order to prevent access to null if they were destroyed immediately.
            while (parent.childCount > 0)
            {
                GameObject.Destroy(parent.GetChild(0).gameObject);
            }
        }

        public static void RemoveAllChildrensImmediately(Transform parent)
        {
            while (parent.childCount > 0)
            {
                GameObject.DestroyImmediate(parent.GetChild(0).gameObject);
            }
        }
    }
}
