using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int numberOfDots;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] float dotMaxScale = 0.3f;
    [SerializeField] float dotMinScale = 0.05f;

    Transform[] dotsList;
    Vector3 dotPos;
    float timeStamp;
    
    
    public GameObject DotsParent
    {
        get { return dotsParent; }
    }

    void Start()
    {
        Hide();
        PrepareDots();
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
    }

    void PrepareDots()
    {
        dotsList = new Transform[numberOfDots];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / numberOfDots;

        for(int i = 0; i < numberOfDots; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;

        }
    }

    public void UpdateDots( Vector3 handPos, Vector3 forceApplied)
    {
        timeStamp = dotSpacing;
        for(int i = 0; i < numberOfDots; i++)
        {
            dotPos.x = (handPos.x + forceApplied.x * timeStamp);
            dotPos.y = (handPos.y + forceApplied.y * timeStamp) - (Physics.gravity.magnitude * timeStamp * timeStamp)/2f;
            dotPos.z = handPos.z + GameManager.instance.player.transform.forward.z;

            dotsList[i].position = new Vector3(dotPos.x, dotPos.y, dotPos.z);
            timeStamp += dotSpacing;
        }
    }
}
