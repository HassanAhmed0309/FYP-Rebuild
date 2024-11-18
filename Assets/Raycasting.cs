//using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Raycasting : MonoBehaviour
{
    // public GameObject Sphere;
    public Material[] SkyboxMaterial;
    public GameObject[] hotspot;
    public GameObject[] itabs_icons;
    public GameObject[] itabs;


    public float delay = 10f;
    float timer;
    string GameObjName;
    public LayerMask teleportMask;
    Dictionary<string, List<int>> SkyboxToHotspotMapping = new Dictionary<string, List<int>>(30);

    Transform origin;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    float rayDistance = 100f;
    RaycastHit hit;

    public Sprite[] S = new Sprite[2];
    // Start is called before the first frame update
    void Start()
    {
        /* Complete Lecture Theater */
        SkyboxToHotspotMapping["Back_LT"] = new List<int> { 0 };
        SkyboxToHotspotMapping["Middle_LT"] = new List<int> { 1, 2, 3, 4 };
        SkyboxToHotspotMapping["Entrance_LT"] = new List<int> { 0, 2, 5 };
        SkyboxToHotspotMapping["Dice_LT"] = new List<int> { 2 };
        SkyboxToHotspotMapping["Board_LT"] = new List<int> { 0, 3, 4 };
        SkyboxToHotspotMapping["Outside_LT"] = new List<int> { 4, 6 };

        /* Corridor from LT to Glass Door */
        SkyboxToHotspotMapping["Reception_Corridor"] = new List<int> { 5, 7 };
        SkyboxToHotspotMapping["Auditorium_Corridor"] = new List<int> { 6, 8 };
        SkyboxToHotspotMapping["OneWindowOffice_Corridor"] = new List<int> { 7, 15 };

        /* From Glass Door to Main lift */
        SkyboxToHotspotMapping["Tables_Walkway"] = new List<int> { 8, 14 };
        SkyboxToHotspotMapping["GreyCarpet_Walkway"] = new List<int> { 13, 15 };
        SkyboxToHotspotMapping["OutsideLibrary_Walkway"] = new List<int> { 12, 14, 16 };
        SkyboxToHotspotMapping["MainLift_Walkway"] = new List<int> { 11, 13, 17 };

        /* Complete Library */
        SkyboxToHotspotMapping["Entrance_Library"] = new List<int> { 13 };

        /* Complete CSDepartment */
        SkyboxToHotspotMapping["Entrance_CSDepartment"] = new List<int> { 12, 18 };
        SkyboxToHotspotMapping["Dr.Mohsen_CSDepartment"] = new List<int> { 17 };

        /* Main Entrance to Elevator */
        SkyboxToHotspotMapping["Arfa_MainEntrance"] = new List<int> { 10 };
        SkyboxToHotspotMapping["InsideArfa_MainEntrance"] = new List<int> { 9, 11 };
        SkyboxToHotspotMapping["Elevator_MainEntrance"] = new List<int> { 10, 12 };
    }

    // Update is called once per frame
    void Update()
    {
        origin = this.transform;
        rayOrigin = transform.position;
        rayDirection = transform.forward.normalized;
        rayDistance = 100f;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, teleportMask))
        {
            if (hit.collider.gameObject.tag == "Hotspot")
            {
                Debug.DrawLine(origin.position, hit.point, Color.red);
                // Debug.Log("hit " + hit.collider.gameObject.tag);
                timer += Time.deltaTime;
                if (timer > delay)
                {
                    //Debug.Log("Here I am");
                    var ind = ChangeSkyboxTo(hit.collider.gameObject.name, SkyboxMaterial);
                    TurnHotspotsON(SkyboxMaterial[ind].name, ind);
                    timer = 0f;
                }
            }
        }
        else
        {
            for (int j = 0; j < itabs_icons.Length; j++)
            {
                itabs[j].SetActive(false);
            }
        }

        turnOnItabs();
    }

    void turnOnItabs()
    { 
        if(Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, teleportMask))
        {
            if(hit.collider.gameObject.tag == "Entrance")
            {
                itabs[0].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Inside Entrance")
            {
                itabs[1].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Lift Area")
            {
                itabs[2].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "6th Floor")
            {
                itabs[3].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Down Lift")
            {
                itabs[4].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Dr.Mohsen")
            {
                itabs[5].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Dr.Ibrahim")
            {
                itabs[6].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Library")
            {
                itabs[7].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Grey Carpet")
            {
                itabs[8].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "One Window Office")
            {
                itabs[9].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Auditorium")
            {
                itabs[10].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Admission Office")
            {
                itabs[11].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "Facilitation Desk")
            {
                itabs[12].SetActive(true);
            }
            else if (hit.collider.gameObject.tag == "LT")
            {
                itabs[13].SetActive(true);
            }

        }
    }

    void TurnHotspotsON(string Skybox_Name, int n)
    {
        Quaternion Q;
        TextMeshPro G;
        hotspot[n].SetActive(false);
        //Closing all working hotspots
        foreach (var x in hotspot)
        {
            x.SetActive(false);
        }
        //Turning on hotspots relevant to the Skybox
        foreach (int num in SkyboxToHotspotMapping[Skybox_Name])
        {
            hotspot[num].SetActive(true);
            Debug.Log("Turning on " + hotspot[num].name);
        }
        //The location of each hotspot relevant to the skybox
        switch (Skybox_Name)
        {
            case "GreyCarpet_Walkway":
                /* OutsideLibrary_Walkway */
                //Vector3(-13.6379995f,2.8829999f,22.8460007f) pos
                //Vector3(89.972023f,276.400024f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(89.972023f, 276.400024f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.6379995f, 2.8829999f, 22.8460007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Table_Walkway */
                //Vector3(-16.5429993f,2.92799997f,23.1229992f) pos
                //Vector3(90f,95.1999817f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 95.1999817f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.5429993f, 2.92799997f, 23.1229992f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                G.text = "Entrance";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, -0.74000001f, -0.419999987f), Q);

                //activate itab_MainLift
                itabs_icons[8].SetActive(true);

                break;
            case "OutsideLibrary_Walkway":
                /* MainLift_Walkway */
                //Vector3(-13.8789997f,2.87700009f,22.6110001f) pos
                //Vector3(90f,284.339996f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 284.339996f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.8789997f, 2.87700009f, 22.6110001f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Main Lift Area";
                Q = Quaternion.Euler(new Vector3(83.0088348f, 104.695732f, 283.741028f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.19999981f, -2.77999997f, 1.94000006f), Q);

                /* GreyCarpet_Walkway */
                //Vector3(-15.4189997f,2.65499997f,21.2999992f) pos
                //Vector3(90f,17.6000004f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 17.6000004f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.4189997f, 2.65499997f, 21.2999992f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                G.text = "Grey Carpet";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.26000023f, 0, 1.71000004f), Q);

                /* Entrance_Library */
                //Vector3(-16.5020008f,2.88400006f,23.2700005f) pos
                //Vector3(0,270f,180f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0, 270f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.5020008f, 2.88400006f, 23.2700005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponentInChildren<TextMeshPro>();
                G.text = "Library";
                Q = Quaternion.Euler(new Vector3(180, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, -0.639999986f, 0), Q);
                SpriteRenderer Si = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponent<SpriteRenderer>();
                Si.sprite = S[1];

                //activate itab_MainLift
                itabs_icons[3].SetActive(false);
                
                //activate itab_MainLift
                itabs_icons[4].SetActive(false);

                //activate itab_MainLift
                itabs_icons[7].SetActive(false);

                //activate itab_MainLift
                itabs_icons[8].SetActive(false);
       
                break;
            case "MainLift_Walkway":
                /* Elevator_MainEntrance */
                //Vector3(-14.8170004f,3.11400008f,21.0540009f) pos
                //Vector3(0f,171.199997f,2.99999762f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0f, 171.199997f, 2.99999762f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-14.8170004f, 3.11400008f, 21.0540009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "First Floor";
                Q = Quaternion.Euler(new Vector3(357.194916f, 0.56882596f, 359.766144f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(5.61000013f, -0.899999976f, -0.119999997f), Q);

                /* OutsideLibrary_Walkway */
                //Vector3(-16.2999992f,2.79200006f,22.6420002f) pos
                //Vector3(90f,83.6999893f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 83.6999893f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.2999992f, 2.79200006f, 22.6420002f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                G.text = "Library";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.560000002f), Q);

                /* Entrance_CSDepartment */
                //Vector3(-13.4189997f,2.65199995f,23.0799999f) pos
                //Vector3(90f,265.070007f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 265.070007f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.4189997f, 2.65199995f, 23.0799999f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponentInChildren<TextMeshPro>();
                G.text = "CS Department";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-8.89999962f, 0, 1.82000005f), Q);

                //Deactivate itab_Lift
                itabs_icons[3].SetActive(true);

                //deactivate itab_Dr.Mohsen
                itabs_icons[4].SetActive(true);

                //Deactivate itab_Lift
                itabs_icons[2].SetActive(false);

                itabs_icons[7].SetActive(false);


                break;
            case "Entrance_Library":
                /* OutsideLibrary_Walkway */
                //Vector3(-17.1450005f,3.03699994f,23.1879997f) pos
                //Vector3(0.899999201f,270f,177.600006f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0.899999201f, 270f, 177.600006f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-17.1450005f, 3.03699994f, 23.1879997f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(0, 0, 180));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, -0.720000029f, -0.5f), Q);

                Si = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponent<SpriteRenderer>();
                Si.sprite = S[1];

                //activate itab_MainLift
                itabs_icons[7].SetActive(true);
                break;
            case "Entrance_CSDepartment":
                /* MainLift_Walkway */
                //Vector3(-15.7559996f,2.62199998f,21.4050007f) pos
                //Vector3(90f,27.5f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 27.5f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.7559996f, 2.62199998f, 21.4050007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.77999973f, 0, 1.76999998f), Q);

                /* Middle_CSDepartment */
                //Vector3(-13.7209997f,2.546f,22.0480003f) pos
                //Vector3(90f,297.899994f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 297.899994f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.7209997f, 2.546f, 22.0480003f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                G.text = "Move Forward";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.07999992f, 0, 1.77999997f), Q);

                //activate itab_MainLift
                itabs_icons[3].SetActive(false);
  
                //activate itab_MainLift
                itabs_icons[4].SetActive(false);

                //activate itab_MainLift
                itabs_icons[5].SetActive(false);
                //activate itab_MainLift
                itabs_icons[6].SetActive(false);

                break;
            case "Dr.Mohsen_CSDepartment":
                /* Entrance_CSDepartment */
                //Vector3(-13.4090004f,2.61400008f,23.0300007f) pos
                //Vector3(90f,270f,0) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 270f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.4090004f, 2.61400008f, 23.0300007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Move Forward";

                //activate itab_MainLift
                itabs_icons[5].SetActive(true);
                itabs_icons[6].SetActive(true);
                break;

            case "Back_LT":
                /* Middle_LT */
                //Vector3(-13.7799997f,2.5710001f,23.2080002f) pos
                //Vector3(90,280,0) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 280, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.7799997f, 2.5710001f, 23.2080002f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new 
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                Q = Quaternion.Euler(new Vector3(80.0000153f, 90, 270));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.600000024f), Q);
                break;
            case "Middle_LT":
                /* Back_LT */
                //Vector3(-17.4249992f, 2.10800004f, 23.8099995f) pos
                //Vector3(90,113.700012f,0) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(90, 113.700012f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-17.4249992f, 2.10800004f, 23.8099995f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new 
                    Vector3(0.5f, 0.5f, 0.5f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                //G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.629999995f), Q);

                /* Board_LT */
                // Vector3(-11.4200001f, 2.00999999f, 20.8400002f) pos
                //Vector3(90,294.800018f,0) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(90, 294.800018f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-11.4200001f, 2.00999999f, 20.8400002f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new 
                    Vector3(0.5f, 0.5f, 0.5f);

                /* Dice_LT */
                //Vector3(-8.68999958f,2.3499999f,23.3400002f) pos
                //Vector3(0,268.100006f,0) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(0, 268.100006f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-8.68999958f, 2.3499999f, 23.3400002f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new 
                    Vector3(0.5f, 0.5f, 0.5f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponentInChildren<TextMeshPro>();
                //G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(0, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 1.16999996f, 2.95000005f), Q);

                /* Entrance_LT */
                //Vector3(-13.0600004f,3.02999997f,20.25f) pos
                //Vector3(0f,324.679993f,0f) rot
                //Vector3(0.25f,0.25f,0.25f) 
                Q = Quaternion.Euler(new Vector3(0, 130.5f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.0600004f, 3.02999997f, 20.25f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].transform.localScale = new 
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].GetComponentInChildren<TextMeshPro>();
                Q = Quaternion.Euler(new Vector3(0, 0, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-0.899999976f, 0.899999976f, -3.29999995f), Q);
                break;

            case "Entrance_LT":
                /* Middle_LT */
                //Vector3(-13.7259998f, 2.86100006f, 21.8789997f) pos
                //Vector3(282.700012f,312.199982f,180f) rot
                //Vector3(0.200000003f,0.200000003f,0.200000003f) scale
                Q = Quaternion.Euler(new Vector3(282.700012f, 270, 180));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.7259998f, 2.86100006f, 21.8789997f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new 
                    Vector3(0.200000003f, 0.200000003f, 0.200000003f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();

                Q = Quaternion.Euler(new Vector3(90f, 0, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-1.05999994f, 1.13999999f, 1.22000003f), Q);

                /* Board_LT */
                //Vector3(-14.5799999f,0.109999999f,17.4899998f) pos
                //Vector3(90f,0f,0f) rot
                //Vector3(1,1,1) scale
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.5799999f, 0.109999999f, 17.4899998f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new 
                    Vector3(1f, 1f, 1f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                //G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.670000017f), Q);

                /* Outside_LT */
                //Vector3(-16.5139999,2.72600007,22.4899998)
                //Vector3(-15.7270002f,2.72600007f,22.5890007f) pos
                //Vector3(0f,82.7900009f,0f) rot
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0f, 82.7900009f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.5139999f, 2.72600007f, 22.5890007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new 
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponentInChildren<TextMeshPro>();
                G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(0, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0.829999983f, 0), Q);

                Si = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponent<SpriteRenderer>();
                Si.sprite = S[1];
                break;

            case "Dice_LT":
                //0 = Board_LT
                //Vector3(-17.3700008f, 1.00999999f, 18.6100006f) pos
                //Vector3(90,36.2000046f,0) rot
                Q = Quaternion.Euler(new Vector3(90, 36.2000046f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation( new
                    Vector3(-17.3700008f, 1.00999999f, 18.6100006f), Q);
                
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                //G.text = "Exit";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-0.0299999993f, 0, -0.529999971f), Q);
                break;

            case "Board_LT":
                //0 = Middle_LT
                //Vector3(-13.8380003f,2.773f,23.6289997f) pos  
                //Vector3(90,256.600006f,0) rot
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 256.600006f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.8380003f,2.773f,23.6289997f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new 
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                //G.text = "";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.569999993f), Q);

                //3 = Dice_LT
                //Vector3(-14.9729996f, 1.74000001f, 20.8950005f) pos
                //Vector3(90f,0f,0f) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(90, 0, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.9729996f, 1.74000001f, 20.8950005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new 
                    Vector3(0.5f, 0.5f, 0.5f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                //G.text = "";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.74000001f), Q);
                //4 = Entrance_LT
                //Vector3(-15.0030003f,2.63199997f,24.5470009f) pos
                //Vector3(90,80.0000076f,0) rot
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 80.0000076f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.0030003f, 2.63199997f, 24.5470009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new 
                    Vector3(0.25f, 0.25f, 0.25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].GetComponentInChildren<TextMeshPro>();
                //G.text = "";
                Q = Quaternion.Euler(new Vector3(0, 270, 90));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, -0.0900000036f, -0.699999988f), Q);
                break;
            case "Outside_LT":
                /* Entrance_LT */
                //Vector3(-10.1599998f,1.87f,24.0100002f) pos
                //Vector3(0f,90f,0f) rot
                Q = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-10.1599998f, 1.87f, 24.0100002f), Q);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                //G.text = "";
                Q = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0.680000007f, 1.52999997f, -3.51999998f), Q);
                /* Reception_Corridor */
                //Vector3(-15.3850002f,2.54099989f,21.2539997f) pos
                //Vector3(90f,0f,0f) rot
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.3850002f, 2.54099989f, 21.2539997f), Q);

                //activate itab_MainLift
                itabs_icons[13].SetActive(true);

                //activate itab_MainLift
                itabs_icons[12].SetActive(false);
                break;

            case "Reception_Corridor":
                //5 = Outside_LT
                //Vector3(-31.5699997f,-0.0299999993f,24.25f) pos
                //Vector3(90f,0f,0f) rot
                Q = Quaternion.Euler(new Vector3(90f, 100.499992f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.4500008f, 2.55900002f, 23.1110001f), Q);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Door";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.469999999f), Q);

                //8 = Auditorium_Corridor
                //Vector3(-15.2819996f,2.6960001f,21.4060001f)
                //Vector3(-15.25f,2.6960001f,21.6350002f) pos
                //Vector3(90,8.0999918f,0) rot
                Q = Quaternion.Euler(new Vector3(90, 8.0999918f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.2819996f,2.6960001f,21.4060001f), Q);

                //activate itab_MainLift
                itabs_icons[12].SetActive(true);

                //activate itab_MainLift
                itabs_icons[10].SetActive(false);

                //activate itab_MainLift
                itabs_icons[11].SetActive(false);

                //activate itab_MainLift
                itabs_icons[13].SetActive(false);
                break;

            case "Auditorium_Corridor":
                /* Reception_Corridor */
                //Vector3(-27.9599991f,2.02999997f,21f) pos
                //Vector3(95,90,0) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(85, 260, 180));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-17.1369991f, 2.74499989f, 22.6299992f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Reception";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.6f), Q);

                /* OneWindowOffice_Corridor */
                //Vector3(-3.70000005f,0.689999998f,24.6800003f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 270f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-12.9860001f, 2.3499999f, 23.3920002f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                //G.text = "Move Forward";
                Q = Quaternion.Euler(new Vector3(76.2878189f, 87.9999847f, 267.826477f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-0.560000002f, 2.41000009f, -1.65999997f), Q);

                //activate itab_MainLift
                itabs_icons[10].SetActive(true);

                //activate itab_MainLift
                itabs_icons[11].SetActive(true);

                //activate itab_MainLift
                itabs_icons[9].SetActive(false);

                //activate itab_MainLift
                itabs_icons[12].SetActive(false);

                break;

            case "OneWindowOffice_Corridor":
                /* Auditorium_Corridor */
                //Vector3(-13.2299995f,0.689999998f,10.9200001f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 349.309998f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.0559998f, 2.6960001f, 21.6720009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Admission Office";
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0, -0.5f), Q);
                /* Tables_Walkway */
                //Vector3(-13.5080004f,2.75999999f,22.8630009f) pos
                //Vector3(90,271.100006,0) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 271.100006f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.5080004f, 2.75999999f, 22.8630009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                //activate itab_MainLift
                itabs_icons[9].SetActive(true);

                //activate itab_MainLift
                itabs_icons[10].SetActive(false);

                //activate itab_MainLift
                itabs_icons[11].SetActive(false);

                break;

            case "Tables_Walkway":
                /* OneWindowOffice_Corridor */
                //Vector3(-21.6700001f,2.33800006f,22.6599998f) pos
                //Vector3(90f,90f,0f) rotation
                //Vector3(1,1,1) scale
                Q = Quaternion.Euler(new Vector3(90f, 90f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-21.6700001f, 2.33800006f, 22.6599998f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(1, 1, 1);

                /* GreyCarpet_Walkways */
                //Vector3(-14.8929996f,2.85299993f,21.3040009f) pos
                //Vector3(90f,355.800018f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 355.800018f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.8929996f, 2.85299993f, 21.3040009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].GetComponentInChildren<TextMeshPro>();
                //G.text = "Main Lift Area";
                Q = Quaternion.Euler(new Vector3(87.4392166f, 303.332642f, 121.958176f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.14999962f, -0.109999999f, 1.95000005f), Q);

                //activate itab_MainLift
                itabs_icons[8].SetActive(false);

                //activate itab_MainLift
                itabs_icons[9].SetActive(false);

                break;

            case "Arfa_MainEntrance":
                /* InsideArfa_MainEntrance */
                //Vector3(-17.8400002f,1.53499997f,22.6989994f) pos
                //Vector3(90,90,0) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(90, 90, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-17.8400002f, 1.53499997f, 22.6989994f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.5f, .5f, .5f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Go To ITU";
                Q = Quaternion.Euler(new Vector3(87.6001205f, 89.9999924f, 270));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(3.13000011f, -0.119999997f, 4.78000021f), Q);

                //activate itab_Entrance
                itabs_icons[0].SetActive(true);

                //Deactivate itab_InsideEntrance
                itabs_icons[1].SetActive(false);
                
                break;
            case "InsideArfa_MainEntrance":
                /* Arfa_MainEntrance */
                //Vector3(-17.1790009f,2.70799994f,22.5330009f) pos
                //Vector3(79.4000092f,254.360001f,180f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 260.999969f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-13.5629997f, 1.88699996f, 24.5130005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.5f, .5f, .5f);

                /* Elevator_MainEntrance */
                //Vector3(-16.8829994f,2.6170001f,22.8169994f) pos
                //Vector3(90,90,0) rotation
                //Vector3(0.25f,0.25f,0.25f) scale 
                Q = Quaternion.Euler(new Vector3(90, 90, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                   Vector3(-16.8829994f, 2.6170001f, 22.8169994f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, .25f);

                //itab Reception Main Entrance
                itabs_icons[1].SetActive(true);
                //Deactivate itab_Entrance
                itabs_icons[0].SetActive(false);
                //Deactivate itab_Lift
                itabs_icons[2].SetActive(false);

                break;
            case "Elevator_MainEntrance":
                /* InsideArfa_MainEntrance */
                //Vector3(-14.8059998f,2.70300007f,21.7099991f) pos
                //Vector3(90f,344.800018f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale 
                Q = Quaternion.Euler(new Vector3(90f, 344.800018f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-14.8059998f, 2.70300007f, 21.7099991f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, .25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Exit Lift Area";
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0));
                G.transform.SetLocalPositionAndRotation(new Vector3(3.1500001f, -0.550000012f, 6.46000004f), Q);
                /* MainLift_Walkway */
                //Vector3(-6.88999987f,2.88000011f,25.5200005f) pos
                //Vector3(0f,252.630005f,180f) rotation
                //Vector3(1f,1f,1f) scale 
                Q = Quaternion.Euler(new Vector3(0f, 252.630005f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                   Vector3(-6.88999987f, 2.88000011f, 25.5200005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(1f, 1f, 1f);

                //activate itab_Lift
                itabs_icons[2].SetActive(true);
                //Deactivate itab_Entrance
                itabs_icons[1].SetActive(false);
                //deactivate itab_MainLiftWalkway
                itabs_icons[3].SetActive(false);
                break;
        }
    }
    int ChangeSkyboxTo(string Hotspot_Name, Material[] Skybox)
    {
        //Debug.Log("Finding the correct Skybox...");
        for (int i = 0; i < Skybox.Length; i++)
        {
            //Debug.Log(Skybox[i].name + " " + Hotspot_Name);
            if(Hotspot_Name == Skybox[i].name)
            {
                Debug.Log("The Skybox to change to is:" + Skybox[i].name);
                RenderSettings.skybox = Skybox[i];
                return i;
            }
        }
        return 0;
    }
}
