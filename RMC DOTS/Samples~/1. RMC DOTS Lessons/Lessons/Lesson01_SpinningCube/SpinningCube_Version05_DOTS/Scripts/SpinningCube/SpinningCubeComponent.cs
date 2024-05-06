using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version05_DOTS
{
    //  Component  ------------------------------------
    public struct SpinningCubeComponent : IComponentData
    {
        public Vector3 RotationDelta;
    }
}