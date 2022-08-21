using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BadEngine;

namespace DemoGameScript
{
    public static class DemoGame
    {
        public static float CubeSpeed = 1f;
        public static float RotateSpeed = 1f;
        public static float CubeSize = 0.05f;
        public static GameObject Cube = new GameObject();

        public static void Start()
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

        public static void GLStart()
        {
            Cube = Instatiate.Cube(CubeSize);
            GL.Viewport(0, 0, 700, 700);
        }

        public static void Update()
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

        public static void Stop()
        {
            Cube.Destroy();
        }

        private static void Game_MouseMove(object sender, MouseMoveEventArgs args)
        {
            GL.Rotate(RotateSpeed * Time.deltaTime, new Vector3(args.YDelta, args.XDelta, 0));
        }
    }
}
