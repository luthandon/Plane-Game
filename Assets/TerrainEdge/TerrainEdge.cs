/*
---------------------- TerrainEdge ----------------------
--
-- Terrain Edge (Beta Release 0.1)
-- By Tim Leonard

-- TerrainEdge.cs
--
-------------------------------------------------------------------
*/

using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode()]
[AddComponentMenu("Terrain/Terrain Edge")]

public class TerrainEdge : MonoBehaviour {
		
	public int toolModeInt = 0;
	public Transform neighborTerrain;
	
	public void teImport() {
		// Error checking...
		Terrain ter = (Terrain) GetComponent(typeof(Terrain));
		if (ter == null) {
			return;
		}
		if (neighborTerrain==null){
			Debug.Log("You will need to select a piece of terrain first!");
			return;
		}
		if (neighborTerrain.GetComponent(typeof(Terrain)) == null) {
			Debug.Log("You will need to select a piece of terrain first!");
			return;
		}
		Terrain srcTer = (Terrain) neighborTerrain.GetComponent(typeof(Terrain));
		TerrainData srcData = srcTer.terrainData;
		TerrainData terData = ter.terrainData;
		int Sw = srcData.heightmapWidth;
		int Sh = srcData.heightmapHeight;
		float targetX = 0.0f;
		float targetZ = 0.0f;
		float[,] heightMap = srcData.GetHeights(0, 0,  Sw, Sh);
		terData.SetHeights(0, 0, heightMap);

		for(int Ty=0; Ty < Sh; Ty++)
		{
			for(int Tx=0; Tx < Sw; Tx++)
			{
				heightMap[Tx,Ty]=0;
			}
		}
		
		terData.SetHeights(Sw-1, 0, heightMap);
		terData.SetHeights(Sw-1, Sh-1, heightMap);
		terData.SetHeights(0, Sh-1, heightMap);
		
		targetX = srcTer.transform.position.x+srcData.size[0];
		targetZ = srcTer.transform.position.z+srcData.size[2];

		object[] terrains = UnityEngine.Object.FindObjectsOfType(typeof(Terrain));
		foreach (Terrain tmpTerrain in terrains) 
        {
				if(tmpTerrain.transform.position.x==targetX&&tmpTerrain.transform.position.z==srcTer.transform.position.z){
					srcData = tmpTerrain.terrainData;
					heightMap = srcData.GetHeights(0, 0,  Sw, Sh);
					terData.SetHeights(Sw-1, 0, heightMap);
				}
				if(tmpTerrain.transform.position.x==targetX&&tmpTerrain.transform.position.z==targetZ){
					srcData = tmpTerrain.terrainData;
					heightMap = srcData.GetHeights(0, 0,  Sw, Sh);
					terData.SetHeights(Sw-1, Sw-1, heightMap);
				}
				if(tmpTerrain.transform.position.x==srcTer.transform.position.x&&tmpTerrain.transform.position.z==targetZ){
					srcData = tmpTerrain.terrainData;
					heightMap = srcData.GetHeights(0, 0,  Sw, Sh);
					terData.SetHeights(0, Sw-1, heightMap);
				}
        }
	}
	
	
	public void teExport() {
		Terrain ter = (Terrain) GetComponent(typeof(Terrain));
		if (ter == null) {
			return;
		}
		Terrain srcTer = (Terrain) neighborTerrain.GetComponent(typeof(Terrain));
		if (srcTer == null) {
			Debug.Log("You will need to select a piece of terrain first!");
			return;
		}
		TerrainData srcData = srcTer.terrainData;
		TerrainData terData = ter.terrainData;
		int Sw = srcData.heightmapWidth;
		int Sh = srcData.heightmapHeight;
		float targetX = 0.0f;
		float targetZ = 0.0f;
		float[,] heightMap = terData.GetHeights(0, 0,  Sw, Sh);
		srcData.SetHeights(0, 0, heightMap);

		targetX = srcTer.transform.position.x+srcData.size[0];
		targetZ = srcTer.transform.position.z+srcData.size[2];

		object[] terrains = UnityEngine.Object.FindObjectsOfType(typeof(Terrain));
		foreach (Terrain tmpTerrain in terrains) 
        {
				if(tmpTerrain.transform.position.x==targetX&&tmpTerrain.transform.position.z==srcTer.transform.position.z){
					srcData = tmpTerrain.terrainData;
					heightMap = terData.GetHeights(Sw-1, 0,  Sw, Sh);
					srcData.SetHeights(0, 0, heightMap);
				}
				if(tmpTerrain.transform.position.x==targetX&&tmpTerrain.transform.position.z==targetZ){
					srcData = tmpTerrain.terrainData;
					heightMap = terData.GetHeights(Sw-1, Sw-1,  Sw, Sh);
					srcData.SetHeights(0, 0, heightMap);
				}
				if(tmpTerrain.transform.position.x==srcTer.transform.position.x&&tmpTerrain.transform.position.z==targetZ){
					srcData = tmpTerrain.terrainData;
					heightMap = terData.GetHeights(0, Sw-1,  Sw, Sh);
					srcData.SetHeights(0, 0, heightMap);
				}
        }
	}	
		
	// -------------------------------------------------------------------------------------------------------- END
}
