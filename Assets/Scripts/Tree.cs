using TMPro;
using UnityEngine;

public class Tree : MonoBehaviour {

    public float waterPoints = 5f;

    public int score = 0;
    public TextMeshProUGUI scoreText;


    [Header("Branch")]
    public float growthSpeedHeight;
    public float growthSpeedWidth;
    public float branchStartSize;
    public float branchStopSize;
    public float branchChance = 10f;
    public float waterRequired = 50f;
    public float branchAngle = 90f;
    public int maxBranches;

    public AudioClip branchSFX;

    [Header("Leaf")]
    public float leafChance = 10f;
    public float leafLife = 300f;
    public int maxLeaves;




    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public static Tree Instance { get; set; }
    public GameObject branchPrefab;


    public GameObject SpawnBranch(Branch branch) {
        GameObject branchObj = Instantiate(branchPrefab, branch.transform.position, Quaternion.Euler(0f, 0f, Random.Range(branchAngle * -1, branchAngle)), branch.transform);

        GameObject rootObj = new GameObject("RootBranch");
        rootObj.transform.SetParent(branch.transform);
        rootObj.transform.position = branch.transform.position;

        Branch b = branchObj.GetComponent<Branch>();
        b.rootTransform = rootObj.transform;
        b.parentBranch = branch;

      //  SoundManager.PlayRandomSfx(branchSFX);

        return branchObj;
    }

    public void AddScore() {
        score++;
        scoreText.text = score.ToString();
    }
}
