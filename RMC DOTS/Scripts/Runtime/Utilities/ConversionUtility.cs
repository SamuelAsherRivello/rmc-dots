namespace RMC.DOTS.Utilities
{
    public static class ConversionUtility
    {
        // Converts from UnityEngine.Vector3 to System.Numerics.Vector3
        public static System.Numerics.Vector3 ToNumericsVector3 (UnityEngine.Vector3 unityVector)
        {
            return new System.Numerics.Vector3(unityVector.x, unityVector.y, unityVector.z);
        }

        // Converts from System.Numerics.Vector3 to UnityEngine.Vector3
        public static UnityEngine.Vector3 ToUnityEngineVector3(System.Numerics.Vector3 systemVector)
        {
            return new UnityEngine.Vector3(systemVector.X, systemVector.Y, systemVector.Z);
        }
        
        // Converts from System.Numerics.Vector3 to Unity.Mathematics.float3
        public static Unity.Mathematics.float3 ToMathmaticsFloat3(System.Numerics.Vector3 systemVector)
        {
            return new Unity.Mathematics.float3(systemVector.X, systemVector.Y, systemVector.Z);
        }

        // Converts from Unity.Mathematics.float3 to System.Numerics.Vector3
        public static System.Numerics.Vector3 ToNumericsVector3(Unity.Mathematics.float3 float3Vector)
        {
            return new System.Numerics.Vector3(float3Vector.x, float3Vector.y, float3Vector.z);
        }
    }
}