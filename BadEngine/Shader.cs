using System;
using System.IO;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BadEngine
{
    public class Shader
    {
        public int ShaderID;
        public void CreateFromFile(ShaderType type, string path)
        {
            ShaderID = GL.CreateShader(type);
            if (File.Exists(path))
            {
                GL.ShaderSource(ShaderID, File.ReadAllText(path));
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        public void Compile()
        {
            GL.CompileShader(ShaderID);
        }
        public string GetInfoLog()
        {
            return GL.GetShaderInfoLog(ShaderID);
        }

        public void Destroy()
        {
            GL.DeleteShader(ShaderID);
        }

        public void Attach(ShaderProgram program)
        {
            GL.AttachShader(program.ProgramID, ShaderID);
        }

        public void Detach(ShaderProgram program)
        {
            GL.DetachShader(program.ProgramID, ShaderID);
        }
    }
}
