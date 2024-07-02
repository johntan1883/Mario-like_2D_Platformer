using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagPole : MonoBehaviour
{
    [SerializeField] private Transform flag;
    [SerializeField] private Transform poleBottom;
    [SerializeField] private Transform marioBottom;
    [SerializeField] private Transform castleEntrance;
    [SerializeField] private float speed = 5f;
    [SerializeField] private AudioClip flagPoleSFX;
    [SerializeField] private AudioClip levelClearSFX;
    [SerializeField] private GameObject nextLevelScreen;
    [SerializeField] private bool nextLevel = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundFXManager.Instance.StopBackgroundMusic();
            SoundFXManager.Instance.PlaySoundFXClip(flagPoleSFX, transform, 1f, "SFX");

            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));

            
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<Player>().enabled = false;

        yield return MoveTo(player, marioBottom.position);
        //yield return MoveTo(player, player.position + Vector3.right);
        //yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castleEntrance.position);
        SoundFXManager.Instance.PlaySoundFXClip(levelClearSFX, transform, 1f, "SFX");
        player.gameObject.SetActive(false);
        NextLevel();
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelSequence());
    }

    IEnumerator NextLevelSequence()
    {
        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        nextLevelScreen.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (nextLevel)
        {
            SceneManager.LoadScene("Level_2");
        }
    }
}
