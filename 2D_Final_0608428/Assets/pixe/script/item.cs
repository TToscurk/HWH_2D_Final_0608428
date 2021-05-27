using UnityEngine;

public class item: MonoBehaviour
{
    [Header("掉落物品")]
    public GameObject prop;
     [Header("掉落機率"), Range(0f, 1f)]
    public float percent = 0.7f;
    

    /// <summary>
    /// 掉落道具
    /// </summary>
    public void DropProp()
    {
        float r = Random.Range(0f, 1f);

        //print("隨機值:" + r);

        //如果 隨機直 <= 掉落機率
        if (r <= percent)
        {
            // 生成物件 座標 角度)
            Instantiate(prop, transform. position, transform.rotation);
        }
        //刪除(此物件)
        Destroy (gameObject);
    }
}
