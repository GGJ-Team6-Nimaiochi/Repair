using UnityEngine;

namespace KanekoUtilities
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ConfigButton : UGUIButton
    {
        [SerializeField]
        ConfigValueName valueName = 0;

        [SerializeField]
        GameObject enableVisual = null;
        [SerializeField]
        GameObject disableVisual = null;

        CanvasGroup group;
        CanvasGroup Group
        {
            get
            {
                if (group != null) return group;

                group = GetComponent<CanvasGroup>();

                return group;
            }
        }
        
        bool currentEnable = true;

        public override void Init()
        {
            base.Init();

            SetEnable(GameConfig.Instance.GetValue(valueName));

            AddListener(() =>
            {
                GameConfig.Instance.OnChangeValue(valueName, !currentEnable);
                SetEnable(!currentEnable);
            });
        }

        public void SetEnable(bool enable)
        {
            currentEnable = enable;
            enableVisual.SetActive(enable);
            disableVisual.SetActive(!enable);
        }

        public override float Alpha
        {
            get
            {
                return Group.alpha;
            }

            set
            {
                Group.alpha = value;
            }
        }
    }
}