// V1.0

// using OpenTK.Mathematics;
// using PointWorld.Graphics;

// namespace PointWorld.World;

// /// <summary>
// /// Represents a section of the world (like Minecraft chunks)
// /// </summary>
// public class Chunk
// {
//     public Vector2i ChunkPosition { get; }
//     public const int ChunkSize = 16;
    
//     private PointBuffer? _pointBuffer;
//     private bool _isGenerated = false;
    
//     public Chunk(Vector2i position)
//     {
//         ChunkPosition = position;
//     }
    
//     public void Generate(int seed)
//     {
//         if (_isGenerated) return;
        
//         var random = new Random(seed + ChunkPosition.X * 1000 + ChunkPosition.Y);
//         var points = new List<PointBuffer.PointData>();
        
//         // World coordinates for this chunk
//         int worldX = ChunkPosition.X * ChunkSize;
//         int worldZ = ChunkPosition.Y * ChunkSize;
        
//         // Generate points for this chunk
//         for (int x = 0; x < ChunkSize; x++)
//         {
//             for (int z = 0; z < ChunkSize; z++)
//             {
//                 float wx = worldX + x;
//                 float wz = worldZ + z;
                
//                 // Simple terrain generation (will be replaced with proper generation later)
//                 float dist = MathF.Sqrt(wx * wx + wz * wz);
//                 float height = MathF.Sin(dist * 0.1f) * 3f;
//                 height += (float)(random.NextDouble() - 0.5) * 0.5f;
                
//                 // Color based on height
//                 float colorFactor = (height + 3f) / 6f;
//                 Vector4 color = new Vector4(
//                     0.2f + colorFactor * 0.3f,
//                     0.3f + colorFactor * 0.5f,
//                     0.4f + colorFactor * 0.4f,
//                     1f
//                 );
                
//                 points.Add(new PointBuffer.PointData(
//                     new Vector3(wx * 0.5f, height, wz * 0.5f),
//                     color
//                 ));
//             }
//         }
        
//         _pointBuffer = new PointBuffer();
//         _pointBuffer.SetData(points.ToArray());
//         _isGenerated = true;
//     }
    
//     public void Render()
//     {
//         _pointBuffer?.Draw();
//     }
    
//     public void Dispose()
//     {
//         _pointBuffer?.Dispose();
//     }
// }
using OpenTK.Mathematics;
using PointWorld.Graphics;

namespace PointWorld.World;

/// <summary>
/// Represents a section of the world (like Minecraft chunks)
/// </summary>
public class Chunk
{
    public Vector2i ChunkPosition { get; }
    public const int ChunkSize = 16;
    
    private PointBuffer? _pointBuffer;
    private bool _isGenerated = false;
    
    public Chunk(Vector2i position)
    {
        ChunkPosition = position;
    }
    
    public void Generate(int seed)
    {
        if (_isGenerated) return;
        
        var random = new Random(seed + ChunkPosition.X * 1000 + ChunkPosition.Y);
        var points = new List<PointBuffer.PointData>();
        
        // World coordinates for this chunk
        int worldX = ChunkPosition.X * ChunkSize;
        int worldZ = ChunkPosition.Y * ChunkSize;
        
        // INCREASED DENSITY: Generate more points with finer spacing
        float spacing = 0.25f;  // Halved spacing = 4x more points
        int samplesPerChunk = (int)(ChunkSize / spacing);
        
        for (int x = 0; x < samplesPerChunk; x++)
        {
            for (int z = 0; z < samplesPerChunk; z++)
            {
                float wx = worldX + (x * spacing);
                float wz = worldZ + (z * spacing);
                
                // Simple terrain generation
                float dist = MathF.Sqrt(wx * wx + wz * wz);
                float height = MathF.Sin(dist * 0.1f) * 3f;
                height += (float)(random.NextDouble() - 0.5) * 0.5f;
                
                // Color based on height
                float colorFactor = (height + 3f) / 6f;
                Vector4 color = new Vector4(
                    0.2f + colorFactor * 0.3f,
                    0.3f + colorFactor * 0.5f,
                    0.4f + colorFactor * 0.4f,
                    1f
                );
                
                points.Add(new PointBuffer.PointData(
                    new Vector3(wx * 0.5f, height, wz * 0.5f),
                    color
                ));
            }
        }
        
        _pointBuffer = new PointBuffer();
        _pointBuffer.SetData(points.ToArray());
        _isGenerated = true;
    }
    
    public void Render()
    {
        _pointBuffer?.Draw();
    }
    
    public void Dispose()
    {
        _pointBuffer?.Dispose();
    }
}