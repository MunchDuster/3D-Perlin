using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public float maxDist = 10;
    public AnimationCurve cameraLerp = AnimationCurve.Constant(0, 1, 1);
    public Vector3 cameraOffset = new Vector3(0, 3, -5);
    public new Transform camera;

    private List<Vector3> lastPoss;
    void Start()
    {
        lastPoss = new List<Vector3>();
        camera.position = transform.TransformPoint(cameraOffset * 2);
        camera.LookAt(transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 direction = transform.position - camera.position;
        float dist = direction.magnitude;
        Vector3 target = transform.TransformPoint(cameraOffset);
        //lastPoss.Add(target);

        camera.position = Vector3.Lerp(target, camera.position, 5 * Time.deltaTime);
        camera.rotation = Quaternion.Lerp(camera.rotation, Quaternion.LookRotation(direction.normalized, transform.up), 5 * Time.deltaTime);
        /*
        if (dist <= maxDist)
            camera.position += direction.normalized * cameraLerp.Evaluate(dist / maxDist);
        else
            camera.position += direction.normalized * (dist - maxDist);


        camera.LookAt(transform);
        */

    }
}
