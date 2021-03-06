using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    // T12 we will need another arrayto store the Shapes being used, under the NEXT label.  The Next shape to be used in the gane,
    // will be diplayed, under the Next label. The shape will not have any movement, because we removed the script component, so that it wouldn't.
    public GameObject[] nextShapes;

    // T12 refer to the GameObject that will represent our nextShapes
    GameObject upNextObject = null;

    // T12 shape index
    int shapeIndex = 0;

    // T12 NextShape index
    int nextShapeIndex = 0;

    // T8 We will need to randomly open up shapes
    // there are a total of 7 shapes

    // T8 We will need a place to store those shapes...
    // this is public so we can put them in the array, after being defined in Unity
    // in an array of GameObjects[]
    public GameObject[] shapes;

    // T8 spawn these random shapes function
    public void SpawnShape()
    {
        // T8 generate a random index between 0 and 6, to accomidate the 7 shapes
        // T12 we will generate the random index, in nextShapeIndex below, shapeIndex will equal the nextShapeIndex.
        //int shapeIndex = Random.Range(0, 7);
        int shapeIndex = nextShapeIndex;

        // This will show it on the screen, call shapes and in particular,
        // the random shapeIndex number the shape is assigned to.
        // transform.position will spawn at the position determined by where our shape spawner currently is on the screen
        // Quaternion.identity handles our rotation, which we are not using
        Instantiate(shapes[shapeIndex], transform.position, Quaternion.identity);

        // T12 generate a random index between 0 and 6, to accomidate the 7 shapes
        nextShapeIndex = Random.Range(0, 7);

        // T12 define where the next shape will be positioned
        Vector3 nextShapePosition = new Vector3(-7.4f, 17.8f, 0f);

        // T12 destroy the former Next screens sprite, when the next one is spawned, 
        // as not to have a pile of sprites stacked on one another, destroy all of them as we print a new shape.
        if(upNextObject != null)
        {
            Destroy(upNextObject);
        }

        // T12 create the next shape, determined by the random generated shapes, and identified by its index number, 
        // then position it, where we defined above, and handle the rotation with Quaternion.identity
        upNextObject = Instantiate(nextShapes[nextShapeIndex], nextShapePosition, Quaternion.identity);
    }

    // T8 Use this for initialization
    // Start is called before the first frame update
    void Start()
    {
        // T12 generate a random shape to use in the Next shape then in the game
        nextShapeIndex = Random.Range(0, 7);

        // T8 test by calling SpawnShape() as soon as the program starts(test this in unity then uncomment it)
        SpawnShape();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
