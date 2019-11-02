/*
© Siemens AG, 2017-2019
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Assets.RosSharp.Scripts.RosBridgeClient.RosCommuncation;
using RosSharp.RosBridgeClient.MessageTypes.Std;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Valve.VR;

namespace RosSharp.RosBridgeClient
{
    public abstract class WandStatePublisher<T> : UnityPublisher<T>, IMessagePublisher where T : Message
    {
        public SteamVR_Input_Sources inputSource;
        public string FrameId = "Unity";
        protected T message;

        public bool IsEnabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        public abstract Utilities.WandStateType MessageType
        {
            get;
        }

        protected override void Start()
        {
            Debug.Log($"{this.GetType()}.Start()");
            base.Start();
            InitializeMessage();
        }

        protected virtual void FixedUpdate()
        {
            UpdateMessage();
        }

        protected virtual void InitializeMessage()
        {
            message = (T)Activator.CreateInstance(typeof(T));
            var header = (Header)typeof(T).GetField("header", BindingFlags.Public | BindingFlags.Instance).GetValue(message);
            header.frame_id = FrameId;
        }

        private void UpdateMessage()
        {
            var header = (Header)typeof(T).GetField("header", BindingFlags.Public | BindingFlags.Instance).GetValue(message);
            if (UpdateWandState(message))
            {
                header.Update();
                Publish(message);
            }
        }

        protected abstract bool UpdateWandState(T message);
    }
}
