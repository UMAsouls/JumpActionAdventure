using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private SpriteRenderer sprRend;
    private BoxCollider2D coldr;

    [SerializeField] float colliderCorrection;

    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        coldr = GetComponent<BoxCollider2D>();

        Vector2 size = sprRend.size;

        if (size.x < 0) { size.Set(-1 * size.x, size.y); }
        if (size.y < 0) { size.Set(size.x, -1 * size.y); }

        size.y -= colliderCorrection/2;

        coldr.size = size;

        Vector2 offset = coldr.offset;
        offset.y -= colliderCorrection / 2;

        coldr.offset = offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
