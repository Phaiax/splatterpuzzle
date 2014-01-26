using UnityEngine;
using System.Collections;

public class FogManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject[] fog_rects = GameObject.FindGameObjectsWithTag ("fog_rects");

		float w = 5;
		float h = 5;

		float circle_edge_length = 600.0f / 100.0f;
		float rect_edge_length = 32.0f / 100.0f;

		// left rect
		float factor = circle_edge_length / rect_edge_length;
		Vector3 scale_lr = new Vector3(5 * factor, factor, 1);


		float width = rect_edge_length * scale_lr.x;

		fog_rects [3].transform.localScale = scale_lr;
		fog_rects [3].transform.localPosition = new Vector3(-width/2 - circle_edge_length / 2, 0, 0);

		// right rect
		fog_rects [0].transform.localScale = scale_lr;
		fog_rects [0].transform.localPosition = new Vector3(width/2 + circle_edge_length / 2, 0, 0);
		
		// rect top
		float total_width = 2 * width + circle_edge_length;
		float factor2 = total_width / rect_edge_length;
		Vector3 scale_tb = new Vector3 (factor2, factor2);

		fog_rects [1].transform.localScale = scale_tb;
		fog_rects [2].transform.localScale = scale_tb;

		fog_rects [1].transform.localPosition = new Vector3 (0, total_width / 2 + circle_edge_length / 2, 0);
		fog_rects [2].transform.localPosition = new Vector3 (0, - total_width / 2 - circle_edge_length / 2, 0);

		float alpha = GetComponent<SpriteRenderer> ().color.a;

		foreach (GameObject fog_rect in fog_rects) {
			SpriteRenderer r = fog_rect.GetComponent<SpriteRenderer>();
			Color c = new Color(r.color.r, r.color.g, r.color.b, alpha);
			r.color = c;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
