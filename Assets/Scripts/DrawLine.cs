using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer LineRenderer;
    private float counter; // used as a incrementeral element.
    private float distance;
    private GameObject Test;
    private GameObject Test2;
    bool isStart = false;
    //public Transform Tarabeza1; //Originmat
    //public Transform Tarabeza2; //Destination
    [SerializeField] List<Material> materials;
    [SerializeField] float acceptedDistanc = 0f;

    public float lineDrawSpeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        //Test = GameObject.Find("Tarabeza1");
        // Test2 = GameObject.Find("Tarabeza2");

       // acceptedDistanc = 5f;




    }

    public void Draw(Transform o1, Transform o2) {
        Test = o1.gameObject;
        Test2 = o2.gameObject;
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.SetPosition(0, Test.transform.position);
        LineRenderer.SetWidth(.25f, .25f);
        LineRenderer.SetWidth(.25f, .25f);

        distance = Vector3.Distance(Test.transform.position, Test2.transform.position);
        isStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        if (counter < distance)
        {

            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, distance, counter);
            Vector3 pointA = Test.transform.position;
            Vector3 pointB = Test2.transform.position;

            Vector3 LongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            LineRenderer.SetPosition(1, LongLine);
        }
        else {
            LineRenderer.SetPosition(0, Test.transform.position);
            LineRenderer.SetPosition(1, Test2.transform.position);
        }
       
        //if (Test.transform.hasChanged || Test2.transform.hasChanged){

            var dis = Vector3.Distance(Test.transform.position, Test2.transform.position);
            if (dis >= acceptedDistanc)
            {
                LineRenderer.material = materials[0];
            }
            else {
                LineRenderer.material = materials[1];
            }
        //}
    }
}
