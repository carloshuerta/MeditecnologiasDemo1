using Assets.Scripts.General;
using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.UX.Buttons;
using MixedRealityToolkit.UX.Receivers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class MainMenuManager: InteractionReceiver
    {
        public string[] ButtonsToRegister;

        public override void OnEnable()
        {
            base.OnEnable();

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

            if (!SessionData.Instance.HasLogged)
            {
                EnableDependentButtons(false);
            }
        }

        protected override void InputClicked(GameObject obj, InputClickedEventData eventData)
        {
            if (eventData.selectedObject.name == "Authenticate")
            {
                SceneManager.LoadScene("AuthenticateUser");
            }
            else
            {
                base.InputClicked(obj, eventData);
            }
        }

        private void EnableDependentButtons(bool enabled)
        {
            var btnViewPatients = GameObject.Find("ViewPatients");
            var compountButton = btnViewPatients.GetComponent<CompoundButton>();

            if (enabled)
            {
                compountButton.ButtonState = MixedRealityToolkit.UX.Buttons.Enums.ButtonStateEnum.Observation;
            }
            else
            {
                compountButton.ButtonState = MixedRealityToolkit.UX.Buttons.Enums.ButtonStateEnum.Disabled;
            }
        }
    }
}
