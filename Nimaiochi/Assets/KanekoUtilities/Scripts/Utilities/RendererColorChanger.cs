using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class RendererColorChanger : MonoBehaviour
    {
        [SerializeField]
        protected Renderer[] rends = null;

        [SerializeField]
        string colorPropertyName = "_Color";

        protected MaterialPropertyBlock block;

        public virtual void SetColor(Color color)
        {
            if (block == null)
            {
                block = new MaterialPropertyBlock();
            }
            block.SetColor(colorPropertyName, color);

            foreach (var r in rends)
            {
                if (r == null) continue;
                r.SetPropertyBlock(block);
            }
        }
    }
}
