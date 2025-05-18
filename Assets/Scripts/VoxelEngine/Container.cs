using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelEngine
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Container : MonoBehaviour
    {
        public Vector3 containerPosition;
    
        [SerializeField]private Dictionary<Vector3, Voxel> data;
        private MeshData meshData = new MeshData();

        private MeshRenderer meshRenderer;
        private MeshFilter meshFilter;
        private MeshCollider meshCollider;

        public void Initialize(Material mat, Vector3 position)
        {
            ConfigureComponent();
            data = new Dictionary<Vector3, Voxel>();
            meshRenderer.sharedMaterial = mat;
            containerPosition = position;
        }
    
        public void ClearData() {
            data.Clear();
        }

        private void ConfigureComponent()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }

        public void GenerateMesh()
        {
            meshData.ClearData();
        
            Vector3 blockPos;
            Voxel block = default;

            int counter = 0;
            Vector3[] faceVertices = new Vector3[4];
            Vector3[] faceUVs = new Vector3[4];

            VoxelColor voxelColor;
            Color voxelColorAlpha;
            Vector2 voxelSmoothness;
            
            foreach (KeyValuePair<Vector3, Voxel> kvp in data)
            {
                if (kvp.Value.ID == 0){ 
                    continue;
                }
                blockPos = kvp.Key;
                block = kvp.Value;
                if(block.ID == 1) {
                    //Change to Soil Mesh
                    VoxelDatabase.SetToSoil();
                }
                if(block.ID == 2) {
                    //Change to Grass Mesh
                    VoxelDatabase.SetToGrass();
                }

                voxelColor = WorldManager.Instance.WorldColors[block.ID - 1];
                voxelColorAlpha = voxelColor.color;
                voxelColorAlpha.a = 1;
                voxelSmoothness = new Vector2(voxelColor.metallic, voxelColor.smoothness);
                //Interate over each face Direction
                for (int i = 0; i < 6; i++)
                {
                    //Loop through each face to check is this block at this location is facing aany solid block
                    //basically if the block face to the voxelFaceChunkDirections is not an empty voxel it doesn't  draw the face
                    //what we want is to check if the block to the facechunk is the same block (have the same ID)
                
                    if (this[blockPos + VoxelDatabase.VoxelFaceChunks[i]].isSolid)
                        continue;
                    
                    
                    
                    
                    
                    

                    //Draw this face
                    //Collect the appropriate vertices from the default vertices and add the block position
                    for (int j = 0; j < 4; j++)
                    {
                        faceVertices[j] = VoxelDatabase.VoxelVertices[VoxelDatabase.VoxelVertexIndex[i, j]] + blockPos;
                        faceUVs[j] = VoxelDatabase.VoxelUVs[j];
                    }

                    for (int j = 0; j < 6; j++)
                    {
                        meshData.vertices.Add(faceVertices[VoxelDatabase.VoxelTris[i, j]]);
                        meshData.UVs.Add(faceUVs[VoxelDatabase.VoxelTris[i, j]]);
                        meshData.colors.Add(voxelColorAlpha);
                        meshData.UVs2.Add(voxelSmoothness);
                    
                        meshData.triangles.Add(counter++);
                    }
                }
            }
        }

        public void UploadMesh()
        {
            meshData.UploadMesh();

            if (meshRenderer == null)
                ConfigureComponent();

            meshFilter.mesh = meshData.mesh;
            if (meshData.vertices.Count > 3)
                meshCollider.sharedMesh = meshData.mesh;
        }
    
        public Voxel this[Vector3 index]
        {
            get
            {
                if (data.ContainsKey(index))
                    return data[index];
                else
                    return emptyVoxel;
            }
        
            set
            {
                if (data.ContainsKey(index))
                    data[index] = value;
                else
                    data.Add(index, value);
            }
        }

        public static Voxel emptyVoxel = new Voxel() { ID = 0 };
        public static Voxel soilVoxel = new Voxel() { ID = 1 };
        public static Voxel grassVoxel = new Voxel() { ID = 2 };

        #region Mesh_Data

        public struct MeshData
        {
            public Mesh mesh;
            public List<Vector3> vertices;
            public List<int> triangles;
            public List<Vector2> UVs;
            public List<Vector2> UVs2;
            public List<Color> colors;

            public bool Initialized;

            public void ClearData()
            {
                if (!Initialized)
                {
                    vertices = new List<Vector3>();
                    triangles = new List<int>();
                    UVs = new List<Vector2>();
                    UVs2 = new List<Vector2>();
                    colors = new List<Color>();

                    Initialized = true;
                    mesh = new Mesh();
                }
                else
                {
                    vertices.Clear();
                    triangles.Clear();
                    UVs.Clear();
                    UVs2.Clear();
                    colors.Clear();
                    mesh.Clear();
                }
            }

            public void UploadMesh(bool sharedVertices = false)
            {
                mesh.SetVertices(vertices);
                mesh.SetTriangles(triangles, 0, false);
                mesh.SetColors(colors);
            
                mesh.SetUVs(0, UVs);
                mesh.SetUVs(2, UVs2);

                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
                mesh.UploadMeshData(false);
            }
        }

        #endregion


        public void GetNeighbourVoxels()
        {
        }
    }
}