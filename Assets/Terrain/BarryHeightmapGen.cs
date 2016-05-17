using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarryHeightmapGen : MonoBehaviour
{
    public GameObject[] spawnPoints;
    // Use this for initialization
    void Start()
    {
        Random.seed = 786572;
        GenerateTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2)) 
        {
            ToggleGrass();
            //GenerateTerrain();
        }
    }


    int[,] detailPoints = null;
    bool showGrass = true;
    void ToggleGrass()
    {
        if (showGrass)
        {
            print("Grass is disabled. Press F2 to enable.");
            showGrass = false;
            int[,] emptyDetailPoints = new int[Terrain.activeTerrain.terrainData.detailWidth, Terrain.activeTerrain.terrainData.detailHeight];
            Terrain.activeTerrain.terrainData.SetDetailLayer(0, 0, 0, emptyDetailPoints);
        }
        else
        {
            print("Grass is enabled. Press F2 to disable.");
            showGrass = true;
            Terrain.activeTerrain.terrainData.SetDetailLayer(0, 0, 0, detailPoints);
        }
    }

    float getSizeFromDefiningPoints(List<Vector3> definingPoints, int x, int y)
    {
        float totalHeight=0;
        float scale=0;
        float isNear = 0;
        foreach (Vector3 definingPoint in definingPoints)
        {
            float pointImpact = Vector2.Distance(new Vector2(x, y), new Vector2(definingPoint.x, definingPoint.z));
            pointImpact = Mathf.Pow(pointImpact, 5f);
            scale += 1f / pointImpact;
            totalHeight += definingPoint.y / pointImpact;
            if (pointImpact < 30)
                isNear = definingPoint.y;
        }
        if (isNear > 0)
            return isNear;
        return totalHeight / scale;
    }

    void GenerateTerrain()
    {
        List<Vector3> definingPoints = new List<Vector3>();

        Vector3 definingPointMiddle = new Vector3();
        definingPointMiddle.x = 256;
        definingPointMiddle.y = 1;
        definingPointMiddle.z = 256;
        definingPoints.Add(definingPointMiddle);
        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            Vector3 definingPoint = new Vector3();
            definingPoint.x = Random.Range(-100, 613);
            definingPoint.y = Random.Range(0f, 1f);
            definingPoint.z = Random.Range(-100, 613);
            definingPoints.Add(definingPoint);
        }

        Terrain terrain = Terrain.activeTerrain;
        ArrayList treeList = new ArrayList();
        var nRows = 513;
        var nCols = 513;
        var numTextures = 3;
        var heights = new float[nRows, nCols];
        float perlinSeedX = Random.Range(0, 1000);
        float perlinSeedY = Random.Range(0, 1000);
        float perlinFrequency = Random.Range(20, 800);
        var textureAlphas = new float[nRows - 1, nCols - 1, numTextures];
        float grassGreenery = Random.Range(-1.5f, 1.5f);
        detailPoints = new int[Terrain.activeTerrain.terrainData.detailWidth, Terrain.activeTerrain.terrainData.detailHeight];

        for (var j = 0; j < nRows; j++)
            for (var i = 0; i < nCols; i++)
            {
                heights[j, i] = getSizeFromDefiningPoints(definingPoints, j, i)*0.8f + Mathf.PerlinNoise(j / perlinFrequency + perlinSeedX, i / perlinFrequency + perlinSeedY) * 0.199f + Mathf.PerlinNoise(j / 5f + perlinSeedX+50, i / 5f + perlinSeedY+50) * 0.001f;//Mathf.Sin(j * Mathf.PI / 513) * 0.5f + Mathf.Sin(i * Mathf.PI / 513) * 0.5f;
                                                                                 //if (i < nRows - 1 && j < nRows - 1)
                                                                             //{    
                                                                             //    textureAlphas[j, i, 0] = (Mathf.Pow((heights[j, i]*4-1), 3) +1)/2f;
                                                                             //    textureAlphas[j, i, 1] = 1 - textureAlphas[j, i, 0];
                                                                             //}

                //quick slope calculation
                if (i > 0 && j > 0)
                {
                    float[] heightList = { heights[j, i], heights[j-1, i], heights[j, i-1], heights[j-1, i-1], };
                    float slope = Mathf.Max(heightList) - Mathf.Min(heightList);
                    textureAlphas[j-1, i-1, 0] = Mathf.Clamp(slope*100, 0.1f, 1.2f) - 0.1f;
                    //textureAlphas[j-1, i-1, groundType] = 1 - textureAlphas[j-1, i-1, 0];
                    float amountForGrass = 1 - textureAlphas[j - 1, i - 1, 0];
                    textureAlphas[j - 1, i - 1, 1] = amountForGrass * Mathf.Clamp(Mathf.PerlinNoise(j / perlinFrequency + perlinSeedX, i / perlinFrequency + perlinSeedY)-grassGreenery, 0, 1);
                    textureAlphas[j - 1, i - 1, 2] = amountForGrass - textureAlphas[j - 1, i - 1, 1];


                    detailPoints[j * 2 - 2, i * 2 - 2] = (int)Random.Range(0, (textureAlphas[j - 1, i - 1, 1] + textureAlphas[j - 1, i - 1, 2]+0.1f) * 3);
                    detailPoints[j * 2 - 1, i * 2 - 2] = (int)Random.Range(0, (textureAlphas[j - 1, i - 1, 1] + textureAlphas[j - 1, i - 1, 2] + 0.1f) * 3);
                    detailPoints[j * 2 - 2, i * 2 - 1] = (int)Random.Range(0, (textureAlphas[j - 1, i - 1, 1] + textureAlphas[j - 1, i - 1, 2] + 0.1f) * 3);
                    detailPoints[j * 2 - 1, i * 2 - 1] = (int)Random.Range(0, (textureAlphas[j - 1, i - 1, 1] + textureAlphas[j - 1, i - 1, 2] + 0.1f) * 3);
                }




            }


        for (int a = 0; a <= Random.Range(1, 7); a++)
        {
            //pick an area
            float centerX = Random.Range(-20, 612);
            float centerY = Random.Range(-20, 612);
            float radius = Random.Range(20, 120);
            int treeType = Random.Range(0, 8);
            
            for (int b = 0; b < radius*Mathf.PI*1.5f; b++)
            {
                TreeInstance tree = new TreeInstance();
                tree.color = Color.Lerp(Color.white, Color.red, Random.Range(0, 0.6f));
                float treeSize = Random.Range(0.7f, 1.5f);
                tree.heightScale = treeSize;
                tree.widthScale = treeSize;
                tree.lightmapColor = Color.white;
                float distanceFromCenter = Random.Range(0, radius);
                float angle = Random.Range(0, 2 * Mathf.PI);
                float xPosition = centerX + distanceFromCenter * Mathf.Cos(angle);
                float yPosition = centerY + distanceFromCenter * Mathf.Sin(angle);
                if (xPosition >= 0 && xPosition < 512 && yPosition >= 0 && yPosition < 512)
                {
                    tree.position = new Vector3(xPosition / 512f, heights[(int)yPosition, (int)xPosition], yPosition / 513f);
                    tree.prototypeIndex = treeType;
                    tree.rotation = Random.Range(0, 2 * Mathf.PI);
                    if (textureAlphas[(int)yPosition, (int)xPosition, 0] < 0.4f)
                    treeList.Add(tree);
                }
            }
        }

        terrain.detailObjectDistance = 300;
        terrain.terrainData.SetDetailLayer(0, 0, 0, detailPoints);
        ToggleGrass();
        terrain.terrainData.wavingGrassAmount = 0;
        terrain.terrainData.treeInstances = (TreeInstance[])treeList.ToArray(typeof(TreeInstance));
        terrain.terrainData.SetHeights(0, 0, heights);
        terrain.terrainData.SetAlphamaps(0, 0, textureAlphas);

        foreach (var spawnpoint in spawnPoints)
        {
            float xPos = spawnpoint.transform.position.x;
            float zPos = spawnpoint.transform.position.z;
            float yPos = heights[(int)(zPos * 512f / 1000f), (int)(xPos * 512f / 1000f)] * 200 + 2;
            spawnpoint.transform.position = new Vector3(xPos, yPos, zPos);
        }
    }
}