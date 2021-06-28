using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlanePlacing : MonoBehaviour
{

    [SerializeField] private GameObject placingPerfap;
    [SerializeField] private GameObject planeTraget;
    [SerializeField] private GameObject objectToPlaceParent;
    [SerializeField] private GameObject indecator;
    [SerializeField] private Vector2 targetSize;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager arPlane;
    //[SerializeField] PlaneDetictionControl pDControl;
    [SerializeField] private Material[] materials;
    [SerializeField] private ARSessionOrigin aRSO;
    private Pose targetPose;
    private bool canSet;
    private bool planeFound;
    private bool isPlaced;
    private static PlanePlacing instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void UpdatePose()
    {
        if (aRSO is null || raycastManager is null)
            return;
        var screenCenter = aRSO.camera.ViewportToScreenPoint(new Vector2(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        planeFound = hits.Count > 0;
        if (planeFound)
        {
            planeTraget.transform.SetPositionAndRotation(targetPose.position, targetPose.rotation);
            targetPose = hits[0].pose;
            var cameraForward = aRSO.camera.transform.forward;
            var cameraBarrier = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            targetPose.rotation = Quaternion.LookRotation(cameraBarrier);
            ARPlane ARP = arPlane.GetPlane(hits[0].trackableId);
            if(ARP.size.x >=targetSize.x && ARP.size.y >= targetSize.y)
            {
                canSet = true;
                planeTraget.GetComponentInChildren<MeshRenderer>().material = materials[0];
                
            }
            else
            {
                canSet = false;
                planeTraget.GetComponentInChildren<MeshRenderer>().material = materials[1];
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        indecator.transform.localScale = new Vector3(targetSize.x, targetSize.y, indecator.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePose();
    }
}
