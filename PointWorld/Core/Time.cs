namespace PointWorld.Core;

/// <summary>
/// Manages time-related values for smooth, frame-independent updates
/// </summary>
public static class Time
{
    /// <summary>
    /// Time in seconds since the last frame
    /// </summary>
    public static float DeltaTime { get; private set; }
    
    /// <summary>
    /// Total time in seconds since the game started
    /// </summary>
    public static float TotalTime { get; private set; }
    
    /// <summary>
    /// Current frame rate (frames per second)
    /// </summary>
    public static float FPS => DeltaTime > 0 ? 1f / DeltaTime : 0f;
    
    /// <summary>
    /// Updates the time values (call once per frame)
    /// </summary>
    public static void Update(float deltaTime)
    {
        DeltaTime = deltaTime;
        TotalTime += deltaTime;
    }
}