using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WayPointDebugScript : MonoBehaviour
{
	private Transform[] points;

	private void Start()
	{
		points = transform.GetComponentsInChildren<Transform>().Where((t) => t != transform).ToArray();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		if (points == null)
		{
			return;
		}

		for (int i = 0; i < points.Length; i++)
		{
			Gizmos.DrawLine(points[i].position, points[(i+1 >= points.Length) ? 0 : i + 1].position);
		}

	}

}
