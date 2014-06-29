using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileMarkovChain
{
	//Random random;
	float[] data;
	int maxHeight;
	static int enumSize = 5;
	
	public TileMarkovChain(string fileName, int maxHeight)
	{
		data = new float[enumSize * enumSize];
		TextReader tr = new StreamReader(fileName);
		for (int i = 0; i < enumSize; i++)
		{
			string[] strs = tr.ReadLine().Split(' ');
			for (int j = 0; j < enumSize; j++)
				data[i * enumSize + j] = System.Convert.ToSingle(strs[j]);
		}
		//random = new Random();
		this.maxHeight = maxHeight;
	}
	
	public TileTypes NextType(TileTypes tileType, int height)
	{
		float factor = Random.value;//(float)random.NextDouble();
		int baseIndex = enumSize * (int)tileType;
		TileTypes ret = (TileTypes)0;
		for (int i = 0; i < enumSize; i++)
			if (factor < data[baseIndex + i])
		{
			ret = (TileTypes)i;
			break;
		}
		else
			factor -= data[baseIndex + i];
		int candidateMod = TileHeightMod(ret) + height;
		if (candidateMod < 0 || candidateMod > maxHeight)
			return (TileTypes)0;
		return ret;
	}
	
	public static int TileHeightMod(TileTypes t)
	{
		switch (t)
		{
		case TileTypes.SoftIncrease:
		case TileTypes.HardIncrease:
			return 1;
		case TileTypes.SoftDecrease:
		case TileTypes.HardDecrease:
			return -1;
		default:
			return 0;
		}
	}
	
	public static char TileRep(TileTypes t)
	{
		switch (t)
		{
		default:
			return '-';
		case TileTypes.SoftIncrease:
			return '/';
		case TileTypes.HardIncrease:
			return '[';
		case TileTypes.SoftDecrease:
			return '\\';
		case TileTypes.HardDecrease:
			return ']';
		}
	}
}

public enum TileTypes
{
	Flat, SoftIncrease, SoftDecrease, HardIncrease, HardDecrease
}

public struct LaneNode
{
	public int Height { get; set; }
	public TileTypes TileType { get; set; }
	
	public override string ToString()
	{
		return string.Format("{0}@{1}", TileMarkovChain.TileRep(TileType), Height);
	}
}

class LaneManager
{
	TileMarkovChain tmc;
	LaneNode[] previous;
	LaneNode[] current;
	int nodes;
	int numInitialFlatNodes;
	int maxHeight;
	
	public LaneManager(TileMarkovChain tmc, int numLanes, int numInitialFlatNodes, int maxHeight)
	{
		this.tmc = tmc;
		previous = new LaneNode[numLanes];
		current = new LaneNode[numLanes];
		this.numInitialFlatNodes = numInitialFlatNodes;
		this.maxHeight = maxHeight;
		nodes = 0;
	}
	
	private void SwapBuffers()
	{
		LaneNode[] temp = previous;
		previous = current;
		current = temp;
	}
	
	public void GenerateNewRow()
	{
		SwapBuffers();
		if (nodes < numInitialFlatNodes)
			for (int i = 0; i < current.Length; i++)
		{
			current[i].TileType = TileTypes.Flat;
			current[i].Height = 1;
		}
		else
			for (int i = 0; i < current.Length; i++)
		{
			current[i].TileType = tmc.NextType(previous[i].TileType, previous[i].Height);
			current[i].Height = previous[i].Height + TileMarkovChain.TileHeightMod(current[i].TileType);
		}
		nodes++;
	}
	
	public LaneNode GetLaneNode(int laneID)
	{
		return current[laneID];
	}
	
	public int NumLanes
	{
		get
		{
			return current.Length;
		}
	}
	
	public override string ToString()
	{
		string s = "";
		for (int i = 0; i < current.Length; i++)
		{
			s += current[i].ToString() + " ";
		}
		return s;
	}
}


public class MeshBuilder {
	List<Vector3> vertices;
	List<int> triangles;
	List<Vector2> texCoords;

	public MeshBuilder()
	{
		vertices = new List<Vector3> ();
		triangles = new List<int> ();
		texCoords = new List<Vector2> ();
	}

	public void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		triangles.Add (vertices.Count);
		vertices.Add (v1);
		triangles.Add (vertices.Count);
		vertices.Add (v3);
		triangles.Add (vertices.Count);
		vertices.Add (v2);
	}
	
	public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
	{
		triangles.Add (vertices.Count);
		triangles.Add (vertices.Count + 2);
		triangles.Add (vertices.Count + 1);
		triangles.Add (vertices.Count + 3);
		triangles.Add (vertices.Count + 2);
		triangles.Add (vertices.Count);
		vertices.Add (v1);
		vertices.Add (v2);
		vertices.Add (v3);
		vertices.Add (v4);
	}

	public Mesh CreateMesh()
	{
		Mesh m = new Mesh ();
		m.vertices = vertices.ToArray ();
		m.triangles = triangles.ToArray ();
		m.RecalculateNormals();
		m.RecalculateBounds();
		m.Optimize();
		return m;
	}

	public Vector3 GetVertex(int index)
	{
		return vertices[index];
	}

	public int NumVertices { get { return vertices.Count; } }
}

public class GroundLaneManager : MonoBehaviour {
	public int NumLanes;
	public int MaxHeight;
	public int RunwayLength;
	public int GenerationInterval;
	public int BlocksPerUnit;
	public GroundMesh MeshPrefab;
	TileMarkovChain markov;
	LaneManager lm;
	int time;
	LinkedList<GroundMesh> destructionQueue;

	// Use this for initialization
	void Start () {
		markov = new TileMarkovChain ("Assets/Markov/input.txt", MaxHeight);
		lm = new LaneManager(markov, NumLanes, 10, MaxHeight);
		destructionQueue = new LinkedList<GroundMesh> ();
		for (int i = 0; i < RunwayLength * BlocksPerUnit; i++)
			GenerateNewRow (-RunwayLength + (float)i / BlocksPerUnit);
	}

	private void GenerateNewRow(float offset)
	{
		if (lm != null)
			lm.GenerateNewRow();
		for (int i = 0; i < NumLanes; i++) {
			GroundMesh g = (GroundMesh)Instantiate (MeshPrefab);
			g.transform.position = new Vector3(10 + offset, -2, -5);
			g.transform.localScale = new Vector3(1, 1, 5) / BlocksPerUnit;
			g.LifeSpan = GenerationInterval * RunwayLength * BlocksPerUnit;
			g.ScrollRate = 1f / GenerationInterval / BlocksPerUnit;
			g.transform.parent = this.transform;
			LaneNode ln = lm.GetLaneNode(i);
			g.TileType = ln.TileType;
			g.Lane = i;
			g.Height = ln.Height;
			g.GenerateMesh();
			destructionQueue.AddLast (g);
			//set tile type
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (time == 0)
		{
			GenerateNewRow(0);
			time = GenerationInterval;
		}
		time--;
		//go through the queue and delete stale blocks
		while (destructionQueue.Count > 0 && destructionQueue.First.Value.LifeSpan < 0) {
			Destroy (destructionQueue.First.Value.gameObject);
			destructionQueue.RemoveFirst();
		}
	}
}
