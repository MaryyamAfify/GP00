using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
public class PlacementWithMultipleDraggingDroppingController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;

    [SerializeField] FlexibleColorPicker fcp;
    [SerializeField] ColorChanger changer;
    [SerializeField]
    private Camera arCamera;

    [SerializeField] private PlacementObject[] placedObjects;
    [SerializeField] private DrawLine drawLinePrefab;
    private Vector2 touchPosition = default;

   [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField]List<LineHandler> lines;
    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private PlacementObject lastSelectedObject;



    [SerializeField] GraphicRaycaster m_Raycaster;

    [SerializeField] PointerEventData m_PointerEventData;

    [SerializeField] EventSystem m_EventSystem;
    private GameObject PlacedPrefab
    {
        get
        {
            return placedPrefab;
        }
        set
        {
            placedPrefab = value;
        }
    }


    void Awake()
    {
        
       
    }
    bool IsTouchUI() {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);
        return results.Count > 0;
    }
    public void ChangePrefabSelection(int index)
    {
        var _index = Mathf.Clamp(index, 0, placedObjects.Length - 1);
        placedPrefab = placedObjects[_index].gameObject;
    }

    public void ChangeColor() {
        changer.ChangeColor(fcp.color);
    }
    

    void Update()
    {
        if (IsTouchUI()) return;
       
       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                    if (lastSelectedObject != null)
                    {
                        PlacementObject[] allOtherObjects = FindObjectsOfType<PlacementObject>();
                        foreach (PlacementObject placementObject in allOtherObjects)
                        {
                            placementObject.Selected = placementObject == lastSelectedObject;
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lastSelectedObject.Selected = false;
            }
        }

        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (lastSelectedObject == null)
            {
                lastSelectedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation).GetComponent<PlacementObject>();
                HandleLines(lastSelectedObject.gameObject);
            }
            else
            {
                if (lastSelectedObject.Selected)
                {
                    lastSelectedObject.transform.position = hitPose.position;
                    lastSelectedObject.transform.rotation = hitPose.rotation;
                }
            }
        }
    }

    public void HandleLines(GameObject obj) {
        LineHandler line = obj.GetComponent<LineHandler>();
        if (line == null) return;
        for (int i = 0; i < lines.Count; i++)
        {
            var draw = Instantiate(drawLinePrefab, Vector3.zero, Quaternion.identity);
            draw.Draw(line.Pivot, lines[i].Pivot);

        }
        lines.Add(line);
    }
}
