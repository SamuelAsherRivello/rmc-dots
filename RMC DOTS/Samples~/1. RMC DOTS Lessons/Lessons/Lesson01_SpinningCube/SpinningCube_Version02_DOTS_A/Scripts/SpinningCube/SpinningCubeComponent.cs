using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.SpinningCube_Version02_DOTS_A
{
    //  Component  ------------------------------------
    public struct SpinningCubeComponent : IComponentData
    {
        public Vector3 RotationDelta;
    }
}