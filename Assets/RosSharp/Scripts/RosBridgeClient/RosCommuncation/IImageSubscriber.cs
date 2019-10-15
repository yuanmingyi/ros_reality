using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    interface IImageSubscriber
    {
        string TexName { get; }

        Texture2D Texture2D { get; }
    }
}
