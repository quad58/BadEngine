using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BadEngine;

namespace BadEngine
{
    public class DemoGame
    {
        public DemoGame()
        {
            InitializeScript();
        }

        public float CubeSpeed = 1f;
        public float RotateSpeed = 1f;
        public float CubeSize = 0.05f;
        public GameObject Cube = new GameObject();

        public void InitializeScript()
        {
            BadEngine.Program.Start = Start;
            BadEngine.Program.GLStart = GLStart;
            BadEngine.Program.Update = Update;
            BadEngine.Program.Stop = Stop;
        }

        public void Start()
        {
            Console.WriteLine("");
            Console.WriteLine("Esc - Stop game");
            Console.WriteLine("");
            Console.WriteLine("Mouse - Rotate");
            Console.WriteLine("");
            Console.WriteLine("Red - Forward (W)");
            Console.WriteLine("Green - Backward (S)");
            Console.WriteLine("Yellow - Left (A)");
            Console.WriteLine("Blue - Right (D)");
            Console.WriteLine("Pink - Up (E)");
            Console.WriteLine("Ligth Blue - Down (Q)");
            Console.WriteLine("");

            Program.RunGame();
        }

        public void GLStart()
        {
            Cube = Instatiate.Cube(CubeSize);
            GL.Viewport(0, 0, 700, 700);
        }

        public void Update()
        {
            Cube.Render();

            if (Keyboard.GetState().IsKeyDown(Key.Escape))
            {
                Program.StopGame();
            }
            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                GL.Translate(new Vector3(0, 0, CubeSpeed * Time.deltaTime));
            }
            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                GL.Translate(new Vector3(0, 0, -CubeSpeed * Time.deltaTime));
            }
            if (Keyboard.GetState().IsKeyDown(Key.A))
            {
                GL.Translate(new Vector3(-CubeSpeed * Time.deltaTime, 0, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Key.D))
            {
                GL.Translate(new Vector3(CubeSpeed * Time.deltaTime, 0, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Key.Q))
            {
                GL.Translate(new Vector3(0, -CubeSpeed * Time.deltaTime, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Key.E))
            {
                GL.Translate(new Vector3(0, CubeSpeed * Time.deltaTime, 0));
            }
            Program.Game.MouseMove += Game_MouseMove;
        }

        public void Stop()
        {
            Cube.Destroy();
        }

        private void Game_MouseMove(object sender, MouseMoveEventArgs args)
        {
            GL.Rotate(RotateSpeed * Time.deltaTime, new Vector3(args.YDelta, args.XDelta, 0));
        }
    }
}
