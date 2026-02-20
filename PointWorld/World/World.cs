using OpenTK.Mathematics;

namespace PointWorld.World;

/// <summary>
/// Manages the entire game world (terrain, entities, chunks)
/// </summary>
public class World
{
    public int Seed { get; private set; }
    
    private Dictionary<Vector2i, Chunk> _chunks = new();
    private List<Entity> _entities = new();
    private const int RenderDistance = 5; // Chunks around player to keep loaded
    
    public World(int seed)
    {
        Seed = seed;
        Console.WriteLine($"World created with seed: {seed}");
    }
    
    /// <summary>
    /// Update world based on player position (load/unload chunks)
    /// </summary>
    public void Update(Vector3 playerPosition, float deltaTime)
    {
        // Calculate which chunk the player is in
        Vector2i playerChunk = new Vector2i(
            (int)MathF.Floor(playerPosition.X / (Chunk.ChunkSize * 0.5f)),
            (int)MathF.Floor(playerPosition.Z / (Chunk.ChunkSize * 0.5f))
        );
        
        // Load chunks around player
        for (int x = -RenderDistance; x <= RenderDistance; x++)
        {
            for (int z = -RenderDistance; z <= RenderDistance; z++)
            {
                Vector2i chunkPos = playerChunk + new Vector2i(x, z);
                
                if (!_chunks.ContainsKey(chunkPos))
                {
                    var chunk = new Chunk(chunkPos);
                    chunk.Generate(Seed);
                    _chunks[chunkPos] = chunk;
                }
            }
        }
        
        // Update entities
        foreach (var entity in _entities.Where(e => e.IsActive))
        {
            entity.Update(deltaTime);
        }
        
        // TODO: Unload chunks that are too far away (memory optimization for later)
    }
    
    /// <summary>
    /// Render all visible chunks
    /// </summary>
    public void Render()
    {
        foreach (var chunk in _chunks.Values)
        {
            chunk.Render();
        }
        
        // Render entities
        foreach (var entity in _entities.Where(e => e.IsActive))
        {
            entity.Render();
        }
    }
    
    /// <summary>
    /// Add an entity to the world
    /// </summary>
    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }
    
    /// <summary>
    /// Remove an entity from the world
    /// </summary>
    public void RemoveEntity(Entity entity)
    {
        _entities.Remove(entity);
    }
    
    public void Dispose()
    {
        foreach (var chunk in _chunks.Values)
        {
            chunk.Dispose();
        }
        _chunks.Clear();
    }
}