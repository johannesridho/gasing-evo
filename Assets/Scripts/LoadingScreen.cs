using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{

    public Texture2D texture;
    static LoadingScreen instance;
    private bool isLoading = false;
    #region untuk GUI
    private float guiRatioX;
    private float guiRatioY;
    private float sWidth;
    private float sHeight;
    private Vector3 GUIsF;
    private int sizegui;
    #endregion
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            hide();
            return;
        }
        instance = this;
        gameObject.AddComponent<GUITexture>().enabled = false;
        guiTexture.texture = texture;
        transform.position = new Vector3(0.5f, 0.5f, 1f);
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //get the screen's width
        sWidth = Screen.width;
        sHeight = Screen.height;
        //calculate the rescale ratio
        guiRatioX = sWidth / 1280;
        guiRatioY = sHeight / 720;
        //create a rescale Vector3 with the above ratio
        GUIsF = new Vector3(guiRatioX, guiRatioY, 1);
    }

    void Update()
    {
        if (!Application.isLoadingLevel)
            hide();
    }

    void OnGUI()
    {
        if (isLoading)
        {
            GUI.matrix = Matrix4x4.TRS(new Vector3(GUIsF.x, GUIsF.y, 0), Quaternion.identity, GUIsF);
            GUI.DrawTexture(new Rect(0, 0, 1280, 720), texture);
        }
    }

    public static void show()
    {
        if (!InstanceExists())
        {
            return;
        }
        //instance.guiTexture.enabled = true;
        instance.isLoading = true;
    }

    public static void hide()
    {
        if (!InstanceExists())
        {
            return;
        }
        //instance.guiTexture.enabled = false;
        instance.isLoading = false;
    }

    static bool InstanceExists()
    {
        if (!instance)
        {
            return false;
        }
        return true;

    }
}
