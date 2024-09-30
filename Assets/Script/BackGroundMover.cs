using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

//背景スクロールのスクリプト
public class BackGroundMover : MonoBehaviour
{
    //スクロールする際の限界値
    private const float MAXLENGTH = 1f;
    //setTextureOffsetのpropname
    private const string PROPNAME = "_MainTex";

    private Material material;

    private Vector2 pos1;
    private Vector2 pos2;

    private float x;

    [SerializeField] private float speed;
    private float velDiv = 100f;

    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);

        if(GetComponent<Image>() is Image i)
        {
            material = i.material;
        }

        pos1 = Gmanager.instance.getCameraPos();
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(material)
        {
            pos2 = Gmanager.instance.getCameraPos();

            x = (x +(pos2.x - pos1.x) * (speed / velDiv) ) % MAXLENGTH;
            var offset = new Vector2(x,0);

            material.SetTextureOffset(PROPNAME, offset);
            pos1 = pos2;
        }
        
    }

    private void OnDestroy()
    {
        if(material )
        {
            material.SetTextureOffset(PROPNAME, Vector2.zero);
        }
    }
}
