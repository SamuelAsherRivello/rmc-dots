using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version04_DOTS_C
{
    //  Component  ------------------------------------
    public struct SpinningCubeComponent : IComponentData
    {
        public Vector3 RotationDelta;
    }
}