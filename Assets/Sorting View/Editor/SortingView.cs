//**********************************************
//            Sorting View
// Copyright © 2014 - Free Compass Studio
// Email: freecompassstudio@gmail.com
//**********************************************

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


public class SortingView : EditorWindow
{
    #region Main Data

    /// <summary>
    /// The class contains all information for renderers in sorting view.
    /// </summary>
    class SortingRendererInfo
    {
        public Renderer renderer = null;
        public int order = 0;
        public int defaultOrder = 0;
        public Rect leftEventRect = new Rect();
        public Rect rightEventRect = new Rect();
        public int controlID = 0;
    }

    /// <summary>
    /// The class contains all information for sorting layers in sorting view.
    /// </summary>
    class SortingLayerInfo
    {
        public string name = string.Empty;
        public List<SortingRendererInfo> rendererList = new List<SortingRendererInfo>();
        public Rect dropEventRect = new Rect();
        public int controlID = 0;
    }

    // Sorting layer information list.
    List<SortingLayerInfo> _layerList;

    #endregion

    #region Texture Data

    // The class contains all custom GUI textures encoded on text.
    static class TextureResources
    {
        static string warnIconEncode = "iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAADRUlEQVQ4EYWU30sUURTHv3d21jUtisJ8tF8UWWjpRm7lbmas9AOzWlqj3VFCksLI16A/oXqIlAgjdzeIIjDwoR9uSYWUpvmDelEy6KXC0jT8sTs7M50zubIu7nbgzMy955zPPfecuVfUnb+MP3PZ0A0BSRjQdAk6qc2qQjMAixSDzaK3CsnAdHRZraZZIKBDCJC/RnGSGWeVVdgkFTL+L6EG1zVfzFiO6y8vWcjdny5ESmckW/Ciu9VXXLERu8uzcM4Z8PFcuph0wECj+6rfUWpBbM0QtJx3KN87jAsH73CGgVTQVMDAGddTxbF9EsbcYaoYoIlcIGLHgaIRNBx6pKSCLgUMeveElSP2LmACMHSqPgt5GnoGMA647B9w8fADhgZNW8IjGRjyOsL+U84wECWvGGn0i+kuYpSn+o2oNCRoadEg6ise8/ZDpsP8I7HLwWpHh6+q9BUwS9YIqRUQcyOmq9B+Q6hf6T+ioUZK0LKdfZAlzdf0xMPLcMa8EVMC3pIX/qp9CTDeKVlFZBSCkoP6i5RIHME2hk5ypgNorLzPmQZIzf+w5ayzXXGXdAMzNMNb5QAWziaqkv4EdAJyCShrU+JQMpUUfkIsJiut4SOqxCckO4v2mEluvGqy0JyIDhN07F/9ku28KO1A0vlBGc5GM+uannmsEdWqHNjdZ3bWBMezpOqI6Y8UNLUYxVXjDqwCXry1o6WzMkijOjlGZ5Ok5lbHCSFbNL9z14BZ8AUo13F2iFwoVdOVPhNgL7vtuBU+HmIGKSwFxfsh0cEnbesd3bopd9l4Qd76H9QAsnIzuNMT7yGmegEbh5AwmDPrKQYlco9GCk+zWPJ3lNOCIq5tPZ+3bczNHC/M2/B9/j8kbs5RGNmbIWaolgxbDbzpL0Tzc0+Ibh2Fb564yoxKEqU5fNIwDKG4ivqp0DmIbmmnCOpb71pgxRi6h/LR/OxEkK42c5uJ8TLfZ0tITVPYg0hEV9x7B2GbeAhDI+LKMfQMrsONp96gJP7VLDk2FZD9am6/rhaGkeGvcHghaN2ugW242XF6oQHJMB7LdHSWmo/PKXe7joks24xPljWGLWpA3CnxLWrrrySOU313Uk3ZVpbKIT7/Fy2PJ3XZ1ja/AAAAAElFTkSuQmCC";
        public static Texture2D warnIcon;

        static string contentBackGroundEncode0 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAASCAYAAABB7B6eAAABvUlEQVQ4EaVUO47CMBB1AnQgRAMIhdAtW7jb5RK0SEicAiHOAYGDUME1qKiyDRIgFK+2SUHDd+dZa2QiJyxgyZqJ38x7M7ZjazabsUwmw9LpNEulUsy2bWkZDay/0SDrA9fH8Xhkh8Oh/kWDrIROpxM7n88M9g9ntp5k8Plut/ORtN/vbybWgFEON+Rdl5IEeKVSWWSzWVkVCKMTGGKSROIEuOu6kvxyubCkCRHExomYBLjjOItcLndt856DWOSYRKICvFqtSvLodtz7hghyoyK6AK/Vaot8Pn+v4FgcueDQRZSArBz7iSv2ygSH3gkEZOWFQiG2skcBcKlO7Ol0+kMVh69UHZMbgtumPy4YDofvQogw6To+goELnOC2Op2O2oFSv9/3y+Xy86dMTEEQhIPBoE6uALE6ZPgCwHa7fboT5OrkILXa7TasPkrdbtenJ+ChTkA+Ho+vlStCnIF8+TQrO1mv15v/7jtiUTlxCI1H8upbpERhxWg0+lwul5toQvQbMYhFjk6gfKvZbCrfZIu9Xm9OP45jAlG553kfhH2bcKxZjUYjDlPrxVarNacX80ZktVptJpNJIjkIfgHTKSaOq5HP1wAAAABJRU5ErkJggg==";
        public static Texture2D contentBackGround0;

        static string contentBackGroundEncode1 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAASCAYAAABB7B6eAAAB70lEQVQ4EZWVO2sCQRDH9x5eIYqxsfCtKJciVsmXCQQD+Qhek/gFUvkEm4iW6az8Bn4AQS+oCYriOxKUK6x8ZUZiiHpr3IFlvd2Z/292bvbkptPps9FofOQ4jrDYcrkki8VC/gCDeRu6Wq3Ier0mOP/sE3EwGDzBruRwOCIGg4GFQebz+TsEhGC80QJ5pMNQOp1OHH9jBucOk8lE7Ha7CuJXVMCfjS0Ej7bZbM4eCHG73VQIf5Ct0m634whhMbPZTJxOpy6ER7GDoTSbTeZyIcTlch1BeEqmSqvVYj6JxWIhHo9nD8KVSiUKY7scCwaDEVEUT/kc7c1mMwKlDkHTvIk2m43wPE9QBO/CbhYEAZ+VRqNBECJJ0pEQbcFqtWInqlDq0H+pCQC/wEbAy8Ni4K8Vi8WvUwABsn/xer0PeCps3XNtMplo6XT6EvzHNIBQq9VyPp/vHsVZsh+Px1osFpNB/BMT0gMI9Xo9D+Jh/HSwZD4ajbREIvErrgcQKpVKPhAIhPEls1y44XCopVKpPfFDgKSqahbFsWNYMu+DJZPJGxDclgWFd7YrkVQul7OyLIex5iyZ93q9fiaT0RVHCAKkarWag16/w5pjS55rKA41vwb/CS2Gi0ajr36//xb+dGg+uuvdbrdfKBROimPgN8aIXy3HR0aeAAAAAElFTkSuQmCC";
        public static Texture2D contentBackGround1;

