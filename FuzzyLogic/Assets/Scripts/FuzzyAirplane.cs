using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;
using FLS.MembershipFunctions;

public class FuzzyAirplane : MonoBehaviour
{
    //Variables
    IFuzzyEngine engine;
    LinguisticVariable distance, direction;

    void Start()
    {
        // Here we need to setup the Fuzzy Inference System
        distance = new LinguisticVariable("distance");
        var right = distance.MembershipFunctions.AddTrapezoid("right", -50, -50, -5, -1);
        var none = distance.MembershipFunctions.AddTrapezoid("none", -5, -0.5, 0.5, 5);
        var left = distance.MembershipFunctions.AddTrapezoid("left", 1, 5, 50, 50);

        direction = new LinguisticVariable("distance");
        var d_right = direction.MembershipFunctions.AddTrapezoid("right", -50, -50, -5, -1);
        var d_none = direction.MembershipFunctions.AddTrapezoid("none", -5, -0.5, 0.5, 5);
        var d_left = direction.MembershipFunctions.AddTrapezoid("left", 1, 5, 50, 50);

        engine = new FuzzyEngineFactory().Default();

        var rule1 = Rule.If(distance.Is(right)).Then(direction.Is(d_left));
        var rule3 = Rule.If(distance.Is(none)).Then(direction.Is(d_none));
        var rule2 = Rule.If(distance.Is(left)).Then(direction.Is(d_right));

        engine.Rules.Add(rule1, rule2, rule3);
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        
    }
}
