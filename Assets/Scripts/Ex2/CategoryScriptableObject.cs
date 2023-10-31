using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Category", menuName = "GameConfiguration/Categories", order = 1)]
public class CategoryScriptableObject : ScriptableObject
{
    public List<Category> Categories = new();

    public CategoryScriptableObject()
    {
        for (int i = 1; i <= 10; i++)
        {
            Categories.Add(new Category
            {
                Name = $"Category {i}",
                Chapters = new List<Chapter>
                {
                    new Chapter
                    {
                        Name = $"{i}-Chapter 1",
                        DoKho = new List<string> { "1-Difficulty 1", "1-Difficulty 2", "1-Difficulty 3", "1-Difficulty 4", "1-Difficulty 5" }
                    },
                    new Chapter
                    {
                        Name = $"{i}-Chapter 2",
                        DoKho = new List<string> { "2-Difficulty 1", "2-Difficulty 2", "2-Difficulty 3", "2-Difficulty 4", "2-Difficulty 5" }
                    },
                    new Chapter
                    {
                        Name = $"{i}-Chapter 3",
                        DoKho = new List<string> { "3-Difficulty 1", "3-Difficulty 2", "3-Difficulty 3", "3-Difficulty 4", "3-Difficulty 5" }
                    },
                    new Chapter
                    {
                        Name = $"{i}-Chapter 4",
                        DoKho = new List<string> { "4-Difficulty 1", "4-Difficulty 2", "4-Difficulty 3", "4-Difficulty 4", "4-Difficulty 5" }
                    },
                    new Chapter
                    {
                        Name = $"{i}-Chapter 5",
                        DoKho = new List<string> { "5-Difficulty 1", "5-Difficulty 2", "5-Difficulty 3", "5-Difficulty 4", "5-Difficulty 5" }
                    },
                }
            });
        }
    }
}

public class Category
{
    public string Name { get; set; }

    public List<Chapter> Chapters { get; set; }
}

public class Chapter
{
    public string Name { get; set; }

    public List<string> DoKho { get; set; }
}
