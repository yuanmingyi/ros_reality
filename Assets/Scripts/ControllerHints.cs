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
    public class ControllerHints : MonoBehaviour
    {
        [Tooltip("action hints map")]
        public string teleport;
        public string interactUI;
        public string menu;
        public string squeeze;
        public string grabGrip;
        public string grabPinch;
        public string snapTurnLeft;
        public string snapTurnRight;

        [Tooltip("Hands")]
        public Hand leftHand;
        public Hand rightHand;

        public Dictionary<ISteamVR_Action_In, string> ActionHints
        {
            get;
            private set;
        }

        void Awake()
        {
            ActionHints = new Dictionary<ISteamVR_Action_In, string>();
            if (!String.IsNullOrEmpty(teleport))
            {
                ActionHints.Add(SteamVR_Actions.default_Teleport, teleport);
            }
            if (!String.IsNullOrEmpty(interactUI))
            {
                ActionHints.Add(SteamVR_Actions.default_InteractUI, interactUI);
            }
            if (!String.IsNullOrEmpty(menu))
            {
                ActionHints.Add(SteamVR_Actions.default_Menu, menu);
            }
            if (!String.IsNullOrEmpty(squeeze))
            {
                ActionHints.Add(SteamVR_Actions.default_Squeeze, squeeze);
            }
            if (!String.IsNullOrEmpty(grabGrip))
            {
                ActionHints.Add(SteamVR_Actions.default_GrabGrip, grabGrip);
            }
            if (!String.IsNullOrEmpty(grabPinch))
            {
                ActionHints.Add(SteamVR_Actions.default_GrabPinch, grabPinch);
            }
            if (!String.IsNullOrEmpty(snapTurnLeft))
            {
                ActionHints.Add(SteamVR_Actions.default_SnapTurnLeft, snapTurnLeft);
            }
            if (!String.IsNullOrEmpty(snapTurnRight))
            {
                ActionHints.Add(SteamVR_Actions.default_SnapTurnRight, snapTurnRight);
            }
        }

        void Start()
        {
        }
    }
}
