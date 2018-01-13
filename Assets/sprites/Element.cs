using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {
    public bool mine;
    // Use this for initialization
    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    void Start () {
        mine = Random.value < 0.15;
        // TEST

        // Register in Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Grid.elements[x, y] = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    // Load another texture
    public void loadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }
    void OnMouseUpAsButton()
    {
        if (mine)
        {
            // Uncover all mines
            Grid.uncoverMines();

            // game over
            print("you lose");
        }
        // It's not a mine
        else
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Grid.adjacentMines(x, y));
            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);
        }
        // find out if the game was won now
        if (Grid.isFinished())
            print("you win");
    }

}
