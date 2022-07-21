global using static Raylib_CsLo.Raylib;
global using static Raylib_CsLo.RayMath;
global using static ShapeEngineCore.ShapeEngine;
using ShapeEngineCore;


//either copy into project file or download those 2 nuget packages.
//< ItemGroup >
//    < PackageReference Include = "Raylib-CsLo" Version = "4.0.1" />
//    < PackageReference Include = "Vortice.XInput" Version = "2.1.19" />
//</ ItemGroup >

//C:\Users\daveg\Desktop\raylib\repos\ShapeEngine\ResourcePacker\bin\Release\net6.0\ResourcePacker.exe $(ProjectDir) "resources" $(OutDir)

//< ItemGroup >
//	< Content Include = "resources\**" >
//		< CopyToOutputDirectory > PreserveNewest </ CopyToOutputDirectory >
//	</ Content >
//</ ItemGroup >

namespace ShapeEngineDemo
{
    static class Program
    {
        public static void Main(params string[] launchParams)
        {
            //ShapeEngine.SetTempDirectory("/solobytegames/shape-engine-demo/temp/");
            //ShapeEngine.LoadTempData("resource.txt", "resourceNames.txt");
            ScreenInitInfo screenInitInfo = new ScreenInitInfo(1920, 1080, 0.25f, 2.0f, "Raylib Template", 60, true, false, 0, false);
            DataInitInfo dataInitInfo = new DataInitInfo("resources/data/test-properties.json", new ShapeEngineDemo.DataObjects.DefaultDataResolver(), "asteroids", "player", "guns", "projectiles", "colors", "engines");
            ShapeEngine.Start(new Demo(), screenInitInfo, dataInitInfo, false);
        }
    }
}

