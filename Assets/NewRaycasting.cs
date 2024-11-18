//using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class NewRaycasting : MonoBehaviour
{
    // public GameObject Sphere;
    public Material[] SkyboxMaterial;
    public GameObject[] hotspot;
    public GameObject Camera;

    public float delay = 10f;
    float timer;
    public LayerMask teleportMask;
    Dictionary<string, List<int>> SkyboxToHotspotMapping = new Dictionary<string, List<int>>(30);

    // Start is called before the first frame update
    void Start()
    {
        /* Complete Lecture Theater */
        SkyboxToHotspotMapping["Back_LT"] = new List<int> { 0 };
        SkyboxToHotspotMapping["Middle_LT"] = new List<int> { 1, 2, 3, 4 };
        SkyboxToHotspotMapping["Entrance_LT"] = new List<int> { 0, 2, 3, 5 };
        SkyboxToHotspotMapping["Dice_LT"] = new List<int> { 0, 2, 4 };
        SkyboxToHotspotMapping["Board_LT"] = new List<int> { 0, 3, 4 };
        SkyboxToHotspotMapping["Outside_LT"] = new List<int> { 4, 6 };

        /* Corridor from LT to Glass Door */
        SkyboxToHotspotMapping["Reception_Corridor"] = new List<int> { 5, 7, 8 };
        SkyboxToHotspotMapping["Auditorium_Corridor"] = new List<int> { 6, 9 };
        SkyboxToHotspotMapping["LT1_Corridor"] = new List<int> { 9, 11 };
        SkyboxToHotspotMapping["OneWindowOffice_Corridor"] = new List<int> { 10, 23 };

        /* From Glass Door to Main lift */
        SkyboxToHotspotMapping["Tables_Walkway"] = new List<int> { 11, 22 };
        SkyboxToHotspotMapping["GreyCarpet_Walkway"] = new List<int> { 20, 23 };
        SkyboxToHotspotMapping["Benches_Walkway"] = new List<int> { 20, 22 };
        SkyboxToHotspotMapping["StudentLift_Walkway"] = new List<int> { 19, 22 };
        SkyboxToHotspotMapping["OutsideLibrary_Walkway"] = new List<int> { 17, 20, 24 };
        SkyboxToHotspotMapping["OutsideMainLift_Walkway"] = new List<int> { 16, 19 };
        SkyboxToHotspotMapping["MainLift_Walkway"] = new List<int> { 15, 17, 27 };
        SkyboxToHotspotMapping["OutsideCSDepartment_Walkway"] = new List<int> { 16, 27 };

        /* Complete Library */
        SkyboxToHotspotMapping["Entrance_Library"] = new List<int> { 19, 25, 26 };
        SkyboxToHotspotMapping["Sofa_Library"] = new List<int> { 24, 26 };
        SkyboxToHotspotMapping["Edge_Library"] = new List<int> { 24, 25 };

        /* Complete CSDepartment */
        SkyboxToHotspotMapping["Entrance_CSDepartment"] = new List<int> { 16, 29 };
        SkyboxToHotspotMapping["Dr.Mohsen_CSDepartment"] = new List<int> { 27 };

        /* Main Entrance to Elevator */
        SkyboxToHotspotMapping["Arfa_MainEntrance"] = new List<int> { 13 };
        SkyboxToHotspotMapping["InsideArfa_MainEntrance"] = new List<int> { 12, 14 };
        SkyboxToHotspotMapping["Reception_MainEntrance"] = new List<int> { 13, 15 };
        SkyboxToHotspotMapping["Elevator_MainEntrance"] = new List<int> { 14, 16 };

    }

    // Update is called once per frame
    void Update()
    {
        Transform origin = this.transform;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward.normalized;
        float rayDistance = 100f;
        RaycastHit hit;
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

    }

    void TurnHotspotsON(string Skybox_Name, int n)
    {
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
        }
        //The location of each hotspot relevant to the skybox
        switch (Skybox_Name)
        {
            case "GreyCarpet_Walkway":
                /* Benches_Walkway */
                //Vector3(-13.6379995f,2.8829999f,22.8460007f) pos
                //Vector3(89.972023f,276.400024f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Quaternion Q = Quaternion.Euler(new Vector3(89.972023f, 276.400024f, 0f));
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
                break;
            case "Benches_Walkway":
                /* StudentLift_Walkway */
                //Vector3(-15.1890001f,2.77200007f,21.3980007f) pos
                //Vector3(90f,0f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.1890001f, 2.77200007f, 21.3980007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* GreyCarpet_Walkway */
                //Vector3(-13.8479996f,2.54299998f,22.7560005f) pos
                //Vector3(90f,273.099976f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 273.099976f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.8479996f, 2.54299998f, 22.7560005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "StudentLift_Walkway":
                /* OutsideLibrary_Walkway */
                //Vector3(-14.698f,2.75500011f,24.2619991f) pos
                //Vector3(90f,192.930008f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 192.930008f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.698f, 2.75500011f, 24.2619991f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);

                TextMeshPro G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                Q = Quaternion.Euler(new Vector3(78.1149368f, 14.1026735f, 194.314957f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.51000023f, 0.409999996f, 1.38999999f), Q);

                /* Benches_Walkway */
                //Vector3(-16.1089993f,2.75399995f,22.4960003f) pos
                //Vector3(90f,145.699982f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 145.699982f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.1089993f, 2.75399995f, 22.4960003f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "OutsideLibrary_Walkway":
                /* OutsideMainLift_Walkway */
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

                //Q = Quaternion.Euler(new Vector3(2.90000415f, 10.2499971f, 0f));
                //Camera.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Q);

                /* StudentLift_Walkway */
                //Vector3(-15.4189997f,2.65499997f,21.2999992f) pos
                //Vector3(90f,17.6000004f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 17.6000004f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.4189997f, 2.65499997f, 21.2999992f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Entrance_Library */
                //Vector3(-16.5020008f,2.88400006f,23.2700005f) pos
                //Vector3(0,270f,180f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0, 270f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.5020008f, 2.88400006f, 23.2700005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "OutsideMainLift_Walkway":
                /* MainLift_Walkway */
                //Vector3(-14.2329998f,2.60100007f,22.3390007f) pos
                //Vector3(90f,287.799988f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 287.799988f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-14.2329998f, 2.60100007f, 22.3390007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);

                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Main Lift Area";
                Q = Quaternion.Euler(new Vector3(86.0980148f, 339.145264f, 157.636353f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(-9.34000015f, 0.25999999f, 1.95000005f), Q);
                /* OutsideLibrary_Walkway */
                //Vector3(-14.4759998f,2.66199994f,24.2800007f) pos
                //Vector3(90f,207.000015f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 207.000015f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.4759998f, 2.66199994f, 24.2800007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
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
                Q = Quaternion.Euler(new Vector3(357.194916f, 0.568825901f, 359.766144f));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(6.38999987f, -1.49000001f, -0.100000001f), Q);
                /* OutsideMainLift_Walkway */
                //Vector3(-16.2999992f,2.79200006f,22.6420002f) pos
                //Vector3(90f,83.6999893f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 83.6999893f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.2999992f, 2.79200006f, 22.6420002f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* OutsideCSDepartment_Walkway */
                //Vector3(-13.4189997f,2.65199995f,23.0799999f) pos
                //Vector3(90f,265.070007f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 265.070007f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.4189997f, 2.65199995f, 23.0799999f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "OutsideCSDepartment_Walkway":
                /* MainLift_Walkway*/
                //Vector3(-15.8959999f,2.72099996f,23.4880009f) pos
                //Vector3(90f,120.400009f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 120.400009f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-15.8959999f, 2.72099996f, 23.4880009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Entrance_CSDepartment */
                //Vector3(-13.8459997f,2.74600005f,22.3899994f) pos
                //Vector3(90f,21.1999931f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 21.1999931f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.8459997f, 2.74600005f, 22.3899994f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
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
                /* Sofa_Library */
                //Vector3(-13.2130003f,2.50900006f,22.4529991f) pos
                //Vector3(90f,315.099976f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 315.099976f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.2130003f, 2.50900006f, 22.4529991f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Edge_Library */
                //Vector3(-15.0120001f,2.80100012f,21.5639992f) pos
                //Vector3(90,0f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.0120001f, 2.80100012f, 21.5639992f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "Sofa_Library":
                /* Entrance_Library */
                //Vector3(-14.4949999f,2.72300005f,20.9220009f) pos
                //Vector3(90f,307.899994f,0) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 307.899994f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.4949999f, 2.72300005f, 20.9220009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Edge_Library */
                //Vector3(-12.2270002f,3.01600003f,24.2810001f) pos
                //Vector3(0f,90f,180f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0f, 90f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-12.2270002f, 3.01600003f, 24.2810001f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "Edge_Library":
                /* Entrance_Library */
                //Vector3(-19.0300007f,3.46000004f,21.5300007f) pos
                //Vector3(-1.70754731e-06f,76.4299927f,90.6199951f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0f, 76.4300003f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-1.70754731e-06f, 76.4299927f, 90.6199951f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Sofa_Library */
                //Vector3(-18.7800007f,3.31999993f,24.5699997f) pos
                //Vector3(0f,300.700012f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0f, 300.700012f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-18.7800007f, 3.31999993f, 24.5699997f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "Entrance_CSDepartment":
                /* OutsideCSDepartment_Walkway */
                //Vector3(-15.7559996f,2.62199998f,21.4050007f) pos
                //Vector3(90f,27.5f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 27.5f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-15.7559996f, 2.62199998f, 21.4050007f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                /* Middle_CSDepartment */
                //Vector3(-13.7209997f,2.546f,22.0480003f) pos
                //Vector3(90f,297.899994f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 297.899994f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.7209997f, 2.546f, 22.0480003f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            //case "Middle_CSDepartment":
            //break;
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
                break;

            case "Back_LT":
                /* Middle_LT */
                //Vector3(-13.4440002f,2.5710001f,23.0540009f) pos
                //Vector3(90f,267.299957f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 267.299957f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.4440002f, 2.5710001f, 23.0540009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "Middle_LT":
                /* Back_LT */
                //Vector3(-50.2999992f,-7.69999981f,37.2999992f) pos
                //Vector3(90f,300f,0f) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(90f, 300f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-50.2999992f, -7.69999981f, 37.2999992f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.5f, 0.5f, 0.5f);
                /* Board_LT */
                //Vector3(20.3999996f,-5.19999981f,5.30000019f) pos
                //Vector3(78.3000412f,122.300003f,132.400009f) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new(78.3000412f, 122.300003f, 132.400009f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(20.3999996f, -5.19999981f, 5.30000019f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.5f, 0.5f, 0.5f);
                /* Dice_LT */ //Dice
                //Vector3(54.7999992f,0.100000001f,28.6000004f) pos
                //Vector3(0f,268.100006f,0f) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(0f, 268.100006f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(54.7999992f, 0.100000001f, 28.6000004f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.5f, 0.5f, 0.5f);
                /* Entrance_LT */
                //Vector3(4.80000019f,2.45000005f,-3.29999995f) pos
                //Vector3(0f,324.679993f,0f) rot
                //Vector3(0.25f,0.25f,0.25f) 
                Q = Quaternion.Euler(new Vector3(0f, 324.679993f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].transform.SetLocalPositionAndRotation(new
                    Vector3(4.80000019f, 2.45000005f, -3.29999995f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;

            case "Entrance_LT":
                /* Middle_LT */
                //Vector3(-2.71000004f,1.72000003f,14.0299997f) pos
                //Vector3(282.700012f,312.199982f,180f) rot
                //Vector3(0.200000003f,0.200000003f,0.200000003f) scale
                Q = Quaternion.Euler(new Vector3(282.700012f, 312.199982f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-2.71000004f, 1.72000003f, 14.0299997f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.200000003f, 0.200000003f, 0.200000003f);
                /* Board_LT */
                //Vector3(-12.5f,-12.8999996f,-28.1000004f) pos
                //Vector3(90f,0f,0f) rot
                //Vector3(1,1,1) scale
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-12.5f, -12.8999996f, -28.1000004f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(1f, 1f, 1f);
                /* Dice_LT */
                //Vector3(-14.8400002f,1.19000006f,7.15999985f) pos
                //Vector3(83.2000656f,180f,180f) rot
                //Vector3(0.2f,0.2f,0.2f) scale
                Q = Quaternion.Euler(new Vector3(83.2000656f, 180f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-14.8400002f, 1.19000006f, 7.15999985f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.2f, 0.2f, 0.2f);
                /* Outside_LT */
                //Vector3(-23.7399998f,-4.03999996f,16.8700008f) pos
                //Vector3(0f,82.7900009f,0f) rot
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(0f, 82.7900009f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].transform.SetLocalPositionAndRotation(new
                    Vector3(-23.7399998f, -4.03999996f, 16.8700008f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][3]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;

            case "Dice_LT":
                //0 = Middle_LT
                Q = Quaternion.Euler(new Vector3(90f, 86.1999969f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-34.7900009f, -1.5f, 21.2999992f), Q);
                //2 = Board_LT
                Q = Quaternion.Euler(new Vector3(89.972023f, 108.500008f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-25.7000008f, -7.82999992f, 2.02999997f), Q);
                //4 = Entrance_LT
                Q = Quaternion.Euler(new Vector3(83.0000229f, 204.799973f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-26.0699997f, -0.699999988f, 5.57999992f), Q);
                break;

            case "Board_LT":
                //0 = Middle_LT
                //Vector3(9.64000034f,-1.32000005f,34.2700005f) pos
                //Vector3(76.6999969f,84.7999725f,180f) rot
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(76.6999969f, 84.7999725f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(9.64000034f, -1.32000005f, 34.2700005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                //3 = Dice_LT
                //Vector3(-10f,-18.5f,-5.5999999f) pos
                //Vector3(90f,0f,0f) rot
                //Vector3(0.5f,0.5f,0.5f) scale
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-10f, -18.5f, -5.5999999f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(0.5f, 0.5f, 0.5f);
                //4 = Entrance_LT
                //Vector3(-16.25f,-5.42000008f,50.4199982f) pos
                //Vector3(90f,0f,0f) rot
                //Vector3(0.25f,0.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.25f, -5.42000008f, 50.4199982f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.localScale = new
                    Vector3(0.25f, 0.25f, 0.25f);
                break;
            case "Outside_LT":
                /* Entrance_LT */
                //Vector3(2.94000006f,1.12f,26.4200001f) pos
                //Vector3(0f,90f,0f) rot
                Q = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(2.94000006f, 1.12f, 26.4200001f), Q);
                /* Reception_Corridor */
                //Vector3(-17.5300007f,-0.289999992f,8.14999962f) pos
                //Vector3(90f,0f,0f) rot
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-17.5300007f, -0.289999992f, 8.14999962f), Q);
                break;

            case "Reception_Corridor":
                //5 = Outside_LT
                //Vector3(-31.5699997f,-0.0299999993f,24.25f) pos
                //Vector3(90f,0f,0f) rot
                Q = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-31.5699997f, -0.0299999993f, 24.25f), Q);
                //7 = LT5_Corridor
                //Vector3(33.4000015f,-4.4000001f,22.5f) pos
                //Vector3(87.4000702f,90f,0f)rot
                Q = Quaternion.Euler(new Vector3(87.4000702f, 90f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(33.4000015f, -4.4000001f, 22.5f), Q);
                //8 = Auditorium_Corridor
                //Vector3(-17.2600002f,1.29999995f,4.23999977f) pos
                //Vector3(86.4000015f,180f,180f) rot
                Q = Quaternion.Euler(new Vector3(86.4000015f, 180f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][2]].transform.SetLocalPositionAndRotation(new
                    Vector3(-17.2600002f, 1.29999995f, 4.23999977f), Q);
                break;

            case "Auditorium_Corridor":
                /* Reception_Corridor */
                //Vector3(-27.9599991f,2.02999997f,21f) pos
                //Vector3(95,90,0) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(95, 90, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-27.9599991f, 2.02999997f, 21f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                /* ElectronicsLab_Corridor */
                //Vector3(-3.70000005f,0.689999998f,24.6800003f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-3.70000005f, 0.689999998f, 24.6800003f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                break;

            case "ElectronicsLab_Corridor":
                /* Auditorium_Corridor */
                //Vector3(-17.0499992f,1.95000005f,13.7299995f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-17.0499992f, 1.95000005f, 13.7299995f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                /* LT1_Corridor */
                //Vector3(-3.3499999f,1.55999994f,20.6100006f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-3.3499999f, 1.55999994f, 20.6100006f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                break;

            case "LT1_Corridor":
                /* ElectronicsLab_Corridor */
                //Vector3(-32.5200005f,1.84000003f,22.75f) pos
                //Vector3(79.900032f,270f,180f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(79.900032f, 270f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-32.5200005f, 1.84000003f, 22.75f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                /* OneWindowOffice_Corridor */
                //Vector3(-16.2199993f,0.689999998f,31.2399998f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-16.2199993f, 0.689999998f, 31.2399998f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);
                break;

            case "OneWindowOffice_Corridor":
                /* LT1_Corridor */
                //Vector3(-13.2299995f,0.689999998f,10.9200001f) pos
                //Vector3(90f,180f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 180f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.2299995f, 0.689999998f, 10.9200001f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

                /* Tables_Walkway */
                //Vector3(-13.5080004f,2.75999999f,22.8630009f) pos
                //Vector3(90,271.100006,0) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90, 271.100006f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                    Vector3(-13.5080004f, 2.75999999f, 22.8630009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);
                break;

            case "Tables_Walkway":
                /* OneWindowOffice_Corridor */
                //Vector3(-24.8199997f,1.87f,22.7900009f) pos
                //Vector3(90f,90f,0f) rotation
                //Vector3(.25f,.25f,0.25f) scale
                Q = Quaternion.Euler(new Vector3(90f, 90f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                    Vector3(-24.8199997f, 1.87f, 22.7900009f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, 0.25f);

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
                break;

            case "Arfa_MainEntrance":
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

                /* Reception_MainEntrance */
                //Vector3(-17.4459991f,2.8900001f,22.8169994f) pos
                //Vector3(79.4000092f,270f,180f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale 
                Q = Quaternion.Euler(new Vector3(79.4000092f, 270f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                   Vector3(-17.4459991f, 2.8900001f, 22.8169994f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, .25f);
                break;
            case "Reception_MainEntrance":
                /* InsideArfa_MainEntrance */
                //Vector3(-16.1730003f,2.80800009f,22.5249996f) pos
                //Vector3(90f,66.1199875f,0f) rotation
                //Vector3(0.25f,0.25f,0.25f) scale 
                Q = Quaternion.Euler(new Vector3(90f, 66.1199875f, 0f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.SetLocalPositionAndRotation(new
                   Vector3(-16.1730003f, 2.80800009f, 22.5249996f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].transform.localScale = new
                    Vector3(.25f, .25f, .25f);
                G = hotspot[SkyboxToHotspotMapping[Skybox_Name][0]].GetComponentInChildren<TextMeshPro>();
                G.text = "Exit Building";
                Q = Quaternion.Euler(new Vector3(80.6459045f, 270, 90));
                G.gameObject.transform.SetLocalPositionAndRotation(new Vector3(3.13000011f, -0.119999997f, 6.03000021f), Q);

                // Camera.transform.localRotation.eulerAngles.Set(Camera.transform.rotation.x, 60f, Camera.transform.rotation.z);

                /* Elevator_MainEntrance */
                //Vector3(-13.618f,2.66599989f,23.3869991f) pos
                //Vector3(90f,270f,0) rotation
                //Vector3(0.25f,0.25f,0.25f) scale 
                Q = Quaternion.Euler(new Vector3(90f, 270f, 0));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                   Vector3(-13.618f, 2.66599989f, 23.3869991f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(.25f, .25f, .25f);
                break;
            case "Elevator_MainEntrance":
                /* Reception_MainEntrance */
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
                Q = Quaternion.Euler(new Vector3(90, 180, 0));
                G.transform.SetLocalPositionAndRotation(new Vector3(-1.80999994f, -0.550000012f, 1.48000002f), Q);
                /* MainLift_Walkway */
                //Vector3(-6.88999987f,2.88000011f,25.5200005f) pos
                //Vector3(0f,252.630005f,180f) rotation
                //Vector3(1f,1f,1f) scale 
                Q = Quaternion.Euler(new Vector3(0f, 252.630005f, 180f));
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.SetLocalPositionAndRotation(new
                   Vector3(-6.88999987f, 2.88000011f, 25.5200005f), Q);
                hotspot[SkyboxToHotspotMapping[Skybox_Name][1]].transform.localScale = new
                    Vector3(1f, 1f, 1f);
                break;
        }
    }
    int ChangeSkyboxTo(string Hotspot_Name, Material[] Skybox)
    {
        //Debug.Log("Finding the correct Skybox...");
        for (int i = 0; i < Skybox.Length; i++)
        {
            //Debug.Log(Skybox[i].name + " " + Hotspot_Name);
            if (Hotspot_Name == Skybox[i].name)
            {
                Debug.Log("The Skybox to change to is:" + Skybox[i].name);
                RenderSettings.skybox = Skybox[i];
                return i;
            }
        }
        return 0;
    }
}
