using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Valve.VR;

namespace Assets.Scripts
{
    [RequireComponent(typeof(ControllerHints))]
    public abstract class WorldController : MonoBehaviour
    {
        public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
        public SceneSwitch sceneSwitch;

        public bool IsLoaded
        {
            get { return gameObject.activeInHierarchy; }
        }

        public ControllerHints Hints
        {
            get { return GetComponent<ControllerHints>(); }
        }

        public virtual void Reset()
        {
        }

        public virtual void OnLoad()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnUnload()
        {
            gameObject.SetActive(false);
        }

        public abstract void HandleInput();
    }
}
