using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem tooltipSystem;
    public Tooltip tooltip;
    public void Awake()
    {
        tooltipSystem = this;
    }
    private static IEnumerator WaitForUnscaledSeconds(float time)
    {
        float ttl = 0;
        while (time > ttl)
        {
            ttl += Time.unscaledDeltaTime;
            yield return null;
        }
    }
    public static IEnumerator Show(string content, string header)
    {
        tooltipSystem.tooltip.SetText(content, header);
        yield return WaitForUnscaledSeconds(1f);
        tooltipSystem.tooltip.gameObject.SetActive(true);
        
        
    }
    public static void Hide()
    {
        tooltipSystem.tooltip.gameObject.SetActive(false);
        tooltipSystem.tooltip.SetText("", "");
    }


}
