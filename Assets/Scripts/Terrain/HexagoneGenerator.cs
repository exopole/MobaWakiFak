/*
 * source : https://forum.unity.com/threads/open-source-procedural-hexagon-terrain.233296/
*/

using UnityEngine;

namespace Terrain
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class HexagoneGenerator : MonoBehaviour
    {
        public Texture texture;
        public bool HasElement;
        private Mesh _mesh;
        private Vector3[] _vertices;
        private Vector2[] _uv;
        private int[] _triangles;

        public HexagoneGenerator NE, E, SE, SO, O, NO;
        // Start is called before the first frame update
        void Start()
        {
            SetupMesh();
        }

        public void SetupMesh()
        {
            //create a mesh object to pass our data into
            _mesh = new Mesh();
            //set the GO's meshFilter's mesh to be the one we just made
            GetComponent<MeshFilter>().mesh = _mesh;

            CreateShape();
            UpdateMesh();
        }

        public Vector2 GetPosition2D()
        {
            var position = transform.position;
            return new Vector2(position.x, position.z);
        }

        private void UpdateMesh()
        {
            //clear the mesh 
            _mesh.Clear();
            //add our vertices to the mesh
            _mesh.vertices = _vertices;
            //add our triangles to the mesh
            _mesh.triangles = _triangles;
            //add out UV coordinates to the mesh
            _mesh.uv = _uv;
        
            //make it play nicely with lighting
            _mesh.RecalculateNormals();
        
            //UV TESTING
            GetComponent<Renderer>().material.mainTexture = texture;

            GetComponent<MeshCollider>().sharedMesh = _mesh;
        }

        private void CreateShape()
        {
            #region verts
            float floorLevel = 0;
            _vertices = new Vector3[]
            {
                new Vector3(-1f , floorLevel, -.5f),
                new Vector3(-1f, floorLevel, .5f),
                new Vector3(0f, floorLevel, 1f),
                new Vector3(1f, floorLevel, .5f),
                new Vector3(1f, floorLevel, -.5f),
                new Vector3(0f, floorLevel, -1f)
            };
            #endregion
 
            #region triangles
            _triangles = new int[]
            {
                1,5,0,
                1,4,5,
                1,2,4,
                2,3,4
            };

            #endregion
 
            #region uv
            _uv = new Vector2[]
            {
                new Vector2(0,0.25f),
                new Vector2(0,0.75f),
                new Vector2(0.5f,1),
                new Vector2(1,0.75f),
                new Vector2(1,0.25f),
                new Vector2(0.5f,0),
            };
            #endregion
 
            #region finalize
 
            #endregion
        }
    }
}
