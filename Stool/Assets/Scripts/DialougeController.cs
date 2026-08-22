using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class DialougeController : MonoBehaviour
{
    public static DialougeController Instance;

    [HeaderAttribute("Text Elements")]
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI DialougeText;
    [SerializeField] private string interact_text;

    [HeaderAttribute("Text Attributes")]
    [SerializeField] private float typeSpeed = 4.0f;
    [SerializeField] private float punctuation_typeSpeed = 1.0f;
    [SerializeField] private char[] punctuation;

    public void Awake()
    {
        Instance = this;

        interact_text = interact_text.ToUpper();

        Activate_Text(false);
    }

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;
    private bool isInteractText;

    private string n;
    private string p;

    private Coroutine typeDialougeCoroutine;

    private const float MAX_TYPE_TIME = 0.1f;
    private const int MAX_CHAR_GRACE = 5;

    public void DisplayNextParagraph(string name, DialougeText dialougeText)
    {
        if (isInteractText)
        {
            FinishInteractText();
            interact_after = true;
        }

        if (name != string.Empty) n = name.ToUpper();

        //if there is nothing in the queue
        if (paragraphs.Count == 0)
        {
            if (!conversationEnded) StartConversation(dialougeText);
            else if (conversationEnded && !isTyping)
            {
                EndConversation();
                return;
            }
        }

        //update convo text
        if (!isTyping) New_Paragraph();
        else FinishParagraphEarly();

        if (paragraphs.Count == 0) conversationEnded = true;
    }

    private void New_Paragraph()
    {
        if (paragraphs.Count == 0 && conversationEnded)
        {
            EndConversation();
            return;
        }

        p = paragraphs.Dequeue().ToUpper();

        typeDialougeCoroutine = StartCoroutine(TypeDialougeText(p));
    }

    private void StartConversation(DialougeText dialougeText)
    {
        //activate gameObject

        Activate_Text(true);

        //Update Speaker nme
        Set_Name_Text(n);

        //add dialouge text to the queue
        for (int i = 0; i < dialougeText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialougeText.paragraphs[i]);
        }
    }

    public void Set_Name_Text(string _n)
    {
        NameText.text = _n;

        if (NameText.transform.childCount <= 0) return;

        for (var i = 0; i < NameText.transform.childCount; i++)
        {
            TextMeshProUGUI t = NameText.transform.GetChild(i).GetComponent<TextMeshProUGUI>();

            if (t != null) t.text = _n;
        }
    }
    
    public void EndConversation()
    {
        //clear the queue 
        paragraphs.Clear();

        //return bool to false
        conversationEnded = false;

        if(interact_after)
        {
            interact_after = false;
            StartInteractText();
            return;
        }

        //deactivate gameobject
        Activate_Text(false);
    }

    public void Activate_Text(bool active)
    {
        if (active) Set_Name_Text("");
        if (NameText.IsActive() != active) NameText.gameObject.SetActive(active);
        if (DialougeText.IsActive() != active) DialougeText.gameObject.SetActive(active);
    }

    int maxVisibleChars = 0;

    public IEnumerator TypeDialougeText(string p)
    {
        isTyping = true;

        maxVisibleChars = 0;

        DialougeText.text = p;
        DialougeText.maxVisibleCharacters = maxVisibleChars;

        foreach (char c in p.ToCharArray())
        {
            maxVisibleChars++;
            DialougeText.maxVisibleCharacters = maxVisibleChars;

            float _typeSpeed = typeSpeed;
            if (punctuation.Contains(c)) _typeSpeed = punctuation_typeSpeed;

            yield return new WaitForSeconds(MAX_TYPE_TIME / _typeSpeed);
        }

        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        //stop corotuine
        StopCoroutine(typeDialougeCoroutine);

        isTyping = false;

        if (maxVisibleChars >= p.ToCharArray().Length - MAX_CHAR_GRACE)
        {
            New_Paragraph();
            return;
        }

        //finish text
        DialougeText.maxVisibleCharacters = p.ToCharArray().Length;
        DialougeText.text = p;
    }

    public void StartInteractText()
    {
        Activate_Text(true);

        isInteractText = true;

        typeDialougeCoroutine = StartCoroutine(TypeDialougeText(interact_text));
    }

    bool interact_after = false;

    public void FinishInteractText()
    {
        interact_after = false;

        if (!isInteractText) return;

        //stop corotuine
        if(isTyping) StopCoroutine(typeDialougeCoroutine);
        isTyping = false;
        isInteractText = false;

        print("Interacting! 3");

        Activate_Text(false);
    }

    public bool IsInTextRightNow()
    {
        if (paragraphs.Count > 0 || conversationEnded) return true;
        else return false;
    }
}
