using UnityEngine;
using System.Collections;

public static class TileBuilder {
	private static MeshBuilder BuildFlat(float lane, float height)
	{
		MeshBuilder mb = new MeshBuilder ();
		mb.AddQuad (new Vector3 (0, height, lane),
		            new Vector3 (1, height, lane),
		            new Vector3 (1, height, lane + 1),
		            new Vector3 (0, height, lane + 1)
		            );
		return mb;
	}
	private static MeshBuilder BuildSoftIncrease(float lane, float height)
	{
		MeshBuilder mb = new MeshBuilder ();
		mb.AddQuad (new Vector3 (0, height - 1, lane),
		            new Vector3 (1, height, lane),
		            new Vector3 (1, height, lane + 1),
		            new Vector3 (0, height - 1, lane + 1)
		            );
		return mb;
	}
	private static MeshBuilder BuildHardIncrease(float lane, float height)
	{
		MeshBuilder mb = new MeshBuilder ();
		mb.AddQuad (new Vector3 (0, height - 1, lane),
		            new Vector3 (0, height, lane),
		            new Vector3 (0, height, lane + 1),
		            new Vector3 (0, height - 1, lane + 1)
		            );
		mb.AddQuad (new Vector3 (0, height, lane),
		            new Vector3 (1, height, lane),
		            new Vector3 (1, height, lane + 1),
		            new Vector3 (0, height, lane + 1)
		            );
		return mb;
	}
	private static MeshBuilder BuildSoftDecrease(float lane, float height)
	{
		MeshBuilder mb = new MeshBuilder ();
		mb.AddQuad (new Vector3 (0, height + 1, lane),
		            new Vector3 (1, height, lane),
		            new Vector3 (1, height, lane + 1),
		            new Vector3 (0, height + 1, lane + 1)
		            );
		return mb;
	}
	private static MeshBuilder BuildHardDecrease(float lane, float height)
	{
		MeshBuilder mb = new MeshBuilder ();
		mb.AddQuad (new Vector3 (0, height + 1, lane),
		            new Vector3 (0, height, lane),
		            new Vector3 (0, height, lane + 1),
		            new Vector3 (0, height + 1, lane + 1)
		            );
		mb.AddQuad (new Vector3 (0, height, lane),
		            new Vector3 (1, height, lane),
		            new Vector3 (1, height, lane + 1),
		            new Vector3 (0, height, lane + 1)
		            );
		return mb;
	}
	public static Mesh BuildTile(TileTypes tileType, float lane, float height)
	{
		MeshBuilder tileBuilder;
		switch (tileType) {
			case TileTypes.SoftIncrease:
				tileBuilder = BuildSoftIncrease(lane, height);
			break;
			case TileTypes.HardIncrease:
				tileBuilder = BuildHardIncrease(lane, height);
			break;
			case TileTypes.SoftDecrease:
				tileBuilder = BuildSoftDecrease(lane, height);
			break;
			case TileTypes.HardDecrease:
				tileBuilder = BuildHardDecrease(lane, height);
			break;
			default:
				tileBuilder = BuildFlat(lane, height);
			break;
		}
		//build base
		int count = tileBuilder.NumVertices;
		Vector3 b1 = tileBuilder.GetVertex (count - 1); b1.y = 0;
		Vector3 b2 = tileBuilder.GetVertex (count - 2); b2.y = 0;
		Vector3 b3 = tileBuilder.GetVertex (count - 3); b3.y = 0;
		Vector3 b4 = tileBuilder.GetVertex (count - 4); b4.y = 0;
		tileBuilder.AddQuad (tileBuilder.GetVertex (count - 1), tileBuilder.GetVertex (count - 2), b2, b1);
		tileBuilder.AddQuad (tileBuilder.GetVertex (count - 2), tileBuilder.GetVertex (count - 3), b3, b2);
		tileBuilder.AddQuad (tileBuilder.GetVertex (count - 3), tileBuilder.GetVertex (count - 4), b4, b3);
		tileBuilder.AddQuad (tileBuilder.GetVertex (count - 4), tileBuilder.GetVertex (count - 1), b1, b4);
		Mesh tile = tileBuilder.CreateMesh ();
		//Vector2[] uv = new Vector2[tile.vertices.Length];
		//for (int i = 0; i < uv.Length; i++)
		//	uv [i] = new Vector2 (0, 0);
		//tile.uv = uv;
		return tile;
	}
}

public class GroundMesh : MonoBehaviour {
	public int LifeSpan;
	public TileTypes TileType;
	public int Lane;
	public int Height;
	public float ScrollRate;
	MeshFilter filter;
	Mesh m;

	// Use this for initialization
	void Start () {
	}

	public void GenerateMesh() {
		filter = GetComponent<MeshFilter>();
		//m = TileBuilder.BuildTile (TileType, Lane, Height);
		if (filter != null)
			filter.mesh = TileBuilder.BuildTile (TileType, Lane, Height);
		GetComponent<MeshCollider> ().sharedMesh = filter.mesh;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = this.transform.position;
		position.x -= ScrollRate;
		this.transform.position = position;
		LifeSpan--;
	}
}
