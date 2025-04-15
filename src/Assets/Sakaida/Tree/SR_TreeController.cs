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

            SR_Tree sR_Tree = CL_Trees.GetComponent<SR_Tree>();
            sR_Tree.Right = tree.Right; sR_Tree.Left = tree.Left;sR_Tree.SquirrelHole = tree.SquirrelHole;

            i++;
        }
    
    }


    [System.Serializable]
    public class TreeInfo
    {
        public bool Right = true; 
        public bool Left = true;

        public bool SquirrelHole = true;

        public Sprite TreeImage;
    }
}
