using UnityEngine;

public enum ItemType { MoneyBag, Promise, Security }

public class Item : MonoBehaviour
{
    public ItemType type;
    public int points = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case ItemType.MoneyBag:
                    GameManager.Instance.AddScore(points);
                    break;
                case ItemType.Promise:
                    GameManager.Instance.ApplySpeedBoost();
                    break;
                case ItemType.Security:
                    GameManager.Instance.GiveShield();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
