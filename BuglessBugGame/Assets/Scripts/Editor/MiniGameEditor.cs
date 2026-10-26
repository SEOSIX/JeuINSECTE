using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameEditor : EditorWindow
{
    private const string OutputFolder = "Assets/Prefab/UI/MiniGameUI";

    private static readonly Vector2Int[] Directions =
    {
        new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1)
    };

    private bool[,] grid = new bool[14, 8];
    private string prefabName = "GridImage";
    private Color fillColor = Color.white;
    private int cellPixelSize = 32;
    private int lineThickness = 4;
    private float roundness = 0.5f;
    private bool dashed = false;
    private int dashLength = 6;
    private int gapLength = 4;
    private int dashPeriod;

    private enum LineAxis { None, Horizontal, Vertical }
    private GameObject childPrefab;
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
        cellPixelSize = Mathf.Max(2, EditorGUILayout.IntField("Taille d'une case (px)", cellPixelSize));
        lineThickness = Mathf.Clamp(EditorGUILayout.IntField("Épaisseur du trait (px)", lineThickness), 1, cellPixelSize);
        roundness = EditorGUILayout.Slider("Arrondi des angles", roundness, 0f, 1f);

        dashed = EditorGUILayout.Toggle("Trait en pointillé", dashed);
        if (dashed)
        {
            EditorGUI.indentLevel++;
            dashLength = Mathf.Max(1, EditorGUILayout.IntField("Longueur du trait (px)", dashLength));
            gapLength = Mathf.Max(1, EditorGUILayout.IntField("Longueur du vide (px)", gapLength));
            EditorGUI.indentLevel--;
        }

        childPrefab = EditorGUILayout.ObjectField("Prefab enfant", childPrefab, typeof(GameObject), false) as GameObject;

        EditorGUILayout.Space(3);
        EditorGUILayout.LabelField("InsectData", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        insectData = EditorGUILayout.ObjectField("InsectData", insectData, typeof(InsectData), false) as InsectData;
        if (EditorGUI.EndChangeCheck())
        {
            if (dataEditor != null) DestroyImmediate(dataEditor);
            dataEditor = insectData != null ? Editor.CreateEditor(insectData) : null;
        }

        if (GUILayout.Button("Create Prefab"))
        {
            CreatePrefab();
        }

        if (dataEditor != null)
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            dataEditor.OnInspectorGUI();
            EditorGUILayout.EndScrollView();
        }
    }

    private void OnDisable()
    {
        if (dataEditor != null) DestroyImmediate(dataEditor);
    }

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
            EditorUtility.DisplayDialog("Create Prefab", "La grille est vide, coche au moins une case.", "OK");
            return;
        }

        EnsureOutputFolder();

        string pngPath = AssetDatabase.GenerateUniqueAssetPath($"{OutputFolder}/{prefabName}.png");
        string prefabPath = Path.ChangeExtension(pngPath, ".prefab");

        Sprite sprite = CreateSprite(pngPath);
        if (sprite == null) return;

        GameObject instance = BuildHierarchy(sprite);

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
        if (AssetDatabase.IsValidFolder(OutputFolder)) return;

        string current = "Assets";
        string[] folders = OutputFolder.Replace("Assets/", "").Split('/');

        foreach (string folder in folders)
        {
            string next = current + "/" + folder;
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(current, folder);
            }
            current = next;
        }
    }

    private Sprite CreateSprite(string path)
    {
        int cols = grid.GetLength(0);
        int rows = grid.GetLength(1);

        int texWidth = cols * cellPixelSize;
        int texHeight = rows * cellPixelSize;

        Color32[] pixels = new Color32[texWidth * texHeight];
        Color32 color = fillColor;

        int radius = Mathf.RoundToInt(roundness * cellPixelSize * 0.5f);
        dashPeriod = dashed ? dashLength + gapLength : 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (grid[x, y]) DrawCell(pixels, texWidth, texHeight, x, y, radius, color);
            }
        }

        Texture2D texture = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
        texture.SetPixels32(pixels);

        File.WriteAllBytes(path, texture.EncodeToPNG());
        DestroyImmediate(texture);

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
        SetupImporter(path);

        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
        if (sprite == null)
        {
            Debug.LogError("MiniGameEditor : impossible de charger le sprite " + path);
        }
        return sprite;
    }

    private bool IsChecked(int x, int y)
    {
        return x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1) && grid[x, y];
    }

    private void DrawCell(Color32[] pixels, int texW, int texH, int x, int y, int radius, Color32 color)
    {
        bool left = IsChecked(x - 1, y);
        bool right = IsChecked(x + 1, y);
        bool up = IsChecked(x, y - 1);
        bool down = IsChecked(x, y + 1);

        int cell = cellPixelSize;
        int t = lineThickness;
        int half = t / 2;

        int leftEdge = x * cell, rightEdge = leftEdge + cell;
        int topEdge = y * cell, bottomEdge = topEdge + cell;
        int cx = leftEdge + cell / 2;
        int cy = topEdge + cell / 2;

        if (radius > 0 && left != right && up != down)
        {
            DrawBend(pixels, texW, texH, cx, cy, right ? 1 : -1, down ? 1 : -1, radius, color,
                leftEdge, rightEdge, topEdge, bottomEdge);
            return;
        }

        if (!left && !right && !up && !down)
        {
            FillRect(pixels, texW, texH, cx - half, cy - half, t, t, color);
            return;
        }
        if (right) FillRect(pixels, texW, texH, cx - half, cy - half, rightEdge - (cx - half), t, color, LineAxis.Horizontal);
        if (left) FillRect(pixels, texW, texH, leftEdge, cy - half, (cx - half + t) - leftEdge, t, color, LineAxis.Horizontal);
        if (down) FillRect(pixels, texW, texH, cx - half, cy - half, t, bottomEdge - (cy - half), color, LineAxis.Vertical);
        if (up) FillRect(pixels, texW, texH, cx - half, topEdge, t, (cy - half + t) - topEdge, color, LineAxis.Vertical);
    }

    private void DrawBend(Color32[] pixels, int texW, int texH, int cx, int cy, int sx, int sy, int radius,
        Color32 color, int leftEdge, int rightEdge, int topEdge, int bottomEdge)
    {
        int t = lineThickness;
        int half = t / 2;

        if (sx > 0) FillRect(pixels, texW, texH, cx + radius - 1, cy - half, rightEdge - (cx + radius) + 1, t, color, LineAxis.Horizontal);
        else FillRect(pixels, texW, texH, leftEdge, cy - half, (cx - radius) - leftEdge + 1, t, color, LineAxis.Horizontal);

        if (sy > 0) FillRect(pixels, texW, texH, cx - half, cy + radius - 1, t, bottomEdge - (cy + radius) + 1, color, LineAxis.Vertical);
        else FillRect(pixels, texW, texH, cx - half, topEdge, t, (cy - radius) - topEdge + 1, color, LineAxis.Vertical);

        float halfT = t * 0.5f;
        float centerX = cx - half + halfT;
        float centerY = cy - half + halfT;
        float arcX = centerX + sx * radius;
        float arcY = centerY + sy * radius;

        int minX = Mathf.Max(0, Mathf.FloorToInt(Mathf.Min(centerX, arcX) - halfT - 1));
        int maxX = Mathf.Min(texW - 1, Mathf.CeilToInt(Mathf.Max(centerX, arcX) + halfT + 1));
        int minY = Mathf.Max(0, Mathf.FloorToInt(Mathf.Min(centerY, arcY) - halfT - 1));
        int maxY = Mathf.Min(texH - 1, Mathf.CeilToInt(Mathf.Max(centerY, arcY) + halfT + 1));

        for (int py = minY; py <= maxY; py++)
        {
            float dy = py + 0.5f - arcY;
            if (dy * sy > 0) continue;

            for (int px = minX; px <= maxX; px++)
            {
                float dx = px + 0.5f - arcX;
                if (dx * sx > 0) continue;

                float distance = Mathf.Sqrt(dx * dx + dy * dy);
                float coverage = Mathf.Clamp01(halfT + 0.5f - Mathf.Abs(distance - radius));
                if (coverage <= 0f) continue;

                if (dashPeriod > 0)
                {
                    float phi = Mathf.Atan2(Mathf.Abs(dx), -dy * sy);
                    float phase = phi < Mathf.PI * 0.25f
                        ? arcX - sx * radius * phi
                        : arcY - sy * radius * (Mathf.PI * 0.5f - phi);
                    if (!IsDashOn(phase)) continue;
                }

                byte alpha = (byte)(color.a * coverage);
                int index = (texH - 1 - py) * texW + px;
                if (alpha > pixels[index].a)
                    pixels[index] = new Color32(color.r, color.g, color.b, alpha);
            }
        }
    }

    private bool IsDashOn(float position)
    {
        return Mathf.Repeat(position, dashPeriod) < dashLength;
    }

    private void FillRect(Color32[] pixels, int texWidth, int texHeight, int startX, int startY, int w, int h,
        Color32 color, LineAxis axis = LineAxis.None)
    {
        bool dashHorizontal = dashPeriod > 0 && axis == LineAxis.Horizontal;
        bool dashVertical = dashPeriod > 0 && axis == LineAxis.Vertical;

        for (int py = startY; py < startY + h; py++)
        {
            if (py < 0 || py >= texHeight) continue;
            if (dashVertical && !IsDashOn(py + 0.5f)) continue;

            int textureY = texHeight - 1 - py;

            for (int px = startX; px < startX + w; px++)
            {
                if (px < 0 || px >= texWidth) continue;
                if (dashHorizontal && !IsDashOn(px + 0.5f)) continue;

                pixels[textureY * texWidth + px] = color;
            }
        }
    }

    private void SetupImporter(string path)
    {
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.filterMode = FilterMode.Bilinear;
        importer.mipmapEnabled = false;
        importer.alphaIsTransparency = true;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.SaveAndReimport();
    }

    private void AddChildPrefab(Transform parent)
    {
        if (childPrefab == null) return;

        GameObject child = (GameObject)PrefabUtility.InstantiatePrefab(childPrefab, parent);

        RectTransform childRect = child.transform as RectTransform;
        if (childRect != null) childRect.anchoredPosition = Vector2.zero;
        else child.transform.localPosition = Vector3.zero;
    }

    #region Insect icon & AI

    private RectTransform CreateInsectIcon(Transform parent)
    {
        GameObject iconObject = new GameObject("InsectIcon", typeof(RectTransform), typeof(Image));
        iconObject.transform.SetParent(parent, false);

        RectTransform rect = iconObject.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = new Vector2(100f, 100f);

        Image icon = iconObject.GetComponent<Image>();
        icon.sprite = insectData != null ? insectData.insectIcon : null;
        icon.preserveAspect = true;
        icon.raycastTarget = false;

        return rect;
    }

    private void AddMiniGameAI(GameObject parent, RectTransform insectIcon)
    {
        if (insectData == null)
        {
            Debug.LogWarning("MiniGameEditor : aucun InsectData assigné, aucun script d'IA ajouté.");
            return;
        }

        switch (insectData.typeMiniGame)
        {
            case InsectData.TypeMiniGame.Hider:
                HideInsectAI hideAi = parent.AddComponent<HideInsectAI>();
                AssignInsectTransform(hideAi, insectIcon);
                break;
            case InsectData.TypeMiniGame.Line:
                LineInsectAI lineAi = parent.AddComponent<LineInsectAI>();
                lineAi.SetPath(BuildPath());
                AssignInsectTransform(lineAi, insectIcon);
                break;
            default:
                Debug.LogWarning("MiniGameEditor : pas de script d'IA prévu pour le type " + insectData.typeMiniGame);
                break;
        }
    }

    private void AssignInsectTransform(InsectBase ai, RectTransform insectIcon)
    {
        SerializedObject so = new SerializedObject(ai);
        so.FindProperty("insectTransform").objectReferenceValue = insectIcon;
        so.ApplyModifiedPropertiesWithoutUndo();
    }

    #endregion

    #region MiniGameUI

    // Ajoute MiniGameUI sur la racine (même GameObject que l'IA) et remplit ce qui peut l'être
    private void AddMiniGameUI(GameObject parent, RectTransform drawingArea, RectTransform insectIcon)
    {
        MiniGameUI ui = parent.AddComponent<MiniGameUI>();
        SerializedObject so = new SerializedObject(ui);

        SetReference(so, "insectIconRect", insectIcon);
        SetReference(so, "drawingArea", drawingArea);

        if (insectData != null)
        {
            SetEnum(so, "miniGameType", insectData.typeMiniGame.ToString());
            SetEnum(so, "catchType", insectData.typeCatch.ToString());
        }

        so.ApplyModifiedPropertiesWithoutUndo();
    }

    private void SetReference(SerializedObject so, string fieldName, Object value)
    {
        SerializedProperty property = so.FindProperty(fieldName);
        if (property == null)
        {
            Debug.LogWarning("MiniGameEditor : champ introuvable sur MiniGameUI : " + fieldName);
            return;
        }
        property.objectReferenceValue = value;
    }

    private void SetEnum(SerializedObject so, string fieldName, string valueName)
    {
        SerializedProperty property = so.FindProperty(fieldName);
        if (property == null)
        {
            Debug.LogWarning("MiniGameEditor : champ introuvable sur MiniGameUI : " + fieldName);
            return;
        }

        int index = System.Array.IndexOf(property.enumNames, valueName);
        if (index >= 0) property.enumValueIndex = index;
    }

    #endregion

    #region Path (LineInsectAI)

    private List<Vector2> BuildPath()
    {
        List<Vector2> path = new List<Vector2>();

        Vector2Int start = FindStartCell();
        if (start.x < 0) return path;
        if (IsClosedLoop())
        {
            WalkLoop(start, path);
            return path;
        }
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        VisitCell(start.x, start.y, visited, path);
        return path;
    }

    private bool IsClosedLoop()
    {
        bool anyChecked = false;

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (!grid[x, y]) continue;
                anyChecked = true;
                if (CountNeighbours(x, y) != 2) return false;
            }
        }
        return anyChecked;
    }
    private void WalkLoop(Vector2Int start, List<Vector2> path)
    {
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        Vector2Int current = start;

        while (true)
        {
            visited[current.x, current.y] = true;
            path.Add(CellToNormalized(current.x, current.y));

            bool moved = false;
            foreach (Vector2Int d in Directions)
            {
                int nx = current.x + d.x, ny = current.y + d.y;
                if (IsChecked(nx, ny) && !visited[nx, ny])
                {
                    current = new Vector2Int(nx, ny);
                    moved = true;
                    break;
                }
            }
            if (!moved) break;
        }
    }

    private Vector2Int FindStartCell()
    {
        Vector2Int first = new Vector2Int(-1, -1);

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (!grid[x, y]) continue;
                if (first.x < 0) first = new Vector2Int(x, y);
                if (CountNeighbours(x, y) == 1) return new Vector2Int(x, y);
            }
        }
        return first;
    }

    private int CountNeighbours(int x, int y)
    {
        int count = 0;
        foreach (Vector2Int d in Directions)
        {
            if (IsChecked(x + d.x, y + d.y)) count++;
        }
        return count;
    }

    private void VisitCell(int x, int y, bool[,] visited, List<Vector2> path)
    {
        visited[x, y] = true;
        path.Add(CellToNormalized(x, y));

        foreach (Vector2Int d in Directions)
        {
            int nx = x + d.x, ny = y + d.y;
            if (IsChecked(nx, ny) && !visited[nx, ny])
            {
                VisitCell(nx, ny, visited, path);
                path.Add(CellToNormalized(x, y));
            }
        }
    }

    private Vector2 CellToNormalized(int x, int y)
    {
        return new Vector2((x + 0.5f) / grid.GetLength(0), (y + 0.5f) / grid.GetLength(1));
    }

    #endregion

    private GameObject BuildHierarchy(Sprite sprite)
    {
        GameObject rootObject = new GameObject(prefabName + "_Root", typeof(RectTransform));

        RectTransform rootRect = rootObject.GetComponent<RectTransform>();
        rootRect.anchorMin = Vector2.zero;
        rootRect.anchorMax = Vector2.one;
        rootRect.offsetMin = Vector2.zero;
        rootRect.offsetMax = Vector2.zero;

        GameObject imageObject = new GameObject(prefabName, typeof(RectTransform), typeof(Image));
        imageObject.transform.SetParent(rootObject.transform, false);

        RectTransform rect = imageObject.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Image image = imageObject.GetComponent<Image>();
        image.sprite = sprite;
        image.raycastTarget = false;

        RectTransform insectIcon = CreateInsectIcon(rootObject.transform);

        AddChildPrefab(rootObject.transform);
        AddMiniGameAI(rootObject, insectIcon);
        AddMiniGameUI(rootObject, rect, insectIcon);

        return rootObject;
    }
}