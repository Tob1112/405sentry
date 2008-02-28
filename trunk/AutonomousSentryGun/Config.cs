using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonomousSentryGun
{
  class Config
  {
    private Config() {}
    public static Config Instance
    {
      get
      {
        return SingletonCreator.CreatorInstance;
      }
    }
    private sealed class SingletonCreator
    {
      private static readonly Config _instance = new Config();
      public static Config CreatorInstance
      {
        get {return _instance;}
      }
    }
  }
}
