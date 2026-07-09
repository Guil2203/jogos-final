using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<TargetSO> _possibleTargets;
    [SerializeField] private Target _targetPrefab;
    private bool _gameStarted;
    [SerializeField] private float _spawnCooldown = 1f;
    private float _timer;
    [SerializeField] private float _gameDuration = 60f;
    private float _spawnTimer;
    private int _score;
    private float _timeUpdate = 0f;
    
    [SerializeField] private TextMeshProUGUI _scoreText, _timeLeftText;
    [SerializeField] private Button _gameOverBtn;
    
    //[Header("Audios")]
    

    

    void Awake()
    {
        _gameOverBtn.onClick.AddListener(EndGame);
    }

    void OnEnable()
    {
        Target.OnHit += AddScore;
        
    }

    void OnDisable()
    {
        Target.OnHit -= AddScore;
    }

    private void AddScore(int score)
    {
        _score += score;   
        _scoreText.text = _score.ToString();
    }

    public void StartGame()
    {
        _gameStarted = true;
    }

    private void GameOver()
    {
        _gameOverBtn.gameObject.SetActive(true);
        _gameStarted = false;
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameStarted) return;
        _timer += Time.deltaTime;
        _spawnTimer += Time.deltaTime;
        _timeUpdate += Time.deltaTime;


        if (_timeUpdate > 0.5f)
        {
            TimeSpan time = TimeSpan.FromSeconds(_gameDuration - _timer);
            string formattedTime = time.ToString(@"m\:ss");
            _timeLeftText.text = formattedTime;
            _timeUpdate = 0f;
        }
        if (_timer >= _gameDuration)
        {
            GameOver();
        }

        if (_spawnTimer >= _spawnCooldown)
        {
            var randomPos = Random.insideUnitCircle * 5f;
            
            float result = Random.Range(0, _possibleTargets.Sum(x => x.Chance)); // expressão Lambda (bem importante 👀)
            (int intID, int intSum) chancePackage = (0, _possibleTargets[0].Chance);
            while (result > chancePackage.intSum)
            {
                Debug.Log("Result: "  + result + " ID: " + chancePackage.intID + " Sum: " + chancePackage.intSum);
                chancePackage.intID++;
                chancePackage.intSum += _possibleTargets[chancePackage.intID].Chance;
                
            }
            var randomTargetSO = _possibleTargets[chancePackage.intID];
            var target = Instantiate(_targetPrefab, new Vector3(randomPos.x, randomPos.y, 0f), Quaternion.Euler(-90, 0, 0));
            target.SetTarget(randomTargetSO);
            _spawnTimer = 0f;
        }
    }
}