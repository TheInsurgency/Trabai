// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// using OpenTK.Windowing.Desktop;
// using OpenTK.Windowing.Common;
// using OpenTK.Graphics.OpenGL4;
// using OpenTK.Mathematics;

// class Game : GameWindow
// {
    
//     public Game()
//         : base(GameWindowSettings.Default,
//                new NativeWindowSettings()
//                {
//                    Size = new Vector2i(800, 600),
//                    Title = "PointWorld"
//                })
//     { }

//     protected override void OnLoad()
//     {
//         GL.ClearColor(0.05f, 0.05f, 0.07f, 1f);
//         base.OnLoad();
//     }

//     protected override void OnRenderFrame(FrameEventArgs e)
//     {
//         GL.Clear(ClearBufferMask.ColorBufferBit);
//         SwapBuffers();
//         base.OnRenderFrame(e);
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         using var game = new Game();
//         game.Run();
//     }
// }


using PointWorld.Core;

namespace PointWorld;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting PointWorld...");
        
        try
        {
            using var game = new Game();
            game.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Console.ReadKey();
        }
    }
}