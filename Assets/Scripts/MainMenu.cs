using System.Collections;
using UnityEngine;

public class ImageSwitcher : MonoBehaviour
{
    public GameObject[] images;  // Assign the 3 image GameObjects in the Inspector
    public float delayForFirstImage = 3f;  // Delay for the first image (3 seconds)
    public float delayForOtherImages = 1f; // Delay for other images (1 second)

    private int currentIndex = 0;

    void Start()
    {
        // Start the coroutine to cycle through the images
        StartCoroutine(SwitchImages());
    }

    IEnumerator SwitchImages()
    {
        while (currentIndex < images.Length)
        {
            // Set all images inactive
            foreach (GameObject image in images)
            {
                image.SetActive(false);
            }

            // Activate the current image
            images[currentIndex].SetActive(true);

            // Wait based on whether the current image is the first one (index 0)
            if (currentIndex == 0)
            {
                yield return new WaitForSeconds(delayForFirstImage);
            }
            else
            {
                yield return new WaitForSeconds(delayForOtherImages);
            }

            // Increment the index
            currentIndex++;

            // If it's the last index, break the loop and stop switching
            if (currentIndex == images.Length)
            {
                break;
            }
        }
    }
}
