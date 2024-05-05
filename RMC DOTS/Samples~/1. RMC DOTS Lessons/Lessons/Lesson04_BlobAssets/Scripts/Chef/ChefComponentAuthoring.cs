using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.BlobAssets
{


    
    public class ChefComponentAuthoring : MonoBehaviour
    {
        public class ChefComponentAuthoringBaker : Baker<ChefComponentAuthoring>
        {
            public override void Bake(ChefComponentAuthoring authoring)
            {
                //  Entity  ------------------------------------
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                // NOTE 1: You could certainly store a COPY of recipes 
                // on every ChefComponent, but that would be wasteful.
                // So, we won't do that here.

                // NOTE 2: It's better to store a reference to ONE set of recipes
                // that is shared by all ChefComponents. This is more efficient.
                // So, we will do that here.
                
                // CREATE BLOB ASSET
                var chefRecipeDataRef = CreateChefRecipeData();
                AddBlobAsset(ref chefRecipeDataRef, out var hash);
                
                // STORE BLOB ASSET REFERENCE
                AddComponent(entity, new ChefComponent
                {
                    FlourInKilogramsRemaining = 10f,
                    WaterInLitersRemaining = 30f,
                    RecipeDataRef = chefRecipeDataRef
                });
                
                //Trigger to cook once
                AddComponent<ChefCookOnceTag>(entity);
                
            }
            
            BlobAssetReference<ChefRecipeData> CreateChefRecipeData()
            {
                // Create a new builder that will use temporary memory to construct the blob asset
                var builder = new BlobBuilder(Allocator.Temp);

                // Construct the root object for the blob asset. Notice the use of `ref`.
                ref ChefRecipeData chefRecipeData = ref builder.ConstructRoot<ChefRecipeData>();

                // Now fill the constructed root with the data:
                chefRecipeData.FlourInKilogramsRequired = 1f;
                chefRecipeData.WaterInLitersRequired = 2f;
                
                // Now copy the data from the builder into its final place, which will
                // use the persistent allocator
                var result = builder.CreateBlobAssetReference<ChefRecipeData>(Allocator.Persistent);

                // Make sure to dispose the builder itself so all internal memory is disposed.
                builder.Dispose();
                return result;
            }
        }
    }
}


