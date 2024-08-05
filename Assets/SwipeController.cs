using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int max_page;
    int _current_page;
    Vector3 target_position;
    [SerializeField] Vector3 page_step;
    [SerializeField] RectTransform level_page_rect;
    [SerializeField] float tween_time;
    [SerializeField] LeanTweenType tween_type;

    private void Awake()
    {
        _current_page = 3;
        target_position = level_page_rect.localPosition;
    }

    public void NextPage()
    {
        if (_current_page < max_page)
        {
            _current_page++;
            target_position += page_step;
            MovePage();
        }
    }

    public void PreviosPage()
    {
        if (_current_page > 1)
        {
            _current_page--;
            target_position -= page_step;
            MovePage();
        }
    }

    private void MovePage()
    {
        level_page_rect.LeanMoveLocal(target_position, tween_time).setEase(tween_type);
    }
}
