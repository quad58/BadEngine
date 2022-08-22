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
        public bool linked;
        public static string DefaultVertexShaderPath = @"data/shaders/glsl/Default.glsl";

        public static Dictionary<string, int> UniformLocations = new Dictionary<string, int>();

        public void CreateProgram()
        {
            ProgramID = GL.CreateProgram();
            exists = true;
            linked = false;
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

            GL.GetProgram(ProgramID, GetProgramParameterName.LinkStatus, out var code);

            if (code != (int)All.True)
            {
                linked = false;
                throw new Exception($"Shader program {ProgramID}: shader program link error." + GetInfoLog());
            }
            else
            {
                GL.GetProgram(ProgramID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

                UniformLocations.Clear();

                for (var i = 0; i < numberOfUniforms; i++)
                {
                    var key = GL.GetActiveUniform(ProgramID, i, out _, out _);

                    var location = GL.GetUniformLocation(ProgramID, key);

                    UniformLocations.Add(key, location);
                }

                linked = true;
            }
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

            Matrix4 model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-55.0f));
            Matrix4 view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Program.WindowWidth / Program.WindowHeight, 0.1f, 100.0f);

            SetMatrix4("model", model);
            SetMatrix4("view", view);
            SetMatrix4("projection", projection);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            if (linked)
            {
                if (UniformLocations.ContainsKey(name))
                {
                    GL.UniformMatrix4(UniformLocations[name], true, ref data);
                }
                else
                {
                    throw new Exception($"Shader program {ProgramID}: this name of uniform matrix4 does not exists.");
                }
            }
            else
            {
                throw new Exception($"Shader program {ProgramID}: You trying to set matrix4, when shader program not linked.");
            }
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
            linked = false;
        }

        public static void DisableAll()
        {
            GL.UseProgram(0);
        }
    }
}
