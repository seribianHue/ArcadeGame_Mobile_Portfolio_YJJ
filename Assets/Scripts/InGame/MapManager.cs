using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map, Map Prefabs")]
    //[SerializeField] GameObject _map;
    [SerializeField] GameObject _planePf;
    [SerializeField] GameObject[] _treePf;
    [SerializeField] GameObject _rockPf;
    public int _bushNum;
    [SerializeField] GameObject[] _bushPf;

    [Header("Player")]
    [SerializeField] GameObject _player;

    public int _numTreeMin;

    private void Awake()
    {
        
    }

    GameObject[,] _planeArray = new GameObject[5, 5];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _planeArray.GetLength(0); i++)
        {
            for(int j = 0; j <  _planeArray.GetLength(1); j++)
            {
                GameObject plane = Instantiate(_planePf, transform);
                CreatePlane(plane);
                plane.transform.position = new Vector3(-100 + (50 * j), -1, 100 - (50 * i));
                _planeArray[i, j] = plane;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CreateMap();
    }

    enum PLAYERPOS { UP, DOWN, RIGHT, LEFT, NONE }
    float _curPlayerX = 0;
    float _curPlayerZ = 0;
    
    void CreateMap()
    {
        PLAYERPOS state = CheckPlayerXZ();

        if(state != PLAYERPOS.NONE)
        {
            GameObject[,] newPlaneArray = new GameObject[5, 5];
            switch (state)
            {
                case PLAYERPOS.UP:
                    for(int i = 0; i < _planeArray.GetLength(0); i++)
                    {
                        Destroy(_planeArray[4, i]);
                        GameObject plane = Instantiate(_planePf, transform);
                        CreatePlane(plane);
                        plane.transform.position = new Vector3(_curPlayerX - 100 + (50 * i), -1, _curPlayerZ + 100);
                        newPlaneArray[0, i] = plane;
                    }
                    for(int i = 1; i < _planeArray.GetLength(0); i++)
                    {
                        for(int j = 0; j < _planeArray.GetLength(1); j++)
                        {
                            newPlaneArray[i, j] = _planeArray[i - 1, j];
                        }
                    }
                    _planeArray = newPlaneArray;
                    break;
                case PLAYERPOS.DOWN:
                    for (int i = 0; i < _planeArray.GetLength(0); i++)
                    {
                        Destroy(_planeArray[0, i]);
                        GameObject plane = Instantiate(_planePf, transform);
                        CreatePlane(plane);
                        plane.transform.position = new Vector3(_curPlayerX - 100 + (50 * i), -1, _curPlayerZ - 100);
                        newPlaneArray[4, i] = plane;
                    }
                    for (int i = 0; i < _planeArray.GetLength(0) - 1; i++)
                    {
                        for (int j = 0; j < _planeArray.GetLength(1); j++)
                        {
                            newPlaneArray[i, j] = _planeArray[i + 1, j];
                        }
                    }
                    _planeArray = newPlaneArray;
                    break;
                case PLAYERPOS.RIGHT:
                    for (int i = 0; i < _planeArray.GetLength(0); i++)
                    {
                        Destroy(_planeArray[i, 0]);
                        GameObject plane = Instantiate(_planePf, transform);
                        CreatePlane(plane);
                        plane.transform.position = new Vector3(_curPlayerX + 100, -1, _curPlayerZ + 100 - (50 * i));
                        newPlaneArray[i, 4] = plane;
                    }
                    for (int i = 0; i < _planeArray.GetLength(0) - 1; i++)
                    {
                        for (int j = 0; j < _planeArray.GetLength(1); j++)
                        {
                            newPlaneArray[j, i] = _planeArray[j, i + 1];
                        }
                    }
                    _planeArray = newPlaneArray;
                    break;
                case PLAYERPOS.LEFT:
                    for (int i = 0; i < _planeArray.GetLength(0); i++)
                    {
                        Destroy(_planeArray[i, 4]);
                        GameObject plane = Instantiate(_planePf, transform);
                        CreatePlane(plane);
                        plane.transform.position = new Vector3(_curPlayerX - 100, -1, _curPlayerZ + 100 - (50 * i));
                        newPlaneArray[i, 0] = plane;
                    }
                    for (int i = 1; i < _planeArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < _planeArray.GetLength(1); j++)
                        {
                            newPlaneArray[j, i] = _planeArray[j, i - 1];
                        }
                    }
                    _planeArray = newPlaneArray;
                    break;
            }
        }
        return;
    }

    PLAYERPOS CheckPlayerXZ()
    {
        float diffX = _player.transform.position.x - _curPlayerX;
        float diffZ = _player.transform.position.z - _curPlayerZ;

        if (diffX > 50) { _curPlayerX += 50; return PLAYERPOS.RIGHT; }
        else if (diffX < -50) { _curPlayerX -= 50; return PLAYERPOS.LEFT; }
        else if (diffZ > 50) { _curPlayerZ += 50; return PLAYERPOS.UP; }
        else if ( diffZ < -50) { _curPlayerZ -= 50; return PLAYERPOS.DOWN; }
        return PLAYERPOS.NONE;
    }

    void CreatePlane(GameObject plane)
    {
        int numTree = Random.Range(_numTreeMin, _numTreeMin + 3);
        int[,] treespos = new int[numTree, 2];

        for(int i = 0; i < numTree; i++)
        {
            int x, z;
            do 
            {
                x = Random.Range(-25, 26);
                z = Random.Range(-25, 26);
            } while (CheckTreePosDup(treespos, x, z));

            int ranTree = Random.Range(0, _treePf.Length);
            GameObject tree = Instantiate(_treePf[ranTree], plane.transform);
            tree.transform.localPosition += new Vector3(x/25f, 0, z/25f);
        }

        for(int j = 0; j < _bushNum; j++)
        {
            float x, z;
            x = Random.Range(-5f, 6f);
            z = Random.Range(-5f, 6f);

            int bushIndex;

            if (CommonMath.ProbabilityMethod(10))
            {
                bushIndex = 0;
            }
            else { bushIndex = Random.Range(1, 5); }
            GameObject bush = Instantiate(_bushPf[bushIndex], plane.transform);
            bush.transform.localPosition += new Vector3(x / 5f, 0, z / 5f);
        }


    }

    bool CheckTreePosDup(int[,] treesPos, int x, int z)
    {
        for(int i = 0; i < treesPos.GetLength(0); i++)
        {
            if (treesPos[i, 0] == x)
            {
                if (treesPos[i, 1] == z)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }

}
