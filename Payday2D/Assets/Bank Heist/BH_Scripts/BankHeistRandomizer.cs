using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankHeistRandomizer : MonoBehaviour
{
    [Header("Lists")]
    public List<GameObject> BankLayouts;
    public List<Sprite> VaultLayouts;
    public List<Transform> PlayerSpawnLocations;
    public GameObject[] GoldBars = new GameObject[12];
    public GameObject[] AiSpawnPoints;
    public GameObject[] AiSittingSpawnPoints;

    [Header("Ints")]
    public int BankLayout;
    public int VaultLayout;
    public int PlayerSpawnLocation;
    private int[] GoldBarsEnabled = new int[12];
    public int TotalGoldBarsEnabled;

    [Header("Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject BackDoor1;
    public GameObject BackDoor2;

    [Header("Bank Transform")]
    public Transform Bank;
    public Transform BankLayoutTransform;

    [Header("Game Objects")]
    public GameObject OpenBackDoor1;
    public GameObject OpenBackDoor2;

    [Header("Civilians")]
    [Range(0, 20)]
    public int CiviliansRoaming;
    [Range(0, 20)]
    public int CiviliansStanding;
    [Range(0, 20)]
    public int CiviliansSittingDown;
    public GameObject CivilianPrefab;
    private int civilianIndex;

    private int[] AiStandingLocation;
    private int[] AiSittingDownLocation;

    private void Start()
    {
        GoldBarsEnabled = new int[12];
        BankLayout = Random.Range(0, BankLayouts.Count);
        VaultLayout = Random.Range(0, VaultLayouts.Count);
        PlayerSpawnLocation = Random.Range(0, PlayerSpawnLocations.Count);
        GoldBars = GameObject.FindGameObjectsWithTag("GoldBars");
        AiSpawnPoints = GameObject.FindGameObjectsWithTag("AiTargets");
        AiSittingSpawnPoints = GameObject.FindGameObjectsWithTag("AiSpawnPoint");
        AiStandingLocation = new int[AiSpawnPoints.Length];
        AiSittingDownLocation = new int[AiSittingSpawnPoints.Length];

        GenerateDoors();
        GenerateGold();
        generateBankCivilians();

        Instantiate(PlayerPrefab, PlayerSpawnLocations[PlayerSpawnLocation].transform.position, Quaternion.identity);

        if(TotalGoldBarsEnabled <= 1)
        {
            for(int i = 0; i < 3; i++)
            {
                GoldBars[Random.Range(0, GoldBars.Length)].SetActive(true);
            }
        }
    }

    private void GenerateGold()
    {
        for (int i = 0; i < GoldBars.Length; i++)
        {
            GoldBarsEnabled[i] = Random.Range(0, 3);

            if (GoldBarsEnabled[i] == 0)
            {
                GoldBars[i].SetActive(false);
            }

            if (GoldBarsEnabled[i] >= 1)
            {
                GoldBars[i].SetActive(true);
            }

            TotalGoldBarsEnabled++;
        }
    }

    private void GenerateDoors()
    {
        int generateDoor1 = Random.Range(0, 4);
        int generateDoor2 = Random.Range(0, 8);

        //Debug.Log(generateDoor1);
        //Debug.Log(generateDoor2);

        if (generateDoor1 >= 1)
        {
            var InstantiatedDoor = Instantiate(BackDoor1, Vector3.zero, Quaternion.identity);
            InstantiatedDoor.transform.parent = BankLayoutTransform;
            InstantiatedDoor.transform.localPosition = new Vector3(-5.925f, -3.67f, 0);
            InstantiatedDoor.transform.localScale = new Vector3(1, 1, 1);
        }

        if (generateDoor1 == 0)
            OpenBackDoor1.SetActive(true);

        if (generateDoor2 >= 1)
        {
            var InstantiatedDoor = Instantiate(BackDoor2, Vector3.zero, Quaternion.identity);
            InstantiatedDoor.transform.parent = BankLayoutTransform;
            InstantiatedDoor.transform.localPosition = new Vector3(4.2625f, 8.105f, 0);
            InstantiatedDoor.transform.localScale = new Vector3(1, 1, 1);
        }

        if (generateDoor2 == 0)
            OpenBackDoor2.SetActive(true);
    }

    private void generateWalkingCivillians(int target)
    {
        int x;
        bool Taken = false;
        if(AiStandingLocation.Length > 0)
        {
            for (x = 0; x < AiStandingLocation.Length; x++)
            {
                if (AiStandingLocation[x] == target)
                {
                    target = Random.Range(0, AiSpawnPoints.Length);
                    generateWalkingCivillians(target);
                    Taken = true;
                }
            }

            if (x == AiStandingLocation.Length && Taken == false)
            {
                var instatiatedCivilian = Instantiate(CivilianPrefab, AiSpawnPoints[target].transform.position, AiSpawnPoints[target].transform.rotation);
                instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
                civilianIndex++;
            }
        }
        else
        {
            var instatiatedCivilian = Instantiate(CivilianPrefab, AiSpawnPoints[target].transform.position, AiSpawnPoints[target].transform.rotation);
            instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
            civilianIndex++;
        }
    }

    private void generateSittingCivilians(int target)
    {
        int x;
        bool Taken = false;
        if (AiSittingDownLocation.Length > 0)
        {
            for (x = 0; x < AiSittingDownLocation.Length; x++)
            {
                if (AiSittingDownLocation[x] == target)
                {
                    target = Random.Range(0, AiSittingDownLocation.Length);
                    generateSittingCivilians(target);
                    Taken = true;
                }
            }

            if (x == AiSittingDownLocation.Length && Taken == false)
            {
        var instatiatedCivilian = Instantiate(CivilianPrefab, AiSittingSpawnPoints[target].transform.position, AiSittingSpawnPoints[target].transform.rotation);
        instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().enabled = false;
        instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
        civilianIndex++;
            }
        }
        else
        {
            var instatiatedCivilian = Instantiate(CivilianPrefab, AiSittingSpawnPoints[target].transform.position, AiSittingSpawnPoints[target].transform.rotation);
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().enabled = false;
            instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
            civilianIndex++;
        }
    }

    private void generateStandingCivilians(int target)
    {
        int x;
        bool Taken = false;
        if (AiStandingLocation.Length > 0)
        {
            for (x = 0; x < AiStandingLocation.Length; x++)
            {
                if (AiStandingLocation[x] == target)
                {
                    target = Random.Range(0, AiSpawnPoints.Length);
                    generateWalkingCivillians(target);
                    Taken = true;
                }
            }

            if (x == AiStandingLocation.Length && Taken == false)
            {
                var instatiatedCivilian = Instantiate(CivilianPrefab, AiSpawnPoints[target].transform.position, AiSpawnPoints[target].transform.rotation);
                instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().enabled = false;
                instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
                civilianIndex++;
            }
        }
        else
        {
            var instatiatedCivilian = Instantiate(CivilianPrefab, AiSpawnPoints[target].transform.position, AiSpawnPoints[target].transform.rotation);
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().enabled = false;
            instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
            civilianIndex++;
        }
    }

    private void generateBankCivilians()
    {
        for (int i = 0; i < CiviliansRoaming; i++)
        {
            int target = Random.Range(0, AiSpawnPoints.Length);
            generateWalkingCivillians(target);
        }
        for (int x = 0; x < CiviliansStanding; x++)
        {
            int target = Random.Range(0, AiSpawnPoints.Length);
            generateStandingCivilians(target);
        }

        for(int j = 0; j < CiviliansSittingDown; j++)
        {
            int target = Random.Range(0, AiSittingSpawnPoints.Length);
            generateSittingCivilians(target);
        }
    }
}
