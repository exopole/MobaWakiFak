using System.Collections.Generic;
using Controller.Player;
using UnityEngine;

namespace Controller
{
    public class ProjectilesController : MonoBehaviour
    {
        [SerializeField] private Transform _ProjectileParent;
        [SerializeField] private ProjectileBehaviour _ProjectilePrefab;

        private List<ProjectileBehaviour> _projectiles = new List<ProjectileBehaviour>();

        public void Fire(PlayerController playerController)
        {
            var projectile = GetProjectile();
            projectile.Activate(playerController);
        }

        private ProjectileBehaviour GetProjectile()
        {
            ProjectileBehaviour projectile = null;

            foreach (var item in _projectiles)
            {
                if (!item.enabled)
                {
                    projectile = item;
                }
            }

            if (projectile == null)
            {
                projectile = Instantiate(_ProjectilePrefab, _ProjectileParent);
            }

            return projectile;
        }
    }
}