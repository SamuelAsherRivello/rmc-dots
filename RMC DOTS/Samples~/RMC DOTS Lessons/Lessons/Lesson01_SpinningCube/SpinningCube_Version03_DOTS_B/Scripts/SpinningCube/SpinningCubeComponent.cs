using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version03_DOTS_B
{
    //  Component  ------------------------------------
    public struct SpinningCubeComponent : IComponentData
    {
        public Vector3 RotationDelta;
    }
}