        static string layerBackGroundEncode = "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";
        public static Texture2D layerBackGround;

        static string layerBackGroundLightEncode = "iVBORw0KGgoAAAANSUhEUgAAAEAAAAAgCAYAAACinX6EAAACvUlEQVRoBeWZS2/TQBRGCSJCPKRSgdiUh4QKS55d0PIH2PD4q9ANa5BYAFtYAEIqjxVqoRHPQlvOKVxpZMWx40xoUl/p1EljT+b7fOfOxNPZ2t7eN4bo0OZ+6MJROAFzcBZOwXE4BAfA2IRvsArvYAXe/3v/leMv2ILsnY0O0Ha2CPEHaXEGzsB1uA2aUid+c9J9eAKa0YOfkN2ETuYMKIqfp9M3YQmaxGMuegCvYR2ym5DTgFT8MTqr+FuwAKPEMy5ehleQ3YRcQ6Ao/jydNeWvwqihgbavCS8hqwk5DEjFz9LBEH+F17niGg35PfcgayaMakBR/AU66J2/DLkjssnimC0TRjEgFe+Y987fhYswrshuQt1pqSioKN47fwfGKT76oAkWVw13mnW6VYd9GjqaGFAUH2N+HGlfJsiaoOEaP5IJwxrQT7wdyVnwaK5WZMmEYWpAKn5c1b6W8uQkM8FoPEXWNaAoPsb8pb/fv6t/Y4psNDvUGQKp+Kj2TnWTID6cdzjYp6FrQpUBZeL/Z8ELkVXHRiYMMqBM/G4UvCrx8fnQhbGsBqTiJ6XghciqYxRGa0LlsrmfAf3EO9VNYtqXmVHbhKIBe0F8mBImLPOP0t8OaQ1IxUe1n7Y7H+LjqAkDZ4cwoCje6cQLpyntQ3TxOHB2cAik4qet4BXFlr3XBCMK42de7zxe0wCzwF9Uip+kFR7dyRqa4M3WBGvCjgmK78IMzIM/MydphUd3soZrGDWqVc1dM+AInAaf3nrCXg8z4Qd8hw0N0AmF++y+LbGE0Lew6hBwyrsBbYtFBM9qQAyBthngNt1hDXBfzq2otsUGgjc1oAfuwbUtniK4pwFr8BBeQFviOUIfwZp7g25fuwiag3Pg2DgJ1gbXCC4eBkXZlnXVdWVtpu01bSNt2x1lh/gX+Agr8AY+wKc/nTublQrpb+QAAAAASUVORK5CYII=";
        public static Texture2D layerBackGroundLight;

        static string verticalMoveIconEncode = "iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAACDElEQVQ4EaWUO2hiQRSGz71eHyAa8QGWaUy1RSDpQrBOEdhgIF22stpmCWyXzq0WlnQp0gVSpAgL26VTDC5C8IFgZWEhGBdF8dGsD9z/DDsyGR8xyYFzz8w/53zMnTn3GtPplNawM+Tsl8vlI8uySNYYhkEul4uGwyGFw2GBMdeAxZDzA/4RfvFS/ktAht0xZDweUzAY/IJhgufLbBVwBuNiflW/309er/cc06+vBT6DyWK73U6BQICh36HFpa7GRTtcCJNFDodDQD0ezxW0E6nLqAM/YUGcmUxYFJ1OJ58nud3uW6wfqjmGbAGIp/DrarVKlUqFTNMkbotlkSG8W26ZUCgUR9v8gtS0FGA1l8tFAWvabDYXdAsJS53XYdPJZBJpNBom4qYA4iHtQQ7eE9VXfjOnXq+Tz+cT9fqlvBkqC/mMpB1j8AFegtuluCLyT2AC34an4AV4WwX+gcBtYIOvZb1ej7LZbA8XcoOCNhcZrVZLLd7ADT/iPCKqqI8HgwGl02kqFApltM4O1v/KHP0MuwBu9fv9tEzQI9YolUpRPp/PoXX4iGYwztWBopnR0FHs4qfSo4Lb7XYpmUxSsVj8DWFXiNpjDsiQ/x4D9FJCO52O2FmpVEqCsadxZlP1UoTIn5Nin/FpPY1Go0QmkyHA7rF2oKzPDeeAtVpNT/oGwQk9ih/CHIwvSLV/WOW9jdg1oM0AAAAASUVORK5CYII=";
        public static Texture2D verticalMoveIcon;

        // Initialize all custom GUI textures.
        static TextureResources()
        {
            warnIcon = new Texture2D(20, 20, TextureFormat.ARGB32, false);
            warnIcon.name = "SortingView_WarnIcon";
            warnIcon.hideFlags = HideFlags.HideAndDontSave;
            warnIcon.LoadImage(Convert.FromBase64String(warnIconEncode));

            contentBackGround0 = new Texture2D(24, 18, TextureFormat.ARGB32, false);
            contentBackGround0.name = "SortingView_ContentBackGround0";
            contentBackGround0.hideFlags = HideFlags.HideAndDontSave;
            contentBackGround0.LoadImage(Convert.FromBase64String(contentBackGroundEncode0));

            contentBackGround1 = new Texture2D(24, 18, TextureFormat.ARGB32, false);
            contentBackGround1.name = "SortingView_ContentBackGround1";
            contentBackGround1.hideFlags = HideFlags.HideAndDontSave;
            contentBackGround1.LoadImage(Convert.FromBase64String(contentBackGroundEncode1));

            layerBackGround = new Texture2D(64, 32, TextureFormat.ARGB32, false);
            layerBackGround.name = "SortingView_LayerBackGround";
            layerBackGround.hideFlags = HideFlags.HideAndDontSave;
            layerBackGround.LoadImage(Convert.FromBase64String(layerBackGroundEncode));

            layerBackGroundLight = new Texture2D(64, 32, TextureFormat.ARGB32, false);
            layerBackGroundLight.name = "SortingView_LayerBackGroundLight";
            layerBackGroundLight.hideFlags = HideFlags.HideAndDontSave;
            layerBackGroundLight.LoadImage(Convert.FromBase64String(layerBackGroundLightEncode));

            verticalMoveIcon = new Texture2D(20, 20, TextureFormat.ARGB32, false);
            verticalMoveIcon.name = "SortingView_VerticalMoveIcon";
            verticalMoveIcon.hideFlags = HideFlags.HideAndDontSave;
            verticalMoveIcon.LoadImage(Convert.FromBase64String(verticalMoveIconEncode));
        }
    }

