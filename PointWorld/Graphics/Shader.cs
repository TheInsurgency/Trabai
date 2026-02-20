using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace PointWorld.Graphics;

/// <summary>
/// Loads and manages OpenGL shaders
/// </summary>
public class Shader : IDisposable
{
    public int Handle { get; private set; }
    private bool _disposed = false;
    
    public Shader(string vertexPath, string fragmentPath)
    {
        // Load shader source code
        string vertexSource = File.ReadAllText(vertexPath);
        string fragmentSource = File.ReadAllText(fragmentPath);
        
        // Create and compile vertex shader
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSource);
        GL.CompileShader(vertexShader);
        CheckCompileErrors(vertexShader, "VERTEX");
        
        // Create and compile fragment shader
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSource);
        GL.CompileShader(fragmentShader);
        CheckCompileErrors(fragmentShader, "FRAGMENT");
        
        // Link shaders into a program
        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);
        GL.LinkProgram(Handle);
        CheckCompileErrors(Handle, "PROGRAM");
        
        // Cleanup
        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }
    
    public void Use()
    {
        GL.UseProgram(Handle);
    }
    
    public void SetMatrix4(string name, Matrix4 matrix)
    {
        int location = GL.GetUniformLocation(Handle, name);
        GL.UniformMatrix4(location, false, ref matrix);
    }
    
    public void SetVector3(string name, Vector3 value)
    {
        int location = GL.GetUniformLocation(Handle, name);
        GL.Uniform3(location, value);
    }
    
    public void SetFloat(string name, float value)
    {
        int location = GL.GetUniformLocation(Handle, name);
        GL.Uniform1(location, value);
    }
    
    private void CheckCompileErrors(int shader, string type)
    {
        if (type != "PROGRAM")
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string log = GL.GetShaderInfoLog(shader);
                throw new Exception($"Shader compilation error ({type}):\n{log}");
            }
        }
        else
        {
            GL.GetProgram(shader, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string log = GL.GetProgramInfoLog(shader);
                throw new Exception($"Shader linking error:\n{log}");
            }
        }
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            GL.DeleteProgram(Handle);
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}