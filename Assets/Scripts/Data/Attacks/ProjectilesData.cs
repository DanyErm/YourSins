using UnityEngine;

[CreateAssetMenu(fileName = "ProjectilesData", menuName = "Configs/ProjectilesData")]
public class ProjectilesData : ScriptableObject
{
    [SerializeField] private GameObject boneShard;
    public GameObject BoneShard { get { return boneShard; } }

    [SerializeField] private GameObject chasingPacman;
    public GameObject ChasingPacman { get { return chasingPacman; } }

    [SerializeField] private GameObject circlingPacman;
    public GameObject CirclingPacman { get { return circlingPacman; } }

    [SerializeField] private GameObject face;
    public GameObject Face { get { return face; } }

    [SerializeField] private GameObject spinningBone;
    public GameObject SpinningBone { get { return spinningBone; } }

    [SerializeField] private GameObject spinningKnife;
    public GameObject SpinningKnife { get { return spinningKnife; } }

    [SerializeField] private GameObject straightFlyingBone;
    public GameObject StraightFlyingBone { get { return straightFlyingBone; } }

    [SerializeField] private GameObject straightFlyingKnife;
    public GameObject StraightFlyingKnife { get { return straightFlyingKnife; } }

    [SerializeField] private GameObject defaultSquare;
    public GameObject DefaultSquare { get { return defaultSquare; } }
}