using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quest manager berfungsi sebagai pemegang score yang akan ditampilkan di akhir. Didalam script ini juga terdapat method untuk menambahkan score!
//Quest manager juga berfungsi untuk memberikan tanda bahwa quest sudah selesai dilakukan.

public class QuestManager : MonoBehaviour
{
    public int score;

    public void AddScore()
    {
        score =+ 10;
    }


}
