using UnityEngine;

namespace VoxelEngine
{
    public class VoxelDatabase : MonoBehaviour
    {
        [SerializeField] private byte iD;
        public static Vector3[] VoxelVertices = new Vector3[8];

        public static Vector3[] VoxelFaceChunks = new Vector3[6];
        public static int[,]    VoxelVertexIndex = new int[6, 4];
        public static Vector2[] VoxelUVs = new Vector2[4];

        public static int[,]    VoxelTris = new int[6, 6];

    
        void Update()
        {
            iD = WorldManager.Instance.myID;
            switch (iD)
            {
                case 0:
                    break;
                case 1:
                    SetToSoil();
                    break;
                case 2:
                    SetToGrass();
                    break;
            }
        }

        public static void SetToGrass()
        {
            VoxelVertices = new Vector3[8]
            {
                new Vector3(0, 0, 0), //0
                new Vector3(1, 0, 0), //1
                new Vector3(0, 0.4f, 0), //2
                new Vector3(1, 0.4f, 0), //3

                new Vector3(0, 0, 1), //4
                new Vector3(1, 0, 1), //5
                new Vector3(0, 0.4f, 1), //6
                new Vector3(1, 0.4f, 1) //7
            };

            VoxelFaceChunks = new Vector3[6]
            {
                new Vector3(0, 0, -1), //back
                new Vector3(0, 0, 1), //front
                new Vector3(-1, 0, 0), //left
                new Vector3(1, 0, 0), //right
                new Vector3(0, -1, 0), //bottom
                new Vector3(0, 1, 0) //top
            };

            VoxelVertexIndex = new int[6, 4]
            {
                { 0, 1, 2, 3 },
                { 4, 5, 6, 7 },
                { 4, 0, 6, 2 },
                { 5, 1, 7, 3 },
                { 0, 1, 4, 5 },
                { 2, 3, 6, 7 }
            };

            VoxelUVs = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };

            VoxelTris = new int[6, 6]
            {
                { 0, 2, 3, 0, 3, 1 },
                { 0, 1, 2, 1, 3, 2 },
                { 0, 2, 3, 0, 3, 1 },
                { 0, 1, 2, 1, 3, 2 },
                { 0, 1, 2, 1, 3, 2 },
                { 0, 2, 3, 0, 3, 1 }
            };
        }
    
        public static void SetToSoil()
        {
            VoxelVertices = new Vector3[8]
            {
                new Vector3(0, -0.2f, 0), //0
                new Vector3(1, -0.2f, 0), //1
                new Vector3(0, 0, 0), //2
                new Vector3(1, 0, 0), //3
    
                new Vector3(0, -0.2f, 1), //4
                new Vector3(1, -0.2f, 1), //5
                new Vector3(0, 0, 1), //6
                new Vector3(1, 0, 1) //7
            };
    
            VoxelFaceChunks = new Vector3[6]
            {
                new Vector3(0, 0, -1), //back
                new Vector3(0, 0, 1), //front
                new Vector3(-1, 0, 0), //left
                new Vector3(1, 0, 0), //right
                new Vector3(0, -1, 0), //bottom
                new Vector3(0, 1, 0) //top
            };
    
            VoxelVertexIndex = new int[6, 4]
            {
                { 0, 1, 2, 3 },
                { 4, 5, 6, 7 },
                { 4, 0, 6, 2 },
                { 5, 1, 7, 3 },
                { 0, 1, 4, 5 },
                { 2, 3, 6, 7 }
            };
    
            VoxelUVs = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1)
            };
    
            VoxelTris = new int[6, 6]
            {
                { 0, 2, 3, 0, 3, 1 },
                { 0, 1, 2, 1, 3, 2 },
                { 0, 2, 3, 0, 3, 1 },
                { 0, 1, 2, 1, 3, 2 },
                { 0, 1, 2, 1, 3, 2 },
                { 0, 2, 3, 0, 3, 1 }
            };
        }
    }
}
