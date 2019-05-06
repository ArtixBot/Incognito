using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

public class PrefabGen : MonoBehaviour
{
    private Stream Deflated { get; set; }
    private BinaryReader Reader { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        // TXT Parsing
        // string path = "Assets/Prefabs/prefab_test.txt";
        // StreamReader reader = new StreamReader(path); 
        // string ASCII_codes = reader.ReadToEnd();
        // byte[] asciiBytes = Encoding.UTF8.GetBytes(ASCII_codes);
        // for(int i = 0; i < asciiBytes.Length; i++){
        //     Debug.Log("Tile " + i + ": " + asciiBytes[i]);
        // }
        // reader.Close();
        
        // Rudimentary .xp parser.
        // It actually works, somehow...
        FileInfo path = new FileInfo("Assets/Prefabs/prefab_test.xp");
        FileStream originalFileStream = path.OpenRead();
        GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);

        MemoryStream bigStreamOut = new MemoryStream();
        decompressionStream.CopyTo(bigStreamOut);
        byte[] results = bigStreamOut.ToArray();
        foreach(byte x in results){
            Debug.Log(x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
