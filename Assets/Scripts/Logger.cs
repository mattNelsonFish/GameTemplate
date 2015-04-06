using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Logger : MonoBehaviour{
	static Logger _instance;

	private Logger(){}

	public static Logger Instance{
		get
		{
			if (!_instance)
			{
				_instance = (Logger)GameObject.FindObjectOfType(typeof(Logger));
				if (!_instance)
				{
					GameObject container = new GameObject();
					container.name = "Logger";
					_instance = container.AddComponent(typeof(Logger)) as Logger;
				}
			}
			
			return _instance;
		}
	}

	private static string m_dataPath = "";
	private static string m_fileName = "";
	private static string m_filePath = "";
	private static string m_toDumpToFile = "";

	void Awake(){
		if(_instance != null){
			Destroy(gameObject);
		}
		else{
			_instance = this;

			DateTime datetime = DateTime.Now;
			m_fileName = datetime.Day+"_"+datetime.Month+"_"+datetime.Hour+"h"+datetime.Minute+"m"+datetime.Second+"s__log.txt";
			
			m_dataPath = Application.dataPath + "/Logs";
			if(!Directory.Exists(m_dataPath)){
				Directory.CreateDirectory(m_dataPath);
			}
			
			m_filePath = m_dataPath+"/"+m_fileName;
			
			if(!File.Exists(m_filePath)){
				File.Create(m_filePath);
			}
			
			StartCoroutine(DumpToLogOnTick());
		}
	}

	public void LogWarning(string sWarning){
		addToPile ("<WRN> "+CurrentMinSecMs+"\t"+sWarning);
	}

	public void LogError(string sError){
		addToPile ("<ERR> "+CurrentMinSecMs+"\t"+sError);
	}

	public void LogComment(string sComment){
		addToPile ("[cmt] "+CurrentMinSecMs+"\t"+sComment);
	}

	public string CurrentMinSecMs{
		get{
			return DateTime.Now.Minute+":"+DateTime.Now.Second+":"+DateTime.Now.Millisecond;
		}
	}

	private void addToPile(string logToPile){
		m_toDumpToFile += logToPile+"\n";
	}

	private IEnumerator DumpToLogOnTick(){
		yield return new WaitForSeconds(1f);
		if(m_toDumpToFile!=""){
			writeLog(m_toDumpToFile);
			m_toDumpToFile = "";
		}
	}

	private void writeLog(string logToAdd){
		File.AppendAllText(m_filePath, logToAdd);
	}
}
