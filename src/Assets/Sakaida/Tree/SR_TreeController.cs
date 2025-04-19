using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_TreeController : MonoBehaviour
{

    public List<TreeInfo> Trees = new List<TreeInfo>();
    [Space]
    public List<Sprite> TreeImage = new List<Sprite>();
    public List<Sprite> RightTreeImage = new List<Sprite>();
    public List<Sprite> LeftTreeImage = new List<Sprite>();

    [SerializeField] GameObject TreePrefab;

    public float UpY = 1;

    // Start is called before the first frame update
    void Start()
    {
        TreeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TreeSpawn() 
    {
        int i = 0;
        foreach (TreeInfo tree in Trees) 
        {

            Vector2 TreePos = transform.position;
            TreePos.y += UpY * i;
            GameObject CL_Trees = Instantiate(TreePrefab, TreePos, Quaternion.identity);
            CL_Trees.transform.parent = transform;

            tree.MyTree = CL_Trees;

            SR_Tree sR_Tree = CL_Trees.GetComponent<SR_Tree>();

           int RandomTreeImage = Random.Range(0,TreeImage.Count);
           sR_Tree.TreeImage.sprite = TreeImage[RandomTreeImage];

            int RandomBranchImage_R = Random.Range(0, RightTreeImage.Count);
            sR_Tree.RightBranceImage.sprite = RightTreeImage[RandomBranchImage_R];

            int RandomBranchImage_L = Random.Range(0, LeftTreeImage.Count);
            sR_Tree.LeftBranceImage.sprite = LeftTreeImage[RandomBranchImage_L];


            sR_Tree.Right = tree.Right; sR_Tree.Left = tree.Left;sR_Tree.SquirrelHole = tree.SquirrelHole;

            i++;
        }
    
    }


    [System.Serializable]
    public class TreeInfo
    {
        public bool Right = true; 
        public bool Left = true;

        public  GameObject MyTree;

        public bool SquirrelHole = true;

        public Sprite _TreeImage;
    }
}
