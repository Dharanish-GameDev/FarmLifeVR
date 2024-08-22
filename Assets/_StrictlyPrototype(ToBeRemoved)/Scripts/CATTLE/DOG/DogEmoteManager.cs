using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DogEmoteManager : MonoBehaviour
{
    [SerializeField][Required] private GameObject emoteUIObject;
	[SerializeField][Required] private Button sitDownEmote_Btn;
	[SerializeField][Required] private Button standUpEmote_Btn;
	[SerializeField][Required] private Button dieEmote_Btn;
	[SerializeField][Required] private Button feedEmote_Btn;


    private void Start()
    {
        ValidateConstraints();

    }


    private void ValidateConstraints()
    {
        Assert.IsNotNull(emoteUIObject, "Emote UI Object is Null");
        Assert.IsNotNull(sitDownEmote_Btn, "SitDown Button is Null");
        Assert.IsNotNull(standUpEmote_Btn, "StandUp Button is Null");
        Assert.IsNotNull(dieEmote_Btn, "Die Button is Null");
        Assert.IsNotNull(feedEmote_Btn, "Feed Button is Null");
    }
}
