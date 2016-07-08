// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

namespace UnrealBuildTool.Rules
{
	public class EmotivPlugin : ModuleRules
	{
        public EmotivPlugin(TargetInfo Target)
		{
			PublicIncludePaths.AddRange(
                new string[] {
                    "EmotivPlugin/Public",
					// ... add public include paths required here ...
				}
				);

			PrivateIncludePaths.AddRange(
				new string[] {
					"EmotivPlugin/Private",
                    "EmotivPlugin/Public",
					// ... add other private include paths required here ...
				}
				);

			PublicDependencyModuleNames.AddRange(
				new string[]
				{
					"Core",
                    "CoreUObject",
                    "Engine",
					// ... add other public dependencies that you statically link with here ...
				}
				);

			PrivateDependencyModuleNames.AddRange(
				new string[]
				{
					// ... add private dependencies that you statically link with here ...
				}
				);

			DynamicallyLoadedModuleNames.AddRange(
				new string[]
				{
					// ... add any modules that your module loads dynamically here ...
				}
				);

            LoadEmotivSDK( Target );
		}

        public void LoadEmotivSDK(TargetInfo Target)
        {
            bool isLibrarySupported = false;

            if (Target.Platform == UnrealTargetPlatform.Win64)
            {
                isLibrarySupported = true;

                PublicAdditionalLibraries.Add(Path.Combine(ThirdPartyPath, "EmotivSDK", "Libraries", "win64", "edk.lib"));
                PublicDelayLoadDLLs.Add("edk.dll");
                RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(BinariesPath, "win64", "edk.dll")));
            }
            else if ((Target.Platform == UnrealTargetPlatform.Win32))
            {
                isLibrarySupported = true;

                PublicAdditionalLibraries.Add(Path.Combine(ThirdPartyPath, "EmotivSDK", "Libraries", "win32", "edk.lib"));
                PublicDelayLoadDLLs.Add("edk.dll");
                RuntimeDependencies.Add(new RuntimeDependency(Path.Combine(BinariesPath, "win32", "edk.dll")));
            }

            if (isLibrarySupported)
            {
                PublicIncludePaths.Add(Path.Combine(ThirdPartyPath, "EmotivSDK", "Includes"));
            }

            Definitions.Add(string.Format("WITH_EMOTIVSDK_BINDING={0}", (isLibrarySupported ? 1 : 0)));
        }

        private string ThirdPartyPath
        {
            get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "../../ThirdParty/")); }
        }

        private string BinariesPath
        {
            get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "../../Binaries/")); }
        }
    }
}