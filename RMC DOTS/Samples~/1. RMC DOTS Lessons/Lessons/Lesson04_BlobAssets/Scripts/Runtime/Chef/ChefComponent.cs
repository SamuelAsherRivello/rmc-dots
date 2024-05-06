using Unity.Entities;

namespace RMC.DOTS.Lessons.BlobAssets
{
    //  Component  ------------------------------------
    public struct ChefComponent : IComponentData
    {
        public float WaterInLitersRemaining;
        public float FlourInKilogramsRemaining;
        public BlobAssetReference<ChefRecipeData> RecipeDataRef;
    }
}