    #endregion

    #region GUI Data

    // Custom GUI styles.
    GUIStyle style_layertitle;
    GUIStyle style_layertitlelight;
    GUIStyle style_contentbg0;
    GUIStyle style_contentbg1;
    GUIStyle style_contentlabel;
    GUIStyle style_contenttoggle;

    bool _isFocus;

    [SerializeField]
    int _stride = 1;
    [SerializeField]
    bool _fullPath = false;
    [SerializeField]
    Vector2 _scrollPosition = Vector2.zero;

    Dictionary<string, bool> _showSortingLayerContent;
    [SerializeField]
    string[] _showSortingLayerContentName = new string[0];
    [SerializeField]
    bool[] _showSortingLayerContentBoolean = new bool[0];

    HashSet<Renderer> _selectRenderer;
    [SerializeField]
    Renderer[] _selectRendererArray = new Renderer[0];

    [SerializeField]
    Renderer _focusSelectRenderer = null;
    [SerializeField]
    Renderer _editSelectRenderer = null;

    bool _isDragRight;
    bool _isDragLeft;
    enum FocusState
    {
        None,
        Left,
        Right
    };
    FocusState _focusState;
    Vector2 _originMousePos;
    float _originShiftPos;
    int _originOrder;

    float _guiTime;
    enum KeyBoardDamp
    {
        None,
        Damp,
        Continuous
    };
    KeyBoardDamp _keyDampState;

    GUIContent _warnContent;

    #endregion

    #region Main Process

