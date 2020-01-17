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
     public int captureWidth = 768;
     public int captureHeight = 768;
 
     // optional game object to hide during screenshots (usually your scene canvas hud)
     public GameObject hideGameObject; 
 
     // optimize for many screenshots will not destroy any objects so future screenshots will be fast
     public bool optimizeForManyScreenshots = true;
 
     // configure with raw, jpg, png, or ppm (simple raw format)
     public enum Format { RAW, JPG, PNG, PPM };
     public Format format = Format.PNG;
 
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

        //BOTTOM LEFT
        //create list
        GameObject[] bottom_left;
        bottom_left = GameObject.FindGameObjectsWithTag ("bottom_left");

        // record pos of bottom left coords
        Vector3[] screen_shot_pos_left = new Vector3[bottom_left.Length];

        // record names of bottom left coords
        string[] names_bottom_left = new string[bottom_left.Length];

        // iterate through all bottom left points
        for (int i=0;i<bottom_left.Length;i++)
        {
              // convert coords to camera coords
              screen_shot_pos_left[i] = cam.WorldToScreenPoint(bottom_left[i].transform.position);

              // records names into arr
              names_bottom_left[i] = bottom_left[i].name;

              //Debug.Log(string.Format("position = {0},  name = {1}", screen_shot_pos_left[i], names_bottom_left[i]));
        }

        // initialise serialized data string
        string serializedData = "";

        // prep serialized data string
        for (int i=0;i<screen_shot_pos_left.Length;i++)
        {
            serializedData = serializedData + filename + ", " + names_bottom_left[i] + ", " + screen_shot_pos_left[i].x + "," + screen_shot_pos_left[i].y + "\n";
        } 

        
        //TOP RIGHT
        //create list
        GameObject[] top_right;
        top_right = GameObject.FindGameObjectsWithTag ("top_right");

        // record pos of bottom left coords
        Vector3[] screen_shot_pos_right = new Vector3[top_right.Length];

        // record names of bottom left coords
        string[] names_top_right = new string[top_right.Length];

        // iterate through all bottom left points
        for (int i=0;i<top_right.Length;i++)
        {
              // convert coords to camera coords
              screen_shot_pos_right[i] = cam.WorldToScreenPoint(top_right[i].transform.position);

              // records names into arr
              names_top_right[i] = top_right[i].name;

              //Debug.Log(string.Format("position = {0},  name = {1}", screen_shot_pos_right[i], names_top_right[i]));  
             
        }

        // prep serialized data string
        for (int i=0;i<screen_shot_pos_right.Length;i++)
        {
            serializedData = serializedData + filename + ", " + names_top_right[i] + ", " + screen_shot_pos_right[i].x + "," + screen_shot_pos_right[i].y + "\n";
        } 


        //TOP LEFT
        //create list
        GameObject[] top_left;
        top_left = GameObject.FindGameObjectsWithTag ("top_left");

        // record pos of bottom left coords
        Vector3[] screen_shot_pos_top_left = new Vector3[top_left.Length];

        // record names of bottom left coords
        string[] names_top_left = new string[top_left.Length];

        // iterate through all bottom left points
        for (int i=0;i<top_left.Length;i++)
        {
              // convert coords to camera coords
              screen_shot_pos_top_left[i] = cam.WorldToScreenPoint(top_left[i].transform.position);

              // records names into arr
              names_top_left[i] = top_left[i].name;

              //Debug.Log(string.Format("position = {0},  name = {1}", screen_shot_pos_top_left[i], names_top_left[i]));  
             
        }

        // prep serialized data string
        for (int i=0;i<screen_shot_pos_top_left.Length;i++)
        {
            serializedData = serializedData + filename + ", " + names_top_left[i] + ", " + screen_shot_pos_top_left[i].x + "," + screen_shot_pos_top_left[i].y + "\n";
        } 


        //BOTTOM RIGHT
        //create list
        GameObject[] bottom_right;
        bottom_right = GameObject.FindGameObjectsWithTag ("bottom_right");

        // record pos of bottom left coords
        Vector3[] screen_shot_pos_bottom_right = new Vector3[bottom_right.Length];

        // record names of bottom left coords
        string[] names_bottom_right = new string[bottom_right.Length];

        // iterate through all bottom left points
        for (int i=0;i<bottom_right.Length;i++)
        {
              // convert coords to camera coords
              screen_shot_pos_bottom_right[i] = cam.WorldToScreenPoint(bottom_right[i].transform.position);

              // records names into arr
              names_bottom_right[i] = bottom_right[i].name;

              //Debug.Log(string.Format("position = {0},  name = {1}", screen_shot_pos_bottom_right[i], names_bottom_right[i]));  
             
        }

        // prep serialized data string
        for (int i=0;i<screen_shot_pos_bottom_right.Length;i++)
        {
            serializedData = serializedData + filename + ", " + names_bottom_right[i] + ", " + screen_shot_pos_bottom_right[i].x + "," + screen_shot_pos_bottom_right[i].y + "\n";
        } 



        //  write to txt
        System.IO.File.WriteAllText("Assets/data/"+filename+".txt", serializedData);

     } 

     public void take_screenshot(bool captureScreenshot, bool captureVideo) {

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
                 //Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
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

 
     void Update()
     {
        // check keyboard 'k' for one time screenshot capture and holding down 'v' for continious screenshots
        //captureScreenshot |= Input.GetKeyDown("k");
        //captureVideo = Input.GetKey("v");
        captureScreenshot = true;


        double left_x = -60;
        double right_x = 450;
        double top_z = -100.3;
        double bottom_z =  -348;

        // go across
        Vector3 cam_pos = Camera.main.transform.position;
        if (left_x<=cam_pos.x && cam_pos.x<=right_x) {

            transform.Translate(10, 0, 0);
            take_screenshot(captureScreenshot, captureVideo);

        }
        else if (bottom_z<cam_pos.z && cam_pos.z<top_z) {

            //move down by 38 points
            Vector3 new_pos = new Vector3 ((float)left_x, (float)Camera.main.transform.position.y,(float)(Camera.main.transform.position.z-38)); 
            Camera.main.transform.position = new_pos;

        }
        

        Debug.Log(string.Format("position = {0}", Camera.main.transform.position));
        

         
     }
 }