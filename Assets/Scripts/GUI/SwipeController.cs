using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    int MAX_PAGE;
    [SerializeField] Vector3 page_step;
    [SerializeField] RectTransform cards_container_rect;
    [SerializeField] RectTransform[] pages;
    [SerializeField] float TWEEN_TIME;
    [SerializeField] LeanTweenType tween_type;
    [SerializeField] float SCALE_UP;
    [SerializeField] float SCALE_DOWN;

    int _current_page = 3;
    Vector3 _target_position;

    private void Awake()
    {
        MAX_PAGE = pages.Length;
        _target_position = cards_container_rect.localPosition;
        UpdatePageScales();
    }

    public void NextPage()
    {
        if (_current_page < MAX_PAGE)
        {
            _current_page++;
            _target_position += page_step;
            MovePage();
        }
    }

    public void PreviousPage()
    {
        if (_current_page > 1)
        {
            _current_page--;
            _target_position -= page_step;
            MovePage();
        }
    }

    private void MovePage()
    {
        cards_container_rect.LeanMoveLocal(_target_position, TWEEN_TIME).setEase(tween_type);
        UpdatePageScales();
    }

    private void UpdatePageScales()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            float targetScale = (i + 1 == _current_page) ? SCALE_UP : SCALE_DOWN;
            pages[i].LeanScale(Vector3.one * targetScale, TWEEN_TIME).setEase(tween_type);
        }
    }
}