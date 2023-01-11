using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleScriptableObject))]
public class ObstacleTool : Editor
{

    private ObstacleScriptableObject obstacleSO;
    private bool[,] toggles;

    private void OnEnable()
    {
        obstacleSO = (ObstacleScriptableObject)target;
        toggles = new bool[10, 10];
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Check toggle to place obstacle on grid");
        for (int j = 9; j >= 0; j--)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < 10; i++)
            {
                toggles[i, j] = EditorGUILayout.Toggle(obstacleSO.IsObstacle(i, j));
                if (toggles[i, j] != obstacleSO.IsObstacle(i, j))
                {
                    obstacleSO.SetObstacleAt(i, j, toggles[i, j]);
                }
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.LabelField("(0,0)");

    }
}
