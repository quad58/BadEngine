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
        public static string DefaultVertexShaderPath = @"data/shaders/glsl/Default.glsl";
        public void CreateProgram()
        {
            ProgramID = GL.CreateProgram();
        }

        public void AttachShader(Shader shader)
        {
            shader.Attach(ProgramID);
        }

        public void Link()
        {
            GL.LinkProgram(ProgramID);
        }

        public string GetInfoLog()
        {
            return GL.GetProgramInfoLog(ProgramID);
        }

        public void CreateDefaultProgram()
        {
            CreateProgram();
            Shader DefaultVertexShader = new Shader();
            DefaultVertexShader.CreateFromFile(ShaderType.VertexShader, DefaultVertexShaderPath);
            DefaultVertexShader.Compile();
            DefaultVertexShader.Attach(ProgramID);
            Link();
        }
    }
}
