using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Debugging
{
    public class DebugUtils 
    {
        public static void Log(params float[] values) {
            string output = "";
            for(int i = 0; i < values.Length; i++) {
                output += "e" + i + ": " + values[i] + ". \n";
            }

            UnityEngine.Debug.Log(output);
        }
    }
}
