using System.Collections;
using UnityEngine;

public interface IColorShiftable
{
    void ShiftColor(SpriteRenderer renderer, Color targetColor, float duration = 0.1f);
    IEnumerator SwitchColorBack(SpriteRenderer renderer, float duration, Color originalColor);
}
