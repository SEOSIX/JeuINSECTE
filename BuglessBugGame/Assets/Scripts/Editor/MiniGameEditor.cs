using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameEditor : EditorWindow
{
    private const string OutputFolder = "Assets/Prefab/UI/MiniGameUI";

    private bool[,] grid = new bool[14, 8];
    private string prefabName = "GridImage";
    private Color fillColor = Color.white;
    private int cellPixelSize = 32;
    private InsectData insectData;
    private Editor dataEditor;
    private Vector2 scroll;

    [MenuItem("Tools/MiniGame")]
    public static void Open()
    {
        GetWindow<MiniGameEditor>("MiniGameEditor");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Drawing Grid", EditorStyles.boldLabel);
        DrawGrid();

        EditorGUILayout.Space(3);
        EditorGUILayout.LabelField("Génération", EditorStyles.boldLabel);

        prefabName = EditorGUILayout.TextField("Nom du prefab", prefabName);
        fillColor = EditorGUILayout.ColorField("Couleur des cases cochées", fillColor);
        cellPixelSize = Mathf.Max(1, EditorGUILayout.IntField("Taille d'une case (px)", cellPixelSize));
        
        EditorGUILayout.Space(3);
        EditorGUILayout.LabelField("InsectData", EditorStyles.boldLabel);
        insectData = EditorGUILayout.ObjectField("InsectData", insectData, typeof(InsectData), false) as InsectData;
        

        if (GUILayout.Button("Create Prefab"))
        {
            CreatePrefab();
        }
        
        if (EditorGUI.EndChangeCheck())
        {
            if (dataEditor != null) DestroyImmediate(dataEditor);
            dataEditor = insectData != null ? Editor.CreateEditor(insectData) : null;
        }
        
        scroll = EditorGUILayout.BeginScrollView(scroll);
        dataEditor.OnInspectorGUI();
        EditorGUILayout.EndScrollView();
    }

    //grille de bool pour parcours de l'insecte
    private void DrawGrid()
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                grid[x, y] = EditorGUILayout.Toggle(grid[x, y], GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void CreatePrefab()
    {
        if (string.IsNullOrWhiteSpace(prefabName))
        {
            EditorUtility.DisplayDialog("Create Prefab", "Donne un nom au prefab.", "OK");
            return;
        }

        if (!AnyCellChecked())
        {
            EditorUtility.DisplayDialog("Create Prefab", "La grille est vide coche au moins une case", "OK");
            return;
        }

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        EnsureOutputFolder();

        string pngPath = AssetDatabase.GenerateUniqueAssetPath($"{OutputFolder}/{prefabName}.png");
        string prefabPath = AssetDatabase.GenerateUniqueAssetPath(Path.ChangeExtension(pngPath, ".prefab"));

        Sprite sprite = CreateSpriteAsset(pngPath, width, height);
        GameObject instance = BuildHierarchy(sprite, width, height);

        PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
        DestroyImmediate(instance);

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        EditorGUIUtility.PingObject(prefab);
        Selection.activeObject = prefab;
    }

    private bool AnyCellChecked()
    {
        foreach (bool cell in grid)
        {
            if (cell) return true;
        }
        return false;
    }

    private void EnsureOutputFolder()
    {
        if (!AssetDatabase.IsValidFolder(OutputFolder))
        {
            AssetDatabase.AddObjectToAsset(this, "Assets/Prefab/UI/MiniGameUI");
        }
    }
    
    private void OnDisable()
    {
        if (dataEditor != null) DestroyImmediate(dataEditor);
    }
    
    private Sprite CreateSpriteAsset(string path, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        Color32 clear = new Color32(0, 0, 0, 0);
        Color32 filled = fillColor;
        Color32[] pixels = new Color32[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int textureY = height - 1 - y;
                pixels[textureY * width + x] = grid[x, y] ? filled : clear;
            }
        }

        texture.SetPixels32(pixels);
        texture.Apply();

        File.WriteAllBytes(path, texture.EncodeToPNG());
        DestroyImmediate(texture);
        AssetDatabase.ImportAsset(path);

        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.filterMode = FilterMode.Point;
        importer.mipmapEnabled = false;
        importer.alphaIsTransparency = true;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.SaveAndReimport();

        return AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }
    private GameObject BuildHierarchy(Sprite sprite, int width, int height)
    {
        GameObject imageObject = new GameObject(prefabName, typeof(RectTransform), typeof(Image));

        Image image = imageObject.GetComponent<Image>();
        image.sprite = sprite;
        image.raycastTarget = false;

        RectTransform rect = imageObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width * cellPixelSize, height * cellPixelSize);

        GameObject canvasObject = new GameObject(prefabName + "_Canvas",
            typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        rect.SetParent(canvasObject.transform, false);
        rect.anchoredPosition = Vector2.zero;

        return canvasObject;
    }
}