using OpenTK.Mathematics;

namespace PointWorld.Graphics;

/// <summary>
/// First-person camera with mouse look and WASD movement
/// </summary>
public class Camera
{
    public Vector3 Position { get; set; }
    public Vector3 Front { get; private set; }
    public Vector3 Up { get; private set; }
    public Vector3 Right { get; private set; }
    
    private float _yaw = -90f;
    private float _pitch = 0f;
    private float _fov = 75f;
    
    public float MouseSensitivity { get; set; } = 0.1f;
    public float MovementSpeed { get; set; } = 5f;
    
    public Camera(Vector3 position)
    {
        Position = position;
        Up = Vector3.UnitY;
        UpdateVectors();
    }
    
    public Matrix4 GetViewMatrix()
    {
        return Matrix4.LookAt(Position, Position + Front, Up);
    }
    
    public Matrix4 GetProjectionMatrix(float aspectRatio)
    {
        return Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(_fov),
            aspectRatio,
            0.1f,
            1000f
        );
    }
    
    public void ProcessMouseMovement(float xOffset, float yOffset)
    {
        xOffset *= MouseSensitivity;
        yOffset *= MouseSensitivity;
        
        _yaw += xOffset;
        _pitch -= yOffset;
        
        // Clamp pitch to prevent camera flipping
        if (_pitch > 89f) _pitch = 89f;
        if (_pitch < -89f) _pitch = -89f;
        
        UpdateVectors();
    }
    
    public void ProcessKeyboard(CameraMovement direction, float deltaTime)
    {
        float velocity = MovementSpeed * deltaTime;
        
        switch (direction)
        {
            case CameraMovement.Forward:
                Position += Front * velocity;
                break;
            case CameraMovement.Backward:
                Position -= Front * velocity;
                break;
            case CameraMovement.Left:
                Position -= Right * velocity;
                break;
            case CameraMovement.Right:
                Position += Right * velocity;
                break;
            case CameraMovement.Up:
                Position += Up * velocity;
                break;
            case CameraMovement.Down:
                Position -= Up * velocity;
                break;
        }
    }
    
    private void UpdateVectors()
    {
        // Calculate new front vector
        Front = new Vector3(
            MathF.Cos(MathHelper.DegreesToRadians(_yaw)) * MathF.Cos(MathHelper.DegreesToRadians(_pitch)),
            MathF.Sin(MathHelper.DegreesToRadians(_pitch)),
            MathF.Sin(MathHelper.DegreesToRadians(_yaw)) * MathF.Cos(MathHelper.DegreesToRadians(_pitch))
        );
        Front = Vector3.Normalize(Front);
        
        // Recalculate right and up vectors
        Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
        Up = Vector3.Normalize(Vector3.Cross(Right, Front));
    }
}

public enum CameraMovement
{
    Forward,
    Backward,
    Left,
    Right,
    Up,
    Down
}