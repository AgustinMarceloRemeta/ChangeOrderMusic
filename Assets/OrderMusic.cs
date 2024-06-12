
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderMusic : MonoBehaviour
{
    public string folderPath;
    public GameObject text;
    public Text textCoroutine;
    public int calledCoroutine = 10;
    int actualCalled;

    public void ChangeMusic()
    {
        folderPath = textCoroutine.text;
        print(folderPath);
        if (Directory.Exists(folderPath))
        {
            List<string> filePaths = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).ToList();
            int number = 0;
            while (filePaths.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, filePaths.Count);
                print(filePaths.Count);
                string name = filePaths[random];
                name = Path.GetFileName(name);
                name = Regex.Replace(name, @"[\d+]", "");
                name = Regex.Replace(name, ".mp", "");
                string numberstring;
                if (number < 10) numberstring = "0" + number.ToString();
                else numberstring = number.ToString();
                string newFilePath = folderPath + "\\" + numberstring + " " + name + ".mp3";
                number++;
                string currentFilePath = filePaths[random];
                print(newFilePath);
                print(currentFilePath);
                File.Move(currentFilePath, newFilePath);
                filePaths.RemoveAt(random);
            }
        }
        else
        {
            StopAllCoroutines();
            actualCalled = calledCoroutine;
            StartCoroutine(FolderNoExist());
        }
    }

    IEnumerator FolderNoExist()
    {
        actualCalled--;
        yield return new WaitForSeconds(.1f);
        text.SetActive(true);
        yield return new WaitForSeconds(.1f);
        text.SetActive(false);
        if(actualCalled > 0)
        {
            StartCoroutine(FolderNoExist());
        }
    }

}
