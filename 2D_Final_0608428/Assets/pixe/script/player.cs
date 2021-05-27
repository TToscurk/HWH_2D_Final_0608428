using UnityEngine;
using UnityEngine.UI; //引用介面 API
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    [Header("等級")]
    [Tooltip("這是角色的等級")]
    public int lv = 1;
    [Header("移動速度"), Range(0, 300)]
    public float speed = 10.5f;
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
    [Header("血量")]
    public float hp = 200;
    [Header("血量系統")]
    public HPManager hpManager;
    [Header("攻擊力"), Range(0, 1000)]
    public float attack = 20;

    private bool isDead = false;
    private float hpMax;


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
        if (isDead) return; //如果死亡就跳出

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
        if (hit && hit.collider.tag == "敵人")hit.collider.GetComponent<Enemy>().Hit(attack);
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(float damage )

    {
        hp -= damage;//扣除傷害直
        hpManager.UpdateHpBar(hp, hpMax);//更新血條
        StartCoroutine(hpManager.ShowDamage(damage));//偕同程序
        if (hp <= 0) Dead();
    }   
    /// <summary>
    /// 死亡
    /// </summary>
    
    private void Dead()
    {
        hp = 0;
        isDead = true;
        Invoke("Replay", 2);//延遲呼叫("方法名稱"延遲時間)

    }
    private void Replay()
    {
        SceneManager.LoadScene("冰原");
    }
    //事件- 特定時間會執行的方法
    private void Start()

    {
        hpMax = hp;//取得血量最大值
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
            textcoin.text = "candy:" + coin;
        }
    }
}
