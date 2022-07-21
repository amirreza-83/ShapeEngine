global using static Raylib_CsLo.Raylib;
global using static Raylib_CsLo.RayMath;
global using static ShapeEngineCore.ShapeEngine;

namespace ShapeEngineCore
{
    public static class ShapeEngine
    {
        public static GameLoop GAMELOOP = new GameLoop();
        public static readonly string CURRENT_DIRECTORY = Environment.CurrentDirectory;
        //public static string TEMP_DIRECTORY = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //-----------DEBUG--------------
        public static bool DEBUGMODE = true;
        public static bool DEBUG_DrawColliders = false;
        public static bool DEBUG_DrawHelpers = false;
        public static Raylib_CsLo.Color DEBUG_ColliderColor = new(0, 25, 200, 100);
        public static Raylib_CsLo.Color DEBUG_ColliderDisabledColor = new(200, 25, 0, 100);
        public static Raylib_CsLo.Color DEBUG_HelperColor = new(0, 200, 25, 100);
        private static byte[] DecodeDecompress(string base64string, string path)
        {
            byte[] bytes = Convert.FromBase64String(base64string);
            return SevenZip.Compression.LZMA.SevenZipHelper.Decompress(bytes);

        }
        public static void LoadTempData(string dataPath, string namePath)
        {
            string[] names = File.ReadAllLines(namePath);
            string[] dataLines = File.ReadAllLines(dataPath);

            for (int i = 0; i < names.Length; i++)
            {
                var bytes = DecodeDecompress(dataLines[i], names[i]);
                string path = names[i];
                //string path = TEMP_DIRECTORY + names[i];
                string? directoryPath = Path.GetDirectoryName(path);
                if (directoryPath == null) continue;
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                File.WriteAllBytes(path, bytes);
            }
        }
        public static void DeleteDirectory(string path)
        {
            if(Directory.Exists(path)) Directory.Delete(path, true);
        }
        //public static void SetTempDirectory(string path)
        //{
        //    TEMP_DIRECTORY = TEMP_DIRECTORY + path;
        //}
        public static void Start(GameLoop gameloop, ScreenInitInfo screenInitInfo, DataInitInfo dataInitInfo, bool loadResFiles, params string[] launchParams)
        {
            GAMELOOP = gameloop;

            if (!DEBUGMODE)
            {
                DEBUG_DrawColliders = false;
                DEBUG_DrawHelpers = false;
            }
            //LoadTempData("resource.txt", "resourceNames.txt");
            //Start of Program
            if(loadResFiles)GAMELOOP.LoadResources();
            GAMELOOP.Initialize(screenInitInfo, dataInitInfo, launchParams);
            GAMELOOP.Start();
            //Data is loaded during the game which leads to problems when resource folder is deleted.
            //GAMELOOP.SetupFinished();

            GAMELOOP.Run();//runs continously

            //End of Program
            GAMELOOP.End();
            bool fullscreen = GAMELOOP.Close();
            if (GAMELOOP.RESTART)
            {
                if (fullscreen) Start(gameloop, screenInitInfo, dataInitInfo, loadResFiles, "fullscreen");
                else Start(gameloop, screenInitInfo, dataInitInfo, loadResFiles);
            }
            else
            {
                if (loadResFiles) GAMELOOP.DeleteResources(); // DeleteDirectory("resources");
            }
        }
    }
}


