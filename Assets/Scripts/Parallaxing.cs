using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; //All the back and foreground to be parallaxed
    private float[] parallaxScales; // proportion of the camera"s movement to move the background by
    public float smoothing = 1f; //How smooth the parallax is going to be. Make sure to set this above 0
    private Transform cam; // reference to the main camera transform
    private Vector3 previousCamPos; //Store the position of the camera in the previous frame

    private void Awake()
    {
        cam = Camera.main.transform;
    }
    void Start()
    {
        previousCamPos = cam.position;

        //assigning corresponding parallaxScales
        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        //for each background
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the current position + the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }


        //set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
