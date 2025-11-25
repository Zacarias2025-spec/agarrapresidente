using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour
{
    private Vector2 startTouch;
    private Vector2 swipeDelta;
    private bool isSwiping = false;
    public float minSwipeDist = 50f; // distância mínima para contar como swipe
    public PlayerController player;

    void Update()
    {
        #if UNITY_EDITOR
        // teclado já presente; no editor isto facilita testes
        #endif

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                isSwiping = true;
                startTouch = t.position;
            }
            else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                Reset();
            }
            else if (isSwiping)
            {
                swipeDelta = t.position - startTouch;
                if (swipeDelta.magnitude > minSwipeDist)
                {
                    float x = swipeDelta.x;
                    float y = swipeDelta.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 0) player.MoveRight();
                        else player.MoveLeft();
                    }
                    else
                    {
                        if (y > 0) player.Jump();
                        else {
                            // deslizar para baixo -> slide (a implementar)
                            // Exemplo: reduzir collider height temporariamente
                        }
                    }
                    Reset();
                }
            }
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isSwiping = false;
    }
}
