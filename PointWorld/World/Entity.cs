using OpenTK.Mathematics;

namespace PointWorld.World;

/// <summary>
/// Base class for all entities in the world (NPCs, monsters, etc.)
/// </summary>
public abstract class Entity
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public bool IsActive { get; set; } = true;
    
    protected Entity(Vector3 position)
    {
        Position = position;
        Rotation = Vector3.Zero;
    }
    
    /// <summary>
    /// Called every frame to update entity logic
    /// </summary>
    public abstract void Update(float deltaTime);
    
    /// <summary>
    /// Called when the entity should be rendered
    /// </summary>
    public abstract void Render();
    
    /// <summary>
    /// Distance to another position
    /// </summary>
    public float DistanceTo(Vector3 other)
    {
        return (Position - other).Length;
    }
}