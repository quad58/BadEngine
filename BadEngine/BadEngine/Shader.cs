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

        public bool compiled;
        public bool exists;
        public bool attached;
        public void CreateFromFile(ShaderType type, string path)
        {
            ShaderID = GL.CreateShader(type);
            compiled = false;
            if (File.Exists(path))
            {
                GL.ShaderSource(ShaderID, File.ReadAllText(path));
                exists = true;
            }
            else
            {
                exists = false;
                throw new FileNotFoundException();
            }
        }
        public void Compile()
        {
            GL.CompileShader(ShaderID);
            GL.GetShader(ShaderID, ShaderParameter.CompileStatus, out var code);
            if (code != (int) All.True)
            {
                throw new Exception($"Shader {ShaderID}: Shader compile error. " + GetInfoLog());
                compiled = true;
            }
        }
        public string GetInfoLog()
        {
            return GL.GetShaderInfoLog(ShaderID);
        }

        public void Destroy()
        {
            GL.DeleteShader(ShaderID);
            exists = false;
        }

        public void Attach(ShaderProgram program)
        {
            GL.AttachShader(program.ProgramID, ShaderID);
            attached = true;
        }

        public void Detach(ShaderProgram program)
        {
            GL.DetachShader(program.ProgramID, ShaderID);
            attached = false;
        }
    }
}
