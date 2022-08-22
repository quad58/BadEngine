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
        public int ID;
        public bool enebled;
        public static string DefaultVertexShaderPath = @"data/shaders/glsl/Default.glsl";
        public void CreateProgram()
        {
            ID = GL.CreateProgram();
        }

        public void AttachShader(Shader shader)
        {
            shader.Attach(ID);
        }

        public void Link()
        {
            GL.LinkProgram(ID);
        }

        public string GetInfoLog()
        {
            return GL.GetProgramInfoLog(ID);
        }

        public void CreateDefaultProgram()
        {
            CreateProgram();
            Shader DefaultVertexShader = new Shader();
            DefaultVertexShader.CreateFromFile(ShaderType.VertexShader, DefaultVertexShaderPath);
            DefaultVertexShader.Compile();
            DefaultVertexShader.Attach(ID);
            Link();
        }

        public void Eneble()
        {
            GL.UseProgram(ID);
            enebled = true;
        }

        public void Disable()
        {
            GL.UseProgram(0);
            enebled = false;
        }

        public static void DisableAll()
        {
            GL.UseProgram(0);
        }
    }
}
