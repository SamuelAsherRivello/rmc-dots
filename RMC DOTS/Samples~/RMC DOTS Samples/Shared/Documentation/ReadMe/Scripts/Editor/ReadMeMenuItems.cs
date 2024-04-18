using RMC.Core.ReadMe;
using UnityEditor;

namespace RMC.MyProject.ReadMe
{
	public static class ReadMeMenuItems
	{
		//  Fields ----------------------------------------
		public const string PathMenuItemWindowCompanyProject = "Window/" + CompanyName + "/" + ProjectName;
		public const string CompanyName = "RMC";
		public const string ProjectName = "Unity Dots Playground";
		public const int PriorityMenuItem_Examples = -100;
        
		//  Fields ----------------------------------------
		
		[MenuItem( PathMenuItemWindowCompanyProject + "/" + "Open ReadMe", 
			false,
						PriorityMenuItem_Examples)]
		public static void SelectReadmes()
		{
			ReadMeHelper.SelectReadmes();
		}
	}
}
