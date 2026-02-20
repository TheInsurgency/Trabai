using OpenTK.Mathematics;
using PointWorld.Graphics;

namespace PointWorld.World;

/// <summary>
/// Represents a collection of points (for terrain, objects, etc.)
/// </summary>
public class PointCloud
{
    private List<PointBuffer.PointData> _points = new();
    private PointBuffer? _buffer;
    private bool _isDirty = true;
    
    public int PointCount => _points.Count;
    
    /// <summary>
    /// Add a single point to the cloud
    /// </summary>
    public void AddPoint(Vector3 position, Vector4 color)
    {
        _points.Add(new PointBuffer.PointData(position, color));
        _isDirty = true;
    }
    
    /// <summary>
    /// Add multiple points at once
    /// </summary>
    public void AddPoints(IEnumerable<PointBuffer.PointData> points)
    {
        _points.AddRange(points);
        _isDirty = true;
    }
    
    /// <summary>
    /// Clear all points
    /// </summary>
    public void Clear()
    {
        _points.Clear();
        _isDirty = true;
    }
    
    /// <summary>
    /// Upload to GPU (only if data has changed)
    /// </summary>
    public void UpdateBuffer()
    {
        if (!_isDirty) return;
        
        _buffer ??= new PointBuffer();
        _buffer.SetData(_points.ToArray());
        _isDirty = false;
    }
    
    /// <summary>
    /// Render the point cloud
    /// </summary>
    public void Render()
    {
        if (_isDirty) UpdateBuffer();
        _buffer?.Draw();
    }
    
    public void Dispose()
    {
        _buffer?.Dispose();
    }
}