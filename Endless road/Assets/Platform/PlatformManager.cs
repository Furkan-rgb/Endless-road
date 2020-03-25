using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    public Transform prefab;
	public int numberOfObjects;
    public float recycleOffset;
    public Vector3 minSize, maxSize, minGap, maxGap;
    public Vector3 startPosition;
    public float minY, maxY;

	private Vector3 nextPosition;
    private Queue<Transform> objectQueue; //wachtrij voor de cubes om te recyclen

	void Start () {
        objectQueue = new Queue<Transform>(numberOfObjects); //wachtrij aanmaken

        for (int i = 0; i < numberOfObjects; i++) 
        {
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		}

		nextPosition = startPosition; //beginplek voor het spawnen van cubes

            //loop voor het spawnen van eerste reeks cubes
        	for (int i = 0; i < numberOfObjects; i++) {
                Recycle(); //recyclen is in dit geval hetzelfde als nieuw aanmaken
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
		    Recycle();
		}
    }

	private void Recycle () { //Methoden voor het genereren en plaatsen van cubes

    	Vector3 scale = new Vector3( //min en max van random cubes
		Random.Range(minSize.x, maxSize.x),
		Random.Range(minSize.y, maxSize.y),
		Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPosition;//positie van cubes
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);

        	nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));
        //zorgt ervoor dat de gap tussen de min en max waardes blijft
		if(nextPosition.y < minY){
			nextPosition.y = minY + maxGap.y;
		}
		else if(nextPosition.y > maxY){
			nextPosition.y = maxY - maxGap.y;
		}
	}

}
