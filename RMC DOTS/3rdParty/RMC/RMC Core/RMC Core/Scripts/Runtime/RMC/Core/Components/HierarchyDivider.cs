using UnityEngine;

namespace RMC.Components
{
    public class HierarchyDivider : MonoBehaviour
    {
        protected virtual string Title
        {
            get { return "Title";  }
        }

        protected virtual void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }

            var newTitle = CenterTitle(Title, 30);
            if (gameObject.name != newTitle)
            {
                gameObject.name = newTitle; 
            }
        }
        
        private static string CenterTitle(string title, int length)
        {
            title = " " + title + " ";
            int totalPadding = length - title.Length;
            int leftPadding = totalPadding / 2;
            int rightPadding = totalPadding - leftPadding;

            string result = new string('-', leftPadding) + title + new string('-', rightPadding);

            return result;
        }
    }
}
