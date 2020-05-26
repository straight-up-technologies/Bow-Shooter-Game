using UnityEngine;
using System.Collections;

public class cameraZoomController : MonoBehaviour {

	public bool maintainWidth=true;  //what should i maintain
	[Range(-1,1)]
	public int adaptPosition=0;		//in what direction to anchor



	Vector3 cameraPos; //gets default camera position

	float defaultHeight; //needed to keep anchor top or bottom
	float defaultAspectRatio; //needed to keep anchor in the sides
	float defaultWidth = 5 ; //not needed, since could be obtained from other two

	void Start () {
		cameraPos = Camera.main.transform.position; //reference to camera position

		defaultAspectRatio = Camera.main.aspect; //reference to default aspect
		defaultHeight = Camera.main.orthographicSize; //reference to default orthosize
		defaultWidth = Camera.main.aspect * Camera.main.orthographicSize; 
		

		cameraPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {


		//ATTENTION IS ONLY HERE FOR TESTING, AFTER TESTING PUT IT ON THE END
		//of Start() TO AVOID REPETITION

		//maintains width of screen for multiple formats
		if (maintainWidth) {
			Camera.main.orthographicSize = defaultWidth/ Camera.main.aspect;



			Camera.main.transform.position = cameraPos+new Vector3 (
				0,
				adaptPosition*(defaultHeight- Camera.main.orthographicSize),
				-10);


		} else
		//screen height is maintained by default
		{
			



			Camera.main.transform.position = cameraPos+new Vector3 (
				adaptPosition*(defaultAspectRatio*Camera.main.orthographicSize - Camera.main.aspect * Camera.main.orthographicSize),
					0,
					-10);
			
		}
	}
}
