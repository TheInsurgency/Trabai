// using OpenTK.Windowing.Desktop;
// using OpenTK.Windowing.Common;
// using OpenTK.Graphics.OpenGL4;
// using OpenTK.Mathematics;
// using OpenTK.Windowing.GraphicsLibraryFramework;
// using PointWorld.Graphics;

// namespace PointWorld.Core;

// public class Game : GameWindow
// {
//     private Camera? _camera;
//     private Shader? _pointShader;
//     private PointBuffer? _pointBuffer;
    
//     private bool _firstMove = true;
//     private Vector2 _lastMousePos;
    
//     public Game()
//         : base(GameWindowSettings.Default,
//                new NativeWindowSettings()
//                {
//                    Size = new Vector2i(1280, 720),
//                    Title = "PointWorld - Alpha",
//                    StartVisible = false // Don't show until loaded
//                })
//     { }
    
//     protected override void OnLoad()
//     {
//         base.OnLoad();
        
//         // OpenGL settings
//         GL.ClearColor(0.05f, 0.05f, 0.07f, 1f);
//         GL.Enable(EnableCap.DepthTest);
//         GL.Enable(EnableCap.ProgramPointSize); // Allow shaders to control point size
        
//         // Initialize camera
//         _camera = new Camera(new Vector3(0f, 5f, 10f));
        
//         // Load shaders
//         _pointShader = new Shader("Shaders/point.vert", "Shaders/point.frag");
        
//         // Create test point cloud (a simple grid for now)
//         _pointBuffer = new PointBuffer();
//         GenerateTestPoints();
        
//         // Lock cursor to window for FPS controls
//         CursorState = CursorState.Grabbed;
        
//         IsVisible = true;
        
//         Console.WriteLine("Game initialized successfully!");
//         Console.WriteLine("Controls: WASD - Move | Mouse - Look | Space/Shift - Up/Down | ESC - Exit");
//     }
    
//     private void GenerateTestPoints()
//     {
//         var random = new Random(42); // Fixed seed for now
//         var points = new List<PointBuffer.PointData>();
        
//         // Create a simple terrain-like point cloud
//         for (int x = -50; x < 50; x++)
//         {
//             for (int z = -50; z < 50; z++)
//             {
//                 // Simple height based on distance from center
//                 float dist = MathF.Sqrt(x * x + z * z);
//                 float height = MathF.Sin(dist * 0.1f) * 3f;
                
//                 // Add some randomness
//                 height += (float)(random.NextDouble() - 0.5) * 0.5f;
                
//                 // Color based on height
//                 float colorFactor = (height + 3f) / 6f; // Normalize to 0-1
//                 Vector4 color = new Vector4(
//                     0.2f + colorFactor * 0.3f,
//                     0.3f + colorFactor * 0.5f,
//                     0.4f + colorFactor * 0.4f,
//                     1f
//                 );
                
//                 points.Add(new PointBuffer.PointData(
//                     new Vector3(x * 0.5f, height, z * 0.5f),
//                     color
//                 ));
//             }
//         }
        
//         _pointBuffer?.SetData(points.ToArray());
//         Console.WriteLine($"Generated {points.Count} points");
//     }
    
//     protected override void OnUpdateFrame(FrameEventArgs args)
//     {
//         base.OnUpdateFrame(args);
        
//         Time.Update((float)args.Time);
        
//         // Exit on ESC
//         if (KeyboardState.IsKeyDown(Keys.Escape))
//         {
//             Close();
//         }
        
//         // Camera movement
//         if (_camera != null)
//         {
//             if (KeyboardState.IsKeyDown(Keys.W))
//                 _camera.ProcessKeyboard(CameraMovement.Forward, Time.DeltaTime);
//             if (KeyboardState.IsKeyDown(Keys.S))
//                 _camera.ProcessKeyboard(CameraMovement.Backward, Time.DeltaTime);
//             if (KeyboardState.IsKeyDown(Keys.A))
//                 _camera.ProcessKeyboard(CameraMovement.Left, Time.DeltaTime);
//             if (KeyboardState.IsKeyDown(Keys.D))
//                 _camera.ProcessKeyboard(CameraMovement.Right, Time.DeltaTime);
//             if (KeyboardState.IsKeyDown(Keys.Space))
//                 _camera.ProcessKeyboard(CameraMovement.Up, Time.DeltaTime);
//             if (KeyboardState.IsKeyDown(Keys.LeftShift))
//                 _camera.ProcessKeyboard(CameraMovement.Down, Time.DeltaTime);
//         }
//     }
    
//     protected override void OnRenderFrame(FrameEventArgs args)
//     {
//         base.OnRenderFrame(args);
        
//         GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
//         if (_camera != null && _pointShader != null && _pointBuffer != null)
//         {
//             _pointShader.Use();
            
