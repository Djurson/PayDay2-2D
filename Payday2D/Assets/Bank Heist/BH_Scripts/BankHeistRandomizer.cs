using System.Collections.Generic;
using UnityEngine;

public class BankHeistRandomizer : MonoBehaviour
{
    public static BankHeistRandomizer instance;

    [Header("Lists")]
    [SerializeField] private List<GameObject> BankLayouts;
    [SerializeField] private List<Sprite> VaultLayouts;
    [SerializeField] private List<Transform> PlayerSpawnLocations;
    [SerializeField] private GameObject[] GoldBars = new GameObject[12];
    [SerializeField] private List<Transform> DrillSpawnLocations;
    [SerializeField] private List<Transform> BodyBagCaseSpawnPosition;
    public List<GameObject> AiSpawnPoints;
    [SerializeField] private List<GameObject> AiSittingSpawnPoints;
    public List<GameObject> GuardSpawnPoints;

    [Header("Ints")]
    [SerializeField] private int BankLayout;
    [SerializeField] private int VaultLayout;
    [SerializeField] private int PlayerSpawnLocation;
    [SerializeField] private int[] GoldBarsEnabled = new int[12];
    [SerializeField] private int TotalGoldBarsEnabled;

    [Header("Prefabs")]
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject BackDoor1;
    [SerializeField] private GameObject BackDoor2;

    [Header("Bank Transform")]
    [SerializeField] private Transform Bank;
    [SerializeField] private Transform BankLayoutTransform;

    [Header("Game Objects")]
    [SerializeField] private GameObject OpenBackDoor1;
    [SerializeField] private GameObject OpenBackDoor2;

    [Header("Purchased Assets")]
    [SerializeField] private GameObject thermalDrillPrefab;
    [SerializeField] private thermalDrillPosition drillPosition;
    [SerializeField] private GameObject BodyBagCasePrefab;
    [SerializeField] private bodyBagsPosition BodyBagCasePosition;

    [Header("Civilians")]
    [SerializeField, Range(0, 20)] private int CiviliansRoaming;
    [SerializeField, Range(0, 20)] private int CiviliansStanding;
    [SerializeField, Range(0, 20)] private int CiviliansSittingDown;
    [SerializeField] private GameObject CivilianPrefab;
    [SerializeField] private int civilianIndex;

    [Header("Guards")]
    [SerializeField, Range(0, 10)] private int Guards;
    [SerializeField] private GameObject GuardPrefab;
    [SerializeField] private int GuardIndex;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    private void Start()
    {
        if(BankHeistSetup.instance != null)
        {
            drillPosition = BankHeistSetup.instance.drillPosition;
            BodyBagCasePosition = BankHeistSetup.instance.bodyPosition;
        }

        if (BankHeistSetup.instance != null)
        {
            Destroy(BankHeistSetup.instance.gameObject);
        }

        GoldBarsEnabled = new int[12];
        BankLayout = Random.Range(0, BankLayouts.Count);
        VaultLayout = Random.Range(0, VaultLayouts.Count);
        PlayerSpawnLocation = Random.Range(0, PlayerSpawnLocations.Count);
        GoldBars = GameObject.FindGameObjectsWithTag("GoldBars");

        GenerateDoors();
        GenerateGold();
        generateBankCivilians();
        GenerateGuards();
        spawnThermalDrill();
        SpawnBodyBagCaseIfPurschased();

        Instantiate(PlayerPrefab, PlayerSpawnLocations[PlayerSpawnLocation].transform.position, Quaternion.identity);

        if(TotalGoldBarsEnabled <= 1)
        {
            for(int i = 0; i < 3; i++)
            {
                GoldBars[Random.Range(0, GoldBars.Length)].SetActive(true);
            }
        }
    }

