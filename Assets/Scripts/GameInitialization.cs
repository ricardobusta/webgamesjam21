using System.Collections;
using Player;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    public bool enable;

    public GameObject PlayerEyes;
    public GameObject TitleScreenPosition;

    private void Start()
    {
#if UNITY_EDITOR
        if (!enable)
        {
            StartGame();
            return;
        }
#endif
        StartCoroutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        var playerTransform = PlayerEyes.transform;
        var titleScreenTransform = TitleScreenPosition.transform;
        playerTransform.position = titleScreenTransform.position;
        playerTransform.rotation = titleScreenTransform.rotation;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact"));
        playerTransform.localPosition = Vector3.zero;
        playerTransform.localRotation = Quaternion.identity;
        
        StartGame();
    }

    private void StartGame()
    {
        PlayerController.BlockInput = false;
    }
}