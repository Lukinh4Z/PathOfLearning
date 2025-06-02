using UnityEngine;
using UnityEngine.UI;

public class UIPoolBar : MonoBehaviour
{
    [SerializeField] Image bar;
     ValuePool pool;

    public void Show(ValuePool pool)
    {
        this.pool = pool;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        this.pool = null;   
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pool == null) { return; }
        bar.fillAmount = Mathf.InverseLerp(0f, pool.maxValue.int_value, pool.currentValue);
    }
}
