using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� ���� ���¸� ǥ���ϰ�, ���� ������ UI�� �����ϴ� ���� �Ŵ���
// ������ �� �ϳ��� ���� �Ŵ����� ������ �� �ִ�.
public class GameManager : MonoBehaviour
{
    public delegate void BestScore();
    public BestScore bestScoreDelegate;
    public static GameManager instance; // �̱����� �Ҵ��� ���� ����

    public GameObject platform; //��ֹ� ���ӿ�����Ʈ
    public GameObject ExplainText; //���� UI�ؽ�Ʈ
    public GameObject ExPlainArrow;
    public GameObject Generator; //��ֹ� ������

    public GameObject GameOverUI; //���� ������ ������� UI
    public bool isGameover = false; // ���� ���� ����
    public Text scoreText; // ������ ����� UI �ؽ�Ʈ
    public Text recordText; // �ְ� ������ ��Ÿ�� �ؽ�Ʈ

    public int Score { get; set; } = 0; // ���� ����

    // ���� ���۰� ���ÿ� �̱����� ����
    void Awake()
    {
        //���� �Ŵ��� ������ ȭ�� ũ�� �����ϰ� 30����������
        Screen.SetResolution(450, 800, true, 30);
        // �̱��� ���� instance�� ����ִ°�?
        if (instance == null)
        {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
        }
        else
        {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���

            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �ΰ� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }

        //���� �Ŵ������� ��������� ���ش�.
        SoundManager.Instance.sounds[0].Play();

        //�ְ��� ��� ���μ����� ��������Ʈ�� ����ϰ�
        bestScoreDelegate = ScoreProcess;
        //�����Ѵ�.
        bestScoreDelegate();
        //���� ���� ��ֹ��� ���� Ǫ����
        platform.GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.blue;
    }

    // ������ ������Ű�� �޼���
    public void AddScore()
    {
        //���ӿ����� �ƴ϶��
        if (!isGameover)
        {
            //��������
            Score++;
            scoreText.text = Score.ToString();
        }
    }

    // �÷��̾� ĳ���Ͱ� ����� ���� ������ �����ϴ� �޼���
    public void OnPlayerDead()
    {
        isGameover = true;  //���� ���� ���� true��
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //���� Ȱ��ȭ �Ǿ��ִ� ���� �����
    }

    //�ְ��� ��� ���μ���
    private void ScoreProcess()
    {
        //�ְ����� BestScore ��� Ű�� �޾ƿ´�
        int bestScore = PlayerPrefs.GetInt("BestScore");

        //���� ������ �ְ����� �Ѿ�����
        if(Score > bestScore)
        {
            bestScore = Score; //�����Ͽ�
            PlayerPrefs.SetInt("BestScore", bestScore); //BestScore Ű�� �����Ѵ�
        }
        //�ؽ�Ʈ ����
        recordText.text = "Best Score : " + bestScore;
    }

    //�ְ����� �����ϴ� �Լ��� �ΰ� ���� ��ũ���� ��ư�� �ϳ��ξ� �����Ͽ� ���
    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0); //BestScore Ű�� 0�� ����
        recordText.text = "Best Score : 0"; //�ؽ�Ʈ ǥ�� ���� �ٲ���
    }
}