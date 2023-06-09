using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoats : MonoBehaviour
{
    public GameObject boatPrefab;
    public float respawnTime = 5f;
    public Slider respawnSlider;

    void Start()
    {
        //Get slider
        GameObject sliderGO = GameObject.Find("Slider");

        if (sliderGO != null)
        {
            respawnSlider = sliderGO.GetComponent<Slider>();
        }

        StartCoroutine(randSpawn());
    }

    private void spawnBoat()
    {
        Vector3 randSpawnPos = new Vector3(Random.Range(-50, 51), 0, 300);
        Instantiate(boatPrefab, randSpawnPos, Quaternion.Euler(new Vector3(-90, 0, 90)));
    }

    IEnumerator randSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);

            // Get the slider value and use it as the new respawnTime
            if (respawnSlider != null)
            {
                respawnTime = respawnSlider.value;
            }

            spawnBoat();
        }
    }
}
