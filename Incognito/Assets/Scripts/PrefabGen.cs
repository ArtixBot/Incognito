using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using UnityEngine;

public class PrefabGen : MonoBehaviour
{
    private Stream Deflated { get; set; }
    private BinaryReader Reader { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
        // .xp file format, as per manual specifications:
        // #-----xp format version (32)
        // A-----number of layers (32)
        //  /----image width (32)
        //  |    image height (32)
        //  |  /-ASCII code (32) (little-endian!)
        // B|  | foreground color red (8)
        //  |  | foreground color green (8)
        //  |  | foreground color blue (8)
        //  | C| background color red (8)
        //  |  | background color green (8)
        //  \--\-background color blue (8)
        
        // Rudimentary .xp parser. Handles single-layer images.
        // It actually works, somehow...
        FileInfo path = new FileInfo("Assets/Prefabs/prefab_test_3.xp");
        FileStream originalFileStream = path.OpenRead();
        GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);

        MemoryStream bigStreamOut = new MemoryStream();
        decompressionStream.CopyTo(bigStreamOut);
        byte[] results = bigStreamOut.ToArray();                            // Contains #, A, B, and C.
        int width = results[8];
        int height = results[12];

        byte[] layer = results.Skip(16).Take(results.Length).ToArray();     // Contains C.
        string ASCII = "";
        string Foreground_RGB = "";
        string Background_RGB = "";
        for (int i = 0; i < 10; i++){
            
            if (i < 4){
                ASCII = ASCII + layer[i] + " ";
            } else if (i < 7){
                Foreground_RGB = Foreground_RGB + layer[i] + " ";
            } else{
                Background_RGB = Background_RGB + layer[i] + " ";
            }
        }
        Debug.Log(ASCII);
        Debug.Log(Foreground_RGB);
        Debug.Log(Background_RGB);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
