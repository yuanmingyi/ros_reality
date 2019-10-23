using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.RosSharp.Scripts.RosBridgeClient.RosCommuncation
{
    public static class Utilities
    {
        public enum WandStateType
        {
            Default,
            Robot
        }

        public static byte BooleanToByte(bool value)
        {
            return value ? (byte)1 : (byte)0;
        }

        public static bool SetByte(ref byte target, byte value)
        {
            var changed = target != value;
            target = value;
            return changed;
        }

        public static bool SetFloat(ref float target, float value, float minChangedValue = 0.001f)
        {
            var changed = Math.Abs(target - value) >= minChangedValue;
            target = value;
            return changed;
        }
    }
}
