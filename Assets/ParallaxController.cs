using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform cam;
    private Vector3 camStartPos;
    private float distance;

    private GameObject[] background;
    private Material[] mat;
    private float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    [SerializeField] private string sortingLayerName = "Background"; // Default sorting layer name
    [SerializeField] private int baseOrderInLayer = 0; // Base order in layer

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        background = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            background[i] = transform.GetChild(i).gameObject;
            mat[i] = background[i].GetComponent<Renderer>().material;

            // Set sorting layer and order in layer
            Renderer renderer = background[i].GetComponent<Renderer>();
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = baseOrderInLayer + i;
        }
        BackSpeedCalculate(backCount);
    }

    private void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((background[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = background[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (background[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < background.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
