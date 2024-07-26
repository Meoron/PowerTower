using UnityEngine;

public static class ProjectileMeshGenerator{
	public static Mesh Generate(float size, float randomOffset){
		Mesh mesh = new Mesh();
		
		randomOffset = size * randomOffset;
		Vector3[] vertices ={
			new Vector3(-size, -size, size) + GetRandomOffset(randomOffset), // 0
			new Vector3(size, -size, size) + GetRandomOffset(randomOffset), // 1
			new Vector3(size, size, size) + GetRandomOffset(randomOffset), // 2
			new Vector3(-size, size, size) + GetRandomOffset(randomOffset), // 3

			new Vector3(-size, -size, -size) + GetRandomOffset(randomOffset), // 4
			new Vector3(size, -size, -size) + GetRandomOffset(randomOffset), // 5
			new Vector3(size, size, -size) + GetRandomOffset(randomOffset), // 6
			new Vector3(-size, size, -size) + GetRandomOffset(randomOffset), // 7
		};

		int[] triangles ={
			0, 2, 1,
			0, 3, 2,

			4, 5, 6,
			4, 6, 7,

			4, 7, 3,
			4, 3, 0,

			1, 2, 6,
			1, 6, 5,

			3, 7, 6,
			3, 6, 2,

			4, 0, 1,
			4, 1, 5
		};
		
		Vector2[] uv ={
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(0, 1),
			
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(0, 1),
		};


		Vector3[] normals = new Vector3[vertices.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			if (i < 4)
				normals[i] = Vector3.forward;
			else
				normals[i] = Vector3.back;
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;
		mesh.normals = normals;

		return mesh;
	}

	private static Vector3 GetRandomOffset(float range){
		return new Vector3(
			Random.Range(-range, range),
			Random.Range(-range, range),
			Random.Range(-range, range)
		);
	}
}