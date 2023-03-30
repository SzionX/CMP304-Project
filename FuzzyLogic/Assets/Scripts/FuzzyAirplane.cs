using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;
using FLS.MembershipFunctions;
using Unity.VisualScripting;

public class FuzzyAirplane : MonoBehaviour
{
    //Serialize Fields
    [SerializeField] private GameObject leftRay;
    [SerializeField] private GameObject midRay;
    [SerializeField] private GameObject rightRay;

    //Variables
    double moveSpeed;
    Rigidbody rb;
    RaycastHit leftRayHit, midRayHit, rightRayHit;

    IFuzzyEngine moveEngine;
    IFuzzyEngine speedEngine;

    //Linguistic Variable initialization
    LinguisticVariable collisionDirection;
    LinguisticVariable collisionDistance;
    LinguisticVariable moveDirection;
    LinguisticVariable speed;

    void Start()
    {
        //Get Components
        rb = GetComponent<Rigidbody>();
        leftRayHit = leftRay.gameObject.GetComponent<RayTracer>().hit;
        midRayHit = midRay.gameObject.GetComponent<RayTracer>().hit;
        rightRayHit = rightRay.gameObject.GetComponent<RayTracer>().hit;

        //Initialize engine factories
        speedEngine = new FuzzyEngineFactory().Default();
        moveEngine = new FuzzyEngineFactory().Default();

        //What direction collision occured in  //Values Set
        collisionDirection = new LinguisticVariable("collisionDirection");
        var cd_left = collisionDirection.MembershipFunctions.AddTriangle("left", -60, -60, -20);
        var cd_midLeft = collisionDirection.MembershipFunctions.AddTriangle("midLeft", -40, -20, 0);
        var cd_straight = collisionDirection.MembershipFunctions.AddTriangle("straight", -10, 0, 10);
        var cd_midRight = collisionDirection.MembershipFunctions.AddTriangle("midRight", 0, 20, 40);
        var cd_right = collisionDirection.MembershipFunctions.AddTriangle("right", 20, 60, 60);

        //How far away boat is  //Values Set
        collisionDistance = new LinguisticVariable("collisionDistance");
        var far = collisionDistance.MembershipFunctions.AddTrapezoid("far", 400, 400, 300, 250);
        var mid = collisionDistance.MembershipFunctions.AddTrapezoid("mid", 300, 250, 150, 100);
        var near = collisionDistance.MembershipFunctions.AddTrapezoid("near", 150, 100, 0, 0);

        //Direction to move in  //Values Set
        moveDirection = new LinguisticVariable("moveDirection");
        var md_left = moveDirection.MembershipFunctions.AddTriangle("left", -30, -30, 0);
        var md_none = moveDirection.MembershipFunctions.AddTriangle("none", -10, 0, 10);
        var md_right = moveDirection.MembershipFunctions.AddTriangle("right", 0, 30, 30);

        //Speed that plane moves
        speed = new LinguisticVariable("speed");
        var fast = moveDirection.MembershipFunctions.AddTrapezoid("left", 30, 40, 50, 50);
        var medium = moveDirection.MembershipFunctions.AddTrapezoid("none", 10, 20, 30, 40);
        var slow = moveDirection.MembershipFunctions.AddTrapezoid("right", 0, 0, 10, 20);

        //Move Rules

        //If left
        var moveRule1 = Rule.If(collisionDirection.Is(cd_left).And(collisionDistance.Is(far))).Then(moveDirection.Is(md_none));
        var moveRule2 = Rule.If(collisionDirection.Is(cd_left).And(collisionDistance.Is(mid))).Then(moveDirection.Is(md_none));
        var moveRule3 = Rule.If(collisionDirection.Is(cd_left).And(collisionDistance.Is(near))).Then(moveDirection.Is(md_none));

        //If left and middle
        var moveRule4 = Rule.If(collisionDirection.Is(cd_midLeft).And(collisionDistance.Is(far))).Then(moveDirection.Is(md_right));
        var moveRule5 = Rule.If(collisionDirection.Is(cd_midLeft).And(collisionDistance.Is(mid))).Then(moveDirection.Is(md_right));
        var moveRule6 = Rule.If(collisionDirection.Is(cd_midLeft).And(collisionDistance.Is(near))).Then(moveDirection.Is(md_right));

        //If middle
        var moveRule7 = Rule.If(collisionDirection.Is(cd_straight).And(collisionDistance.Is(far))).Then(moveDirection.Is(md_right));
        var moveRule8 = Rule.If(collisionDirection.Is(cd_straight).And(collisionDistance.Is(mid))).Then(moveDirection.Is(md_right));
        var moveRule9 = Rule.If(collisionDirection.Is(cd_straight).And(collisionDistance.Is(near))).Then(moveDirection.Is(md_right));

        //If middle and right
        var moveRule10 = Rule.If(collisionDirection.Is(cd_midRight).And(collisionDistance.Is(far))).Then(moveDirection.Is(md_left));
        var moveRule11 = Rule.If(collisionDirection.Is(cd_midRight).And(collisionDistance.Is(mid))).Then(moveDirection.Is(md_left));
        var moveRule12 = Rule.If(collisionDirection.Is(cd_midRight).And(collisionDistance.Is(near))).Then(moveDirection.Is(md_left));

        //If right
        var moveRule13 = Rule.If(collisionDirection.Is(cd_right).And(collisionDistance.Is(far))).Then(moveDirection.Is(md_none));
        var moveRule14 = Rule.If(collisionDirection.Is(cd_right).And(collisionDistance.Is(mid))).Then(moveDirection.Is(md_none));
        var moveRule15 = Rule.If(collisionDirection.Is(cd_right).And(collisionDistance.Is(near))).Then(moveDirection.Is(md_none));

        //Add Rules to return plane to center
        var returnRule1 = Rule.If(collisionDistance.Is(far)).Then(moveDirection.Is(md_left));
        var returnRule2 = Rule.If(collisionDistance.Is(mid)).Then(moveDirection.Is(md_none));
        var returnRule3 = Rule.If(collisionDistance.Is(near)).Then(moveDirection.Is(md_right));

        //Speed Rules
        var speedRule1 = Rule.If(collisionDistance.Is(far)).Then(speed.Is(slow));
        var speedRule2 = Rule.If(collisionDistance.Is(mid)).Then(speed.Is(medium));
        var speedRule3 = Rule.If(collisionDistance.Is(near)).Then(speed.Is(fast));


        //Add rules into engines
        moveEngine.Rules.Add(moveRule1, moveRule2, moveRule3, moveRule4, moveRule4, moveRule5, moveRule6, moveRule7, moveRule8, moveRule9, moveRule10, moveRule11, moveRule12, moveRule13, moveRule14, moveRule15);
        moveEngine.Rules.Add(returnRule1, returnRule2, returnRule3);
        speedEngine.Rules.Add(speedRule1, speedRule2, speedRule3);
    }

    void Update()
    {
        //double planeMove = ; 

        ////Setting Collision Linguistic Variable to the distance of the ray hit
        //speedEngine.Defuzzify(new { collisionDistance = (double)leftRayHit.distance });
        //speedEngine.Defuzzify(new { collisionDistance = (double)midRayHit.distance });
        //speedEngine.Defuzzify(new { collisionDistance = (double)rightRayHit.distance });

        ////Create value to move 
        //double xMove = moveEngine.Defuzzify(new { moveDirection = (double)this.transform.position.x });
        //rb.AddForce(new Vector3((float)(xMove), 0, 0));
    }

    void FixedUpdate()
    {
    }

    //Stop running if plane crashes
    private void OnCollisionEnter(Collision collision)
    {
        Application.Quit();
    }
}
