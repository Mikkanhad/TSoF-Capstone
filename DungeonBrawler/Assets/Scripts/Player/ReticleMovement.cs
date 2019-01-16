using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ReticleMovement : MonoBehaviour {

    /////////////////////////////////////////////////////////////////
    // MEMBERS
    /////////////////////////////////////////////////////////////////
    private GameObject player;

    public float angle;

    [HideInInspector] public bool cscheme1;
    [HideInInspector] public bool cscheme2;
    [HideInInspector] public bool cscheme3;
    private float m_Horizontal;
    private float m_Vertical;
    private bool m_Left;
    private bool m_Right;
    private int playerNumber;
    private Vector2 mag_check;


    /////////////////////////////////////////////////////////////////
    // MONOBEHAVIOUR
    /////////////////////////////////////////////////////////////////
    void Start ()
    {
        player = transform.parent.gameObject;
        cscheme1 = player.GetComponent<PlayerController>().cscheme1;
        cscheme2 = player.GetComponent<PlayerController>().cscheme2;
        cscheme3 = player.GetComponent<PlayerController>().cscheme3;

        playerNumber = player.GetComponent<PlayerController>().playerNumber;

    }

    void Update()
    {
        playerNumber = player.GetComponent<PlayerController>().playerNumber;
        switch (playerNumber)
        {
            case 1:
                Reticle1Control();
                break;
            case 2:
                Reticle2Control();
                break;
            case 3:
                Reticle3Control();
                break;
        }

        angle = Mathf.Atan2(m_Vertical, m_Horizontal);
        float posx = player.transform.position.x;
        float posy = player.transform.position.y;

        transform.position = new Vector3(Mathf.Cos(angle) * 1.5f + posx, Mathf.Sin(angle) * 1.5f + posy, 0);
        transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        

            // Orient its scale to face left
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
 
        
    }
    private void Reticle1Control()
    {

        if (cscheme1 || cscheme3)
        {
            if (cscheme3 && (CrossPlatformInputManager.GetAxisRaw("RightH") != 0 || CrossPlatformInputManager.GetAxisRaw("RightV") != 0))
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("RightH");
                m_Vertical = CrossPlatformInputManager.GetAxis("RightV");
            }
            else
            {
                m_Vertical = CrossPlatformInputManager.GetAxis("Vertical");
                m_Horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            }


        }

        else if (cscheme2)
        {

            mag_check = new Vector2(CrossPlatformInputManager.GetAxis("RightH"), CrossPlatformInputManager.GetAxis("RightV"));
            if (mag_check.magnitude >= 0.90)
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("RightH");
                m_Vertical = CrossPlatformInputManager.GetAxis("RightV");
            }



        }
    }
    private void Reticle2Control()
    {
        if (cscheme1 || cscheme3)
        {
            if (cscheme3 && (CrossPlatformInputManager.GetAxisRaw("RightH2") != 0 || CrossPlatformInputManager.GetAxisRaw("RightV2") != 0))
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("RightH2");
                m_Vertical = CrossPlatformInputManager.GetAxis("RightV2");
            }
            else
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("HorizontalP2");
                m_Vertical = CrossPlatformInputManager.GetAxis("VerticalP2");

            }
        }

        else if (cscheme2)
        {
            mag_check = new Vector2(CrossPlatformInputManager.GetAxis("RightH2"), CrossPlatformInputManager.GetAxis("RightV2"));
            if (mag_check.magnitude >= 0.90)
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("RightH2");
                m_Vertical = CrossPlatformInputManager.GetAxis("RightV2");
            }
        }
    }
    private void Reticle3Control()
    {
        if (cscheme1 || cscheme3)
        {
            if (cscheme3 && (CrossPlatformInputManager.GetAxisRaw("RightH3") != 0 || CrossPlatformInputManager.GetAxisRaw("RightV3") != 0))
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("RightH3");
                m_Vertical = CrossPlatformInputManager.GetAxis("RightV3");
            }
            else
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("HorizontalP3");
                m_Vertical = CrossPlatformInputManager.GetAxis("VerticalP3");
            }
        }

        else if (cscheme2)
        {
            mag_check = new Vector2(CrossPlatformInputManager.GetAxis("RightH3"), CrossPlatformInputManager.GetAxis("RightV3"));
            if (mag_check.magnitude >= 0.90)
            {
                m_Horizontal = CrossPlatformInputManager.GetAxis("RightH3");
                m_Vertical = CrossPlatformInputManager.GetAxis("RightV3");
            }
        }
    }

    void LateUpdate()
    {
        if (player.transform.localScale.x < 0)
        {
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
