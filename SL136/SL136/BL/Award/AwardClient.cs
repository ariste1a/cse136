using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Microsoft.Practices.Unity;

namespace BL.Award
{
  public class AwardClient
  {
    public void AwardScholarship()
    {
      // Wrote the resolver myself
      Resolver resolver = new Resolver();

      IRanking rankBy1 = resolver.ChooseRanking("undergrad");
      Award award1 = new Award(rankBy1);

      award1.AwardScholarship("100");

      IRanking rankBy2 = resolver.ChooseRanking("grad");
      Award award2 = new Award(rankBy2);
      award2.AwardScholarship("200");

      // using Ninject instead of the custom resolver I wrote.
      StandardKernel kernelContainer = new StandardKernel();
      kernelContainer.Bind<IRanking>().To<RankByGPA>();
      Award award3 = kernelContainer.Get<Award>();
      award3.AwardScholarship("101");

      kernelContainer.Rebind<IRanking>().To<RankByInnovation>();
      Award award4 = kernelContainer.Get<Award>();
      award4.AwardScholarship("201");

      // using Unity instead of custom resolver.
      UnityContainer unityContainer = new UnityContainer();
      unityContainer.RegisterType<IRanking, RankByGPA>();
      Award award5 = unityContainer.Resolve<Award>();
      award5.AwardScholarship("102");

      unityContainer = new UnityContainer();
      unityContainer.RegisterType<IRanking, RankByInnovation>();
      Award award6 = unityContainer.Resolve<Award>();
      award6.AwardScholarship("202");

    }
  }
}