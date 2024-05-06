
namespace RMC.Audio.Data.Types
{
    /// <summary>
    /// Helper values
    /// </summary>
    public static class AudioConstants
    {
        //  Fields  -----------------------------------------------
        
        ///////////////////////////////////////////
        // MenuItem Path
        ///////////////////////////////////////////
        public const string CompanyName = "RMC";
        public const string ProjectName = "Audio";
        public const string OpenReadMe = "Open ReadMe";
        public const string PathCoreWindowMenu = "Window/" + CompanyName + "/" + ProjectName + "/";
        public const string PathCoreCreateAssetMenu = CompanyName + "/" + ProjectName + "/";
        public const string PathCoreAssetsMenu = "Assets/"+ CompanyName + "/" + ProjectName+ "/Samples";
        
        ///////////////////////////////////////////
        // MenuItem Priority
        ///////////////////////////////////////////

        // Skipping ">10" shows a horizontal divider line.
        public const int PriorityTools_Primary = 10;
        public const int PriorityTools_Secondary = 100;
        public const int PriorityTools_Examples = 1000;
        public const int PriorityTools_Examples_Sub = 5000;
        public const int PriorityTools_Samples = 10000;
        public const int PriorityAssets_Examples = 1;
        
        ///////////////////////////////////////////
        // Display Text
        ///////////////////////////////////////////
    }
}