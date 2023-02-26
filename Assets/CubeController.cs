using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

/*
    Accelerates the cube to which it is attached, modelling an harmonic oscillator.
    Writes the position, velocity and acceleration of the cube to a CSV file.
    
    Remark: For use in "Physics Engines" module at ZHAW, part of physics lab
    Author: kemf
    Version: 1.0
*/
public class CubeController : MonoBehaviour
{
    private Rigidbody rigidBody;

    public int springConstant; // N/m

    private float currentTimeStep; // s
    
    private List<List<float>> timeSeries;
    
    private bool isForceX = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        timeSeries = new List<List<float>>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(currentTimeStep >= 20f)) return;
        // rigidBody.velocity = Vector3.zero;
        // rigidBody.angularVelocity = Vector3.zero;
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    // FixedUpdate can be called multiple times per frame
    void FixedUpdate() {
        
        if(isForceX)
        {
            Console.WriteLine("Force X");
            float forceX; // N
            // Calculate spring force on body for x component of force vector
            forceX = -rigidBody.position.x * springConstant;
            rigidBody.AddForce(new Vector3(forceX, 0f, 0f));

            currentTimeStep += Time.deltaTime;
            timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.x, rigidBody.velocity.x, forceX});
        }
        else
        {
            Console.WriteLine("Force Z");
            float forceZ; // N
            // Calculate spring force on body for z component of force vector
            forceZ = -rigidBody.position.z * springConstant;
            rigidBody.AddForce(new Vector3(0f, 0f, forceZ));

            currentTimeStep += Time.deltaTime;
            timeSeries.Add(new List<float>() {currentTimeStep, rigidBody.position.z, rigidBody.velocity.z, forceZ});
            
        }
    }

    void OnApplicationQuit() {
        WriteTimeSeriesToCsv();
        // call function to plot diagram
    }

    void WriteTimeSeriesToCsv() {
        string usedForecAxis = isForceX ? "x" : "z";
        string fileName = string.Format("time_series_{0}.csv", usedForecAxis);
         using (var streamWriter = new StreamWriter(string.Format("{0}/{1}", Application.dataPath, fileName))) {
            streamWriter.WriteLine($"t,{usedForecAxis}(t),v(t),F(t) (added)");
            foreach (List<float> timeStep in timeSeries) {
                streamWriter.WriteLine(string.Join(",", timeStep));
                streamWriter.Flush();
            }
        }
    }
    
    void PlotDiagram() {
        // use time_series.csv to plot diagram
        
    }
}