    /// <summary>
    /// Get existing open window or if none, make a new one.
    /// </summary>
    [MenuItem("Window/Sorting View")]
    static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(SortingView), false, "Sorting View", true);
        window.minSize = new Vector2(250, 200);
    }

    /// <summary>
    /// Serialize process when entering in play mode.
    /// </summary>
    void SerializeProcess()
    {
        if (!EditorApplication.isPaused &&
            !EditorApplication.isPlaying &&
            EditorApplication.isPlayingOrWillChangePlaymode)
        {
            _showSortingLayerContentName = new string[_showSortingLayerContent.Count];
            _showSortingLayerContentBoolean = new bool[_showSortingLayerContent.Count];
            int index = 0;
            foreach (KeyValuePair<string, bool> value in _showSortingLayerContent)
            {
                _showSortingLayerContentName[index] = value.Key;
                _showSortingLayerContentBoolean[index] = value.Value;
                index++;
            }

            _selectRendererArray = new Renderer[_selectRenderer.Count];
            index = 0;
            foreach (Renderer value in _selectRenderer)
            {
                _selectRendererArray[index] = value;
                index++;
            }
        }
    }

    void OnEnable()
    {
        EditorApplication.playmodeStateChanged += SerializeProcess;

        // Initialize sort data.
        _layerList = new List<SortingLayerInfo>();

        // Initialize GUI data.
        
        _isFocus = false;

        if (_showSortingLayerContent == null)
        {
            _showSortingLayerContent = new Dictionary<string, bool>();
            if (_showSortingLayerContentName.Length > 0)
            {
                for (int index = 0; index < _showSortingLayerContentName.Length; index++)
                    _showSortingLayerContent[_showSortingLayerContentName[index]] = _showSortingLayerContentBoolean[index];
            }
        }

        if (_selectRenderer == null)
        {
            _selectRenderer = new HashSet<Renderer>();
            if (_selectRendererArray.Length > 0)
            {
                for (int index = 0; index < _selectRendererArray.Length; index++)
                    _selectRenderer.Add(_selectRendererArray[index]);
            }
        }

        _isDragRight = false;
        _isDragLeft = false;
        _focusState = FocusState.None;
        _originMousePos = Vector2.zero;
        _originShiftPos = 0;
        _originOrder = 0;

        _guiTime = 0;
        _keyDampState = KeyBoardDamp.None;

        _warnContent = new GUIContent(TextureResources.warnIcon, "Unknowable order in same value.");
    }

    void OnFocus()
    {
        OnSelectionChange();
        
        _isFocus = true;
    }

    void OnLostFocus()
    {
        _editSelectRenderer = null;
        _focusState = FocusState.None;

        _isFocus = false;
    }

    /// <summary>
    /// Scan all the scene hierarchy for sortable renderers.
    /// </summary>
    void OnInspectorUpdate()
    {
        _layerList.Clear();

        // Get the sequence of all sorting layers.
        System.Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        string[] sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);

        // Initialize sorting layer info.
        foreach (string name in sortingLayerNames)
        {
            SortingLayerInfo info = new SortingLayerInfo();
            info.name = name;
            _layerList.Add(info);
        }

        // Find all sortable renderers, then sort them.
        Renderer[] rendererArray = UnityEngine.Object.FindObjectsOfType<Renderer>();

        int defaultOrder = 0;
        foreach (Renderer renderer in rendererArray)
        {
            Material mat = renderer.sharedMaterial;
            if (mat != null && mat.renderQueue == 3000)
            {
                string sortingLayerName = renderer.sortingLayerName;
                if (sortingLayerName == string.Empty)
                    sortingLayerName = "Default";
                foreach (SortingLayerInfo layerInfo in _layerList)
                {
                    if (layerInfo.name == sortingLayerName)
                    {
                        int index = 0;
                        foreach (SortingRendererInfo sortedRendererInfo in layerInfo.rendererList)
                        {
                            if (renderer.sortingOrder <= sortedRendererInfo.order)
                                break;
                            index++;
                        }

                        SortingRendererInfo rendererInfo = new SortingRendererInfo();
                        rendererInfo.renderer = renderer;
                        rendererInfo.order = renderer.sortingOrder;
                        rendererInfo.defaultOrder = defaultOrder++;
                        if (index == layerInfo.rendererList.Count)
                            layerInfo.rendererList.Add(rendererInfo);
                        else
                            layerInfo.rendererList.Insert(index, rendererInfo);
                    }
                }
            }
        }

        Repaint();
    }

    /// <summary>
    /// Draw GUI and process GUI event.
    /// </summary>
    void OnGUI()
    {
        InitGUIStyle();
        DrawToolbar();
        DrawContent();

        ClickProcess();
        DragProcess();
        KeyBoardProcess();
    }

    #endregion

    #region Draw Process

    /// <summary>
    /// Initialize custom GUI sytles in sorting view window.
    /// </summary>
    void InitGUIStyle()
    {

        // layer title
        style_layertitle = new GUIStyle("box");
        style_layertitle.fixedHeight = 32;
        style_layertitle.border = new RectOffset(30, 30, 5, 5);
        style_layertitle.margin = new RectOffset(4, 4, 1, 1);
        style_layertitle.padding = new RectOffset(0, 0, 3, 3);
        style_layertitle.overflow = new RectOffset(0, 0, -1, -1);
        style_layertitle.richText = true;
        style_layertitle.alignment = TextAnchor.MiddleCenter;
        style_layertitle.normal.background = TextureResources.layerBackGround;

        // layer title light
        style_layertitlelight = new GUIStyle("box");
        style_layertitlelight.border = new RectOffset(30, 30, 5, 5);
        style_layertitlelight.margin = new RectOffset(4, 4, 1, 1);
        style_layertitlelight.padding = new RectOffset(0, 0, 3, 3);
        style_layertitlelight.overflow = new RectOffset(0, 0, -1, -1);
        style_layertitlelight.normal.background = TextureResources.layerBackGroundLight;

        // content background 0
        style_contentbg0 = new GUIStyle();
        style_contentbg0.stretchWidth = true;
        style_contentbg0.fixedHeight = 18;
        style_contentbg0.border = new RectOffset(10, 10, 2, 2);
        style_contentbg0.margin = new RectOffset(4, 4, 1, 1);
        style_contentbg0.padding = new RectOffset(0, 0, 3, 3);
        style_contentbg0.overflow = new RectOffset(6, 6, 0, 0);
        style_contentbg0.normal.background = TextureResources.contentBackGround0;

        // content background 1
        style_contentbg1 = new GUIStyle();
        style_contentbg1.stretchWidth = true;
        style_contentbg1.fixedHeight = 18;
        style_contentbg1.border = new RectOffset(10, 10, 2, 2);
        style_contentbg1.margin = new RectOffset(4, 4, 1, 1);
        style_contentbg1.padding = new RectOffset(0, 0, 3, 3);
        style_contentbg1.overflow = new RectOffset(6, 6, 0, 0);
        style_contentbg1.normal.background = TextureResources.contentBackGround1;


        // content label
        style_contentlabel = new GUIStyle("label");
        if (EditorGUIUtility.isProSkin)
            style_contentlabel.normal.textColor = new Color(0.9f, 0.9f, 0.9f);

        // content toggle
        style_contenttoggle = new GUIStyle("toggle");
        if (EditorGUIUtility.isProSkin)
        {
            style_contenttoggle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
            style_contenttoggle.active.textColor = new Color(0.9f, 0.9f, 0.9f);
            style_contenttoggle.onNormal.textColor = new Color(0.9f, 0.9f, 0.9f);
            style_contenttoggle.onActive.textColor = new Color(0.9f, 0.9f, 0.9f);
        }

    }

    /// <summary>
    /// Draw toolbar on the top of window.
    /// </summary>
    void DrawToolbar()
    {
        GUILayout.Space(2);
        Rect rect = GUILayoutUtility.GetRect(200f, 24f, GUILayout.ExpandWidth(true));

        GUI.Box(rect, "", "TE NodeBox");

        GUI.Label(new Rect(rect.x + 4f, rect.y + 4f, 48f, 16f), "Stride");
        int[] strideOptionValues = {1, 10, 100, 1000};
        string[] strideOptionDisplay = {"One", "Ten", "Hundred", "Thousand"};
        _stride = EditorGUI.IntPopup(new Rect(rect.x + 42f, rect.y + 5f, 72f, 18f), _stride, strideOptionDisplay, strideOptionValues);

        _fullPath = GUI.Toggle(new Rect(rect.x + 135f, rect.y + 5f, 72f, 18f), _fullPath, "Full Path");
    }

    /// <summary>
    /// Draw two-level hierarchy.
    /// </summary>
    void DrawContent()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        foreach (SortingLayerInfo layerInfo in _layerList)
        {
            bool toggle = false;
            _showSortingLayerContent.TryGetValue(layerInfo.name, out toggle);

            toggle = DrawLayerTitle(toggle, layerInfo);
            _showSortingLayerContent[layerInfo.name] = toggle;

            if (toggle && layerInfo.rendererList.Count > 0)
            {
                for (int index = 0; index < layerInfo.rendererList.Count; index++)
                {
                    SortingRendererInfo rendererInfo = layerInfo.rendererList[index];
                    if (rendererInfo.renderer == null)
                        continue;

                    if (rendererInfo.controlID == 0)
                    {
                        rendererInfo.controlID = GUIUtility.GetControlID(rendererInfo.renderer.GetInstanceID(), FocusType.Passive);
                    }

                    Rect rect = GUILayoutUtility.GetRect(50, 22, GUILayout.ExpandWidth(true));
                    rect.x += 8;
                    rect.width -= 16;

                    bool warn = false;
                    if (rendererInfo.order == layerInfo.rendererList[(index + 1) % layerInfo.rendererList.Count].order ||
                        rendererInfo.order == layerInfo.rendererList[(index + layerInfo.rendererList.Count - 1) % layerInfo.rendererList.Count].order)
                        warn = true;

                    DrawRendererContent(rect,
                                        rendererInfo,
                                        _selectRenderer.Contains(rendererInfo.renderer),
                                        _focusSelectRenderer == rendererInfo.renderer,
                                        warn
                                        );

                }
            }
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// Draw sorting layer title GUI.
    /// </summary>
    /// <param name="toggle">Show renderers or not at last frame.</param>
    /// <param name="info">Sorting layer infomation.</param>
    /// <returns>Show renderers or not at this frame.</returns>
    bool DrawLayerTitle(bool toggle, SortingLayerInfo info)
    {
        Rect rect = GUILayoutUtility.GetRect(50f, 32f, GUILayout.ExpandWidth(true));

        string text = info.name;
        Color bgColor = GUI.backgroundColor;

        foreach (Renderer renderer in _selectRenderer)
        {
            if (renderer != null)
            {
                string layerName = renderer.sortingLayerName;
                if (layerName == "")
                    layerName = "Default";
                if (layerName == info.name)
                {
                    if (EditorGUIUtility.isProSkin)
                        GUI.backgroundColor = new Color(0.4f, 0.7f, 0.9f);
                    else
                        GUI.backgroundColor = new Color(0.55f, 0.75f, 0.9f);
                    GUI.Label(rect, "", style_layertitlelight);
                    break;
                }
            }
        }

        if (EditorGUIUtility.isProSkin)
        {
            text = "<color=white><size=14>" + text + "</size></color>";
            GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            text = "<color=black><size=14>" + text + "</size></color>";
            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        }
        bool newToggle = GUI.Toggle(rect, toggle, text, style_layertitle);
        GUI.backgroundColor = bgColor;
        
        info.dropEventRect = rect;

        return newToggle;
    }

    /// <summary>
    /// Draw all content of sortable a renderer.
    /// </summary>
    /// <param name="rect">The content rect for this renderer.</param>
    /// <param name="info">The renderer information.</param>
    /// <param name="select">The renderer is selected or not.</param>
    /// <param name="primary">The renderer is primary operation object or not.</param>
    /// <param name="warn">The renderer is warned or not.</param>
    void DrawRendererContent(Rect rect, SortingRendererInfo info, bool select, bool primary, bool warn)
    {
        info.leftEventRect = new Rect(rect.x, rect.y, 76f, rect.height);
        info.rightEventRect = new Rect(rect.x + 80f, rect.y, rect.width, rect.height);

        Color bgColor = GUI.backgroundColor;
        if (select)
        {
            if (EditorGUIUtility.isProSkin)
                GUI.backgroundColor = new Color(0.3f, 0.6f, 0.8f);
            else
                GUI.backgroundColor = new Color(0.6f, 0.85f, 1.0f);
            GUI.Label(new Rect(rect.x, rect.y, 76f, rect.height), "", style_contentbg0);
            GUI.Label(new Rect(rect.x + 80f, rect.y, rect.width - 162f, rect.height), "", style_contentbg1);
            GUI.Label(new Rect(rect.x + rect.width - 78f, rect.y, 78f, rect.height), "", style_contentbg1);
        }
        else
        {
            if (EditorGUIUtility.isProSkin)
                GUI.backgroundColor = new Color(0.35f, 0.35f, 0.35f);
            else
                GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f);
            GUI.Label(new Rect(rect.x, rect.y, 76f, rect.height), "", style_contentbg0);
            GUI.Label(new Rect(rect.x + 80f, rect.y, rect.width - 162f, rect.height), "", style_contentbg1);
            GUI.Label(new Rect(rect.x + rect.width - 78f, rect.y, 78f, rect.height), "", style_contentbg1);
        }
        GUI.backgroundColor = bgColor;

        Color ctColor = GUI.contentColor;
        if (EditorGUIUtility.isProSkin)
            GUI.contentColor = new Color(0.5f, 0.5f, 0.5f);
        else
            GUI.contentColor = new Color(0.8f, 0.8f, 0.8f);
        GUI.Label(new Rect(rect.x - 3f, rect.y, 22f, rect.height - 4f), TextureResources.verticalMoveIcon, style_contentlabel);
        GUI.contentColor = ctColor;

        if (info.renderer == _editSelectRenderer)
        {
            GUI.changed = false;
            int tempOrder = EditorGUI.IntField(new Rect(rect.x + 13f, rect.y + 1f, 47f, rect.height - 8f), info.order);
            tempOrder = Mathf.Clamp(tempOrder, -32768, 32767);
            if (GUI.changed && info.order != tempOrder)
            {
                Undo.RecordObject(info.renderer, "Sorting Order Change");
                info.renderer.sortingOrder = tempOrder;
                SceneView.RepaintAll();
            }
        }
        else
        {
            EditorGUIUtility.AddCursorRect(new Rect(rect.x, rect.y, 76f, rect.height), MouseCursor.ResizeVertical);
            GUI.Label(new Rect(rect.x + 14f, rect.y + 1f, 48f, rect.height - 4f), info.renderer.sortingOrder.ToString(), style_contentlabel);
        }
        
        if (warn)
        {
            GUI.Label(new Rect(rect.x + 60f, rect.y, 22f, rect.height - 4f), _warnContent, style_contentlabel);
        }

        Texture rendererIcon;
        System.Type rendererType = info.renderer.GetType();
        if (rendererType != typeof(ParticleSystemRenderer))
            rendererIcon = EditorGUIUtility.ObjectContent(null, rendererType).image;
        else
            rendererIcon = EditorGUIUtility.ObjectContent(null, typeof(ParticleSystem)).image;

        GUI.Label(new Rect(rect.x + 82, rect.y, 22f, rect.height - 4f), rendererIcon, style_contentlabel);
        string text = info.renderer.name;
        if (_fullPath)
        {
            Transform transform = info.renderer.transform;
            while (transform.parent != null)
            {
                text = transform.parent.name + "/" + text;
                transform = transform.parent;
            }
        }
        GUI.Label(new Rect(rect.x + 94, rect.y, rect.width - 178f, rect.height), "  " + text, style_contentlabel);

        bool tempCheck = false;

        GUI.changed = false;
        tempCheck = GUI.Toggle(new Rect(rect.x + rect.width - 72f, rect.y, 80f, rect.height - 6f), info.renderer.enabled, info.renderer.enabled? "Enable":"Disable", style_contenttoggle);
        if (GUI.changed && tempCheck != info.renderer.enabled)
        {
            if (!_selectRenderer.Contains(info.renderer))
            {
                Undo.RecordObject(info.renderer, "Renderer Enabled Change");
                info.renderer.enabled = tempCheck;
            }
            else
            {
                foreach (Renderer renderer in _selectRenderer)
                {
                    if (renderer != null)
                    {
                        Undo.RecordObject(renderer, "Renderer Enabled Change");
                        renderer.enabled = tempCheck;
                    }
                }
            }
        }

    }

    #endregion

    #region Event Process

    /// <summary>
    /// Process the mouse button events.
    /// </summary>
    void ClickProcess()
    {
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            DragAndDrop.PrepareStartDrag();
            _editSelectRenderer = null;

            KeyValuePair<SortingRendererInfo, FocusState> result = GetFocusRenderer(Event.current.mousePosition + _scrollPosition + new Vector2(0, -24f));
            SortingRendererInfo focusRendererInfo = result.Key;

            if (focusRendererInfo != null)
            {
                Renderer focusRenderer = focusRendererInfo.renderer;

                if (Event.current.control)
                {
                    if (_selectRenderer.Contains(focusRenderer))
                    {
                        _selectRenderer.Remove(focusRenderer);
                        if (_focusSelectRenderer == focusRenderer)
                        {
                            _focusSelectRenderer = null;
                            if (_selectRenderer.Count > 0)
                            {
                                foreach (Renderer renderer in _selectRenderer)
                                {
                                    if (renderer != null)
                                    {
                                        _focusSelectRenderer = renderer;

                                        SortingRendererInfo rendererinfo = GetRendererInfo(renderer);
                                        GUIUtility.hotControl = rendererinfo.controlID;

                                        break;
                                    }
                                }
                                GUIUtility.keyboardControl = 0;
                            }
                        }
                    }
                    else
                    {
                        _selectRenderer.Add(focusRenderer);
                        _focusSelectRenderer = focusRenderer;

                        GUIUtility.hotControl = focusRendererInfo.controlID;
                        GUIUtility.keyboardControl = 0;
                    }
                }
                else if (Event.current.shift)
                {
                    if (focusRenderer != _focusSelectRenderer && _focusSelectRenderer != null)
                    {
                        int dstLayerIndex = 0;
                        int srcLayerIndex = 0;
                        string dstLayerName = focusRenderer.sortingLayerName;
                        if (dstLayerName == string.Empty)
                            dstLayerName = "Default";
                        string srcLayerName = _focusSelectRenderer.sortingLayerName;
                        if (srcLayerName == string.Empty)
                            srcLayerName = "Default";
                        for (int index = 0; index < _layerList.Count; index++)
                        {
                            if (dstLayerName == _layerList[index].name)
                                dstLayerIndex = index;
                            if (srcLayerName == _layerList[index].name)
                                srcLayerIndex = index;
                        }
                        // dstLayerIndex < srcLayerIndex
                        if (dstLayerIndex < srcLayerIndex)
                        {
                            bool start = false;
                            foreach (SortingRendererInfo rendererInfo in _layerList[dstLayerIndex].rendererList)
                            {
                                if (!start)
                                {
                                    if (rendererInfo.renderer == focusRenderer)
                                        start = true;
                                }
                                if (start)
                                {
                                    if (!_selectRenderer.Contains(rendererInfo.renderer))
                                        _selectRenderer.Add(rendererInfo.renderer);
                                }
                            }

                            for (int index = dstLayerIndex + 1; index < srcLayerIndex; index++)
                            {
                                foreach (SortingRendererInfo rendererInfo in _layerList[index].rendererList)
                                {
                                    if (!_selectRenderer.Contains(rendererInfo.renderer))
                                        _selectRenderer.Add(rendererInfo.renderer);
                                }
                            }

                            foreach (SortingRendererInfo rendererInfo in _layerList[srcLayerIndex].rendererList)
                            {
                                if (rendererInfo.renderer == _focusSelectRenderer)
                                    break;
                                if (!_selectRenderer.Contains(rendererInfo.renderer))
                                    _selectRenderer.Add(rendererInfo.renderer);
                            }
                        }
                        // dstLayerIndex > srcLayerIndex
                        else if (dstLayerIndex > srcLayerIndex)
                        {
                            bool start = false;
                            foreach (SortingRendererInfo rendererInfo in _layerList[srcLayerIndex].rendererList)
                            {
                                if (start)
                                {
                                    if (!_selectRenderer.Contains(rendererInfo.renderer))
                                        _selectRenderer.Add(rendererInfo.renderer);
                                }
                                if (!start)
                                {
                                    if (rendererInfo.renderer == _focusSelectRenderer)
                                        start = true;
                                }
                            }

                            for (int index = srcLayerIndex + 1; index < dstLayerIndex; index++)
                            {
                                foreach (SortingRendererInfo rendererInfo in _layerList[index].rendererList)
                                {
                                    if (!_selectRenderer.Contains(rendererInfo.renderer))
                                        _selectRenderer.Add(rendererInfo.renderer);
                                }
                            }

                            foreach (SortingRendererInfo rendererInfo in _layerList[dstLayerIndex].rendererList)
                            {
                                if (!_selectRenderer.Contains(rendererInfo.renderer))
                                    _selectRenderer.Add(rendererInfo.renderer);

                                if (rendererInfo.renderer == focusRenderer)
                                    break;
                            }
                        }
                        // dstLayerIndex == srcLayerIndex
                        else
                        {
                            bool start = false;
                            foreach (SortingRendererInfo rendererInfo in _layerList[dstLayerIndex].rendererList)
                            {
                                if (start)
                                {
                                    if (rendererInfo.renderer == focusRenderer || rendererInfo.renderer == _focusSelectRenderer)
                                        break;

                                    if (!_selectRenderer.Contains(rendererInfo.renderer))
                                        _selectRenderer.Add(rendererInfo.renderer);
                                }

                                if (!start)
                                {
                                    if (rendererInfo.renderer == focusRenderer || rendererInfo.renderer == _focusSelectRenderer)
                                        start = true;
                                }
                            }
                            if (!_selectRenderer.Contains(focusRenderer))
                                _selectRenderer.Add(focusRenderer);
                        }

                        GUIUtility.hotControl = focusRendererInfo.controlID;
                        GUIUtility.keyboardControl = 0;
                    }
                }
                else 
                {
                    _focusState = result.Value;

                    if (!_selectRenderer.Contains(focusRenderer))
                    {
                        _selectRenderer.Clear();
                        _selectRenderer.Add(focusRenderer);
                    }

                    _focusSelectRenderer = focusRenderer;

                    GUIUtility.hotControl = focusRendererInfo.controlID;
                    GUIUtility.keyboardControl = 0;

                    if (result.Value == FocusState.Right)
                    {
                        List<GameObject> objectReferenceList = new List<GameObject>();
                        foreach (Renderer renderer in _selectRenderer)
                        {
                            if (renderer != null && !objectReferenceList.Contains(renderer.gameObject))
                                objectReferenceList.Add(renderer.gameObject);
                        }
                        DragAndDrop.objectReferences = objectReferenceList.ToArray();
                    }
                    else if (result.Value == FocusState.Left)
                    {
                        _originMousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                        _originShiftPos = focusRendererInfo.leftEventRect.y - _scrollPosition.y;
                        _originOrder = _focusSelectRenderer.sortingOrder;
                    }
                }

                Event.current.Use();
            }
            else
            {
                _selectRenderer.Clear();
                _focusSelectRenderer = null;
                GUIUtility.hotControl = 0;
                GUIUtility.keyboardControl = 0;
                _focusState = FocusState.None;
                Event.current.Use();
            }

        }
        else if (Event.current.type == EventType.MouseUp)
        {
            _isDragRight = false;
            _isDragLeft = false;
            KeyValuePair<SortingRendererInfo, FocusState> result = GetFocusRenderer(Event.current.mousePosition + _scrollPosition + new Vector2(0, -24f));
            SortingRendererInfo focusRendererInfo = result.Key;

            GUIUtility.hotControl = 0;

            if (focusRendererInfo != null && _selectRenderer.Contains(focusRendererInfo.renderer))
            {
                Renderer focusRenderer = focusRendererInfo.renderer;

                if (Event.current.button == 0)
                {
                    if (!Event.current.control &&
                        !Event.current.shift &&
                        _selectRenderer.Contains(focusRenderer))
                    {
                        if (_selectRenderer.Count > 1)
                        {
                            _selectRenderer.Clear();
                            _selectRenderer.Add(focusRenderer);
                            _focusSelectRenderer = focusRenderer;
                        }
                        else if (result.Value == FocusState.Left && Selection.activeGameObject == _focusSelectRenderer.gameObject)
                        {
                            _editSelectRenderer = _focusSelectRenderer;
                        }
                    }

                    ChangeSelectObject();
                }
                else if (Event.current.button == 2)
                {
                    if (_selectRenderer.Contains(focusRenderer))
                    {
                        _focusSelectRenderer = focusRenderer;
                    }
                }
                    
                GUIUtility.hotControl = 0;
                GUIUtility.keyboardControl = 0;

                Event.current.Use();
            }
        }
        else if (Event.current.rawType == EventType.MouseUp)
        {
            _isDragRight = false;
            _isDragLeft = false;
            GUIUtility.hotControl = 0;
            GUIUtility.keyboardControl = 0;
            
        }

    }

    /// <summary>
    /// Process the mouse drag events.
    /// </summary>
    void DragProcess()
    {
        if (_isDragLeft && Event.current.type == EventType.Repaint)
        {
            float dstY = GetRendererInfo(_focusSelectRenderer).leftEventRect.y - _originShiftPos;
            if (dstY != _scrollPosition.y)
            {
                _scrollPosition = new Vector2(_scrollPosition.x,
                                              Mathf.Lerp(_scrollPosition.y, GetRendererInfo(_focusSelectRenderer).leftEventRect.y - _originShiftPos, Mathf.Clamp01((Time.realtimeSinceStartup - _guiTime) * 1.0f))
                                              );
                Repaint();
            }
        }

        if (Event.current.type == EventType.MouseDrag)
        {
            if (Event.current.button == 0 && _focusSelectRenderer != null)
            {
                if (_focusState == FocusState.Right)
                {
                    _isDragRight = true;
                    DragAndDrop.activeControlID = GetRendererInfo(_focusSelectRenderer).controlID;
                    DragAndDrop.StartDrag("Dragging Renderers");
                }
                else if (_focusState == FocusState.Left)
                {
                    _isDragLeft = true;
                    Vector2 mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                    int diff = (int)(mousePos.y - _originMousePos.y) / 22;
                    int diffOrder = _originOrder + diff * _stride - _focusSelectRenderer.sortingOrder;
                    if (diffOrder != 0)
                    {
                        foreach (Renderer renderer in _selectRenderer)
                        {
                            if (renderer != null)
                            {
                                Undo.RecordObject(renderer, "Sorting Order Change");
                                renderer.sortingOrder += diffOrder;
                            }
                        }
                        SceneView.RepaintAll();
                        _guiTime = Time.realtimeSinceStartup;
                    }
                }

            }
            Event.current.Use();
        }
        else if (Event.current.type == EventType.DragUpdated && _isDragRight)
        {
            SortingLayerInfo dropLayer = GetDropLayer(Event.current.mousePosition + _scrollPosition + new Vector2(0, -24f));
            if (dropLayer == null)
                DragAndDrop.visualMode = DragAndDropVisualMode.Move;
            else
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            Event.current.Use();
        }
        else if (Event.current.type == EventType.DragPerform && _isDragRight)
        {
            DragAndDrop.AcceptDrag();

            SortingLayerInfo dropLayer = GetDropLayer(Event.current.mousePosition + _scrollPosition + new Vector2(0, -24f));
            if (dropLayer != null)
            {
                foreach (Renderer renderer in _selectRenderer)
                {
                    if (renderer != null)
                    {
                        Undo.RecordObject(renderer, "Sorting Layer Change");
                        renderer.sortingLayerName = dropLayer.name;
                    }
                }
                
            }

            _isDragRight = false;
            DragAndDrop.activeControlID = 0;
            GUIUtility.hotControl = 0;
            GUIUtility.keyboardControl = 0;

            Event.current.Use();
        }
        else if (Event.current.rawType == EventType.DragPerform && _isDragLeft)
        {
            _isDragRight = false;
            _isDragLeft = false;
            GUIUtility.hotControl = 0;
            GUIUtility.keyboardControl = 0;
        }
        
    }

    /// <summary>
    /// Process the keyboard events.
    /// </summary>
    void KeyBoardProcess()
    {
        if (_focusSelectRenderer == null)
            return;

        if (Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.UpArrow)
            {
                _editSelectRenderer = null;
                if (_keyDampState == KeyBoardDamp.None)
                {
                    SortingRendererInfo rendererInfo = GetLastRendererInfo(_focusSelectRenderer);
                    if (rendererInfo != null)
                    {
                        if (Event.current.control || Event.current.shift)
                        {
                            if (!_selectRenderer.Contains(rendererInfo.renderer))
                                _selectRenderer.Add(rendererInfo.renderer);
                            else
                                _selectRenderer.Remove(_focusSelectRenderer);
                        }
                        else
                        {
                            _selectRenderer.Clear();
                            _selectRenderer.Add(rendererInfo.renderer);
                        }
                        Repaint();
                        _focusSelectRenderer = rendererInfo.renderer;
                        GUIUtility.hotControl = rendererInfo.controlID;
                        GUIUtility.keyboardControl = 0;

                        _keyDampState = KeyBoardDamp.Damp;
                        _guiTime = Time.realtimeSinceStartup;
                        ChangeSelectObject();
                    }
                }
                else if (_keyDampState == KeyBoardDamp.Damp)
                {
                    if (Time.realtimeSinceStartup - _guiTime >= 0.5f)
                    {
                        _keyDampState = KeyBoardDamp.Continuous;
                        _guiTime = Time.realtimeSinceStartup;
                    }
                }
                else if (_keyDampState == KeyBoardDamp.Continuous)
                {
                    if (Time.realtimeSinceStartup - _guiTime >= 0.05f)
                    {
                        SortingRendererInfo rendererInfo = GetLastRendererInfo(_focusSelectRenderer);
                        if (rendererInfo != null)
                        {
                            if (Event.current.control || Event.current.shift)
                            {
                                if (!_selectRenderer.Contains(rendererInfo.renderer))
                                    _selectRenderer.Add(rendererInfo.renderer);
                                else
                                    _selectRenderer.Remove(_focusSelectRenderer);
                            }
                            else
                            {
                                _selectRenderer.Clear();
                                _selectRenderer.Add(rendererInfo.renderer);
                            }
                            Repaint();
                            _focusSelectRenderer = rendererInfo.renderer;
                            GUIUtility.hotControl = rendererInfo.controlID;
                            GUIUtility.keyboardControl = 0;

                            _guiTime = Time.realtimeSinceStartup;
                            ChangeSelectObject();
                        }
                    }
                }
                Event.current.Use();
            }
            else if (Event.current.keyCode == KeyCode.DownArrow)
            {
                _editSelectRenderer = null;
                if (_keyDampState == KeyBoardDamp.None)
                {
                    SortingRendererInfo rendererInfo = GetNextRendererInfo(_focusSelectRenderer);
                    if (rendererInfo != null)
                    {
                        if (Event.current.control || Event.current.shift)
                        {
                            if (!_selectRenderer.Contains(rendererInfo.renderer))
                                _selectRenderer.Add(rendererInfo.renderer);
                            else
                                _selectRenderer.Remove(_focusSelectRenderer);
                        }
                        else
                        {
                            _selectRenderer.Clear();
                            _selectRenderer.Add(rendererInfo.renderer);
                        }
                        Repaint();
                        _focusSelectRenderer = rendererInfo.renderer;
                        GUIUtility.hotControl = rendererInfo.controlID;
                        GUIUtility.keyboardControl = 0;

                        _keyDampState = KeyBoardDamp.Damp;
                        _guiTime = Time.realtimeSinceStartup;
                        ChangeSelectObject();
                    }
                }
                else if (_keyDampState == KeyBoardDamp.Damp)
                {
                    if (Time.realtimeSinceStartup - _guiTime >= 0.5f)
                    {
                        _keyDampState = KeyBoardDamp.Continuous;
                        _guiTime = Time.realtimeSinceStartup;
                    }
                }
                else if (_keyDampState == KeyBoardDamp.Continuous)
                {
                    if (Time.realtimeSinceStartup - _guiTime >= 0.05f)
                    {
                        SortingRendererInfo rendererInfo = GetNextRendererInfo(_focusSelectRenderer);
                        if (rendererInfo != null)
                        {
                            if (Event.current.control || Event.current.shift)
                            {
                                if (!_selectRenderer.Contains(rendererInfo.renderer))
                                    _selectRenderer.Add(rendererInfo.renderer);
                                else
                                    _selectRenderer.Remove(_focusSelectRenderer);
                            }
                            else
                            {
                                _selectRenderer.Clear();
                                _selectRenderer.Add(rendererInfo.renderer);
                            }
                            Repaint();
                            _focusSelectRenderer = rendererInfo.renderer;
                            GUIUtility.hotControl = rendererInfo.controlID;
                            GUIUtility.keyboardControl = 0;

                            _guiTime = Time.realtimeSinceStartup;
                            ChangeSelectObject();
                        }
                    }
                }
                Event.current.Use();
            }
        }
        else if (Event.current.type == EventType.KeyUp)
        {
            _guiTime = 0;
            _keyDampState = KeyBoardDamp.None;
            GUIUtility.hotControl = 0;
        }
    }

    /// <summary>
    /// Get the renderer information and focus state, the rect of which contains specified mouse position.
    /// </summary>
    /// <param name="mousePos">Specified mouse position.</param>
    /// <returns>Pair values.</returns>
    KeyValuePair<SortingRendererInfo, FocusState> GetFocusRenderer(Vector2 mousePos)
    {
        foreach (SortingLayerInfo layerInfo in _layerList)
        {
            bool toggle = false;
            _showSortingLayerContent.TryGetValue(layerInfo.name, out toggle);

            if (toggle)
            {
                foreach (SortingRendererInfo rendererInfo in layerInfo.rendererList)
                {
                    if (rendererInfo.leftEventRect.Contains(mousePos))
                        return new KeyValuePair<SortingRendererInfo, FocusState>(rendererInfo, FocusState.Left);
                    else if (rendererInfo.rightEventRect.Contains(mousePos))
                        return new KeyValuePair<SortingRendererInfo, FocusState>(rendererInfo, FocusState.Right);
                }
            }
        }

        return new KeyValuePair<SortingRendererInfo, FocusState>(null, FocusState.None);
    }

    /// <summary>
    /// Get the sorting layer information, the rect of which contains specified mouse position.
    /// </summary>
    /// <param name="mousePos">Specified mouse position.</param>
    /// <returns>Information of sorting layer.</returns>
    SortingLayerInfo GetDropLayer(Vector2 mousePos)
    {
        foreach (SortingLayerInfo layerInfo in _layerList)
        {
            if (layerInfo.dropEventRect.Contains(mousePos))
                return layerInfo;
        }
        return null;
    }

    /// <summary>
    /// Get the infomation of specified renderer.
    /// </summary>
    /// <param name="renderer">Specified renderer.</param>
    /// <returns>Information of renderer.</returns>
    SortingRendererInfo GetRendererInfo(Renderer renderer)
    {
        foreach (SortingLayerInfo layerInfo in _layerList)
        {
            foreach (SortingRendererInfo rendererInfo in layerInfo.rendererList)
            {
                if (rendererInfo.renderer == renderer)
                    return rendererInfo;
            }
        }

        return null;
    }

    /// <summary>
    /// Get the infomation of a renderer before specified renderer.
    /// </summary>
    /// <param name="renderer">Specified renderer.</param>
    /// <returns>Information of renderer.</returns>
    SortingRendererInfo GetLastRendererInfo(Renderer renderer)
    {
        int layerIndex = 0;
        int rendererIndex = 0;
        int lastLayerIndex = -1;
        for (; layerIndex < _layerList.Count; layerIndex++ )
        {
            rendererIndex = 0;
            for (; rendererIndex < _layerList[layerIndex].rendererList.Count; rendererIndex++)
            {
                if (_layerList[layerIndex].rendererList[rendererIndex].renderer == renderer)
                {
                    if (rendererIndex > 0)
                    {
                        return _layerList[layerIndex].rendererList[rendererIndex - 1];
                    }
                    else if (lastLayerIndex != -1)
                    {
                        int count = _layerList[lastLayerIndex].rendererList.Count;
                        return _layerList[lastLayerIndex].rendererList[count - 1];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            if (_layerList[layerIndex].rendererList.Count > 0)
                lastLayerIndex = layerIndex;
        }

        return null;
    }

    /// <summary>
    /// Get the infomation of a renderer after specified renderer.
    /// </summary>
    /// <param name="renderer">Specified renderer.</param>
    /// <returns>Information of renderer.</returns>
    SortingRendererInfo GetNextRendererInfo(Renderer renderer)
    {
        int layerIndex = 0;
        int rendererIndex = 0;
        for (; layerIndex < _layerList.Count; layerIndex++)
        {
            rendererIndex = 0;
            for (; rendererIndex < _layerList[layerIndex].rendererList.Count; rendererIndex++)
            {
                if (_layerList[layerIndex].rendererList[rendererIndex].renderer == renderer)
                {
                    if (rendererIndex < _layerList[layerIndex].rendererList.Count - 1)
                    {
                        return _layerList[layerIndex].rendererList[rendererIndex + 1];
                    }
                    else
                    {
                        for (; layerIndex < _layerList.Count - 1;)
                        {
                            layerIndex++;
                            if (_layerList[layerIndex].rendererList.Count > 0)
                                return _layerList[layerIndex].rendererList[0];
                        }
                        return null;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Change the select objects based on renderers selected in soring view window.
    /// </summary>
    void ChangeSelectObject()
    {
        List<GameObject> objects = new List<GameObject>();
        foreach (Renderer renderer in _selectRenderer)
        {
            if (renderer != null && !objects.Contains(renderer.gameObject))
                objects.Add(renderer.gameObject);
        }

        Selection.objects = objects.ToArray();
    }

    /// <summary>
    /// Change the select renderers based on objects selected in scene.
    /// </summary>
    void OnSelectionChange()
    {
        if (_isFocus || _selectRenderer == null)
            return;

        UnityEngine.Object[] objects = Selection.objects;
        _selectRenderer.Clear();
        foreach (UnityEngine.Object obj in objects)
        {
            if (obj.GetType() == typeof(GameObject))
            {
                GameObject go = obj as GameObject;
                Renderer[] rendererArray = go.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in rendererArray)
                {
                    Material mat = renderer.sharedMaterial;
                    if (mat != null && mat.renderQueue == 3000)
                    {
                        _selectRenderer.Add(renderer);
                        _focusSelectRenderer = renderer;
                    }
                }
            }
        }
    }

    #endregion
}

