using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestionAndAnswerAtlas", menuName = "ScriptableObjects/QuestionAndAnswerAtlas")]
public class QuestionAndAnswerAtlas : ScriptableObject
{
    public QuestionAndAnswers[] questionsAndAnswers;

    public QuestionAndAnswers GetQuestionAndAnswers(int level)
    {
        if (level < questionsAndAnswers.Length)
        {
            return questionsAndAnswers[level];
        }
        else
        {
            var randomIndex = Mathf.RoundToInt(questionsAndAnswers.Length * Random.value);
            return questionsAndAnswers[randomIndex];
        }
    }
}
