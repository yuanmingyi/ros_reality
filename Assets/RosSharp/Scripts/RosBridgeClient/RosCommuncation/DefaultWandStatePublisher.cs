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
using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace RosSharp.RosBridgeClient
{
    public class DefaultWandStatePublisher : WandStatePublisher<MessageTypes.RosSharp.DefaultWandState>
    {
        public override Utilities.WandStateType MessageType => Utilities.WandStateType.Default;

        protected override bool UpdateWandState(MessageTypes.RosSharp.DefaultWandState message)
        {
            bool changed = false;
            changed |= Utilities.SetByte(ref message.grabGrip, Utilities.BooleanToByte(SteamVR_Actions._default.GrabGrip.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.grabPinch, Utilities.BooleanToByte(SteamVR_Actions._default.GrabPinch.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.teleport, Utilities.BooleanToByte(SteamVR_Actions._default.Teleport.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.interactUI, Utilities.BooleanToByte(SteamVR_Actions._default.InteractUI.GetState(inputSource)));
            changed |= Utilities.SetFloat(ref message.squeeze, SteamVR_Actions._default.Squeeze.GetAxis(inputSource), 0.01f);
            return changed;
        }
    }
}
