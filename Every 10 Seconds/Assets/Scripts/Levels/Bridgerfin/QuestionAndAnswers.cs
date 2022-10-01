using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestionAndAnswers", menuName = "ScriptableObjects/QuestionAndAnswers")]
public class QuestionAndAnswers : ScriptableObject
{
    public string question;
    public string correctAnswer;
    public string incorrectAnswer;
}
