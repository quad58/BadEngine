using System;
using System.IO;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BadEngine
{
    public class ShaderProgram
    {
        public int ProgramID;
        public bool enebled;
        public bool exists;
        public static string DefaultVertexShaderPath = @"data/shaders/glsl/Default.glsl";

        public void CreateProgram()
        {
            ProgramID = GL.CreateProgram();
            exists = true;
        }

        public void AttachShader(Shader shader)
        {
            shader.Attach(this);
        }

        public void DetachShader(Shader shader)
        {
            shader.Detach(this);
        }

        public void Link()
        {
            GL.LinkProgram(ProgramID);
        }

        public string GetInfoLog()
        {
            return GL.GetProgramInfoLog(ProgramID);
        }

        public void UseDefaultProgram()
        {
            CreateProgram();
            Shader DefaultVertexShader = new Shader();
            DefaultVertexShader.CreateFromFile(ShaderType.VertexShader, DefaultVertexShaderPath);
            DefaultVertexShader.Compile();
            DefaultVertexShader.Attach(this);
            Link();
            DefaultVertexShader.Detach(this);
            DefaultVertexShader.Destroy();
            Eneble();
        }

        public void Eneble()
        {
            GL.UseProgram(ProgramID);
            enebled = true;
        }

        public void Disable()
        {
            GL.UseProgram(0);
            enebled = false;
        }

        public void Destroy()
        {
            GL.DeleteProgram(ProgramID);
            exists = false;
        }

        public static void DisableAll()
        {
            GL.UseProgram(0);
        }
    }
}
