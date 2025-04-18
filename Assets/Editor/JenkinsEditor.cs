using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

class JenkinsEditor
{
    [MenuItem("Build/Perform Android Build")]
    static void PerformAndroidBuild()
    {
        // 1) Android �÷��� ��ȯ
        EditorUserBuildSettings.SwitchActiveBuildTarget(
            BuildTargetGroup.Android,
            BuildTarget.Android);

        // 2) ������ �� ��� Ȯ��
        string[] scenes = FindEnabledEditorScenes();

        // 3) Jenkins WORKSPACE ȯ�溯������ ���̽� ��� ��������
        string workspace = Environment.GetEnvironmentVariable("WORKSPACE");
        string basePath = !string.IsNullOrEmpty(workspace)
            ? workspace
            : Path.GetFullPath(Path.Combine(Application.dataPath, ".."));

        // 4) ���� ��� ��� ����
        string buildDir = Path.Combine(basePath, "Builds", "Android");
        Directory.CreateDirectory(buildDir);
        string buildPath = Path.Combine(buildDir, "MyGame.apk");

        // 5) APK ���� ����
        BuildPipeline.BuildPlayer(
            scenes,
            buildPath,
            BuildTarget.Android,
            BuildOptions.None
        );

        Debug.Log($"[JenkinsEditor] Build completed at: {buildPath}");
    }

    private static string[] FindEnabledEditorScenes()
    {
        var editorScenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
                editorScenes.Add(scene.path);
        }
        return editorScenes.ToArray();
    }
}