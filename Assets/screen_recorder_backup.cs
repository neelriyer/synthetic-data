/*
using UnityEngine;
 using System.Collections;
 using System.IO;
 
 // Screen Recorder will save individual images of active scene in any resolution and of a specific image format
 // including raw, jpg, png, and ppm.  Raw and PPM are the fastest image formats for saving.
 //
 // You can compile these images into a video using ffmpeg:
 // ffmpeg -i screen_3840x2160_%d.ppm -y test.avi
 
 public class ScreenRecorder : MonoBehaviour
 {

    Camera cam;

     // 4k = 3840 x 2160   1080p = 1920 x 1080
     public int captureWidth = 1920;
     public int captureHeight = 1080;
 
     // optional game object to hide during screenshots (usually your scene canvas hud)
     public GameObject hideGameObject; 
 
     // optimize for many screenshots will not destroy any objects so future screenshots will be fast
     public bool optimizeForManyScreenshots = true;
 
     // configure with raw, jpg, png, or ppm (simple raw format)
     public enum Format { RAW, JPG, PNG, PPM };
     public Format format = Format.PPM;
 
     // folder to write output (defaults to data path)
     public string folder;
 
     // private vars for screenshot
     private Rect rect;
     private RenderTexture renderTexture;
     private Texture2D screenShot;
     private int counter = 0; // image #
 
     // commands
     private bool captureScreenshot = false;
     private bool captureVideo = false;
 
     // create a unique filename using a one-up variable
     private string uniqueFilename(int width, int height)
     {
         // if folder not specified by now use a good default
         if (folder == null || folder.Length == 0)
         {
             folder = Application.dataPath;
             if (Application.isEditor)
             {
                 // put screenshots in folder above asset path so unity doesn't index the files
                 var stringPath = folder + "/..";
                 folder = Path.GetFullPath(stringPath);
             }
             folder += "/assets/screenshots";
 
             // make sure directoroy exists
             System.IO.Directory.CreateDirectory(folder);
 
             // count number of files of specified format in folder
             string mask = string.Format("screen_{0}x{1}*.{2}", width, height, format.ToString().ToLower());
             counter = Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly).Length;
         }
 
         // use width, height, and counter for unique file name
         var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", folder, width, height, counter, format.ToString().ToLower());
 
         // up counter for next call
         ++counter;
 
         // return unique filename
         return filename;
     }

     void Start()
     {
        //delete folder and recreate
         string screenshot_dir = "assets/screenshots";
         Directory.Delete(screenshot_dir, true);
         Directory.CreateDirectory(screenshot_dir);

         string data_dir = "assets/data";
         Directory.Delete(data_dir, true);
         Directory.CreateDirectory(data_dir);
     }
 
     public void CaptureScreenshot()
     {
         captureScreenshot = true;
     }

     public void write_data(string filename)
     {
        cam = GetComponent<Camera>();

        //get coordinates for eachc centroid
        Vector3 bl1 = cam.WorldToScreenPoint(GameObject.Find("bl1").transform.position);
        Vector3 bl1_1 = cam.WorldToScreenPoint(GameObject.Find("bl1 (1)").transform.position);
        Vector3 bl1_2 = cam.WorldToScreenPoint(GameObject.Find("bl1 (2)").transform.position);
        Vector3 bl1_3 = cam.WorldToScreenPoint(GameObject.Find("bl1 (3)").transform.position);
        Vector3 bl1_4 = cam.WorldToScreenPoint(GameObject.Find("bl1 (4)").transform.position);
        Vector3 bl1_5 = cam.WorldToScreenPoint(GameObject.Find("bl1 (5)").transform.position);
        Vector3 bl1_6 = cam.WorldToScreenPoint(GameObject.Find("bl1 (6)").transform.position);
        Vector3 bl1_7 = cam.WorldToScreenPoint(GameObject.Find("bl1 (7)").transform.position);
        Vector3 bl1_8 = cam.WorldToScreenPoint(GameObject.Find("bl1 (8)").transform.position);

        Vector3 tr1 = cam.WorldToScreenPoint(GameObject.Find("tr1").transform.position);
        Vector3 tr1_1 = cam.WorldToScreenPoint(GameObject.Find("tr1 (1)").transform.position);
        Vector3 tr1_2 = cam.WorldToScreenPoint(GameObject.Find("tr1 (2)").transform.position);
        Vector3 tr1_3 = cam.WorldToScreenPoint(GameObject.Find("tr1 (3)").transform.position);
        Vector3 tr1_4 = cam.WorldToScreenPoint(GameObject.Find("tr1 (4)").transform.position);
        Vector3 tr1_5 = cam.WorldToScreenPoint(GameObject.Find("tr1 (5)").transform.position);
        Vector3 tr1_6 = cam.WorldToScreenPoint(GameObject.Find("tr1 (6)").transform.position);
        Vector3 tr1_7 = cam.WorldToScreenPoint(GameObject.Find("tr1 (7)").transform.position);
        Vector3 tr1_8 = cam.WorldToScreenPoint(GameObject.Find("tr1 (8)").transform.position);
        Vector3 tr1_9 = cam.WorldToScreenPoint(GameObject.Find("tr1 (9)").transform.position);

         string serializedData = 
     filename + ", " + "bl1, " + bl1.x + "," + bl1.y + "\n" +
     filename + ", " + "bl1_1, " + bl1_1.x + "," + bl1_1.y + "\n" +
     filename + ", " + "bl1_2, " + bl1_2.x + "," + bl1_2.y + "\n" + 
     filename + ", " + "bl1_3, " + bl1_3.x + "," + bl1_3.y + "\n" +
     filename + ", " + "bl1_3, " + bl1_3.x + "," + bl1_3.y + "\n" +
     filename + ", " + "bl1_4, " + bl1_4.x + "," + bl1_4.y + "\n" +
     filename + ", " + "bl1_5, " + bl1_5.x + "," + bl1_5.y + "\n" +
     filename + ", " + "bl1_6, " + bl1_6.x + "," + bl1_6.y + "\n" + 
     filename + ", " + "bl1_7, " + bl1_7.x + "," + bl1_7.y + "\n" +
     filename + ", " + "bl1_8, " + bl1_8.x + "," + bl1_8.y + "\n" +

     filename + ", " + "tr1, " + tr1.x + "," + tr1.y + "\n" +
     filename + ", " + "tr1_1, " + tr1_1.x + "," + tr1_1.y + "\n" +
     filename + ", " + "tr1_2, " + tr1_2.x + "," + tr1_2.y + "\n" + 
     filename + ", " + "tr1_3, " + tr1_3.x + "," + tr1_3.y + "\n" +
     filename + ", " + "tr1_3, " + tr1_3.x + "," + tr1_3.y + "\n" +
     filename + ", " + "tr1_4, " + tr1_4.x + "," + tr1_4.y + "\n" +
     filename + ", " + "tr1_5, " + tr1_5.x + "," + tr1_5.y + "\n" +
     filename + ", " + "tr1_6, " + tr1_6.x + "," + tr1_6.y + "\n" + 
     filename + ", " + "tr1_7, " + tr1_7.x + "," + tr1_7.y + "\n" +
     filename + ", " + "tr1_8, " + tr1_8.x + "," + tr1_8.y + "\n";

        System.IO.File.WriteAllText("Assets/data/"+filename+".txt", serializedData);

     } 
 
     void Update()
     {
         // check keyboard 'k' for one time screenshot capture and holding down 'v' for continious screenshots
         captureScreenshot |= Input.GetKeyDown("k");
         captureVideo = Input.GetKey("v");
 
         if (captureScreenshot || captureVideo)
         {

             //set to false
             captureScreenshot = false;
 
             // hide optional game object if set
             if (hideGameObject != null) {
                GameObject.Find("lower2").SetActive(false);
             }
 
             // create screenshot objects if needed
             if (renderTexture == null)
             {
                 // creates off-screen render texture that can rendered into
                 rect = new Rect(0, 0, captureWidth, captureHeight);
                 renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
                 screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
             }
         
             // get main camera and manually render scene into rt
             Camera camera = this.GetComponent<Camera>(); // NOTE: added because there was no reference to camera in original script; must add this script to Camera
             camera.targetTexture = renderTexture;
             camera.Render();
 
             // read pixels will read from the currently active render texture so make our offscreen 
             // render texture active and then read the pixels
             RenderTexture.active = renderTexture;
             screenShot.ReadPixels(rect, 0, 0);
 
             // reset active camera texture and render texture
             camera.targetTexture = null;
             RenderTexture.active = null;
 
             // get our unique filename
             string filename = uniqueFilename((int) rect.width, (int) rect.height);
             string[] words = filename.Split('/');
             string last_word = words[words.Length - 1];


 
             // pull in our file header/data bytes for the specified image format (has to be done from main thread)
             byte[] fileHeader = null;
             byte[] fileData = null;
             if (format == Format.RAW)
             {
                 fileData = screenShot.GetRawTextureData();
             }
             else if (format == Format.PNG)
             {
                 fileData = screenShot.EncodeToPNG();
             }
             else if (format == Format.JPG)
             {
                 fileData = screenShot.EncodeToJPG();
             }
             else // ppm
             {
                 // create a file header for ppm formatted file
                 string headerStr = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
                 fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
                 fileData = screenShot.GetRawTextureData();
             }
 
             // create new thread to save the image to file (only operation that can be done in background)
             new System.Threading.Thread(() =>
             {
                 // create file and write optional header with image bytes
                 var f = System.IO.File.Create(filename);
                 if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
                 f.Write(fileData, 0, fileData.Length);
                 f.Close();
                 Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
             }).Start();

             //write data
             write_data(last_word); 
             // unhide optional game object if set
             if (hideGameObject != null) hideGameObject.SetActive(true);
 
             // cleanup if needed
             if (optimizeForManyScreenshots == false)
             {
                 Destroy(renderTexture);
                 renderTexture = null;
                 screenShot = null;
             }
         }
     }
 }

 */