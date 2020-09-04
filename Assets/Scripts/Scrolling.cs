using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {

    //[SerializeField] private float scrollSpeed;
    public bool isMainMenu;
    private Renderer quadRenderer;
    public float direction = 0.0f;
    

    void Start()
    {
        isMainMenu = true;
        quadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(isMainMenu == true)
        {
            Scroll(0.5f);
        }
        else if (isMainMenu == false)
        {
            Scroll(0.0f);
        }
        else
        {
            if(GameManager.Instance.GameState())
            {
                Scroll(0.5f);
            }
        }
    }

    public void Scroll(float scrollSpeed){
        // Continuously scroll the image of the ground to simulate Left to Right movement
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
        quadRenderer.material.mainTextureOffset = textureOffset * direction;
    }
	
}
