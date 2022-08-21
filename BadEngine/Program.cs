using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BadEngine
{
    public class Window : GameWindow
    {
        public int FPS;
        public float FrameTime;

        public Color4 BackgroundColor = Color4.LightBlue;
        public Window
        (
            string WindowTitle,
            int Width, int Height,
            GameWindowFlags Flag
        ) : base
        (
            Width, Height,
            GraphicsMode.Default,
            WindowTitle,
            Flag
        )
        {}
        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            Program.GLStart.Invoke();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90.0f), Width / Height, 0.1f, 100.0f);
            //shader.SetMatrix4("projection", projection);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(BackgroundColor);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Time.deltaTime = (float) args.Time;

            FrameTime += (float) args.Time;
            FPS++;
            if (FrameTime >= 1f)
            {
                Program.ChangeWindowTitle(Program.WindowTitle + " - " + FPS.ToString() + " FPS");
                FPS = 0;
                FrameTime = 0;
            }

            Program.Update.Invoke();

            this.Context.SwapBuffers();
            base.OnUpdateFrame(args);
        }

        
    }
    class Program
    {
        public static Action Start;
        public static Action GLStart;
        public static Action Update;
        public static Action Stop;

        public static string WindowTitle = "BadEngine";
        public static int WindowWidth = 700;
        public static int WindowHeight = 700;
        public static GameWindowFlags WindowFlag = GameWindowFlags.Default;

        public static Window Game = new Window(WindowTitle, WindowWidth, WindowHeight, WindowFlag);
        static void Main(string[] args)
        {
            InitializeScripts();
            Console.WriteLine("Engine Started");
            Start.Invoke();
            Program.RunGame();
        }

        static void InitializeScripts()
        {
            new DemoGame();
        }

        public static void RunGame()
        {
            Game.Run();
        }

        public static void StopGame()
        {
            Game.Exit();
            Stop.Invoke();
            Console.WriteLine("Engine Stopped");
        }

        public static void ChangeWindowTitle(string title)
        {
            Game.Title = title;
        }
    }

    public static class Instatiate
    {
        public static GameObject Cube(float Scale)
        {
            GameObject Object = new GameObject();
            int ListIndex = GL.GenLists(1);
            Object.ListIndex = ListIndex;

            GL.NewList(ListIndex, ListMode.Compile);
            GL.Begin(PrimitiveType.Quads);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);

            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(new Vector3d(-Scale, Scale, Scale));
            GL.Vertex3(new Vector3d(Scale, Scale, Scale));
            GL.Vertex3(new Vector3d(Scale, -Scale, Scale));
            GL.Vertex3(new Vector3d(-Scale, -Scale, Scale));

            GL.Color3(0f, 1f, 0f);
            GL.Vertex3(new Vector3d(-Scale, -Scale, -Scale));
            GL.Vertex3(new Vector3d(Scale, -Scale, -Scale));
            GL.Vertex3(new Vector3d(Scale, Scale, -Scale));
            GL.Vertex3(new Vector3d(-Scale, Scale, -Scale));

            GL.Color3(0f, 0f, 1f);
            GL.Vertex3(new Vector3d(Scale, -Scale, -Scale));
            GL.Vertex3(new Vector3d(Scale, -Scale, Scale));
            GL.Vertex3(new Vector3d(Scale, Scale, Scale));
            GL.Vertex3(new Vector3d(Scale, Scale, -Scale));

            GL.Color3(1f, 1f, 0f);
            GL.Vertex3(new Vector3d(-Scale, Scale, -Scale));
            GL.Vertex3(new Vector3d(-Scale, Scale, Scale));
            GL.Vertex3(new Vector3d(-Scale, -Scale, Scale));
            GL.Vertex3(new Vector3d(-Scale, -Scale, -Scale));

            GL.Color3(1f, 0f, 1f);
            GL.Vertex3(new Vector3d(Scale, Scale, -Scale));
            GL.Vertex3(new Vector3d(Scale, Scale, Scale));
            GL.Vertex3(new Vector3d(-Scale, Scale, Scale));
            GL.Vertex3(new Vector3d(-Scale, Scale, -Scale));

            GL.Color3(0f, 1f, 1f);
            GL.Vertex3(new Vector3d(-Scale, -Scale, -Scale));
            GL.Vertex3(new Vector3d(-Scale, -Scale, Scale));
            GL.Vertex3(new Vector3d(Scale, -Scale, Scale));
            GL.Vertex3(new Vector3d(Scale, -Scale, -Scale));

            GL.End();
            GL.EndList();

            return Object;
        }
    }

    public static class Time
    {
        public static float deltaTime;
    }

    public class GameObject
    {
        public int ListIndex;
        public void Render()
        {
            GL.CallList(ListIndex);
        }
        public void Destroy()
        {
            GL.DeleteLists(ListIndex, 1);
            ListIndex = 0;
        }
    }
}
