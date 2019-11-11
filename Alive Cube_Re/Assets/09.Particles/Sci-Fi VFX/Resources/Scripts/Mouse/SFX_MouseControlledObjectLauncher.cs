using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public class SFX_MouseControlledObjectLauncher : MonoBehaviour
    {
        public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
        public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
        public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

        public SFX_ControlledObject[] ControlledObjects;
        public int MouseButtonCode;
        public bool CallStop = true;

        private void Update()
        {
            if (trigger.GetStateDown(hand))
            {
                foreach (var controlledObject in ControlledObjects)
                {
                    controlledObject.Setup();
                    controlledObject.Run();
                }
            }
            else if (CallStop && trigger.GetStateUp(hand))
            {
                foreach (var controlledObject in ControlledObjects)
                    controlledObject.Stop();
            }
        }
    }
}