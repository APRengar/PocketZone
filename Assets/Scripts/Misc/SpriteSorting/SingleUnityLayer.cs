using UnityEngine;
using System;

namespace Misc
{
    using Misc.Utility;
    public static partial class Extentions
    {
        public static void SetLayerTo(this MonoBehaviour mono, SingleUnityLayer layer, bool includeAllChildren)
        {
            mono.SetLayerTo(layer, includeAllChildren);
        }

        public static GameObject GetChildWithName(GameObject gameObject, string rendName, bool includeInactive = false)
        {
            return GetChildWithName(gameObject, rendName, includeInactive);
        }
    }
}
namespace Misc.Utility
{
    [Serializable]
    public class SingleUnityLayer
    {
        [SerializeField] int layerIndex = 0;
        public void SetLayer(int newLayer)
        {
            layerIndex = Mathf.Clamp(newLayer,0,31);
        }
        public SingleUnityLayer()
        {

        }
        public SingleUnityLayer(int layerIndex)
        {
            SetLayer(layerIndex);
        }

        public int Layer => layerIndex;
        public int Mask => 1 << Layer;
        public static implicit operator int (SingleUnityLayer layer)
        {
            return layer.Layer;
        }
        public static implicit operator LayerMask (SingleUnityLayer layer)
        {
            return layer.Mask;
        }
    }
}