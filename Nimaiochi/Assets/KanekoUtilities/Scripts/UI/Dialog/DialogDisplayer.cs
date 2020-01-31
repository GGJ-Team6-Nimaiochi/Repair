using System;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class DialogDisplayer : SingletonMonobehaviour<DialogDisplayer>
    {
        Queue<Dialog> currentViewDialog = new Queue<Dialog>();

        Dictionary<string, DialogPool> dialogDictionary = new Dictionary<string, DialogPool>();

        public Dialog ShowDialog(string dialogName)
        {
            Dialog dialog = GetDialog(dialogName);

            ShowDialog(dialog);

            return dialog;
        }

        public T ShowDialog<T>(string dialogName) where T : Dialog
        {
            T dialog = GetDialog<T>(dialogName);

            ShowDialog(dialog);

            return dialog;
        }

        public void ShowDialog(Dialog dialog)
        {
            dialog.Show();
            dialog.transform.SetAsLastSibling();
            currentViewDialog.Enqueue(dialog);
        }

        Dialog GetDialog(string dialogName)
        {
            Dialog dialog;
            DialogPool dialogPool;

            if (dialogDictionary.TryGetValue(dialogName, out dialogPool))
            {
                return dialogPool.GetInstance();
            }

            Dialog dialogPrefab = MyAssetStore.Instance.GetAsset<Dialog>(dialogName, "Dialogs/");

            if (dialogPrefab == null) return null;

            dialog = Instantiate(dialogPrefab, transform);
            dialog.DialogName = dialogName;
            dialog.gameObject.SetActive(false);
            dialogPool = gameObject.AddComponent<DialogPool>();
            dialogPool.SetOriginal(dialog);
            
            dialogDictionary.Add(dialogName, dialogPool);

            return dialogPool.GetInstance();
        }

        T GetDialog<T>(string dialogName) where T : Dialog
        {
            Dialog temp = GetDialog(dialogName);
            if (temp == null) return null;

            T dialog = (T)temp;
            if (dialog == null)
            {
                Debug.LogError(dialogName + " does not match the type " + '"' + typeof(T).Name + '"' + ".");
                return null;
            }

            return dialog;
        }

        public void HideDialog()
        {
            if (currentViewDialog.Count <= 0) return;
            Dialog dialog = currentViewDialog.Dequeue();
            DialogPool dialogPool;

            if (!dialogDictionary.TryGetValue(dialog.DialogName, out dialogPool)) return;
            
            StartCoroutine(KKUtilities.WaitAction(dialog.OnHideAnimationEnd, () =>
            {
                dialogPool.ReturnInstance(dialog);
            }));

            dialog.Hide();
        }

        public void HideAllDialog()
        {
            while (currentViewDialog.Count > 0)
            {
                HideDialog();
            }
        }
        
        public MessageDialog ShowMessageDialog(string title, string message)
        {
            MessageDialog dialog = GetDialog<MessageDialog>("DefaultMessageDialog");

            dialog.Init(title, message);

            ShowDialog(dialog);

            return dialog;
        }

        public OKDialog ShowOkDialog(Action onOK)
        {
            OKDialog dialog = GetDialog<OKDialog>("DefaultOkDialog");

            dialog.Init(onOK);

            ShowDialog(dialog);

            return dialog;
        }

        public OkCancelDialog ShowOkCancelDialog(Action onOk, Action onCancel = null)
        {
            OkCancelDialog dialog = GetDialog<OkCancelDialog>("DefaultOkCancelDialog");

            dialog.Init(onOk, onCancel);

            ShowDialog(dialog);

            return dialog;
        }
    }
}