//             // Set matrices
//             Matrix4 view = _camera.GetViewMatrix();
//             Matrix4 projection = _camera.GetProjectionMatrix((float)Size.X / Size.Y);
//             Matrix4 viewProjection = view * projection;
            
//             _pointShader.SetMatrix4("uViewProjection", viewProjection);
//             _pointShader.SetFloat("uPointSize", 4.0f);
            
//             // Draw points
//             _pointBuffer.Draw();
//         }
        
//         SwapBuffers();
//     }
    
//     protected override void OnMouseMove(MouseMoveEventArgs e)
//     {
//         base.OnMouseMove(e);
        
//         if (_firstMove)
//         {
//             _lastMousePos = new Vector2(e.X, e.Y);
//             _firstMove = false;
//             return;
//         }
        
//         float deltaX = e.X - _lastMousePos.X;
//         float deltaY = e.Y - _lastMousePos.Y;
//         _lastMousePos = new Vector2(e.X, e.Y);
        
//         _camera?.ProcessMouseMovement(deltaX, deltaY);
//     }
    
//     protected override void OnResize(ResizeEventArgs e)
//     {
//         base.OnResize(e);
//         GL.Viewport(0, 0, e.Width, e.Height);
//     }
    
//     protected override void OnUnload()
//     {
//         base.OnUnload();
        
//         _pointShader?.Dispose();
//         _pointBuffer?.Dispose();
//     }
// }



using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using PointWorld.Graphics;
using PointWorld.World;

namespace PointWorld.Core;

public class Game : GameWindow
{
    private Camera? _camera;
    private Renderer? _renderer;
    private World.World? _world;
    
    private bool _firstMove = true;
    private Vector2 _lastMousePos;
    
    public Game()
        : base(GameWindowSettings.Default,
               new NativeWindowSettings()
               {
                   Size = new Vector2i(1280, 720),
                   Title = "PointWorld - Alpha",
                   StartVisible = false
               })
    { }
    
    protected override void OnLoad()
    {
        base.OnLoad();
        
        // Initialize renderer
        _renderer = new Renderer();
        _renderer.Initialize();
        
        // Initialize camera
        _camera = new Camera(new Vector3(0f, 5f, 10f));
        _renderer.SetCamera(_camera);
        
        // Create world with a seed
        int seed = 42; // TODO: Make this configurable
        _world = new World.World(seed);
        
        // Lock cursor for FPS controls
        CursorState = CursorState.Grabbed;
        
        IsVisible = true;
        
        Console.WriteLine("Game initialized successfully!");
        Console.WriteLine("Controls: WASD - Move | Mouse - Look | Space/Shift - Up/Down | ESC - Exit");
    }
    
    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        
        Time.Update((float)args.Time);
        
        // Exit on ESC
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
        
        // Camera movement
        if (_camera != null)
        {
            if (KeyboardState.IsKeyDown(Keys.W))
                _camera.ProcessKeyboard(CameraMovement.Forward, Time.DeltaTime);
            if (KeyboardState.IsKeyDown(Keys.S))
                _camera.ProcessKeyboard(CameraMovement.Backward, Time.DeltaTime);
            if (KeyboardState.IsKeyDown(Keys.A))
                _camera.ProcessKeyboard(CameraMovement.Left, Time.DeltaTime);
            if (KeyboardState.IsKeyDown(Keys.D))
                _camera.ProcessKeyboard(CameraMovement.Right, Time.DeltaTime);
            if (KeyboardState.IsKeyDown(Keys.Space))
                _camera.ProcessKeyboard(CameraMovement.Up, Time.DeltaTime);
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
                _camera.ProcessKeyboard(CameraMovement.Down, Time.DeltaTime);
        }
        
        // Update world (load/unload chunks based on player position)
        _world?.Update(_camera?.Position ?? Vector3.Zero, Time.DeltaTime);
    }
    
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        
        _renderer?.BeginFrame();
        
        // Prepare shaders with camera matrices
        _renderer?.PrepareForRendering((float)Size.X / Size.Y);
        
        // Render the world
        _world?.Render();
        
        SwapBuffers();
    }
    
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);
        
        if (_firstMove)
        {
            _lastMousePos = new Vector2(e.X, e.Y);
            _firstMove = false;
            return;
        }
        
        float deltaX = e.X - _lastMousePos.X;
        float deltaY = e.Y - _lastMousePos.Y;
        _lastMousePos = new Vector2(e.X, e.Y);
        
        _camera?.ProcessMouseMovement(deltaX, deltaY);
    }
    
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
    
    protected override void OnUnload()
    {
        base.OnUnload();
        
        _renderer?.Dispose();
        _world?.Dispose();
    }
}