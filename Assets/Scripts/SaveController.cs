using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class SaveData
{
	public List<int> LevelsId = new List<int>();
}

public class SaveController : MonoBehaviour
{
	public static SaveController Instance;

	public string FileName = "save.svs";
	public SaveData Save = null;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void CreateSave(int ID)
	{
		if (Save == null)
		{
			SaveData newSD = new SaveData();
			newSD.LevelsId.Add(ID);
			Save = newSD;
		}
		else
		{
			if (!Save.LevelsId.Contains(ID))
				Save.LevelsId.Add(ID);
		}
	}

	public void SaveData()
	{

		BinaryFormatter bf = new BinaryFormatter();

		string path = getFilePath();
		if (File.Exists(path))
		{
			FileStream file = File.OpenWrite(path);
			bf.Serialize(file, Save);
			file.Close();
		}
		else
		{
			FileStream file = File.Create(path);
			bf.Serialize(file, Save);
			file.Close();
		}
	}

	public void LoadData()
	{
		BinaryFormatter bf = new BinaryFormatter();

		string path = getFilePath();
		if (File.Exists(path))
		{
			FileStream file = File.Open(path, FileMode.Open);
			Save = (SaveData)bf.Deserialize(file);

			file.Close();
		}
	}

	public void DeleteSave()
	{
		Save = null;
		File.Delete(getFilePath());
	}
	string getFilePath()
	{
		string filePath = Path.Combine(Application.persistentDataPath, FileName);
		return filePath;
	}
}
