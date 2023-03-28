using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;
using FLS.MembershipFunctions;
using UnityEditor.PackageManager;

public class FuzzyAirplane : MonoBehaviour
{
    //Serialize Fields
    [SerializeField] private GameObject leftRay;
    [SerializeField] private GameObject midRay;
    [SerializeField] private GameObject rightRay;

    //Variables
    private RayTracer rayHit;

    IFuzzyEngine moveEngine;
    IFuzzyEngine speedEngine;

    LinguisticVariable leftDir, midDir, rightDir;

    LinguisticVariable boatDistance;

    void Start()
    {

        speedEngine = new FuzzyEngineFactory().Default();
        moveEngine = new FuzzyEngineFactory().Default();

        //Test Rayhit variable
        rayHit = GameObject.FindObjectOfType<RayTracer>();

        //Left Direction check
        leftDir = new LinguisticVariable("leftDir");
        var ld_farLeft = leftDir.MembershipFunctions.AddTrapezoid("farLeft", -50, -50, -15, -7);
        var ld_left = leftDir.MembershipFunctions.AddTrapezoid("left", -10, -5, -3, -1);
        var ld_none = leftDir.MembershipFunctions.AddTrapezoid("none", -3, -0.5, 0.5, 3);
        var ld_right = leftDir.MembershipFunctions.AddTrapezoid("right", 1, 3, 5, 10);
        var ld_farRight = leftDir.MembershipFunctions.AddTrapezoid("farRight", 7, 15, 50, 50);

        //Forward Direction check
        midDir = new LinguisticVariable("midDir");
        var md_farLeft = midDir.MembershipFunctions.AddTrapezoid("farLeft", -50, -50, -15, -7);
        var md_left = midDir.MembershipFunctions.AddTrapezoid("left", -10, -5, -3, -1);
        var md_none = midDir.MembershipFunctions.AddTrapezoid("none", -3, -0.5, 0.5, 3);
        var md_right = midDir.MembershipFunctions.AddTrapezoid("right", 1, 5, 10, 15);
        var md_farRight = midDir.MembershipFunctions.AddTrapezoid("farRight", 7, 15, 50, 50);

        //Right Direction check
        rightDir = new LinguisticVariable("rightDir");
        var rd_farLeft = rightDir.MembershipFunctions.AddTrapezoid("farLeft", -50, -50, -15, -7);
        var rd_left = rightDir.MembershipFunctions.AddTrapezoid("left", -10, -5, -3, -1);
        var rd_none = rightDir.MembershipFunctions.AddTrapezoid("none", -3, -0.5, 0.5, 3);
        var rd_right = rightDir.MembershipFunctions.AddTrapezoid("right", -15, -5, 5, 15);
        var rd_farRight = rightDir.MembershipFunctions.AddTrapezoid("farRight", 7, 15, 50, 50);

        //Boat Distance check
        boatDistance = new LinguisticVariable("boatDistance");
        var far = boatDistance.MembershipFunctions.AddTrapezoid("far", 250, 300, 350, 400);
        var medium = boatDistance.MembershipFunctions.AddTrapezoid("medium", 125, 175, 225, 275);
        var close = boatDistance.MembershipFunctions.AddTrapezoid("close", 30, 70, 110, 150);

        //Rules
        //var moveRule1 = Rule.If()

    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        Vector3 hitPoint = rayHit.hit.point;
    }
}
