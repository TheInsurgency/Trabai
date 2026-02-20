#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec4 aColor;

uniform mat4 uViewProjection;
uniform float uPointSize;
uniform vec3 uCameraPosition;

out vec4 vColor;

void main()
{
    gl_Position = uViewProjection * vec4(aPosition, 1.0);
    
    // Distance-based point size (closer = bigger)
    float distance = length(uCameraPosition - aPosition);
    float size = uPointSize * (10.0 / max(distance, 1.0));
    gl_PointSize = clamp(size, 1.0, 20.0);
    
    vColor = aColor;
}
