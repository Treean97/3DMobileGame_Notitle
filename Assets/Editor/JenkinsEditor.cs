using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

class MyEditorScript
{
    [MenuItem("Build/Perform Android Build")]
    static void PerformAndroidBuild()
    {
        // 1) Android를 활성 빌드 타겟으로 전환 (선택 사항이지만 권장)
        EditorUserBuildSettings.SwitchActiveBuildTarget(
            BuildTargetGroup.Android, BuildTarget.Android);

        // 2) 빌드할 씬 목록 가져오기
        string[] scenes = FindEnabledEditorScenes();

        // 3) APK로 빌드
        BuildPipeline.BuildPlayer(
            scenes,
            "Builds/Android/MyGame.apk",
            BuildTarget.Android,
            BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        var editorScenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
            if (scene.enabled)
                editorScenes.Add(scene.path);
        return editorScenes.ToArray();
    }
}