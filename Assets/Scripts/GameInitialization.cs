using System.Collections;
using Player;
using Ui;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    public bool enable;

    public GameObject PlayerEyes;
    public GameObject TitleScreenPosition;
    public GameObject TitleCanvas;

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
        TitleCanvas.SetActive(true);
        var playerTransform = PlayerEyes.transform;
        var titleScreenTransform = TitleScreenPosition.transform;
        playerTransform.position = titleScreenTransform.position;
        playerTransform.rotation = titleScreenTransform.rotation;
        yield return new WaitUntil(() => Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact"));
        TitleCanvas.SetActive(false);
        playerTransform.localPosition = Vector3.zero;
        playerTransform.localRotation = Quaternion.identity;
        
        ShowDialogue.Show(new Dialogue {duration = 2, message = "It's night. You have to complete your research."},
            new Dialogue {duration = 2, message = "Read the book and find the materials."},
            new Dialogue {duration = 2, message = "Use right click to read descriptions."});
        yield return new WaitForSeconds(6);

        StartGame();
    }

    private void StartGame()
    {
        TitleCanvas.SetActive(false);
        PlayerController.BlockInput = false;
    }
}