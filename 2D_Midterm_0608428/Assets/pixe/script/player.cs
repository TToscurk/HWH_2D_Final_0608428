using UnityEngine;
using UnityEngine.UI; //引用介面 API

public class player : MonoBehaviour
{
    [Header("等級")]
    [Tooltip("這是角色的等級")]
    public int lv = 1;
    [Header("移動速度"), Range(0, 300)]
    public float speed = 10.5f;
    [Header("角色是否死亡")]
    public bool isDead = false;
    [Header("角色名稱"), Tooltip("這是角色名稱")]
    public string cName = "貓咪";
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("攻擊音效")]
    public AudioClip soundAttack;

    //事件 :繪製圖示
    private void OnDrawGizmos()
    {
        // 指定圖顏色 (紅,綠 藍 透明)
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        //繪製圖示
        Gizmos.DrawSphere(transform.position, rangeAttack);

    }

    //方法語法 Method-儲存複雜的程式區塊或演算法
    //修飾詞 類型 名稱() { 城市區塊或演算法 }
    //void 無類型
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {

        float h = joystick.Horizontal;

        float v = joystick.Vertical;

        tra.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        ani.SetFloat("水平", h);
        ani.SetFloat("垂直", v);
    }

    public void Attack()
    {
        aud.PlayOneShot(soundAttack, 1.2f);

        // 2d 物理 圖形碰撞(中心點.半徑方向,距離,圖層)

        RaycastHit2D hit  = Physics2D.CircleCast(transform.position, rangeAttack, -transform.up, 0, 1 << 8);
        // 如果 碰到的物件 標籤 為 道具 就刪除 (碰到的碰撞器的遊戲物件)
        if (hit && hit.collider.tag == "道具")hit.collider.GetComponent<item>().DropProp();
    }


    private void Hit()
    {

    }   
    
    private void deat()
    {

    }

    //事件- 特定時間會執行的方法
    private void Start()
    {
        //呼叫方法
        //方法名稱();
        Move();
    }
    //更新事件:大約一秒執行六十次 60FPS
    private void Update()
    {
        Move();
    }
    [Header("吃金條音效")]
    public AudioClip soundEat;
    [Header("金幣數量")]
    public Text textcoin;

    private int coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="金條")
        {
            coin++;
            aud.PlayOneShot(soundEat);
            Destroy(collision.gameObject);
            textcoin.text = "糖果:" + coin;
        }
    }
}
