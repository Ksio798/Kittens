using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class SaveData//Сохраняемая информация
{
	public List<int> LevelsId = new List<int>();
}

public class SaveController : MonoBehaviour
{
	public static SaveController Instance;//Статичный экземпляр объекта для доступа в любой части кода

	public string FileName = "save.svs";
	public SaveData Save = null;

	void Awake()//Назначение синглтона
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);//Отключение удаления объекта при переходе на новую сцену
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void CreateSave(int ID)//Добавление информации в существующее сохранение или создание нового
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

	public void SaveData()//Запись сохранения в файл
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

	public void LoadData()//Загрузка сохранения из файла
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
	string getFilePath()//Автоматическое получение пути к файлу сохранения
	{
		string filePath = Path.Combine(Application.persistentDataPath, FileName);
		return filePath;
	}
}
