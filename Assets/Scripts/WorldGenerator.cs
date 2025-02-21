using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    public int worldSize = 100;
    public float noiseFreq = 0.05f;
    public float seed;
    public Texture2D noiseTexture;
    public Sprite tile;
    public float heightMultiplier = 4f;
    public float heightAddition = 25f;
    public int dirtLayerHeight = 3;
    public Sprite stoneSprite;
    public Sprite dirtSprite;
    public Sprite dirtGrassSprite;

    private void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        GenerateTerrain();
    }

    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);

        for(int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x+seed) * noiseFreq, (y+seed) * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }

    public void GenerateTerrain()
    {
        for(int x = 0; x < worldSize; x++)
        {
            float height =  Mathf.PerlinNoise((x+seed) * noiseFreq, seed * noiseFreq) * heightMultiplier + heightAddition;
            
            for(int y = 0;y < height; y++)
            {
                Sprite tileSprite;
                if (y <  height - dirtLayerHeight)
                {
                    tileSprite = stoneSprite;
                }
                
                else
                {
                    if (y < height - 1)
                    {
                        tileSprite = dirtSprite;
                    }
                    else 
                    { 
                        tileSprite = dirtGrassSprite;

                    }
                }
                GameObject newTile = new GameObject(name = "tile");
                newTile.AddComponent<SpriteRenderer>();
                newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
                newTile.transform.position = new Vector2(x, y);
                newTile.transform.SetParent(transform, false);
                newTile.AddComponent<BoxCollider2D>().usedByComposite = true;
                if (noiseTexture.GetPixel(x,y).g < 0.5f)
                {
                   
                }
            }
        }

        
    }
}
