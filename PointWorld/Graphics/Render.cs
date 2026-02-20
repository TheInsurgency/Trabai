// // using OpenTK.Graphics.OpenGL4;
// // using OpenTK.Mathematics;

// // namespace PointWorld.Graphics;

// // /// <summary>
// // /// Coordinates all rendering operations
// // /// </summary>
// // public class Renderer
// // {
// //     private Shader? _pointShader;
// //     private Camera? _camera;
    
// //     public void Initialize()
// //     {
// //         // OpenGL settings
// //         GL.ClearColor(0.05f, 0.05f, 0.07f, 1f);
// //         GL.Enable(EnableCap.DepthTest);
// //         GL.Enable(EnableCap.ProgramPointSize);
        
// //         // Load shaders
// //         _pointShader = new Shader("Shaders/point.vert", "Shaders/point.frag");
        
// //         Console.WriteLine("Renderer initialized");
// //     }
    
// //     public void SetCamera(Camera camera)
// //     {
// //         _camera = camera;
// //     }
    
// //     public void BeginFrame()
// //     {
// //         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
// //     }
    
// //     public void RenderPointCloud(PointBuffer pointBuffer, float aspectRatio, float pointSize = 4.0f)
// //     {
// //         if (_camera == null || _pointShader == null) return;
        
// //         _pointShader.Use();
        
// //         // Calculate view-projection matrix
// //         Matrix4 view = _camera.GetViewMatrix();
// //         Matrix4 projection = _camera.GetProjectionMatrix(aspectRatio);
// //         Matrix4 viewProjection = view * projection;
        
// //         // Set shader uniforms
// //         _pointShader.SetMatrix4("uViewProjection", viewProjection);
// //         _pointShader.SetFloat("uPointSize", pointSize);
        
// //         // Draw the point cloud
// //         pointBuffer.Draw();
// //     }
    
// //     public void Dispose()
// //     {
// //         _pointShader?.Dispose();
// //     }
// // }

// using OpenTK.Graphics.OpenGL4;
// using OpenTK.Mathematics;

// namespace PointWorld.Graphics;

// /// <summary>
// /// Coordinates all rendering operations
// /// </summary>
// public class Renderer
// {
//     private Shader? _pointShader;
//     private Camera? _camera;
    
//     public void Initialize()
//     {
//         // OpenGL settings
//         GL.ClearColor(0.05f, 0.05f, 0.07f, 1f);
//         GL.Enable(EnableCap.DepthTest);
//         GL.Enable(EnableCap.ProgramPointSize);
        
//         // Load shaders
//         _pointShader = new Shader("Shaders/point.vert", "Shaders/point.frag");
        
//         Console.WriteLine("Renderer initialized");
//     }
    
//     public void SetCamera(Camera camera)
//     {
//         _camera = camera;
//     }
    
//     public void BeginFrame()
//     {
//         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
//     }
    
//     public void RenderPointCloud(PointBuffer pointBuffer, float aspectRatio, float pointSize = 4.0f)
//     {
//         if (_camera == null || _pointShader == null) return;
        
//         _pointShader.Use();
        
//         // Calculate view-projection matrix
//         Matrix4 view = _camera.GetViewMatrix();
//         Matrix4 projection = _camera.GetProjectionMatrix(aspectRatio);
//         Matrix4 viewProjection = view * projection;
        
//         // Set shader uniforms
//         _pointShader.SetMatrix4("uViewProjection", viewProjection);
//         _pointShader.SetFloat("uPointSize", pointSize);
        
//         // Draw the point cloud
//         pointBuffer.Draw();
//     }
    
//     public void PrepareForRendering(float aspectRatio, float pointSize = 4.0f)
//     {
//         if (_camera == null || _pointShader == null) return;
        
//         _pointShader.Use();
        
//         // Calculate view-projection matrix
//         Matrix4 view = _camera.GetViewMatrix();
//         Matrix4 projection = _camera.GetProjectionMatrix(aspectRatio);
//         Matrix4 viewProjection = view * projection;
        
//         // Set shader uniforms
//         _pointShader.SetMatrix4("uViewProjection", viewProjection);
//         _pointShader.SetFloat("uPointSize", pointSize);
//     }
    
//     public void Dispose()
//     {
//         _pointShader?.Dispose();
//     }
// }


// V1.0


using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace PointWorld.Graphics;

/// <summary>
/// Coordinates all rendering operations
/// </summary>
public class Renderer
{
    private Shader? _pointShader;
    private Camera? _camera;
    
    public void Initialize()
    {
        // OpenGL settings
        GL.ClearColor(0.05f, 0.05f, 0.07f, 1f);
        GL.Enable(EnableCap.DepthTest);
        GL.Enable(EnableCap.ProgramPointSize);
        
        // Load shaders
        _pointShader = new Shader("Shaders/point.vert", "Shaders/point.frag");
        
        Console.WriteLine("Renderer initialized");
    }
    
    public void SetCamera(Camera camera)
    {
        _camera = camera;
    }
    
    public void BeginFrame()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }
    
    public void RenderPointCloud(PointBuffer pointBuffer, float aspectRatio, float pointSize = 4.0f)
    {
        if (_camera == null || _pointShader == null) return;
        
        _pointShader.Use();
        
        // Calculate view-projection matrix
        Matrix4 view = _camera.GetViewMatrix();
        Matrix4 projection = _camera.GetProjectionMatrix(aspectRatio);
        Matrix4 viewProjection = view * projection;
        
        // Set shader uniforms
        _pointShader.SetMatrix4("uViewProjection", viewProjection);
        _pointShader.SetFloat("uPointSize", pointSize);
        
        // Draw the point cloud
        pointBuffer.Draw();
    }
    
    public void PrepareForRendering(float aspectRatio, float pointSize = 4.0f)
    {
        if (_camera == null || _pointShader == null) return;
        
        _pointShader.Use();
        
        // Calculate view-projection matrix
        Matrix4 view = _camera.GetViewMatrix();
        Matrix4 projection = _camera.GetProjectionMatrix(aspectRatio);
        Matrix4 viewProjection = view * projection;
        
        // Set shader uniforms
        _pointShader.SetMatrix4("uViewProjection", viewProjection);
        _pointShader.SetFloat("uPointSize", pointSize);
        _pointShader.SetVector3("uCameraPosition", _camera.Position);
    }
    
    public void Dispose()
    {
        _pointShader?.Dispose();
    }
}