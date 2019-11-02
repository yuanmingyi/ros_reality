﻿/*
© Siemens AG, 2017-2018
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

using UnityEngine;

/// <summary>
/// ImageSubscriberV2: image subscriber with no materials rendering
/// </summary>
namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class RawImageSubscriber : UnitySubscriber<MessageTypes.Sensor.Image>, IImageSubscriber
    {
        public int imageWidth = 512;
        public int imageHeight = 424;
        public string texName;
        public TextureFormat textureFormat = TextureFormat.R16;

        private byte[] imageData;
        private bool isMessageReceived;

        public string TexName
        {
            get { return texName; }
        }

        public Texture2D Texture2D
        {
            get;
            private set;
        }

        protected override void Start()
        {
			base.Start();
            Texture2D = new Texture2D(imageWidth, imageHeight, textureFormat, false);
        }

        private void Update()
        {
            if (isMessageReceived)
            {
                ProcessMessage();
            }
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.Image image)
        {
            imageData = image.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Texture2D.LoadRawTextureData(imageData);
            Texture2D.Apply();
            isMessageReceived = false;
        }

    }
}

