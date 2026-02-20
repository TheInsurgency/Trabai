using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace PointWorld.Graphics;

/// <summary>
/// Manages a GPU buffer for rendering point clouds
/// </summary>
public class PointBuffer : IDisposable
{
    private int _vao; // Vertex Array Object
    private int _vbo; // Vertex Buffer Object
    private int _pointCount;
    private bool _disposed = false;
    
    public struct PointData
    {
        public Vector3 Position;
        public Vector4 Color;
        
        public PointData(Vector3 position, Vector4 color)
        {
            Position = position;
            Color = color;
        }
    }
    
    public PointBuffer()
    {
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
    }
    
    /// <summary>
    /// Uploads point data to the GPU
    /// </summary>
    public void SetData(PointData[] points)
    {
        _pointCount = points.Length;
        
        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, points.Length * System.Runtime.InteropServices.Marshal.SizeOf<PointData>(), points, BufferUsageHint.StaticDraw);
        
        // Position attribute (location = 0)
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, System.Runtime.InteropServices.Marshal.SizeOf<PointData>(), 0);
        
        // Color attribute (location = 1)
        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, System.Runtime.InteropServices.Marshal.SizeOf<PointData>(), 12); // 12 = sizeof(Vector3)
        
        GL.BindVertexArray(0);
    }
    
    /// <summary>
    /// Renders all points
    /// </summary>
    public void Draw()
    {
        GL.BindVertexArray(_vao);
        GL.DrawArrays(PrimitiveType.Points, 0, _pointCount);
        GL.BindVertexArray(0);
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            GL.DeleteBuffer(_vbo);
            GL.DeleteVertexArray(_vao);
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}