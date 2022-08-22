using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using BadEngine;
using static BadEngine.Program;

public class DemoGame : MonoBehaviour
{
    float CubeSpeed = 1f;
    float RotateSpeed = 1f;
    float CubeSize = 0.05f;

    GameObject Cube;
    ShaderProgram DefaultShaderProgram = new ShaderProgram();

    public override void Start()
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
    }

    public override void GLStart()
    {
        Cube = GameObject.Instantiate(PrimitiveGameObject.Cube, CubeSize);
        GL.Viewport(0, 0, 700, 700);

        //DefaultShaderProgram.UseDefaultProgram();
    }

    public override void Update()
    {
        DefaultShaderProgram.Eneble();

        Cube.Render();

        if (Keyboard.GetState().IsKeyDown(Key.Escape))
        {
            StopGame();
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
        Game.MouseMove += Game_MouseMove;

        DefaultShaderProgram.Disable();
    }

    public override void Stop()
    {
        Cube.Destroy();
    }

    private void Game_MouseMove(object sender, MouseMoveEventArgs args)
    {
        GL.Rotate(RotateSpeed * Time.deltaTime, new Vector3(args.YDelta, args.XDelta, 0));
    }
}
