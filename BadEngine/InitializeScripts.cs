using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BadEngine;

namespace BadEngine
{
    public class InitializeScripts
    {
        public InitializeScripts ()
        {
            // There instantiate your scripts
            // Class must be inherited from MonoBehaviour
            new DemoGame(); // Like this
        }
    }
}