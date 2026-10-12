using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class DropDownInsecte : MonoBehaviour
{
    [Header("InsecteSpace")]
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    public TMP_Dropdown dropdown3;
    public TMP_Dropdown dropdown4;

    [Header("vivarium")]
    public GameObject Vivarium1;
    public GameObject Vivarium2;
    public GameObject Vivarium3;

    [Header("Humeur")]
    public Text humeur1;
    public Text humeur2;
    public Text humeur3;
    public Text humeur4;


    [Header("Décors")]
    public TMP_Dropdown dropdowndecors1Vivarium1;
    public TMP_Dropdown dropdowndecors2Vivarium1;


    [Header("Decors name")]
    public int Calloux = 0;
    public int Terre = 1;
    public int Branche = 2;
    public int plante = 3;
    public int bassin = 4;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnDropDownChange();
        Debug.Log("Décor 1: " + dropdowndecors1Vivarium1.value);
    }
    public void OnDropDownChange()
    {

        //Gestion du bonheur de l'abeille en fonction des décors choisis dans le vivarium 1
        if (dropdown1.value == 0)
        {
            if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
            {
                humeur1.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur1.text = "Heureuse";
                Debug.Log(" heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur1.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur1.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur1.text = "Très triste";
                Debug.Log("Très triste");
            }
        }



        // Gestion du Bombyx 

        if (dropdown1.value == 1)
        {
            if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur1.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre))
                humeur1.text = "Heureuse";



            else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
            {
                humeur1.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur1.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur1.text = "Très triste";
                Debug.Log("Très triste");
            }



            // Gestion de la sauterelle

            if (dropdown1.value == 2)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la mante religieuse 

            if (dropdown1.value == 3)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la mante religieuse 

            if (dropdown1.value == 4)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la lucane

            if (dropdown1.value == 5)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }




            // Gestion de la libellule 

            if (dropdown1.value == 6)
            {
                if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la cigale 

            if (dropdown1.value == 7)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du phasme

            if (dropdown1.value == 8)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du Papillion 
            if (dropdown1.value == 9)
            {
                if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur1.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                    humeur1.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur1.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur1.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur1.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }
        }



        // Humeur 2
        if (dropdown2.value == 0)
        {
            if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
            {
                humeur2.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur2.text = "Heureuse";
                Debug.Log(" heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur2.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur2.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur2.text = "Très triste";
                Debug.Log("Très triste");
            }
        }



        // Gestion du Bombyx 

        if (dropdown2.value == 1)
        {
            if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur2.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre))
                humeur2.text = "Heureuse";


            else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
            {
                humeur2.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur2.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur2.text = "Très triste";
                Debug.Log("Très triste");
            }



            // Gestion de la sauterelle

            if (dropdown2.value == 2)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur2.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la mante religieuse 

            if (dropdown1.value == 3)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur2.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la mante religieuse 

            if (dropdown2.value == 4)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur2.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la lucane

            if (dropdown2.value == 5)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur2.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }




            // Gestion de la libellule 

            if (dropdown2.value == 6)
            {
                if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                    humeur2.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la cigale 

            if (dropdown2.value == 7)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur2.text = "Heureuse";


                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du phasme

            if (dropdown2.value == 8)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur2.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du Papillion 
            if (dropdown2.value == 9)
            {
                if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur2.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                    humeur2.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur2.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur2.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur2.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }
        }


        // Humeur 3
        if (dropdown3.value == 0)
        {
            if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
            {
                humeur3.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur3 .text = "Heureuse";
                Debug.Log(" heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur3.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur3.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur3.text = "Très triste";
                Debug.Log("Très triste");
            }
        }



        // Gestion du Bombyx 

        if (dropdown3.value == 1)
        {
            if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur3.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre))
                humeur3.text = "Heureuse";

            else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
            {
                humeur3.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur3.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur3.text = "Très triste";
                Debug.Log("Très triste");
            }



            // Gestion de la sauterelle

            if (dropdown3.value == 2)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur3.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la mante religieuse 

            if (dropdown1.value == 3)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur3.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la mante religieuse 

            if (dropdown3.value == 4)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur3.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la lucane

            if (dropdown3.value == 5)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur3.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }




            // Gestion de la libellule 

            if (dropdown3.value == 6)
            {
                if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                    humeur3.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la cigale 

            if (dropdown3.value == 7)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur3.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du phasme

            if (dropdown3.value == 8)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur3.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du Papillion 
            if (dropdown3.value == 9)
            {
                if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur3.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                    humeur3.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur3.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur3.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur3.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }
        }










        // Humeur 4


        if (dropdown4.value == 0)
        {
            if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
            {
                humeur4.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur4.text = "Heureuse";
                Debug.Log(" heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur4.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur4.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux))
            {
                humeur4.text = "Très triste";
                Debug.Log("Très triste");
            }
        }



        // Gestion du Bombyx 

        if (dropdown4.value == 1)
        {
            if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
            {
                humeur4.text = "Très herueuse";
                Debug.Log("Très heureuse");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre))
                humeur4.text = "Heureuse";

            else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
            {
                humeur4.text = "Neutre";
                Debug.Log("Neutre");
            }

            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur4.text = "Triste";
                Debug.Log("Triste");
            }
            else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
            {
                humeur4.text = "Très triste";
                Debug.Log("Très triste");
            }



            // Gestion de la sauterelle

            if (dropdown2.value == 2)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur4.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la mante religieuse 

            if (dropdown4.value == 3)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur4.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la mante religieuse 

            if (dropdown4.value == 4)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur4.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }

            // Gestion de la lucane

            if (dropdown4.value == 5)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                    humeur4.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }




            // Gestion de la libellule 

            if (dropdown4.value == 6)
            {
                if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin))
                    humeur4.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Branche))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }



            // Gestion de la cigale 

            if (dropdown4.value == 7)
            {
                if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                    humeur4.text = "Heureuse";

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du phasme

            if (dropdown4.value == 8)
            {
                if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche))
                    humeur4.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Calloux))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == bassin)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }


            // Gestion du Papillion 
            if (dropdown4.value == 9)
            {
                if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == plante) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == bassin))
                {
                    humeur4.text = "Très herueuse";
                    Debug.Log("Très heureuse");
                }
                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Branche) || (dropdowndecors1Vivarium1.value == Branche) && (dropdowndecors2Vivarium1.value == plante))
                    humeur4.text = "Heureuse";
                else if ((dropdowndecors1Vivarium1.value == plante) && (dropdowndecors2Vivarium1.value == Terre) || (dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == plante))
                {
                    humeur4.text = "Neutre";
                    Debug.Log("Neutre");
                }

                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == bassin) || (dropdowndecors1Vivarium1.value == bassin) && (dropdowndecors2Vivarium1.value == Terre))
                {
                    humeur4.text = "Triste";
                    Debug.Log("Triste");
                }
                else if ((dropdowndecors1Vivarium1.value == Terre) && (dropdowndecors2Vivarium1.value == Calloux || (dropdowndecors1Vivarium1.value == Calloux) && (dropdowndecors2Vivarium1.value == Terre)))
                {
                    humeur4.text = "Très triste";
                    Debug.Log("Très triste");
                }
            }
        }
    }
}





