using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Attacks.AttackSettings
{
    public class AttackLauncher : MonoBehaviour
    {
        [SerializeField] private AttackList attackList;

        [SerializeField] ProjectilesData projectiles;

        [SerializeField] private GameObject locationsContainer;
        private readonly List<BaseSpawner> attackLocations = new();


        private void Start()
        {
            FillAttackLocationsList();
            StartCoroutine(ExecuteAttacks());
        }



        private IEnumerator ExecuteAttacks()
        {
            foreach (AttackParameters parameter in attackList.attackParameters)
            {
                yield return new WaitForSeconds(parameter.DelayTime);
                ChosenSpawner(parameter, ChooseProjectile(parameter));
            }
        }

        private GameObject ChooseProjectile(AttackParameters parameter)
        {

            return parameter.ProjectileType switch
            {
                ProjectileType.boneShard => projectiles.BoneShard,
                ProjectileType.chasingPacman => projectiles.ChasingPacman,
                ProjectileType.circlingPacman => projectiles.CirclingPacman,
                ProjectileType.face => projectiles.Face,
                ProjectileType.spinningBone => projectiles.SpinningBone,
                ProjectileType.spinningKnife => projectiles.SpinningKnife,
                ProjectileType.straightFlyingBone => projectiles.StraightFlyingBone,
                ProjectileType.straightFlyingKnife => projectiles.StraightFlyingKnife,
                _ => projectiles.DefaultSquare,
            };
        }

        private void ChosenSpawner(AttackParameters parameter, GameObject chosenProjectile)
        {
            attackLocations[(int)parameter.AttackLocation].Spawn(parameter.FirstSpawnParameter, parameter.SecondSpawnParameter, chosenProjectile);
        }

        private void FillAttackLocationsList()
        {
            attackLocations.Add(locationsContainer.GetComponent<ArenaBotSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<ArenaLeftSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<ArenaRightSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<ArenaTopSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<ChasingSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<NextToArenaSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<NextToCharSpawn>());

            attackLocations.Add(locationsContainer.GetComponent<RandomSpawn>());
        }
    }
}