using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _dataGame;

    [SerializeField] Road _roadStart;
    [SerializeField] Road _roadEnd;

    [SerializeField] Car  _car;

    [SerializeField] Image _pauseImage;

    private float timePassing;
    private float percentTrack;
    private float percentTension;

    private bool clicPause = false;

    private float pauseTime = 0;
    private float bestTime = 0;

    [Serializable]
    class SaveData
    {
        public float bestTime;
        public int numberAttepts;
    }
    void Update()
    {
        TimePassing();
        Arrow.PercsentArrow += PercentTension;
        PassedTreck();
        Print();
    }
    void Print()
    {
        _dataGame.text = timePassing.ToString("0.0000") + "\n" + percentTrack.ToString() + " %" + "\n" +
                         percentTension.ToString() + " %";
    }
    void TimePassing ()
    {
        if (!clicPause)
        {
            timePassing = Time.deltaTime;
            pauseTime = timePassing;
        }
        else if (pauseTime > bestTime)
            bestTime = pauseTime;      
    }
    void PassedTreck()
    {     
            Vector3 length = _roadEnd.transform.position - _roadStart.transform.position;
            percentTrack = Mathf.Round(_car.transform.position.magnitude * 100 / length.magnitude - 1);  
    }
    void PercentTension(float person)
    {
        percentTension = -person;
    }
    public void OnPauseButton()
    {
        
        clicPause = true;
        SaveGame();
        StartCoroutine(WaitForTime());
    }
    public void OnProceedButton()
    {
        Time.timeScale = 1;
        _pauseImage.gameObject.SetActive(false);
    }
    public void OnAgainButton()
    {
        SceneManager.LoadScene("GamePlay");
        Instantiate(_car.gameObject);
    }
    public void OnExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(5.0f);
        PausePlay();
    }
    void PausePlay ()
    {
        Time.timeScale = 0;
        _pauseImage.gameObject.SetActive(true);
    }
    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ScoreTable.csv");
        SaveData data = new SaveData();
        data.bestTime = bestTime;
        data.numberAttepts = 0;
        formatter.Serialize(file, data);
        file.Close();
    }
}

