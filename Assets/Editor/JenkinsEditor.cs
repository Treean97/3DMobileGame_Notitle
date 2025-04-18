using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

class MyEditorScript
{
    [MenuItem("Build/Perform Android Build")]
    static void PerformAndroidBuild()
    {
        // 1) Android�� Ȱ�� ���� Ÿ������ ��ȯ (���� ���������� ����)
        EditorUserBuildSettings.SwitchActiveBuildTarget(
            BuildTargetGroup.Android, BuildTarget.Android);

        // 2) ������ �� ��� ��������
        string[] scenes = FindEnabledEditorScenes();

        // 3) APK�� ����
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