    private void spawnThermalDrill()
    {
        if(drillPosition == thermalDrillPosition.AboveBank)
        {
            var instantiatedThermalDrill = Instantiate(thermalDrillPrefab, DrillSpawnLocations[0].transform.position, Quaternion.identity);
        } 
        else if(drillPosition == thermalDrillPosition.LeftOfBank1)
        {
            var instantiatedThermalDrill = Instantiate(thermalDrillPrefab, DrillSpawnLocations[1].transform.position, Quaternion.identity);
        }
        else if (drillPosition == thermalDrillPosition.LeftOfBank2)
        {
            var instantiatedThermalDrill = Instantiate(thermalDrillPrefab, DrillSpawnLocations[2].transform.position, Quaternion.identity);
        }
    }

    private void SpawnBodyBagCaseIfPurschased()
    {
        if(BodyBagCasePosition != bodyBagsPosition.None)
        {
            if(BodyBagCasePosition == bodyBagsPosition.AboveBank)
            {
                var instatiatedBodyBagCase = Instantiate(BodyBagCasePrefab, BodyBagCaseSpawnPosition[0].position, Quaternion.identity);
            } else if(BodyBagCasePosition == bodyBagsPosition.RightSideOfBank)
            {
                var instatiatedBodyBagCase = Instantiate(BodyBagCasePrefab, BodyBagCaseSpawnPosition[1].position, Quaternion.identity);
            } else if(BodyBagCasePosition == bodyBagsPosition.InsideBank)
            {
                var instatiatedBodyBagCase = Instantiate(BodyBagCasePrefab, BodyBagCaseSpawnPosition[2].position, Quaternion.identity);
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

    private void generateBankCivilians()
    {
        for (int i = 0; i < CiviliansRoaming; i++)
        {
            int target = Random.Range(0, AiSpawnPoints.Count);
            var instatiatedCivilian = Instantiate(CivilianPrefab, AiSpawnPoints[target].transform.position, AiSpawnPoints[target].transform.rotation);
            instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().startTransform = AiSpawnPoints[target].transform;
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().Standing = false;
            AiSpawnPoints.Remove(AiSpawnPoints[target]);
            civilianIndex++;
        }
        for (int x = 0; x < CiviliansStanding; x++)
        {
            int target = Random.Range(0, AiSpawnPoints.Count);
            var instatiatedCivilian = Instantiate(CivilianPrefab, AiSpawnPoints[target].transform.position, AiSpawnPoints[target].transform.rotation);
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().enabled = false;
            instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().Standing = true;
            civilianIndex++;
            AiSpawnPoints.Remove(AiSpawnPoints[target]);
        }

        for(int j = 0; j < CiviliansSittingDown; j++)
        {
            int target = Random.Range(0, AiSittingSpawnPoints.Count);
            var instatiatedCivilian = Instantiate(CivilianPrefab, AiSittingSpawnPoints[target].transform.position, AiSittingSpawnPoints[target].transform.rotation);
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().enabled = false;
            instatiatedCivilian.GetComponent<CivilianRayCaster>().CivilianIndex = civilianIndex;
            instatiatedCivilian.GetComponent<AiPathFindingRandomTargetChooser>().Standing = true;
            civilianIndex++;
            AiSittingSpawnPoints.Remove(AiSittingSpawnPoints[target]);
        }
    }

    private void GenerateGuards()
    {
        for(int i = 0; i < Guards; i++)
        {
            int target = Random.Range(0, GuardSpawnPoints.Count);
            var instatitedGuard = Instantiate(GuardPrefab, GuardSpawnPoints[target].transform.position, GuardSpawnPoints[target].transform.rotation);
            instatitedGuard.GetComponent<GuardPathRandomizer>().startTransform = GuardSpawnPoints[target].transform;
            instatitedGuard.GetComponent<GuardRayCaster>().GuardIndex = GuardIndex;
            GuardIndex++;
            GuardSpawnPoints.Remove(GuardSpawnPoints[target]);
        }
    }
}
