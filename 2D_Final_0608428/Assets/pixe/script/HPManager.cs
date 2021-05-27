using UnityEngine;
using UnityEngine.UI;
using System.Collections; //引用 系統集合 API 偕同程序

public class HPManager : MonoBehaviour
{
      
    [Header("血條")]
    public Image bar;
    [Header("傷害數值")]
    public RectTransform rectDamage;
    /// <summary>
    /// 輸入血量與血量最大值並更新血條
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="hpBar"></param>
    public void UpdateHpBar(float hp, float hpMax)
    {
        //血量填滿數值= 當前血量/血量最大值

        bar.fillAmount = hp / hpMax;
    }
    /// <summary>
    /// 傷害值
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowDamage(float damage)
    {
        RectTransform rect = Instantiate(rectDamage, transform); //生成傷害數值在血條系統內
        rect.anchoredPosition = new Vector2(0, 0);//指定座標
        rect.GetComponent<Text>().text = damage.ToString();//更新數值

        float y = rect.anchoredPosition.y; //取得原址Y軸
        while(y<400)//當Y小於 400時 持續執行

        {
            y += 3;//每次上升的單位20
            rect.anchoredPosition = new Vector2(0, y);//更新座標
            yield return new WaitForSeconds(0.02f);//等待幾秒(秒數)


        }
        Destroy(rect.gameObject, 0.5f);   //刪除傷害數值物件
         
    }
}