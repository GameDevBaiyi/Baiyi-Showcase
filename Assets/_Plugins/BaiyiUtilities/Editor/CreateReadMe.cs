using System.IO;
using UnityEditor;

namespace BaiyiUtilities.Editor
{
    public class CreateReadMe
    {
        [MenuItem("Assets/Create/ReadMe",priority = -1)]
        public static void Create()
        {
            string path = Assistant.GetCurrentFolderPath();
            File.WriteAllText(path+"/~ReadMe.txt","");
            AssetDatabase.Refresh();
        }
    }
}