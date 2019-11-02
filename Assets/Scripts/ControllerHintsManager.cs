using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Assets.Scripts
{
    public class ControllerHintsManager
    {
        public static void ShowHints(ControllerHints hints)
        {
            foreach (var entry in hints.ActionHints)
            {
                if (hints.leftHand != null && hints.leftHand.isPoseValid && IsHintsHidden(hints.leftHand, entry.Key))
                {
                    Debug.Log($"Show text hint on left hand for action {entry.Key.GetShortName()}: {entry.Value}");
                    ControllerButtonHints.ShowTextHint(hints.leftHand, entry.Key, entry.Value);
                }
                if (hints.rightHand != null && hints.rightHand.isPoseValid && IsHintsHidden(hints.rightHand, entry.Key))
                {
                    Debug.Log($"Show text hint on right hand for action {entry.Key.GetShortName()}: {entry.Value}");
                    ControllerButtonHints.ShowTextHint(hints.rightHand, entry.Key, entry.Value);
                }
            }
        }

        public static void HideAllHints(Hand hand)
        {
            ControllerButtonHints.HideAllTextHints(hand);
        }

        private static bool IsHintsHidden(Hand hand, ISteamVR_Action_In action)
        {
            return string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, action));
        }
    }
}
