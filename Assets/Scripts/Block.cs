using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro[] _scoreTexts;

    public Rigidbody Rb { get; private set; }

    public int Id { get; private set; }
    public int Point { get; private set; }
    public int Score
    {
        get
        {
            var score = 1;
            for (var i = 0; i < Point; i++)
            {
                score *= 2;
            }
            return score;
        }
    }
    private bool _thrown = false;
    public float BadZoneSeconds { get; private set; } = 0;

    private Material _material;

    private void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
        Rb.isKinematic = true;
    }

    public void Initialize(int id, int point)
    {
        Id = id;
        Point = point;
        UpdateScore();
    }

    public void Throw()
    {
        Rb.isKinematic = false;
        Rb.AddForce(Vector3.back * 750);
        _thrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var opponent = collision.gameObject.GetComponent<Block>();
        if (opponent == null)
            return;

        if (opponent.Point == Point && Id < opponent.Id)
        {
            Game.Instance.HitBlocks(this, opponent);
        }
    }

    public void GrowUp(Vector3 opponentPosition, Rigidbody opponentRigidbody)
    {
        // Establezca la posición en el centro de los dos y agregue potencia
        transform.position = (transform.position + opponentPosition) / 2;
        var vec = (transform.position - opponentPosition).normalized;
        Rb.AddForce(vec * opponentRigidbody.velocity.magnitude * opponentRigidbody.mass * 10);

        BadZoneSeconds = 0;

        Point++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        var scoreStr = Score.ToString();

        foreach (var scoreText in _scoreTexts)
        {
            scoreText.text = scoreStr;
            scoreText.fontSize = 44 - Point;
        }

        var color = _blockColors[Point % _blockColors.Length];
        if (_material == null)
        {
            _material = gameObject.GetComponent<MeshRenderer>().material;
        }
        _material.color = color;
    }

    private static Color[] _blockColors;
    public static void InitializeBlockColors()
    {
        const int HNUM = 6;
        const int SNUM = 3;
        const float HSPACE = 1f / HNUM;
        _blockColors = new Color[SNUM * HNUM];
        for (var sCnt = 0; sCnt < SNUM; sCnt++)
        {
            for (var hCnt = 0; hCnt < HNUM; hCnt++)
            {
                var h = HSPACE * hCnt;
                var s = 1f - 0.2f * sCnt;
                var v = 1f;
                var color = Color.HSVToRGB(h, s, v);
                _blockColors[sCnt * HNUM + hCnt] = color;
            }
        }
    }

    private void Update()
    {
        if (_thrown)
        {
            var pos = transform.position;
            if (pos.z > -4 && pos.y < 0.5f)
            {
                BadZoneSeconds += Time.deltaTime;
            }
            else
            {
                BadZoneSeconds = 0;
            }
        }
    }
}
