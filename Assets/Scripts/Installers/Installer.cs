using Control;
using PlayerParts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AppInput>().AsSingle();
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerHand>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle();
        }
    }
    

    public class AppInput:ILateDisposable
    {
        public NewInput Input { get; }

        public AppInput()
        {
            Input = new NewInput();
            Input.Enable();
        }

        public void LateDispose()
        {
            Input.Disable();
            Input.Dispose();
            Debug.Log($" LateDispose: {GetHashCode()}");
        }
    }
}