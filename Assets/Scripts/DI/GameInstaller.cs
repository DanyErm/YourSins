using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Arena _arena;
        [SerializeField] private Character _char;


        public override void InstallBindings()
        {
            BindArenaDependency();
            BindCharacterDependency();
        }

        private void BindArenaDependency()
        {
            Container
                .Bind<Arena>()
                .FromInstance(_arena)
                .AsSingle();
        }

        private void BindCharacterDependency()
        {
            Container
                .Bind<Character>()
                .FromInstance(_char)
                .AsSingle();
        }
    }
}