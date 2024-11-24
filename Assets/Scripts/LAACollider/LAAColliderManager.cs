using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LAAColliderManager : MonoBehaviour
{
    [SerializeField] private GameObject timelapseList;

    public GameObject previousTimelapseCase;

    [SerializeField] GameObject contourWarning;

    public GameObject currentDeviceDisk;
    public GameObject currentDeviceLobe;

    private static LAAColliderManager _instance;
    public static LAAColliderManager Instance
    {
        get { return _instance; }
    }

    public void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContourWarning(bool state)
    {
        contourWarning.SetActive(state);
    }

    public void SetDevice(string deviceSize)
    {
        currentDeviceDisk = GameObject.Find("Amulet_July_Disk_" + deviceSize);
        currentDeviceLobe = GameObject.Find("Amulet_July_Lobe_" + deviceSize);
    }

    /*
     * 
     Case 0: Correct Collider, Correct Size
     Case 1: Correct Collider, Small Size
     Case 2: Correct Collider, Large Size
     Case 3: Incorrect Collider, Correct Size
     Case 4: Incorrect Collider, Small Size
     Case 5: Incorrect Collider, Large Size
     *
     */
    public void displayTimelapseCase(int caseTimelapse)
    {
        if (previousTimelapseCase != null) {
            previousTimelapseCase.SetActive(false);
        }

        timelapseList.transform.GetChild(caseTimelapse).gameObject.SetActive(true);
        previousTimelapseCase = timelapseList.transform.GetChild(caseTimelapse).gameObject;
    }
}
