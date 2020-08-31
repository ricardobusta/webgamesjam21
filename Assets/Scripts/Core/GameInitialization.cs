using System.Collections;
using DG.Tweening;
using Player;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class GameInitialization : MonoBehaviour
    {
        public bool enable;

        public GameObject PlayerEyes;
        public GameObject TitleScreenPosition;
        public GameObject TitleCanvas;

        public AnimationCurve OpenEyeCurve;

        public GameObject crossHair;

        public Image FadeScreen;

        private readonly Vector3 _initialEyePosition = new Vector3(0, -1.23f, 0);
        private readonly Quaternion _initialEyeRotation = Quaternion.Euler(-83.397f, 34.92f, 0);

        private static GameInitialization _instance;
    
        private void Awake()
        {
            _instance = this;
        }

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

        public static void GameOver()
        {
            _instance.StartCoroutine(_instance.GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            PlayerController.BlockMove = true;
            PlayerController.BlockLook = true;
        
            ShowDialogue.ShowNormal(new Dialogue {duration = 3, message = "Finally. My research is done."},
                new Dialogue {duration = 3, message = "Now I will not lose my sanity to the void."},
                new Dialogue {duration = 3, message = "Maybe I should get some rest now."});
            yield return new WaitForSeconds(6);
            FadeScreen.DOFade(1, 5).SetEase(OpenEyeCurve);
            yield return new WaitForSeconds(8);

            SceneManager.LoadScene("Game");
        }

        private IEnumerator InitializeRoutine()
        {
            TitleCanvas.SetActive(true);
            crossHair.SetActive(false);
            var playerTransform = PlayerEyes.transform;
            var titleScreenTransform = TitleScreenPosition.transform;
            playerTransform.position = titleScreenTransform.position;
            playerTransform.rotation = titleScreenTransform.rotation;
            yield return new WaitUntil(() => Input.GetButtonDown("Fire1") || Input.GetButtonDown("Interact"));

            FadeScreen.DOFade(1, 1);
            yield return new WaitForSeconds(1);
            TitleCanvas.SetActive(false);
            playerTransform.localPosition = _initialEyePosition;
            playerTransform.localRotation = _initialEyeRotation;

            FadeScreen.DOFade(0, 3).SetEase(OpenEyeCurve);
            yield return new WaitForSeconds(1);

            DOVirtual.Float(0, 1, 3, t =>
            {
                playerTransform.localPosition = Vector3.Lerp(_initialEyePosition, Vector3.zero, t);
                playerTransform.localRotation = Quaternion.Lerp(_initialEyeRotation, Quaternion.identity, t);
            });
            yield return new WaitForSeconds(3);

            ShowDialogue.ShowNormal(new Dialogue {duration = 2, message = "Damn. I woke too late. It's almost dawn."});
            yield return new WaitForSeconds(3);

            StartGame();

            ShowDialogue.ShowTutorial(
                new Dialogue
                {
                    duration = 4,
                    message = "Move the <color=red>Mouse</color> to look around."
                },
                new Dialogue
                {
                    duration = 4,
                    message = "Use <color=red>WASD keys</color> to move around."
                },
                new Dialogue
                {
                    duration = 4,
                    message = "<color=red>Space Bar</color> makes the character jump."
                },
                new Dialogue
                {
                    duration = 4,
                    message = "<color=red>Left Click</color> to read books and collect materials."
                },
                new Dialogue
                {
                    duration = 4,
                    message = "Use <color=red>Right Click</color> to read descriptions."
                },
                new Dialogue
                {
                    duration = 4,
                    message = "Your objective is to complete all research before dawn."
                });
        }

        private void StartGame()
        {
            TitleCanvas.SetActive(false);
            crossHair.SetActive(true);
            PlayerController.BlockMove = false;
            PlayerController.BlockLook = false;
        }
    }
}