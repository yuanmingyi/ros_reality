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
    public class RobotWandStatePublisher : WandStatePublisher<MessageTypes.RosSharp.RobotWandState>
    {
        public override Utilities.WandStateType MessageType => Utilities.WandStateType.Robot;

        protected override void InitializeMessage()
        {
            base.InitializeMessage();
            message.stearing = new float[2];
        }

        protected override bool UpdateWandState(MessageTypes.RosSharp.RobotWandState message)
        {
            bool changed = false;
            changed |= Utilities.SetByte(ref message.mode, Utilities.BooleanToByte(SteamVR_Actions.robot.Mode.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.grip, Utilities.BooleanToByte(SteamVR_Actions.robot.Grip.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.trigger, Utilities.BooleanToByte(SteamVR_Actions.robot.Trigger.GetState(inputSource)));
            changed |= Utilities.SetFloat(ref message.squeeze, SteamVR_Actions.robot.Squeeze.GetAxis(inputSource), 0.01f);
            var stearing = SteamVR_Actions.robot.Stearing.GetAxis(inputSource);
            changed |= Utilities.SetFloat(ref message.stearing[0], stearing.x, 0.01f);
            changed |= Utilities.SetFloat(ref message.stearing[1], stearing.y, 0.01f);
            changed |= Utilities.SetByte(ref message.teleport, Utilities.BooleanToByte(SteamVR_Actions.robot.Teleport.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.up, Utilities.BooleanToByte(SteamVR_Actions.robot.Up.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.down, Utilities.BooleanToByte(SteamVR_Actions.robot.Down.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.left, Utilities.BooleanToByte(SteamVR_Actions.robot.Left.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.right, Utilities.BooleanToByte(SteamVR_Actions.robot.Right.GetState(inputSource)));
            changed |= Utilities.SetByte(ref message.system, Utilities.BooleanToByte(SteamVR_Actions.robot.System.GetState(inputSource)));
            return changed;
        }
    }
}
