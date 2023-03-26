﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;
using FLS.MembershipFunctions;

public class FuzzyBox : MonoBehaviour
{
	bool selected = false;
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
		if (!selected && this.transform.position.y < 0.6f)
		{
			// Convert position of box to value between 0 and 100
			double result = engine.Defuzzify(new { distance = (double)this.transform.position.x });

			Rigidbody rigidbody = GetComponent<Rigidbody>();
			rigidbody.AddForce(new Vector3((float)(result), 0f, 0f));
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var hit = new RaycastHit();
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name == "FuzzyBox") Debug.Log("You have clicked the FuzzyBox");
				selected = true;
			}
		}

		if (Input.GetMouseButton(0) && selected)
		{
			float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
			transform.position = new Vector3(curPosition.x, curPosition.y, curPosition.z);
		}

		if (Input.GetMouseButtonUp(0))
		{
			selected = false;
		}
	}
}
