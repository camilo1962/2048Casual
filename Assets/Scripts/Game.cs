using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : SingletonMonoBehaviour<Game>
{
    [SerializeField]
    private Block _blockPrefab;

    [SerializeField]
    private ParticleSystem _hitEffectPrefab;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _recordText;

    [SerializeField]
    private int nivelNumero;

    [SerializeField]
    private GameObject _gameOverText;

    [SerializeField]
    private GameObject _button;

    [SerializeField]
    private AudioSource _seAudioSource;

    [SerializeField]
    private AudioClip _breakSe;


    private List<Block> _blocks = new List<Block>();
    private Block _currentBlock = null;
    private DateTime _thrownTime;
    private int _id = 0;
    private List<Tuple<Block, Block>> _hitPairBlocks = new List<Tuple<Block, Block>>();
    private int _totalScore = 0;
    int cambioNivel = 10000;

    private enum GameState
    {
        None,
        Initialize,
        SpawnBlock,
        WaitForDrag,
        Dragging,
        ThrowBlock,
        Thrown,
        GameOver,
    }

    private GameState _state = GameState.None;

    void Start()
    {
        Physics.gravity = Vector3.down * 4.0f;
        Block.InitializeBlockColors();
        _state = GameState.Initialize;
        _button.SetActive(false);
        _recordText.text = PlayerPrefs.GetInt("RecordText" + nivelNumero, 0).ToString();
        
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.Initialize:
                foreach (var b in _blocks)
                {
                    Destroy(b.gameObject);
                }
                _blocks.Clear();
                _currentBlock = null;
                _thrownTime = DateTime.MinValue;
                _id = 0;
                _hitPairBlocks.Clear();
                _totalScore = 0;
                _gameOverText.SetActive(false);
                _button.SetActive(false);
                _scoreText.text = _totalScore.ToString();
               
                _state = GameState.SpawnBlock;
                return;

            case GameState.SpawnBlock:
                var block = Instantiate(_blockPrefab, this.transform);
                _id++;
                block.Initialize(_id, UnityEngine.Random.Range(1, 7));
                _currentBlock = block;
                _blocks.Add(block);
                _state = GameState.WaitForDrag;
                break;
            case GameState.WaitForDrag:
                if (Input.GetMouseButton(0))
                {
                    _state = GameState.Dragging;
                }
                break;
            case GameState.Dragging:
                if (Input.GetMouseButtonUp(0))
                {
                    _state = GameState.ThrowBlock;
                }
                else
                {
                    DragBlock();
                }
                break;
            case GameState.ThrowBlock:
                _currentBlock.Throw();
                _thrownTime = DateTime.UtcNow;
                _state = GameState.Thrown;
                break;
            case GameState.Thrown:
                if ((DateTime.UtcNow - _thrownTime).TotalSeconds > 1)
                {
                    _state = GameState.SpawnBlock;
                }
                break;

            case GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _state = GameState.Initialize;
                   
                }
                return;

            case GameState.None:
            default:
                break;
        }

        foreach (var pair in _hitPairBlocks)
        {
            var growBlock = pair.Item1;
            var destroyBlock = pair.Item2;
            growBlock.GrowUp(destroyBlock.transform.position, destroyBlock.Rb);
            Instantiate(_hitEffectPrefab, destroyBlock.transform.position, Quaternion.identity);
            _blocks.Remove(destroyBlock);
            Destroy(destroyBlock.gameObject);
            _seAudioSource.PlayOneShot(_breakSe);
            _totalScore += growBlock.Score;
            _scoreText.text = _totalScore.ToString();

            if(_totalScore > PlayerPrefs.GetInt("RecordText" + nivelNumero, 0))
            {
                PlayerPrefs.SetInt("RecordText" + nivelNumero, _totalScore);
                _recordText.text = _totalScore.ToString();
                if(_totalScore > cambioNivel)
                {
                    GetComponent<MakeItButton>();
                }
            }

            
            
        }
        _hitPairBlocks.Clear();

        foreach (var block in _blocks)
        {
            if (block.BadZoneSeconds > 3)
            {
                _gameOverText.SetActive(true);
                _state = GameState.GameOver;
                _button.SetActive(true);
            }
        }
    }

    private void DragBlock()
    {
        var mousePos = Input.mousePosition;
        var targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        var pos = _currentBlock.transform.localPosition;
        var targetPosX = Mathf.Clamp(targetPos.x, -2.5f, 2.5f);
        _currentBlock.transform.localPosition = new Vector3(targetPosX, pos.y, pos.z);
    }

    public void HitBlocks(Block a, Block b)
    {
        // ID�̏��������� Item1, �傫������ Item2 �ɂ���
        var pair = a.Id < b.Id ? Tuple.Create(a, b) : Tuple.Create(b, a);
        _hitPairBlocks.Add(pair);
    }

    public void Jugar()
    {
        Physics.gravity = Vector3.down * 4.0f;
        Block.InitializeBlockColors();
        _state = GameState.Initialize;
    }
    public void BorrarRecord()
    {
        PlayerPrefs.DeleteKey("RecordText" + nivelNumero);
        _recordText.text = "0";
    }
    public void Salir()
    {
        Application.Quit();
    }
    public void IrAOtra(string name)
    {
        SceneManager.LoadScene(name);

    }





}
