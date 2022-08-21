using System;
using System.IO;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BadEngine
{
    public static class Shader
    {
        public static int CreateShaderFromFile(ShaderType type, string path)
        {
            int Shader = GL.CreateShader(type);
            GL.ShaderSource(Shader, File.ReadAllText(path));
            return Shader;
        }
        public static string CompileShader(int Shader)
        {
            GL.CompileShader(Shader);
            return GL.GetShaderInfoLog(Shader);
        }
    }
}
