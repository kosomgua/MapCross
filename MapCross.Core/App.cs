using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using MapCross.Core.ViewModels;

namespace MapCross.Core
{
	public class App : MvxApplication
	{ 
		public override void Initialize()
        {
             CreatableTypes()
                 .EndingWith("Service")
                .AsInterfaces()
                 .RegisterAsLazySingleton();
 				
			Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<FirstViewModel>());
         }
	}
}