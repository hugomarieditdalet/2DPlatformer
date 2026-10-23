using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    public void AddSpeed(int speedGiven, float speedDuration)
    {
        PlayerMovement.instance.MoveSpeed += speedGiven;
        StartCoroutine(RemoveSpedd(speedGiven, speedDuration));
    }

    private IEnumerator RemoveSpedd(int speedGiven, float speeDuration)
    {
        yield return new WaitForSeconds(speeDuration);
        PlayerMovement.instance.MoveSpeed -= speedGiven;
    }
}
