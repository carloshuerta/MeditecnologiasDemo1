using System;
using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.UX.AppBarControl;
using MixedRealityToolkit.UX.Receivers;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class MainMenuManager: InteractionReceiver
    {
        public string[] ButtonsToRegister;

        public override void OnEnable()
        {
            base.OnEnable();

            //RegisterCustomButtons();

            Invoke("RegisterCustomButtons", 1);
        }

        private void RegisterCustomButtons()
        {
            foreach (var button in this.ButtonsToRegister)
            {
                var appButton = GameObject.Find(button);

                if (appButton ==null)
                {
                    Debug.LogErrorFormat("Couldn't find the button '{0}'.", button);
                    continue;
                }

                this.Registerinteractable(appButton);
            }
        }

        protected override void InputClicked(GameObject obj, InputClickedEventData eventData)
        {
            base.InputClicked(obj, eventData);
        }

        protected override void InputDown(GameObject obj, InputEventData eventData)
        {
            base.InputDown(obj, eventData);
        }

        protected override void InputUp(GameObject obj, InputEventData eventData)
        {
            base.InputUp(obj, eventData);
        }
    }
}
